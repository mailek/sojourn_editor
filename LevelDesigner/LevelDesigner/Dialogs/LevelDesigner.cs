using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using LevelDesigner.Data;

namespace LevelDesigner
{
    public partial class LevelDesigner : Form
    {
        // Attributes ///////////////////////////////////////////
        private LevelObjectData m_data = new LevelObjectData();
        private bool m_dataDirty = false;
        private Object lastSelectedObject = null;
        private const int MAX_DISPLAY_FILENAME_SZ = 25;

        // Methods //////////////////////////////////////////////
        public LevelDesigner()
        {
            InitializeComponent();
            SetDialogEnabled(false);
            SetTitleText();

            /* Subscribe to changes in the level definition.  
             * This is so we can update the scene in the render pane */
            m_data.ObjectInstanceAdded += new ObjectInstanceAddedEventHandler(OnObjectInstanceAdded);
            m_data.ObjectInstanceRemoved += new ObjectInstanceRemovedEventHandler(OnObjectInstanceRemoved);
            m_data.ObjectTypeAdded += new ObjectTypeAddedEventHandler(OnObjectTypeAdded);
            m_data.ObjectTypeRemoved += new ObjectTypeRemovedEventHandler(OnObjectTypeRemoved);
            m_data.ObjectTerrainAdded += new ObjectTerrainAddedEventHandler(OnObjectTerrainAdded);
            m_data.ObjectTerrainRemoved += new ObjectTerrainRemovedEventHandler(OnObjectTerrainRemoved);

            /* Subscribe to object picking events from the render pane */
            rdr_renderPane.LevelObjectSelected += new RenderPane.LevelObjectSelectedEventHandler(OnLevelObjectSelected);
        
        }

        private void CreateNewObjectInstance(LevelObjectType lot)
        {
            LevelObject lo = m_data.CreateNewLevelObjectInstance();
            lo.TypeId = lot.TypeID;

            /* Add new object to level definition */
            m_data.AddObjectToLvl(lo, false);

            /* Update Dialog with new item */
            lst_lvlObjs.Items.Add(lo.Name);

            DirtyData(true);
        }

        private void CreateNewObjectType()
        {
            LevelObjectType lot = m_data.CreateNewLevelObjectType();

            /* Add new type to type palette */
            m_data.AddObjectTypeToPalette(lot, false);

            /* Update Type Dialog with new item */
            lst_objTypes.Items.Add(lot.name);

            DirtyData(true);
        }

        private void CreateNewLevel()
        {
            // Check for unsaved data 
            if (m_dataDirty == true && PromptForDirtySave() == DialogResult.Cancel)
                return;

            // Get the level name for the user
            String input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter a name for the new level.",
                "Create New Level",
                "", 400, 400);
            input = input.Trim();
            input = input.Replace(" ", "");

            m_data.levelName = input;

            // init/clear the dialog for a new level
            SetDialogEnabled(true);
            DirtyData(false);
            lst_lvlObjs.Items.Clear();
            lbl_FileName.Text = "New ***** not saved *****";
            SetTitleText();
        }

        private void DirtyData(bool isDirty)
        {
            m_dataDirty = isDirty;
            pic_SkullGlow.Visible = isDirty;
            btn_saveLvl.Enabled = isDirty;
        }

        private void LoadObjectType()
        {
            /* Open the load dialog */
            DialogResult loadResult = this.fil_typeLoadDialog.ShowDialog();
            if (loadResult == DialogResult.Abort || loadResult == DialogResult.Cancel)
                return;

            /* Load the type definition from the file */
            System.IO.Stream stream = fil_typeLoadDialog.OpenFile();

            try
            {
                LevelObjectType lot = m_data.LoadTypeFile(stream);

                /* Save the filename */
                lot.FileName = fil_typeLoadDialog.FileName;

                m_data.AddObjectTypeToPalette(lot, false);
            }
            catch
            {
                throw;
            }
            finally
            {
                stream.Close();
            }

            /* Update the Dialog */
            RepopulateObjectPalette();
            DirtyData(true);
        }

