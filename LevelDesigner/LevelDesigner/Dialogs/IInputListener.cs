using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelDesigner
{
    interface IInputListener
    {
        void OnMouseMoved(Object ob, MouseEventArgs e);
        void OnMouseDown(Object ob, MouseEventArgs e);
        void OnMouseUp(Object ob, MouseEventArgs e);
        void OnKeyDown(Object ob, KeyEventArgs e);
        void OnKeyUp(Object ob, KeyEventArgs e);

    }
}
