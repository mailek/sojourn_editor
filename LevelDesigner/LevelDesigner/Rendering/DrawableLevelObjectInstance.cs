using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace LevelDesigner
{
    class DrawableLevelObjectInstance : ISceneObject
    {
        private LevelObject m_lo = null;
        private LevelObjectType m_lot = null;
        private Model m_model = null;
        private ServiceManager m_services = null;
        private ReadOnlyCollection<Effect> m_effects = null;
        private BoundingSphere m_sphereBounds;

        public LevelObject ObjectInstance
        {
            get { return m_lo; }
        }

        public DrawableLevelObjectInstance(LevelObject lo, LevelObjectData data, ServiceManager services)
        {
            m_lo = lo;
            m_services = services;
            Init(data);
        }

        private Matrix CalculateWorldXfm()
        {
            Matrix lotWorld = Matrix.CreateScale(m_lot.Scaling) * m_lot.rotation.GetRotationMatrix() * Matrix.CreateTranslation(m_lot.Translation);
            Matrix loWorld = Matrix.CreateScale(m_lo.Scale) * m_lo.rotation.GetRotationMatrix() * Matrix.CreateTranslation(m_lo.Position);

            return lotWorld * loWorld;
        }

        private void Init(LevelObjectData data)
        {
            /* Create the link between the instance and it's type */
            m_lot = data.GetObjectTypeByGuid(m_lo.TypeId.ToString());

            if (m_lot == null)
            {
                /* ERROR: Could not find the type in the data definition */
                System.Diagnostics.Debug.Assert(false);
                return;
            }

            if (m_lot.ModelFileName.Length > 0)
            {
                Model model = null;
                data.FetchModel(m_lot.ModelFileName, m_services, out model);

                System.Diagnostics.Debug.Assert(model != null);
                m_model = model;

                List<Effect> list = new List<Effect>();
                foreach (ModelMesh mesh in m_model.Meshes)
                {
                    m_sphereBounds.Center += mesh.BoundingSphere.Center;
                    m_sphereBounds.Radius = Math.Max(m_sphereBounds.Radius, mesh.BoundingSphere.Radius);

                    foreach (Effect effect in mesh.Effects)
                    {                        
                        list.Add(effect);
                    }
                }

                m_sphereBounds.Center /= m_model.Meshes.Count;

                m_effects = new ReadOnlyCollection<Effect>(list);
            }

        }

        #region ISceneObject Members

        public void Draw()
        {
            if (m_model != null)
            {
                foreach (ModelMesh mesh in m_model.Meshes)
                {
                    mesh.Draw();
                }
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Xna.Framework.Graphics.Effect> Effects
        {
            get { return m_effects; }
        }

        public Microsoft.Xna.Framework.Matrix WorldXfm
        {
            get { return CalculateWorldXfm(); }
        }

        public string GetID
        {
            get { return m_lo.Name; }
        }

        public BoundingSphere BoundingSphere
        {
            get 
            {
                BoundingSphere sphere = m_sphereBounds;
                
                float instanceScale = Math.Max(Math.Max(m_lo.Scale.X, m_lo.Scale.Y), m_lo.Scale.Z);
                float typeScale = Math.Max(Math.Max(m_lot.Scaling.X, m_lot.Scaling.Y), m_lot.Scaling.Z);

                sphere.Radius *= instanceScale * typeScale;
                sphere.Center *= instanceScale * typeScale;
                sphere.Center += m_lo.Position;

                return sphere; 
            }
        }

        #endregion

        #region ILookAtTarget Members

        public Vector3 GetPosition3D()
        {
            return m_lo.Position;
        }

        #endregion

    }
}