        private void LoadLevel()
        {
            /* Check for unsaved data */
            if (m_dataDirty == true && PromptForDirtySave() == DialogResult.Cancel)
                return;

            /* Open the load dialog */
            DialogResult loadResult = fil_lvlLoadDialog.ShowDialog();
            if (loadResult == DialogResult.Abort || loadResult == DialogResult.Cancel)
                return;

            /* Load the level definition from the file */
            System.IO.Stream stream = fil_lvlLoadDialog.OpenFile();

            SetDialogEnabled(true);
            rdr_renderPane.ChangeViewMode(RenderPane.ViewMode.eLevelViewer);

            try
            {
                /* Save the filename */
                m_data.LevelFileName = fil_lvlLoadDialog.FileName;

                String fileName = m_data.LevelFileName;
                if (fileName.Length > MAX_DISPLAY_FILENAME_SZ)
                {
                    String shortFilePath = fileName.Substring(0, fileName.IndexOf('\\')+1); // get the drive
                    String fileDir = fileName.Substring(fileName.LastIndexOf('\\', fileName.LastIndexOf('\\', fileName.LastIndexOf('\\') - 1) - 1));
                    shortFilePath += "~" + fileDir;
                    fileName = shortFilePath;
                }

                lbl_FileName.Text = fileName;

                m_data.LoadLevelFile(stream);

            }
            catch
            {
                m_data.LevelFileName = "";
                lbl_FileName.Text = "";

                throw;
            }
            finally
            {
                stream.Close();
            }

            /* RecalculateXfms the Dialog */
            RepopulateInstanceList();
            RepopulateObjectPalette();
            DirtyData(false);
            SetTitleText();
            
            /* Populate the scene */
            //foreach (KeyValuePair<String, LevelObject> kv in m_data.worldObjects)
            //{
            //    rdr_renderPane.AddObjectInstanceToScene(kv.Value, m_data);
            //}

            LevelTerrain terrain = m_data.GetTerrain();
            if (terrain != null)
            {
                rdr_renderPane.AddTerrainToScene(terrain);
            }
        }

        private DialogResult PromptForDirtySave()
        {
            /* Prompt user */
            DialogResult dirtyResult = MessageBox.Show(
                "Would you like to save the current level before continuing?",
                "Unsaved Changes",
                MessageBoxButtons.YesNoCancel);

            /* Save the level definition */
            if (dirtyResult == DialogResult.Yes)
                SaveLevel();

            return dirtyResult;
        }

        private void RepopulateInstanceList()
        {
            lst_lvlObjs.Items.Clear();

            foreach (KeyValuePair<String, LevelObject> kv in m_data.worldObjects)
            {
                LevelObject lo = kv.Value;
                lst_lvlObjs.Items.Add(lo.Name);
            }
        }

        private void RepopulateObjectPalette()
        {
            this.lst_objTypes.Items.Clear();

            foreach (KeyValuePair<String, LevelObjectType> kv in m_data.objectPalette)
            {
                LevelObjectType lot = kv.Value;
                lst_objTypes.Items.Add(lot.name);
            }
        }

        private void SaveSelectedObjectType()
        {
            /* Get the selected LevelObject Type from the type list */
            String typeName = lst_objTypes.SelectedItem.ToString();
            LevelObjectType lot = m_data.GetObjectTypeByStringID(typeName);

            if (lot == null)
            {
                throw new System.Exception();
            }

            System.IO.Stream saveStream;
            if (!String.IsNullOrEmpty(lot.FileName))
            {
                // type file exists, try to quick save
                saveStream = System.IO.File.Open(lot.FileName, System.IO.FileMode.Open);
            }
            else
            {
                // pop save dialog
                DialogResult result = fil_typeSaveDialog.ShowDialog();
                
                /* Bail if the user cancelled */
                if (result == DialogResult.Abort || result == DialogResult.Cancel)
                    return;

                saveStream = fil_typeSaveDialog.OpenFile();
                lot.FileName = fil_typeSaveDialog.FileName;
            }

            /* Dump the type definition to the output file */
            m_data.EnsureObjectType(lot, saveStream);

            /* Sync the Dialog state to the object palette */
            RepopulateObjectPalette();
            DirtyData(true);
            
        }

