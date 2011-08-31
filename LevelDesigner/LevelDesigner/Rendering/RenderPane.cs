using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LevelDesigner.Rendering;

namespace LevelDesigner
{
    class RenderPane : Control, ILookAtTarget
    {
        public enum ViewMode
        {
            eDisabled,
            eLevelViewer,
            eTypeViewer,
            eTerrainViewer
        };

        private ViewMode m_mode = ViewMode.eDisabled;
        private GraphicsDeviceService device = null;
        private Camera m_camera = null;
        private RenderQuad m_gridFloor = null;
        private Dictionary<String, ISceneObject> m_scene = new Dictionary<String, ISceneObject>();
        private Dictionary<String, IInputListener> m_inputControllers = new Dictionary<string,IInputListener>();

        ServiceManager services = new ServiceManager();
        public ServiceManager Services
        {
            get { return services; }
            set { services = value; }
        }

        public void AddObjectInstanceToScene(LevelObject lo, LevelObjectData data)
        {
            DrawableLevelObjectInstance drawLo = new DrawableLevelObjectInstance(lo, data, Services);
            m_scene.Add(lo.Name, drawLo);

        }

        public void EnableObjectPicking(bool enable)
        {
            // check if picking is enabled already
            IInputListener controller = null;
            m_inputControllers.TryGetValue("object_picker", out controller);

            if (enable)
            {
                if (controller == null)
                {
                    ObjectPickingController picker = new ObjectPickingController();
                    picker.Init(m_scene, device, m_camera);
                    picker.SceneObjectSelected += new ObjectPickingController.SceneObjectSelectedEventHandler(this.OnSceneObjectPicked);

                    AddInputListener(picker, false);
                    m_inputControllers.Add("object_picker", picker);
                }
            }
            else if (controller != null)
            {
                ObjectPickingController picker = (ObjectPickingController)controller;
                picker.SceneObjectSelected -= new ObjectPickingController.SceneObjectSelectedEventHandler(this.OnSceneObjectPicked);

                AddInputListener(controller, true);
                m_inputControllers.Remove("object_picker");
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            Application.Idle += delegate { Invalidate(); };

            if (!DesignMode)
            {
                device = GraphicsDeviceService.AddRef(Handle, ClientSize.Width, ClientSize.Height);
                services.AddService<IGraphicsDeviceService>(device);
                m_camera = new Camera(device);
                m_gridFloor = new RenderQuad(
                    services, 
                    new Vector3(0.0f, 0.0f, 0.0f), 
                    new Vector3(0.0f, 1.0f, 0.0f), 
                    new Vector3(0.0f, 1.0f, 0.0f), 
                    //10, 10, 
                    100,100,
                    RenderQuad.ETextureMapMode.eWorldScale,
                    "grid_floor");
            }

            CameraController controller = new CameraController();
            controller.Init(m_camera);
            m_inputControllers.Add("camera_controller", controller);
            
            AddInputListener(controller, false);
        }

        protected override void Dispose(bool disposing)
        {
            if (device != null)
            {
                device.Release(disposing);
                device = null;
            }

            base.Dispose(disposing);
        }

        public void LookAtObjectById(string id)
        {
            ISceneObject found_so = null;
            foreach (KeyValuePair<String, ISceneObject> kv in m_scene)
            {
                ISceneObject so = kv.Value;
                if (so.GetID == id)
                {
                    found_so = so;
                    break;
                }
            }

            if (found_so != null)
            {
                m_camera.SetLookAtTarget(found_so);
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (device != null && Enabled)
            {
                RenderScene();
            }
            else
            {
                e.Graphics.Clear(System.Drawing.Color.LightGray);
            }
            
        }

        private void RenderScene()
        {
            device.Device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Microsoft.Xna.Framework.Graphics.Color.Black, 1.0f, 0);
            m_camera.RecalculateXfms();
            Matrix view = m_camera.GetViewMatrix();
            Matrix projection = m_camera.GetProjectionMatrix();

            if (m_mode == ViewMode.eTypeViewer)
            {
                foreach (KeyValuePair<String, ISceneObject> kv in m_scene)
                {
                    ISceneObject so = kv.Value;
                    /* draw the selected model in the viewer */

                        foreach (BasicEffect effect in so.Effects)
                        {
                            effect.World = so.WorldXfm;
                            effect.View = view;
                            effect.Projection = projection;

                            effect.EnableDefaultLighting();
                            effect.PreferPerPixelLighting = true;
                            effect.SpecularPower = 8;
                        }

                        so.Draw();
                }
                
            
                /* draw the floor */
                foreach (BasicEffect effect in m_gridFloor.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = m_gridFloor.WorldXfm;

                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }

                m_gridFloor.Draw();
                
            }
            else if (m_mode == ViewMode.eLevelViewer || m_mode == ViewMode.eTerrainViewer)
            {
                foreach ( KeyValuePair<String, ISceneObject> kv in m_scene)
                {
                    ISceneObject so = kv.Value;
                    foreach (BasicEffect effect in so.Effects)
                    {
                        effect.View = view;
                        effect.Projection = projection;
                        effect.World = so.WorldXfm;

                        effect.LightingEnabled = false;
                    }

                    so.Draw();
                }

                foreach (BasicEffect effect in m_gridFloor.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = m_gridFloor.WorldXfm;

                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }

                m_gridFloor.Draw();
            }

            Rectangle sourceRectangle = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            try
            {
                device.Device.Present(sourceRectangle, null, this.Handle);
            }
            catch
            {
                
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        public void ShowObjectType(LevelObjectType type, LevelObjectData data)
        {
            DrawableLevelObjectType drawLot = new DrawableLevelObjectType(type, data, Services);

            ChangeViewMode(ViewMode.eTypeViewer);
            ResetView();
            m_scene.Add(type.Name, drawLot);
        }

        public void ChangeViewMode(ViewMode mode)
        {
            if (mode != m_mode)
            {
                ResetView();
            }

            switch (mode)
            {
                case ViewMode.eTerrainViewer:
                case ViewMode.eTypeViewer:
                    m_mode = mode;
                    m_camera.ChangeCameraMode(Camera.ECameraMode.eLookAtFree);
                    m_camera.SetLookAtTarget(this);
                    break;
                case ViewMode.eLevelViewer:
                    m_mode = mode;
                    //m_camera.ChangeCameraMode(Camera.ECameraMode.eFreeRoam);
                    m_camera.ChangeCameraMode(Camera.ECameraMode.eLookAtFree);
                    m_camera.SetLookAtTarget(this);
                    EnableObjectPicking(true);
                    break;
                default:
                    m_mode = ViewMode.eDisabled;
                    break;
            }
        }

        private void ResetView()
        {
            m_scene.Clear();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
 	        base.OnEnabledChanged(e);

            ChangeViewMode(ViewMode.eDisabled);
        }

        public void AddTerrainToScene(LevelTerrain terrain)
        {
            System.Diagnostics.Debug.Assert(m_mode == ViewMode.eTerrainViewer || m_mode == ViewMode.eLevelViewer);

            DrawableLevelTerrain drawTerrain = new DrawableLevelTerrain(terrain);
            drawTerrain.Init(Services);
            m_scene.Add(terrain.Name, (ISceneObject)drawTerrain);
        }

        public void AddInputListener(IInputListener listener, bool remove)
        {
            if(remove)
            {
                this.MouseDown -= listener.OnMouseDown;
                this.MouseUp -= listener.OnMouseUp;
                this.MouseMove -= listener.OnMouseMoved;
                this.KeyDown -= listener.OnKeyDown;
                this.KeyUp -= listener.OnKeyUp;
            }
            else
            {
                this.MouseDown += listener.OnMouseDown;
                this.MouseUp += listener.OnMouseUp;
                this.MouseMove += listener.OnMouseMoved;
                this.KeyUp += listener.OnKeyUp;
                this.KeyDown += listener.OnKeyDown;
            }

        }

        public void OnSceneObjectPicked(object sender, ObjectPickingController.PickingEventArgs e)
        {
            ISceneObject selected = e.SceneObject;

            switch (this.m_mode)
            {
                case ViewMode.eLevelViewer:
                    if(LevelObjectSelected != null)
                    {
                        DrawableLevelObjectInstance dlo = (DrawableLevelObjectInstance)selected;
                        RenderPaneEventArgs args = new RenderPaneEventArgs();
                        args.LevelObjectInstance = dlo.ObjectInstance;
                        LevelObjectSelected(this, args);
                    }

                    break;
                default:
                    break;
            }


        }

        #region ILookAtTarget Members

        Vector3 ILookAtTarget.GetPosition3D()
        {
            // always look at the origin 
            return new Vector3(0.0f, 0.0f, 0.0f);
        }

        #endregion

        #region RenderPaneEvents

        public class RenderPaneEventArgs : System.EventArgs
        {
            private LevelObject lo = null;

            public LevelObject LevelObjectInstance
            {
                get { return lo; }
                set { lo = value; }
            }
        }

        public delegate void LevelObjectSelectedEventHandler(object sender, RenderPaneEventArgs e);
        public event LevelObjectSelectedEventHandler LevelObjectSelected;
        #endregion

    }
}
