using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinFormsContentLoading;
using System.Windows.Forms;
using LevelDesigner.Data;
using LevelDesigner.Content_Util;

namespace LevelDesigner
{
    class LevelObjectData : ILevelEvents
    {
        #region Properties
        
        /* Level Definition */
        public Dictionary<String, LevelObject>      worldObjects = new Dictionary<String, LevelObject>();
        public Dictionary<String, LevelObjectType>  objectPalette = new Dictionary<string, LevelObjectType>();
        public Dictionary<String, Model>            models = new Dictionary<string, Model>();
        public Dictionary<String, LevelTerrain>     terrainObjects = new Dictionary<String, LevelTerrain>();

        public String levelName = "";
        public String levelFileName = "";
        
        /* index counter */
        private static int m_nextUnusedObID = 0;
        private static int m_nextUnusedTypeID = 0;
        private static int m_nextUnusedTerrainID = 0;
        const int TYPE_ID_OFFSET = 900;

        public String LevelFileName
        {
            get { return levelFileName; }
            set { levelFileName = value; }
        }

        #endregion

        #region Level Management

        public class UnsavedObjectTypeException : System.Exception { };
        public class IncorrectFileVersionException : System.Exception { };

        public void AddObjectToLvl(LevelObject lo, bool remove)
        {
            if (!remove)
            {
                worldObjects.Add(lo.Name, lo);
                if (ObjectInstanceAdded != null)
                {
                    LevelEventArgs e = new LevelEventArgs();
                    e.lo = lo;
                    ObjectInstanceAdded(this, e);
                }
            }
            else
            {
                worldObjects.Remove(lo.Name);
                if (ObjectInstanceRemoved != null)
                {
                    LevelEventArgs e = new LevelEventArgs();
                    e.lo = lo;
                    ObjectInstanceRemoved(this, e);
                }

            }
        }

        public void AddTerrainToLvl(LevelTerrain terr, bool remove)
        {
            if (!remove)
            {

                bool found = true;
                while (found == true)
                {
                    found = false;
                    foreach (KeyValuePair<String, LevelTerrain> kv in terrainObjects)
                    {
                        if (kv.Key == terr.Name)
                        {
                            /* find a new name that doesn't conflict */
                            int id = Convert.ToInt32(terr.Name.Substring(terr.Name.Length - 1));
                            terr.Name = terr.Name.Substring(0, terr.Name.Length - 1) + id.ToString();
                            found = true;
                            break;
                        }

                    }
                }

                terrainObjects.Add(terr.Name, terr);
                if (ObjectTerrainAdded != null)
                {
                    LevelEventArgs e = new LevelEventArgs();
                    e.terr = terr;
                    ObjectTerrainAdded(this, e);
                }
            }
            else
            {
                terrainObjects.Remove(terr.Name);
                if (ObjectTerrainRemoved != null)
                {
                    LevelEventArgs e = new LevelEventArgs();
                    e.terr = terr;
                    ObjectTerrainRemoved(this, e);
                }
            }
        }

        public void AddObjectTypeToPalette(LevelObjectType lot, bool remove)
        {
            if (!remove)
            {
                bool done = false;
                while (!done)
                {
                    try
                    {
                        objectPalette.Add(lot.Name, lot);
                        done = true;
                    }
                    catch (ArgumentException)
                    {
                        /* Given name is already taken, try to create a new name */
                        String name = lot.Name.Substring(0, lot.Name.Length-1);
                        name += GetNextTypeIdx().ToString();
                        lot.Name = name;
                    }

                }

                if (ObjectTypeAdded != null)
                {
                    LevelEventArgs e = new LevelEventArgs();
                    e.lot = lot;
                    ObjectTypeAdded(this, e);
                }
            }
            else
            {
                objectPalette.Remove(lot.Name);
                if (ObjectTypeRemoved != null)
                {
                    LevelEventArgs e = new LevelEventArgs();
                    e.lot = lot;
                    ObjectTypeRemoved(this, e);
                }

            }
        }

        /* Clears all object instances, terrain and object types, 
         * effectively clearing the level to initial empty state */
        private void ClearAllLocalData()
        {
            /* Reset Local State */
            worldObjects.Clear();
            objectPalette.Clear();
            terrainObjects.Clear();
            m_nextUnusedObID = 0;
        }