        private void SaveLevel()
        {
            /* Assert assumption that save button is not
             * active when level definition is not dirty */
            System.Diagnostics.Debug.Assert(m_dataDirty);

            /* Open Save Dialog */
            String filename = m_data.levelName;
            filename = filename.Replace(" ", "");
            fil_lvlSaveDialog.FileName = filename;
            DialogResult result = fil_lvlSaveDialog.ShowDialog();

            /* Bail if the user cancelled */
            if (result == DialogResult.Abort || result == DialogResult.Cancel)
                return;

            /* Dump the level definition to the output file */
            System.IO.Stream saveStream = fil_lvlSaveDialog.OpenFile();
            try
            {
                m_data.EnsureLevelDefinition(saveStream);
            }
            catch (LevelObjectData.UnsavedObjectTypeException)
            {
                MessageBox.Show("You have unsaved object types.  The level save action has been cancelled.", "Error: Unsaved Object Types");
                return;
            }
            finally
            {
                saveStream.Close();
            }

            m_data.LevelFileName = fil_lvlSaveDialog.FileName;

            /* Sync the Dialog state to the level definition */
            RepopulateInstanceList();
            DirtyData(false);
            SetTitleText();
        }

        private void SetTitleText()
        {
            String fileStr = "";

            if (m_data.LevelFileName.Length > 0)
            {
                fileStr = String.Format(" ({0}) - ", m_data.LevelFileName);
            }

            this.Text = String.Format("{0}{1}Level Designer {2}", 
                m_data.levelName, 
                fileStr, 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void ShowDetailsOnLevelObject(Object o)
        {        
            prp_objProperties.SelectedObject = o;
        }

        private void SetDialogEnabled(bool enabled)
        {
            // main form
            prp_objProperties.Enabled = enabled;
            pic_Skull.Visible = enabled;
            tab_view.Enabled = enabled;
            rdr_renderPane.Enabled = enabled;
            rdr_renderPane.Visible = enabled;
            if (enabled && m_dataDirty == true)
            {
                pic_SkullGlow.Visible = true;
            }
            else
            {
                pic_SkullGlow.Visible = false;
            }

            // instances tab
            btn_CreateObj.Enabled = enabled;
            lst_lvlObjs.Enabled = enabled;
            btn_showObject.Enabled = enabled;

            // types tab
            btn_loadType.Enabled = enabled;
            btn_CreateObjType.Enabled = enabled;
            lst_objTypes.Enabled = enabled;
            if (!enabled)
            {
                btn_saveType.Enabled = false;
                btn_typeLoadModel.Enabled = false;
            }
            
        }

        private void ShowObjectTypeInViewPane(LevelObjectType lot)
        {
            if (lot == null)
            {
                rdr_renderPane.ChangeViewMode(RenderPane.ViewMode.eDisabled);
                return;
            }

            rdr_renderPane.ShowObjectType(lot, m_data);
        }

    /***************************
     * Event Handlers  *
     ***************************/

        private void btn_loadLvl_Click(object sender, EventArgs e)
        {
            try
            {
                LoadLevel();
            }
            catch (LevelObjectData.IncorrectFileVersionException)
            {
                System.Windows.Forms.MessageBox.Show(
                "Incorrect Level Version",
                "Error");

                return;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "ERROR: Unhandled Exception");
            }
        }

        private void btn_ClampObject_Click(object sender, EventArgs e)
        {
            if (lst_lvlObjs.SelectedItem != null)
            {
                LevelObject lo = m_data.GetObjectByStringID(lst_lvlObjs.SelectedItem.ToString());
                if (lo != null)
                {
                    m_data.GroundClampObject(lo);
                }
            }
        }

        private void btn_CreateObj_Click(object sender, EventArgs e)
        {
            LevelObjectType[] types;
            int cnt = m_data.GetObjectTypeList(out types);

            if (cnt <= 0)
            {
                // no types loaded, error box and cancel create action
                MessageBox.Show("No Object Types Loaded", "Error");
                return;
            }

            String[] names = new String[cnt];

            for(int i = 0; i < cnt; i++)
            {
                names[i] = types[i].Name;
            }

            SelectedObjectTypeForm form = new SelectedObjectTypeForm();
            form.Init(names);

            form.ShowDialog();
            int index = form.SelectedIndex;

            if (index >= 0)
            {
                CreateNewObjectInstance(types[index]);
            }
        }

        private void btn_newLvl_Click(object sender, EventArgs e)
        {
            CreateNewLevel();
        }

        private void btn_saveLvl_Click(object sender, EventArgs e)
        {
            SaveLevel();
        }

        private void btn_CreateObjType_Click(object sender, EventArgs e)
        {
            CreateNewObjectType();
        }

        private void btn_loadType_Click(object sender, EventArgs e)
        {
            try
            {
                LoadObjectType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_saveType_Click(object sender, EventArgs e)
        {
            SaveSelectedObjectType();
        }

        private void btn_typeLoadModel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.lst_objTypes.SelectedIndex < 0)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }

            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fil_typeLoadModelDialog.InitialDirectory = contentPath;
            DialogResult result = fil_typeLoadModelDialog.ShowDialog();
            if (result == DialogResult.Abort || result == DialogResult.Cancel)
                return;

            Microsoft.Xna.Framework.Graphics.Model model;
            m_data.FetchModel(fil_typeLoadModelDialog.FileName, rdr_renderPane.Services, out model);

            String typeName = lst_objTypes.SelectedItem.ToString();
            LevelObjectType lot = m_data.GetObjectTypeByStringID(typeName);

            if (lot == null)
            {
                throw new System.Exception();
            }

            lot.ModelFileName = fil_typeLoadModelDialog.FileName;
            prp_objProperties.SelectedObject = lot;

            this.Cursor = Cursors.Arrow;
            ShowObjectTypeInViewPane(lot);
        }

