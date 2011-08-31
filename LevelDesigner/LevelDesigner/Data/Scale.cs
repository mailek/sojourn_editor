using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    public struct Scale
    {
        public Vector3 scales;

        public Vector3 Scales
        {
            get
            {
                return scales;
            }

            set
            {
                scales = value;
            }
        }

        public Matrix GetScalingMatrix()
        {
            return Matrix.CreateScale(scales);
        }

    }
}
