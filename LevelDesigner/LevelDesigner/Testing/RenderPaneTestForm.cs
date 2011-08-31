using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using LevelDesigner.Testing;

namespace LevelDesigner
{
    public partial class RenderPaneTestForm : Form
    {
        LevelObjectData m_data = new LevelObjectData();
        RenderPaneTestData m_testData = new RenderPaneTestData();
        LevelObjectType m_lot = null;

        public RenderPaneTestForm()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            m_testData.Scale = new Vector3(1.0f, 1.0f, 1.0f);
            m_testData.Translate = new Vector3(0.0f, 50.0f, 0.0f);

            m_lot = m_data.CreateNewLevelObjectType();
            m_lot.ModelFileName = "C:\\Users\\Legend\\Documents\\Visual Studio 2008\\Projects\\LevelDesigner\\LevelDesigner\\Content\\tiny.x";

            rdr_renderPane.ChangeViewMode(RenderPane.ViewMode.eTypeViewer);
            rdr_renderPane.ShowObjectType(m_lot, m_data);

            m_testData.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.OnTestDataPropChanged);
            prp_properties.SelectedObject = m_testData;
        }

        void OnTestDataPropChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            m_lot.Translation = m_testData.translate.Position;
            m_lot.Rotation = m_testData.rotate.Euler_Degrees;
            m_lot.Scaling = m_testData.scale.Scales;
        }

    }
}
