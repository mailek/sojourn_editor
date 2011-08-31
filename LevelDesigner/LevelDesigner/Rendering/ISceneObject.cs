using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    interface ISceneObject : ILookAtTarget
    {
        void Draw();
        ReadOnlyCollection<Effect> Effects { get; }
        Matrix WorldXfm { get; }
        String GetID { get; }
        BoundingSphere BoundingSphere { get; }

    }
}
