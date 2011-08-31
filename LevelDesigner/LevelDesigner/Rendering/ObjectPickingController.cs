using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDesigner.Rendering
{
    class ObjectPickingController : IInputListener
    {
        private Dictionary<string, ISceneObject> m_sceneObjects = null;
        private GraphicsDeviceService m_device = null;
        private Camera m_camera = null;

        #region IInputListener Members

        public void OnKeyDown(object ob, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public void OnKeyUp(object ob, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public void OnMouseMoved(object ob, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public void OnMouseDown(object ob, System.Windows.Forms.MouseEventArgs e)
        {
            if (m_sceneObjects != null)
            {
                ISceneObject pickedObject = null;

                /* Find the mouse position in screen space */
                Vector2 screenSpacePosition = new Vector2(e.X, e.Y);
                
                /* Find the picking ray for intersection tests */
                Ray pickingRay = CastPickingRay(screenSpacePosition);

                /* Find the closest sceneobject to the camera with a bound sphere that intersects the picking ray */
                float distanceFromCamera = 99999.0f; // hack, this should be m_camera.FarPlaneDistance
                foreach (KeyValuePair<String, ISceneObject> kv in m_sceneObjects)
                {
                    ISceneObject so = kv.Value;
                    BoundingSphere boundSphere = so.BoundingSphere;
                    float? intersect = pickingRay.Intersects(boundSphere);
                    if (intersect != null && intersect < distanceFromCamera)
                    {
                        pickedObject = so;
                    }
                }

                /* Notify listeners that the sceneObject was picked */
                if (pickedObject != null)
                {
                    if (SceneObjectSelected != null)
                    {
                        PickingEventArgs ea = new PickingEventArgs();
                        ea.SceneObject = pickedObject;
                        SceneObjectSelected(this, ea);
                    }


                    // Fire_ObjectPicked(pickedObject);
                }
            }
        }

        public void OnMouseUp(object ob, System.Windows.Forms.MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        public void Init(Dictionary<string, ISceneObject> scene, GraphicsDeviceService device, Camera camera)
        {
            m_camera = camera;
            m_device = device;
            m_sceneObjects = scene;
        }

        private Ray CastPickingRay(Vector2 ssPos)
        {
            Vector3 rayPos = m_device.Device.Viewport.Unproject(
                new Vector3(ssPos, 0),
                m_camera.GetProjectionMatrix(),
                m_camera.GetViewMatrix(),
                Matrix.CreateTranslation(m_camera.CameraPosition));

            Vector3 far = m_device.Device.Viewport.Unproject(
                new Vector3(ssPos, 1),
                m_camera.GetProjectionMatrix(),
                m_camera.GetViewMatrix(),
                Matrix.CreateTranslation(m_camera.CameraPosition));

            Ray ray = new Ray();
            ray.Position = rayPos;
            ray.Direction = Vector3.Normalize(far - rayPos);

            return ray;

        }

        #region PickingEvents

        public class PickingEventArgs : System.EventArgs
        {
            private ISceneObject so = null;

            public ISceneObject SceneObject
            {
                get { return so; }
                set { so = value; }
            }
        }

        public delegate void SceneObjectSelectedEventHandler(object sender, PickingEventArgs e);
        public event SceneObjectSelectedEventHandler SceneObjectSelected;
        #endregion
    }
}
