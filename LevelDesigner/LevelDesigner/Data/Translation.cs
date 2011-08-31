using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    public struct Translation
    {
        public Vector3         position;

        public Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Matrix GetTranslationMatrix()
        {
            return Matrix.CreateTranslation(position);
        }
    }
}
