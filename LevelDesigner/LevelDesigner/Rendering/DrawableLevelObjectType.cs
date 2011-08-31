using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace LevelDesigner.Rendering
{
    class DrawableLevelObjectType : ISceneObject
    {
        Model m_model = null;
        LevelObjectType m_lot = null;
        private ReadOnlyCollection<Effect> m_effects = null;
        private ServiceManager m_services = null;

        public DrawableLevelObjectType(LevelObjectType type, LevelObjectData data, ServiceManager services)
        {
            m_lot = type;
            m_services = services;
            Init(data);
        }

        private void Init(LevelObjectData data)
        {
            /* Create the link between the instance and it's type */
            

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
                    foreach (Effect effect in mesh.Effects)
                    {
                        list.Add(effect);
                    }
                }

                m_effects = new ReadOnlyCollection<Effect>(list);
            }

        }

        public Matrix CalculateWorldXfm()
        {
            Matrix translation = Matrix.CreateTranslation(m_lot.Translation);
            Matrix rotation = m_lot.rotation.GetRotationMatrix();
            Matrix scaling = Matrix.CreateScale(m_lot.Scaling);

            return scaling * rotation * translation;
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
            get { return m_lot.Name; }
        }

        public BoundingSphere BoundingSphere
        {
            get { return new BoundingSphere();/*throw new NotImplementedException();*/ }
        }

        #endregion

        #region ILookAtTarget Members

        public Vector3 GetPosition3D()
        {
            return m_lot.Translation;
        }

        #endregion

    }
}
