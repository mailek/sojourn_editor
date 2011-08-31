using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    interface ILookAtTarget
    {
        Vector3 GetPosition3D();
    }
}