        private void btn_loadTerrain_Click(object sender, EventArgs e)
        {
            TerrainEditorForm terrForm = new TerrainEditorForm();
            terrForm.Init(m_data, rdr_renderPane.Services);
            terrForm.ShowDialog();

            /* get the terrain and add to scene */
            LevelTerrain terrain = terrForm.Terrain;

            /* Save the terrain in the Level */
            rdr_renderPane.ChangeViewMode(RenderPane.ViewMode.eLevelViewer);
            m_data.AddTerrainToLvl(terrain, false);

        }

        private void btn_showObject_Click(object sender, EventArgs e)
        {
            if (lst_lvlObjs.SelectedIndex >= 0)
            {
                LevelObject lo = m_data.GetObjectByStringID(lst_lvlObjs.SelectedItem.ToString());
                rdr_renderPane.LookAtObjectById(lo.Name);
            }
        }

        private void frm_Closing(object sender, EventArgs e)
        { // TODO: bug here, need to check argument signature from form closing event handler

            /* Check for unsaved data */
            if (m_dataDirty == true)
            {
                DialogResult result = PromptForDirtySave();
                if (result == DialogResult.Cancel)
                {
                    // TODO: stop form from closing
                    return;
                }
            }
        }

        private void lst_lvlObjs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_lvlObjs.SelectedIndex < 0)
                return;

            // find the selected item in the Level Definition
            LevelObject lo = m_data.GetObjectByStringID(lst_lvlObjs.SelectedItem.ToString());

            ShowDetailsOnLevelObject(lo);
        }

        private void lst_objTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_objTypes.SelectedIndex < 0)
            {
                return;
            }

            // find the selected item in the Level Definition
            LevelObjectType lot = m_data.GetObjectTypeByStringID(lst_objTypes.SelectedItem.ToString());

            ShowDetailsOnLevelObject(lot);

            // if the type has not been saved, then enabled the save button
            if (String.IsNullOrEmpty(lot.filename))
            {
                btn_saveType.Enabled = true;
            }

            btn_typeLoadModel.Enabled = true;

            ShowObjectTypeInViewPane(lot.ModelFileName == "none" ? null : lot);            
        }


        void OnLevelObjectTypePropChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LevelObjectType lot = (LevelObjectType)sender;
            ShowObjectTypeInViewPane(lot);

            // enable the save button
