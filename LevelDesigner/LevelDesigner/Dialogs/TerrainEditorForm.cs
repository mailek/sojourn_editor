using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelDesigner
{
    partial class TerrainEditorForm : Form
    {
        public String m_dirTerrain ="";
        //Point m_terrainDimensions = new Point();
        private LevelObjectData m_data = null;
        IServiceProvider m_deviceService = null;
        LevelTerrain m_terrain = null;

        public LevelTerrain Terrain
        {
            get { return m_terrain; }
        }

        public TerrainEditorForm()
        {
            InitializeComponent();
        }

        public void Init(LevelObjectData data, IServiceProvider deviceService)
        {
            m_data = data;
            m_deviceService = deviceService;
        }

        private void btn_loadTerrain_Click(object sender, EventArgs e)
        {
            fil_openTerrain.FileName = m_dirTerrain;
            
            /* Open the load dialog */
            DialogResult loadResult = fil_openTerrain.ShowDialog();
            if (loadResult == DialogResult.Abort || loadResult == DialogResult.Cancel)
                return;

            System.IO.Stream stream = fil_openTerrain.OpenFile();

            /* Create the terrain (just height map, no mesh will be created yet) */
            m_terrain = m_data.LoadTerrainFile(stream);

            if (m_terrain != null)
            {
                prp_terrainProperties.SelectedObject = m_terrain;
                rdr_terrainView.ChangeViewMode(RenderPane.ViewMode.eTerrainViewer);
                rdr_terrainView.AddTerrainToScene(m_terrain);
            }
            
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
