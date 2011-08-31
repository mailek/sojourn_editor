using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace LevelDesigner
{
    public class LevelObject
    {
        private String          name;
        private int             id;
        private Guid            typeId;

        public RotationEuler   rotation;
        private Scale           scale;
        private Translation     translation;

        #region Properties

        [DescriptionAttribute("The displayable name of the object instance.  This should be unique."),
        ReadOnlyAttribute(true)]
        public String Name 
        {
            get { return name; }
            set { name = value; }
        }

        [DescriptionAttribute("The unique hash ID of the object instance"),
        ReadOnlyAttribute(true)]
        public int Index
        {
            get { return id; }
            set { id = value; }
        }

        [DescriptionAttribute("The type of object for this object instance")]
        public Guid TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        [DescriptionAttribute("The facing rotation for the object when it spawns in the level")]
        public Vector3 Rotation
        {
            get { return rotation.Euler_Degrees; }
            set { rotation.Euler_Degrees = value; }
        }

        [DescriptionAttribute("The instance specific scaling for the object.  This could be used to make the same tree appear in the level multiple times with different size for each one.")]
        public Vector3 Scale
        {
            get { return scale.Scales; }
            set { scale.Scales = value; }
        }

        [DescriptionAttribute("The location of the object in the level.")]
        public Vector3 Position
        {
            get { return translation.Position; }
            set { translation.Position = value; }
        }
        #endregion // Properties

        public Matrix GetWorldXfm()
        {
            return scale.GetScalingMatrix() * rotation.GetRotationMatrix() * translation.GetTranslationMatrix();
        }

    }
}