        public LevelTerrain GetTerrain()
        {
            foreach (KeyValuePair<String, LevelTerrain> kv in terrainObjects)
            {
                if (kv.Value != null)
                {
                    return kv.Value;
                }
            }

            return null;
        }

        public LevelObject GetObjectByStringID(String objectID)
        {
            LevelObject retlo = null;
            worldObjects.TryGetValue(objectID, out retlo);

            return retlo;
        }

        public LevelObjectType GetObjectTypeByStringID(String name)
        {
            LevelObjectType lot = null;
            this.objectPalette.TryGetValue(name, out lot);

            return lot;
        }

        public LevelObjectType GetObjectTypeByGuid(String guid)
        {
            foreach (KeyValuePair<String, LevelObjectType> kv in objectPalette)
            {
                LevelObjectType lot = kv.Value;
                if (lot.TypeID.ToString() == guid)
                {
                    return lot;
                }

            }

            return null;
        }

        public int GetObjectTypeList(out LevelObjectType[] types)
        {
            types = new LevelObjectType[objectPalette.Count];
            int index = 0;
            foreach (KeyValuePair<String, LevelObjectType> kv in objectPalette)
            {
                types[index] = kv.Value;
                index++;
            }

            /* return the number of items in the array */
            return index;
        }

        public void GroundClampObject(LevelObject lo)
        {
            Vector3 objectPosition = lo.Position;
            TerrainInfo localTerrain = null;

            /* find the terrain chunk that contains the object's (X,Z) position */
            foreach (KeyValuePair<String, LevelTerrain> kv in terrainObjects)
            {
                LevelTerrain lt = kv.Value;

                TerrainInfo ti = new TerrainInfo(lt);
                if (ti.ContainsPointXZ(objectPosition))
                {
                    localTerrain = ti;
                    break;
                }

            }

            /* find the Y position at the object's X,Z 
             * location and adjust to sit above ground */
            if (localTerrain != null)
            {
                float? newY = localTerrain.GetTerrainHeightAtXZ(objectPosition.X, objectPosition.Z);
                lo.Position = new Vector3(lo.Position.X, newY.Value, lo.Position.Z);
            }
        }

        #endregion

        #region Object Factory
        public LevelObject CreateNewLevelObjectInstance()
        {
            /* Generate a new unique ID */
            int objID = GetNextObjectIdx();

            /* Create a fake name 
             * (TODO: this can come from create object dialog) */
            String name = "NEWOBJECT" + objID;

            /* Create the new level object */
            LevelObject lo = new LevelObject();
            lo.Index = objID;
            lo.Name = name;

            lo.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            return lo;
        }

        public LevelObjectType CreateNewLevelObjectType()
        {
            /* Generate a New Unique ID */
            int typeIdx = GetNextTypeIdx();
            Guid typeId = Guid.NewGuid();

            /* Create a fake name 
             * (TODO: this can come from create type dialog) */
            String name = "NEWTYPE" + typeIdx;

            /* Create the new level object */
            LevelObjectType lot = new LevelObjectType();
            lot.Index = typeIdx;
            lot.TypeID = typeId;
            lot.name = name;
            lot.shaderId = "default";
            lot.IsDirty = true;
            lot.ModelFileName = "none";
            lot.scale.Scales = new Vector3(1.0f, 1.0f, 1.0f);
            lot.rotation.Order = RotationEuler.EOrder.eOrder_ZYX;

            return lot;
        }

        public int GetNextObjectIdx()
        {
            return m_nextUnusedObID++;
        }

        public int GetNextTypeIdx()
        {
            return m_nextUnusedTypeID++;
        }

        public int GetNextTerrainIdx()
        {
            return m_nextUnusedTerrainID++;
        }

        #endregion

