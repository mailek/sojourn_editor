using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    public struct RotationEuler
    {
        public enum EOrder
        {
            eOrder_invalid,
            eOrder_XYZ,
            eOrder_ZYX,
            eOrder_XZY
        };
        Vector3         euler_degrees;

        private EOrder order;

        public const float MAXDEGREES = 180;
        public const float MINDEGREES = -180;

        public EOrder Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public Vector3 Euler_Degrees
        {
            get
            {
                return euler_degrees;
            }
            set
            {
                euler_degrees = value;
            }
        }

        public float XDegrees
        {
            get
            {
                return euler_degrees.X;
            }
            set
            {
                float xr = value;
                xr = Math.Max(xr, MINDEGREES);
                xr = Math.Min(xr, MAXDEGREES);

                euler_degrees.X = xr;
            }
        }

        public float YDegrees
        {
            get
            {
                return euler_degrees.Y;
            }
            set
            {
                float yr = value;
                yr = Math.Max(yr, MINDEGREES);
                yr = Math.Min(yr, MAXDEGREES);

                euler_degrees.Y = yr;
            }
        }

        public float ZDegrees
        {
            get
            {
                return euler_degrees.Z;
            }
            set
            {
                float zr = value;
                zr = Math.Max(zr, MINDEGREES);
                zr = Math.Min(zr, MAXDEGREES);

                euler_degrees.Z = zr;
            }
        }

        public Matrix GetRotationMatrix()
        {
            Matrix[] mat = new Matrix[3];
            
            switch (order)
            {
                default:
                case EOrder.eOrder_ZYX:
                    mat[0] = Matrix.CreateRotationZ((float)Math.PI * 2 * euler_degrees.Z / 360.0f);
                    mat[1] = Matrix.CreateRotationY((float)Math.PI * 2 * euler_degrees.Y / 360.0f);
                    mat[2] = Matrix.CreateRotationX((float)Math.PI * 2 * euler_degrees.X / 360.0f);
                    break;
                case EOrder.eOrder_XZY:
                    mat[0] = Matrix.CreateRotationX((float)Math.PI * 2 * euler_degrees.X / 360.0f);
                    mat[1] = Matrix.CreateRotationZ((float)Math.PI * 2 * euler_degrees.Z / 360.0f);
                    mat[2] = Matrix.CreateRotationY((float)Math.PI * 2 * euler_degrees.Y / 360.0f);
                    break;
            }

            return mat[0] * mat[1] * mat[2];
        }

    }
}
