namespace LevelDesigner
{
    partial class TerrainEditorForm
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
            ServiceManager serviceManager1 = new ServiceManager();
            this.btn_loadTerrain = new System.Windows.Forms.Button();
            this.fil_openTerrain = new System.Windows.Forms.OpenFileDialog();
            this.rdr_terrainView = new RenderPane();
            this.prp_terrainProperties = new System.Windows.Forms.PropertyGrid();
            this.btn_done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_loadTerrain
            // 
            this.btn_loadTerrain.Location = new System.Drawing.Point(254, 488);
            this.btn_loadTerrain.Name = "btn_loadTerrain";
            this.btn_loadTerrain.Size = new System.Drawing.Size(90, 36);
            this.btn_loadTerrain.TabIndex = 0;
            this.btn_loadTerrain.Text = "Set Terrain File";
            this.btn_loadTerrain.UseVisualStyleBackColor = true;
            this.btn_loadTerrain.Click += new System.EventHandler(this.btn_loadTerrain_Click);
            // 
            // rdr_terrainView
            // 
            this.rdr_terrainView.Location = new System.Drawing.Point(350, 12);
            this.rdr_terrainView.Name = "rdr_terrainView";
            this.rdr_terrainView.Services = serviceManager1;
            this.rdr_terrainView.Size = new System.Drawing.Size(670, 512);
            this.rdr_terrainView.TabIndex = 0;
            // 
            // prp_terrainProperties
            // 
            this.prp_terrainProperties.Location = new System.Drawing.Point(12, 248);
            this.prp_terrainProperties.Name = "prp_terrainProperties";
            this.prp_terrainProperties.Size = new System.Drawing.Size(332, 234);
            this.prp_terrainProperties.TabIndex = 1;
            // 
            // btn_done
            // 
            this.btn_done.Location = new System.Drawing.Point(12, 488);
            this.btn_done.Name = "btn_done";
            this.btn_done.Size = new System.Drawing.Size(90, 36);
            this.btn_done.TabIndex = 2;
            this.btn_done.Text = "Done";
            this.btn_done.UseVisualStyleBackColor = true;
            this.btn_done.Click += new System.EventHandler(this.btn_done_Click);
            // 
            // TerrainEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 536);
            this.Controls.Add(this.btn_done);
            this.Controls.Add(this.prp_terrainProperties);
            this.Controls.Add(this.rdr_terrainView);
            this.Controls.Add(this.btn_loadTerrain);
            this.Name = "TerrainEditorForm";
            this.Text = "LoadTerrainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_loadTerrain;
        private System.Windows.Forms.OpenFileDialog fil_openTerrain;
        private RenderPane rdr_terrainView;
        private System.Windows.Forms.PropertyGrid prp_terrainProperties;
        private System.Windows.Forms.Button btn_done;
    }
}