        #region File IO
        public void EnsureLevelDefinition(System.IO.Stream stream)
        {
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(stream);

            /* File Version Stamp */
            bw.Write(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            /* Level Attributes */
            bw.Write(levelName);

            /* Object Type Palette */
            List<FileOps.LevelObjectTypeDefinition> types = new List<FileOps.LevelObjectTypeDefinition>();
            foreach (KeyValuePair<String, LevelObjectType> kv in objectPalette)
            {
                FileOps.LevelObjectTypeDefinition t = new FileOps.LevelObjectTypeDefinition();
                t.TypeId = kv.Value.TypeID;

                String typeFileName = kv.Value.FileName;

                if (String.IsNullOrEmpty(typeFileName))
                {
                    throw new UnsavedObjectTypeException();
                }

                int idx = typeFileName.LastIndexOf('\\')+1;
                typeFileName = typeFileName.Substring(idx, typeFileName.Length - idx);
                t.FileName = typeFileName;

                types.Add(t);

            }

            FileOps.WriteObjectPalette(bw, types);

            /* Level Terrain */
            bw.Write(terrainObjects.Count);
            foreach (KeyValuePair<String, LevelTerrain> kv in terrainObjects)
            {
                LevelTerrain terrain = kv.Value;
                bw.Write(terrain.Name);
                FileOps.WriteVec3(terrain.Position, bw);
                FileOps.WriteVec3(terrain.Scaling.Scales, bw);
                bw.Write(terrain.HeightMapDimensions.X);
                bw.Write(terrain.HeightMapDimensions.Y);

                for (int j = 0; j < terrain.HeightMapDimensions.Y; j++)
                {
                    for (int i = 0; i < terrain.HeightMapDimensions.X; i++)
                    {
                        int index = j * terrain.HeightMapDimensions.X + i;
                        bw.Write(terrain.HeightMap[index]);
                    }
                }
            }

            /* Level Object Instances */
            bw.Write(worldObjects.Count);

            foreach (KeyValuePair<String, LevelObject> kv in worldObjects)
            {
                LevelObject lo = kv.Value;
                bw.Write(lo.Index);
                bw.Write(lo.Name);
                bw.Write(lo.TypeId.ToString());

                /* Level Object Transforms */
                FileOps.WriteVec3(lo.Rotation, bw);
                FileOps.WriteVec3(lo.Scale, bw);
                FileOps.WriteVec3(lo.Position, bw);
            }

            bw.Flush();

            bw.Close();
        }

        public void EnsureObjectType(LevelObjectType lot, System.IO.Stream stream)
        {
            System.Diagnostics.Debug.Assert(lot.IsDirty);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(stream);

            /* File Version Stamp */
            bw.Write(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            /* Object Type Attributes */
            bw.Write(lot.TypeID.ToString());
            bw.Write(lot.Name);
            bw.Write(lot.ShaderId);
            bw.Write(lot.ModelFileName);

            /* Object Type Parent Transforms */
            FileOps.WriteVec3(lot.Translation, bw);
            FileOps.WriteVec3(lot.Rotation, bw);
            FileOps.WriteVec3(lot.Scaling, bw);
        }

        public void LoadLevelFile(System.IO.Stream stream)
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(stream);

            /* Get Current App Version For Comparison */
            String currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            /* Check Loading File's Version To Current */
            String fileVersion = br.ReadString();
            if (fileVersion != currentVersion)
            {
                /* ERROR: File Version Out of Date */
                throw new IncorrectFileVersionException();
            }

            /* Load the Level Attributes */
            levelName = br.ReadString();

            /* Load the Object Type Palette */
            List<FileOps.LevelObjectTypeDefinition> types = FileOps.ReadObjectPalette(br);

            /* Level Terrain */
            int numTerrainChunks = br.ReadInt32();
            for (int i = 0; i < numTerrainChunks; i++)
            {
                LevelTerrain terrain = new LevelTerrain();
                terrain.Name = br.ReadString();
                terrain.Position = FileOps.ReadVec3(br);

                Scale scale = new Scale();
                scale.scales = FileOps.ReadVec3(br);
                terrain.Scaling = scale;

                Point pt = new Point();
                pt.X = br.ReadInt32();
                pt.Y = br.ReadInt32();
                terrain.HeightMapDimensions = pt;
                
                int numSamples = terrain.HeightMapDimensions.X * terrain.HeightMapDimensions.Y;
                byte[] heightMap = new byte[numSamples];
                br.Read(heightMap, 0, numSamples);
                terrain.HeightMap = heightMap;

                terrainObjects.Add(terrain.Name, terrain);
            }

            /* Load Level Object Instances */
            int levelCnt = br.ReadInt32();
            System.Collections.Generic.List<LevelObject> lvlObjs = new List<LevelObject>();

            for (int i = 0; i < levelCnt; i++)
            {
                LevelObject lo = new LevelObject();

                /* Save Object Instance Attributes */
                lo.Index = br.ReadInt32();
                lo.Name = br.ReadString();
                lo.TypeId = new Guid(br.ReadString());

                /* Object Instance Transforms */
                lo.Rotation = FileOps.ReadVec3(br);
                lo.Scale = FileOps.ReadVec3(br);
                lo.Position = FileOps.ReadVec3(br);

                lvlObjs.Add(lo);

                m_nextUnusedObID = Math.Max(lo.Index, m_nextUnusedObID);
                m_nextUnusedObID++;
            }

            br.Close();

            /* Load the Level Object Types Definitions From 
             * Files For Each Type in Level's Object Palette */
            foreach (FileOps.LevelObjectTypeDefinition t in types)
            {
                /* Try to Open the Type's File */
                String typeFilename = levelFileName;
                int idx = typeFilename.LastIndexOf('\\')+1;
                typeFilename = typeFilename.Substring(0, idx);
                typeFilename += t.FileName;

                System.IO.FileStream typeFile = null;

                try
                {
                    typeFile = System.IO.File.Open(typeFilename, System.IO.FileMode.Open);

                    /* Fill Out a New Object Type from The File Definition */
                    LevelObjectType lot = LoadTypeFile(typeFile);

                    /* Verify the Type Matches the Guid From the Level Type Palette */
                    if (lot.TypeID == t.TypeId)
                    {
                        this.objectPalette.Add(lot.Name, lot);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(true);
                    }

                    /* Increment the Type ID counter */
                    m_nextUnusedTypeID = Math.Max(lot.Index, m_nextUnusedTypeID);
                    m_nextUnusedTypeID++;
                }
                finally
                {
                    typeFile.Close();
                }
            }

            /* Add the object to the level linked to their type */
            foreach (LevelObject lo in lvlObjs)
            {
                AddObjectToLvl(lo, false);
            }
        }

        public LevelObjectType LoadTypeFile(System.IO.Stream stream)
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(stream);

            /* Verify the Type Being Loaded Matches the Current Application Version */
            String currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            String fileVersion = br.ReadString();
            if (fileVersion != currentVersion)
            {
                /* ERROR: File Version Out of Date */
                throw new IncorrectFileVersionException();
            }

            /* Read Type */
            LevelObjectType lot = new LevelObjectType();

            lot.TypeID = new Guid(br.ReadString());
            lot.Name = br.ReadString();
            lot.ShaderId = br.ReadString();
            lot.ModelFileName = br.ReadString();

            /* Type Parent Transforms */
            lot.Translation = FileOps.ReadVec3(br);
            lot.Rotation = FileOps.ReadVec3(br);
            lot.Scaling = FileOps.ReadVec3(br);

            return lot;
        }

