namespace LevelDesigner
{
    partial class LevelDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelDesigner));
            ServiceManager serviceManager1 = new ServiceManager();
            this.lbl_LvlObjs = new System.Windows.Forms.Label();
            this.lst_lvlObjs = new System.Windows.Forms.ListBox();
            this.btn_newLvl = new System.Windows.Forms.Button();
            this.btn_loadLvl = new System.Windows.Forms.Button();
            this.btn_saveLvl = new System.Windows.Forms.Button();
            this.grp_FileOps = new System.Windows.Forms.GroupBox();
            this.lbl_FileName = new System.Windows.Forms.Label();
            this.pic_SkullGlow = new System.Windows.Forms.PictureBox();
            this.pic_Skull = new System.Windows.Forms.PictureBox();
            this.fil_lvlLoadDialog = new System.Windows.Forms.OpenFileDialog();
            this.fil_lvlSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.btn_CreateObj = new System.Windows.Forms.Button();
            this.btn_CreateObjType = new System.Windows.Forms.Button();
            this.lst_objTypes = new System.Windows.Forms.ListBox();
            this.tab_view = new System.Windows.Forms.TabControl();
            this.Instances = new System.Windows.Forms.TabPage();
            this.btn_showObject = new System.Windows.Forms.Button();
            this.btn_loadTerrain = new System.Windows.Forms.Button();
            this.Types = new System.Windows.Forms.TabPage();
            this.btn_typeLoadModel = new System.Windows.Forms.Button();
            this.btn_saveType = new System.Windows.Forms.Button();
            this.btn_loadType = new System.Windows.Forms.Button();
            this.lbl_types = new System.Windows.Forms.Label();
            this.prp_objProperties = new System.Windows.Forms.PropertyGrid();
            this.rdr_renderPane = new RenderPane();
            this.fil_typeLoadDialog = new System.Windows.Forms.OpenFileDialog();
            this.fil_typeSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.fil_typeLoadModelDialog = new System.Windows.Forms.OpenFileDialog();
            this.btn_clampObject = new System.Windows.Forms.Button();
            this.grp_FileOps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_SkullGlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Skull)).BeginInit();
            this.tab_view.SuspendLayout();
            this.Instances.SuspendLayout();
            this.Types.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_LvlObjs
            // 
            this.lbl_LvlObjs.AutoSize = true;
            this.lbl_LvlObjs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LvlObjs.Location = new System.Drawing.Point(80, 6);
            this.lbl_LvlObjs.Name = "lbl_LvlObjs";
            this.lbl_LvlObjs.Size = new System.Drawing.Size(106, 17);
            this.lbl_LvlObjs.TabIndex = 1;
            this.lbl_LvlObjs.Text = "Level Contents:";
            // 
            // lst_lvlObjs
            // 
            this.lst_lvlObjs.Enabled = false;
            this.lst_lvlObjs.FormattingEnabled = true;
            this.lst_lvlObjs.Location = new System.Drawing.Point(84, 26);
            this.lst_lvlObjs.Name = "lst_lvlObjs";
            this.lst_lvlObjs.ScrollAlwaysVisible = true;
            this.lst_lvlObjs.Size = new System.Drawing.Size(251, 199);
            this.lst_lvlObjs.TabIndex = 2;
            this.lst_lvlObjs.SelectedIndexChanged += new System.EventHandler(this.lst_lvlObjs_SelectedIndexChanged);
            // 
            // btn_newLvl
            // 
            this.btn_newLvl.Location = new System.Drawing.Point(6, 47);
            this.btn_newLvl.Name = "btn_newLvl";
            this.btn_newLvl.Size = new System.Drawing.Size(75, 23);
            this.btn_newLvl.TabIndex = 3;
            this.btn_newLvl.Text = "New Level";
            this.btn_newLvl.UseVisualStyleBackColor = true;
            this.btn_newLvl.Click += new System.EventHandler(this.btn_newLvl_Click);
            // 
            // btn_loadLvl
            // 
            this.btn_loadLvl.Location = new System.Drawing.Point(87, 47);
            this.btn_loadLvl.Name = "btn_loadLvl";
            this.btn_loadLvl.Size = new System.Drawing.Size(75, 23);
            this.btn_loadLvl.TabIndex = 4;
            this.btn_loadLvl.Text = "Load Level";
            this.btn_loadLvl.UseVisualStyleBackColor = true;
            this.btn_loadLvl.Click += new System.EventHandler(this.btn_loadLvl_Click);
            // 
            // btn_saveLvl
            // 
            this.btn_saveLvl.Enabled = false;
            this.btn_saveLvl.Location = new System.Drawing.Point(168, 47);
            this.btn_saveLvl.Name = "btn_saveLvl";
            this.btn_saveLvl.Size = new System.Drawing.Size(75, 23);
            this.btn_saveLvl.TabIndex = 5;
            this.btn_saveLvl.Text = "Save Level";
            this.btn_saveLvl.UseVisualStyleBackColor = true;
            this.btn_saveLvl.Click += new System.EventHandler(this.btn_saveLvl_Click);
            // 
            // grp_FileOps
            // 
            this.grp_FileOps.Controls.Add(this.lbl_FileName);
            this.grp_FileOps.Controls.Add(this.btn_newLvl);
            this.grp_FileOps.Controls.Add(this.pic_SkullGlow);
            this.grp_FileOps.Controls.Add(this.pic_Skull);
            this.grp_FileOps.Controls.Add(this.btn_saveLvl);
            this.grp_FileOps.Controls.Add(this.btn_loadLvl);
            this.grp_FileOps.Location = new System.Drawing.Point(12, 12);
            this.grp_FileOps.Name = "grp_FileOps";
            this.grp_FileOps.Size = new System.Drawing.Size(349, 79);
            this.grp_FileOps.TabIndex = 6;
            this.grp_FileOps.TabStop = false;
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.AutoSize = true;
            this.lbl_FileName.Location = new System.Drawing.Point(7, 20);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(0, 13);
            this.lbl_FileName.TabIndex = 6;
            // 
            // pic_SkullGlow
            // 
            this.pic_SkullGlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_SkullGlow.Image = ((System.Drawing.Image)(resources.GetObject("pic_SkullGlow.Image")));
            this.pic_SkullGlow.Location = new System.Drawing.Point(272, 21);
            this.pic_SkullGlow.Name = "pic_SkullGlow";
            this.pic_SkullGlow.Size = new System.Drawing.Size(46, 43);
            this.pic_SkullGlow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_SkullGlow.TabIndex = 11;
            this.pic_SkullGlow.TabStop = false;
            this.pic_SkullGlow.Visible = false;
            // 
            // pic_Skull
            // 
            this.pic_Skull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Skull.Image = ((System.Drawing.Image)(resources.GetObject("pic_Skull.Image")));
            this.pic_Skull.Location = new System.Drawing.Point(272, 21);
            this.pic_Skull.Name = "pic_Skull";
            this.pic_Skull.Size = new System.Drawing.Size(46, 43);
            this.pic_Skull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Skull.TabIndex = 9;
            this.pic_Skull.TabStop = false;
            // 
            // fil_lvlLoadDialog
            // 
            this.fil_lvlLoadDialog.Filter = "Level Definition Files (*.lvl) | *.lvl";
            this.fil_lvlLoadDialog.Title = "Load Level Definition";
            // 
            // fil_lvlSaveDialog
            // 
            this.fil_lvlSaveDialog.AddExtension = false;
            this.fil_lvlSaveDialog.Filter = "Level Files (*.lvl) | *.lvl | All Files (*.*) | *.*";
            this.fil_lvlSaveDialog.Title = "Save Level Definition";
            // 
            // btn_CreateObj
            // 
            this.btn_CreateObj.Enabled = false;
            this.btn_CreateObj.Location = new System.Drawing.Point(3, 26);
            this.btn_CreateObj.Name = "btn_CreateObj";
            this.btn_CreateObj.Size = new System.Drawing.Size(75, 49);
            this.btn_CreateObj.TabIndex = 8;
            this.btn_CreateObj.Text = "Create Object Instance";
            this.btn_CreateObj.UseVisualStyleBackColor = true;
            this.btn_CreateObj.Click += new System.EventHandler(this.btn_CreateObj_Click);
            // 
            // btn_CreateObjType
            // 
            this.btn_CreateObjType.Enabled = false;
            this.btn_CreateObjType.Location = new System.Drawing.Point(4, 26);
            this.btn_CreateObjType.Name = "btn_CreateObjType";
            this.btn_CreateObjType.Size = new System.Drawing.Size(75, 49);
            this.btn_CreateObjType.TabIndex = 10;
            this.btn_CreateObjType.Text = "Create Object Type";
            this.btn_CreateObjType.UseVisualStyleBackColor = true;
            this.btn_CreateObjType.Click += new System.EventHandler(this.btn_CreateObjType_Click);
            // 
            // lst_objTypes
            // 
            this.lst_objTypes.FormattingEnabled = true;
            this.lst_objTypes.Location = new System.Drawing.Point(85, 26);
            this.lst_objTypes.Name = "lst_objTypes";
            this.lst_objTypes.ScrollAlwaysVisible = true;
            this.lst_objTypes.Size = new System.Drawing.Size(250, 199);
            this.lst_objTypes.TabIndex = 12;
            this.lst_objTypes.SelectedIndexChanged += new System.EventHandler(this.lst_objTypes_SelectedIndexChanged);
            // 
            // tab_view
            // 
            this.tab_view.Controls.Add(this.Instances);
            this.tab_view.Controls.Add(this.Types);
            this.tab_view.Location = new System.Drawing.Point(12, 94);
            this.tab_view.Name = "tab_view";
            this.tab_view.SelectedIndex = 0;
            this.tab_view.Size = new System.Drawing.Size(349, 293);
            this.tab_view.TabIndex = 14;
            this.tab_view.SelectedIndexChanged += new System.EventHandler(this.tab_view_SelectedIndexChanged);
            // 
            // Instances
            // 
            this.Instances.BackColor = System.Drawing.SystemColors.Control;
            this.Instances.Controls.Add(this.btn_clampObject);
            this.Instances.Controls.Add(this.btn_showObject);
            this.Instances.Controls.Add(this.btn_loadTerrain);
            this.Instances.Controls.Add(this.lst_lvlObjs);
            this.Instances.Controls.Add(this.lbl_LvlObjs);
            this.Instances.Controls.Add(this.btn_CreateObj);
            this.Instances.Location = new System.Drawing.Point(4, 22);
            this.Instances.Name = "Instances";
            this.Instances.Padding = new System.Windows.Forms.Padding(3);
            this.Instances.Size = new System.Drawing.Size(341, 267);
            this.Instances.TabIndex = 0;
            this.Instances.Text = "Instances";
            // 
            // btn_showObject
            // 
            this.btn_showObject.Location = new System.Drawing.Point(291, 231);
            this.btn_showObject.Name = "btn_showObject";
            this.btn_showObject.Size = new System.Drawing.Size(44, 23);
            this.btn_showObject.TabIndex = 10;
            this.btn_showObject.Text = "Show";
            this.btn_showObject.UseVisualStyleBackColor = true;
            this.btn_showObject.Click += new System.EventHandler(this.btn_showObject_Click);
            // 
            // btn_loadTerrain
            // 
            this.btn_loadTerrain.Location = new System.Drawing.Point(3, 81);
            this.btn_loadTerrain.Name = "btn_loadTerrain";
            this.btn_loadTerrain.Size = new System.Drawing.Size(75, 49);
            this.btn_loadTerrain.TabIndex = 9;
            this.btn_loadTerrain.Text = "Edit Terrain";
            this.btn_loadTerrain.UseVisualStyleBackColor = true;
            this.btn_loadTerrain.Click += new System.EventHandler(this.btn_loadTerrain_Click);
            // 
            // Types
            // 
            this.Types.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Types.Controls.Add(this.btn_typeLoadModel);
            this.Types.Controls.Add(this.btn_saveType);
            this.Types.Controls.Add(this.btn_loadType);
            this.Types.Controls.Add(this.lbl_types);
            this.Types.Controls.Add(this.btn_CreateObjType);
            this.Types.Controls.Add(this.lst_objTypes);
            this.Types.Location = new System.Drawing.Point(4, 22);
            this.Types.Name = "Types";
            this.Types.Padding = new System.Windows.Forms.Padding(3);
            this.Types.Size = new System.Drawing.Size(341, 267);
            this.Types.TabIndex = 1;
            this.Types.Text = "Types";
            // 
            // btn_typeLoadModel
            // 
            this.btn_typeLoadModel.Location = new System.Drawing.Point(2, 191);
            this.btn_typeLoadModel.Name = "btn_typeLoadModel";
            this.btn_typeLoadModel.Size = new System.Drawing.Size(75, 49);
            this.btn_typeLoadModel.TabIndex = 18;
            this.btn_typeLoadModel.Text = "Load Model";
            this.btn_typeLoadModel.UseVisualStyleBackColor = true;
            this.btn_typeLoadModel.Click += new System.EventHandler(this.btn_typeLoadModel_Click);
            // 
            // btn_saveType
            // 
            this.btn_saveType.Location = new System.Drawing.Point(3, 136);
            this.btn_saveType.Name = "btn_saveType";
            this.btn_saveType.Size = new System.Drawing.Size(75, 49);
            this.btn_saveType.TabIndex = 17;
            this.btn_saveType.Text = "Save Object Type";
            this.btn_saveType.UseVisualStyleBackColor = true;
            this.btn_saveType.Click += new System.EventHandler(this.btn_saveType_Click);
            // 
            // btn_loadType
            // 
            this.btn_loadType.Location = new System.Drawing.Point(4, 81);
            this.btn_loadType.Name = "btn_loadType";
            this.btn_loadType.Size = new System.Drawing.Size(75, 49);
            this.btn_loadType.TabIndex = 17;
            this.btn_loadType.Text = "Load Object Type";
            this.btn_loadType.UseVisualStyleBackColor = true;
            this.btn_loadType.Click += new System.EventHandler(this.btn_loadType_Click);
            // 
            // lbl_types
            // 
            this.lbl_types.AutoSize = true;
            this.lbl_types.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_types.Location = new System.Drawing.Point(82, 6);
            this.lbl_types.Name = "lbl_types";
            this.lbl_types.Size = new System.Drawing.Size(101, 17);
            this.lbl_types.TabIndex = 14;
            this.lbl_types.Text = "Object Palette:";
            // 
            // prp_objProperties
            // 
            this.prp_objProperties.Location = new System.Drawing.Point(12, 393);
            this.prp_objProperties.Name = "prp_objProperties";
            this.prp_objProperties.Size = new System.Drawing.Size(349, 166);
            this.prp_objProperties.TabIndex = 16;
            this.prp_objProperties.SelectedObjectsChanged += new System.EventHandler(this.prp_objProperties_SelectionChanged);
            // 
            // rdr_renderPane
            // 
            this.rdr_renderPane.Enabled = false;
            this.rdr_renderPane.Location = new System.Drawing.Point(367, 12);
            this.rdr_renderPane.Name = "rdr_renderPane";
            this.rdr_renderPane.Services = serviceManager1;
            this.rdr_renderPane.Size = new System.Drawing.Size(781, 547);
            this.rdr_renderPane.TabIndex = 18;
            this.rdr_renderPane.Text = "button2";
            // 
            // fil_typeLoadDialog
            // 
            this.fil_typeLoadDialog.Filter = "Object Type Files (*.obt) | *.obt";
            this.fil_typeLoadDialog.Title = "Open Object Type";
            // 
            // fil_typeSaveDialog
            // 
            this.fil_typeSaveDialog.Filter = "Object Type Files (*.obt) | *.obt";
            this.fil_typeSaveDialog.Title = "Save Object Type";
            // 
            // fil_typeLoadModelDialog
            // 
            this.fil_typeLoadModelDialog.Filter = "DirectX Model Files (*.x) | *.x";
            // 
            // btn_clampObject
            // 
            this.btn_clampObject.Location = new System.Drawing.Point(241, 231);
            this.btn_clampObject.Name = "btn_clampObject";
            this.btn_clampObject.Size = new System.Drawing.Size(44, 23);
            this.btn_clampObject.TabIndex = 11;
            this.btn_clampObject.Text = "Clamp";
            this.btn_clampObject.UseVisualStyleBackColor = true;
            this.btn_clampObject.Click += new System.EventHandler(this.btn_ClampObject_Click);
            // 
            // LevelDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 571);
            this.Controls.Add(this.rdr_renderPane);
            this.Controls.Add(this.prp_objProperties);
            this.Controls.Add(this.tab_view);
            this.Controls.Add(this.grp_FileOps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LevelDesigner";
            this.Text = "Level Designer 5.11";
            this.grp_FileOps.ResumeLayout(false);
            this.grp_FileOps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_SkullGlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Skull)).EndInit();
            this.tab_view.ResumeLayout(false);
            this.Instances.ResumeLayout(false);
            this.Instances.PerformLayout();
            this.Types.ResumeLayout(false);
            this.Types.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_LvlObjs;
        private System.Windows.Forms.ListBox lst_lvlObjs;
        private System.Windows.Forms.Button btn_newLvl;
        private System.Windows.Forms.Button btn_loadLvl;
        private System.Windows.Forms.Button btn_saveLvl;
        private System.Windows.Forms.GroupBox grp_FileOps;
        private System.Windows.Forms.OpenFileDialog fil_lvlLoadDialog;
        private System.Windows.Forms.SaveFileDialog fil_lvlSaveDialog;
        private System.Windows.Forms.Button btn_CreateObj;
        private System.Windows.Forms.Label lbl_FileName;
        private System.Windows.Forms.PictureBox pic_Skull;
        private System.Windows.Forms.Button btn_CreateObjType;
        private System.Windows.Forms.PictureBox pic_SkullGlow;
        private System.Windows.Forms.ListBox lst_objTypes;
        private System.Windows.Forms.TabControl tab_view;
        private System.Windows.Forms.TabPage Instances;
        private System.Windows.Forms.TabPage Types;
        private System.Windows.Forms.Label lbl_types;
        private System.Windows.Forms.PropertyGrid prp_objProperties;
        private System.Windows.Forms.Button btn_loadType;
        private System.Windows.Forms.Button btn_saveType;
        private System.Windows.Forms.Button btn_typeLoadModel;
        private RenderPane rdr_renderPane;
        private System.Windows.Forms.OpenFileDialog fil_typeLoadDialog;
        private System.Windows.Forms.SaveFileDialog fil_typeSaveDialog;
        private System.Windows.Forms.OpenFileDialog fil_typeLoadModelDialog;
        private System.Windows.Forms.Button btn_loadTerrain;
        private System.Windows.Forms.Button btn_showObject;
        private System.Windows.Forms.Button btn_clampObject;
    }
}

