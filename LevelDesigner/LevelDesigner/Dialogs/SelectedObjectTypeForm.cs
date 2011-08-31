using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelDesigner
{
    public partial class SelectedObjectTypeForm : Form
    {
        private int m_selectedIndex = -1;

        public int SelectedIndex
        {
            get { return m_selectedIndex; }
        }

        public SelectedObjectTypeForm()
        {
            InitializeComponent();
        }

        public void Init(String[] items)
        {
            foreach (String s in items)
            {
                cmb_typeSelect.Items.Add(s);
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            m_selectedIndex = cmb_typeSelect.SelectedIndex;

            this.Close();
        }
    }
}