        public void FetchModel(String modelFileName, IServiceProvider deviceService, out Model model)
        {
            model = null;

            /* Get short filename */
            String modelName = System.IO.Path.GetFileName(modelFileName);
            modelName = modelName.Substring(0, modelName.LastIndexOf('.'));

            /* Check if model is already loaded, and if so return it */
            if (models.TryGetValue(modelName, out model) && model != null)
            {
                return;
            }

            /* Model not found in memory store, so load it */
            ContentLoader loader = ContentLoader.AddRef();
            model = loader.LoadModel(deviceService, modelFileName, modelName);
            loader.Release(true);

            /* Hold on to the model for future display */
            models.Add(modelName, model);
        }

        public LevelTerrain LoadTerrainFile(System.IO.Stream stream)
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(stream);

            byte[] fileBuffer = new byte[stream.Length];
            br.Read(fileBuffer, 0, (int)stream.Length);

            LevelTerrain terrain = new LevelTerrain();
            Scale scales = new Scale();
            scales.Scales = new Vector3(10.0f, 1.0f, 10.0f);
            terrain.Scaling = scales;

            // assume square image
            int dim = (int)Math.Sqrt(stream.Length);
            terrain.HeightMapDimensions = new Point(dim, dim);
            terrain.HeightMap = fileBuffer;

            String name = "TERRAIN" + GetNextTerrainIdx();
            terrain.Name = name;
            return terrain;
        }

        #endregion // FileIO



        #region ILevelEvents Members

        public event ObjectInstanceAddedEventHandler ObjectInstanceAdded;

        public event ObjectInstanceRemovedEventHandler ObjectInstanceRemoved;

        public event ObjectTypeAddedEventHandler ObjectTypeAdded;

        public event ObjectTypeRemovedEventHandler ObjectTypeRemoved;

        public event ObjectTerrainAddedEventHandler ObjectTerrainAdded;

        public event ObjectTerrainRemovedEventHandler ObjectTerrainRemoved;

        #endregion

    }
}
