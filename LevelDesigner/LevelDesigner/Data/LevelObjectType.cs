using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    public class LevelObjectType : System.ComponentModel.INotifyPropertyChanged
    {
        public String name;
        public String shaderId;
        public Guid typeId;
        public int index;
        public String filename;
        public bool isDirty = false;
        public String modelFilename;
        public RotationEuler rotation;
        public Scale scale;
        public Translation translate;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public String ShaderId
        {
            get { return shaderId; }
            set { isDirty = true; shaderId = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("shaderid")); }
        }

        public String Name
        {
            get { return name; }
            set { isDirty = true; name = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("name")); }
        }

        public Guid TypeID
        {
            get { return typeId; }
            set { isDirty = true; typeId = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("typeid")); }
        }

        public String FileName
        {
            get { return filename; }
            set { isDirty = true; filename = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("filename")); }
        }

        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("isdirty")); }
        }

        public String ModelFileName
        {
            get { return modelFilename; }
            set { isDirty = true; modelFilename = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("modelfilename")); }
        }

        public Vector3 Rotation
        {
            get { return rotation.Euler_Degrees; }
            set { isDirty = true; rotation.Euler_Degrees = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("rotation")); }
        }

        public Vector3 Scaling
        {
            get { return scale.Scales; }
            set { isDirty = true; scale.Scales = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("scaling")); }
        }

        public Vector3 Translation
        {
            get { return translate.Position; }
            set { isDirty = true; translate.Position = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("translation")); }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
