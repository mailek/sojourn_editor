using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace LevelDesigner.Testing
{
    class RenderPaneTestData : System.ComponentModel.INotifyPropertyChanged
    {
        public Translation translate = new Translation();
        public RotationEuler rotate = new RotationEuler();
        public Scale scale = new Scale();

        public Vector3 Translate
        {
            get { return translate.Position; }
            set { translate.Position = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("translation")); }
        }

        public Vector3 Rotate
        {
            get { return rotate.Euler_Degrees; }
            set { rotate.Euler_Degrees = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("rotation")); }
        }
        public Vector3 Scale
        {
            get { return scale.Scales; }
            set { scale.Scales = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("scale")); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