//            btn_saveType.Enabled = true;
            DirtyData(true);
        }

        public void prp_objProperties_SelectionChanged(object sender, EventArgs e)
        {
            Object prev = this.lastSelectedObject;

            if (prev != null && prev.GetType().ToString() == "LevelDesigner.LevelObjectType")
            {
                LevelObjectType lot = (LevelObjectType)prev;
                lot.PropertyChanged -= this.OnLevelObjectTypePropChanged;
            }

            this.lastSelectedObject = prp_objProperties.SelectedObject;

            if (this.lastSelectedObject != null && this.lastSelectedObject.GetType().ToString() == "LevelDesigner.LevelObjectType")
            {
                LevelObjectType lot = (LevelObjectType)this.lastSelectedObject;
                lot.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.OnLevelObjectTypePropChanged);
            }
            
        }

        public void tab_view_SelectedIndexChanged(object sender, EventArgs e)
        {
            prp_objProperties.SelectedObject = null;

            if (tab_view.SelectedTab.Name == "Instances")
            {
                rdr_renderPane.ChangeViewMode(RenderPane.ViewMode.eLevelViewer);

                /* Re-populate the scene */
                foreach (KeyValuePair<String, LevelObject> kv in m_data.worldObjects)
                {
                    rdr_renderPane.AddObjectInstanceToScene(kv.Value, m_data);
                }

                LevelTerrain terrain = m_data.GetTerrain();
                if (terrain != null)
                {
                    rdr_renderPane.AddTerrainToScene(terrain);
                }
            }
            else if (tab_view.SelectedTab.Name == "Types")
            {
                rdr_renderPane.ChangeViewMode(RenderPane.ViewMode.eTypeViewer);

                if (lst_objTypes.SelectedIndex >= 0)
                {
                    LevelObjectType lot = m_data.GetObjectTypeByStringID(lst_objTypes.SelectedItem.ToString());
                    rdr_renderPane.ShowObjectType(lot, m_data);
                }
            }
        }

        #region LevelObjectEvent Handlers

        public void OnObjectInstanceAdded(object sender, LevelEventArgs e)
        {
            rdr_renderPane.AddObjectInstanceToScene(e.lo, m_data);
            DirtyData(true);
        }

        public void OnObjectInstanceRemoved(object sender, LevelEventArgs e)
        {
            DirtyData(true);
        }

        public void OnObjectTypeAdded(object sender, LevelEventArgs e)
        {
            DirtyData(true);
        }

        public void OnObjectTypeRemoved(object sender, LevelEventArgs e)
        {
            DirtyData(true);
        }

        public void OnObjectTerrainAdded(object sender, LevelEventArgs e)
        {
            rdr_renderPane.AddTerrainToScene(e.terr);
            DirtyData(true);
        }

        public void OnObjectTerrainRemoved(object sender, LevelEventArgs e)
        {
            DirtyData(true);
        }
        #endregion

        #region RenderPaneEventHandlers

        void OnLevelObjectSelected(object sender, RenderPane.RenderPaneEventArgs e)
        {
            LevelObject lo = e.LevelObjectInstance;
            rdr_renderPane.LookAtObjectById(lo.Name);
        }
        #endregion

    }
}
