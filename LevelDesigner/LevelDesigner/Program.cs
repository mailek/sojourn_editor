using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LevelDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if (RENDERPANETEST == true)
            Application.Run(new RenderPaneTestForm());
#else
            Application.Run(new LevelDesigner());
#endif
        }
    }
}
