using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using System.IO;

namespace LevelDesigner
{
    class FileOps
    {
        public struct LevelObjectTypeDefinition
        {
            Guid typeId;
            String fileName;

            public String FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }

            public Guid TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }
        }

        #region ReadOps
        static public Matrix ReadMatrix(BinaryReader br)
        {
            //Matrix matrix = new Matrix();

            throw new System.Exception("Not Implemented");
            //return matrix;
        }

        static public Vector3 ReadVec3(BinaryReader br)
        {
            Vector3 vec3 = new Vector3();

            vec3.X = br.ReadSingle();
            vec3.Y = br.ReadSingle();
            vec3.Z = br.ReadSingle();
            
            return vec3;
        }

        public static List<FileOps.LevelObjectTypeDefinition> ReadObjectPalette(BinaryReader br)
        {
            List<LevelObjectTypeDefinition> list = new List<LevelObjectTypeDefinition>();

            uint numOfTypes = br.ReadUInt32();

            for (int i = 0; i < numOfTypes; i++)
            {
                FileOps.LevelObjectTypeDefinition t = new LevelObjectTypeDefinition();

                t.FileName = br.ReadString();
                t.TypeId = new Guid(br.ReadString());
                list.Add(t);
            }

            return list;

        }

        #endregion // ReadOps

        #region WriteOps
        public static void WriteMatrix(BinaryWriter bw)
        {
            throw new System.Exception("Not Implemented");
        }

        public static void WriteVec3(Vector3 v, BinaryWriter bw)
        {
            bw.Write(v.X);
            bw.Write(v.Y);
            bw.Write(v.Z);
            bw.Flush();
        }

        public static void WriteObjectPalette(BinaryWriter bw, List<LevelObjectTypeDefinition> types)
        {
            uint numOfTypes = (uint)types.Count;

            bw.Write(numOfTypes);

            foreach (LevelObjectTypeDefinition t in types)
            {
                bw.Write(t.FileName);
                bw.Write(t.TypeId.ToString());
            }
        }
        #endregion // WriteOps


    }

    
}
