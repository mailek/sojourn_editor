using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LevelDesigner
{
    class CameraController : IInputListener
    {
        
        private bool m_bMouseLeftButtonDown = false;
        private bool m_bMouseRightButtonDown = false;
        private Point m_ptLastMousePos;
        private Microsoft.Xna.Framework.Vector2 m_vecMouseSpeed = new Microsoft.Xna.Framework.Vector2(1.0f, 0.5f);
        private float m_fMouseDollySpeed = 0.02f;
        private Camera m_camera = null;

        public void Init(Camera camera)
        {
            m_camera = camera;
        }

        private void UpdateCamera(Point posDelta)
        {
            float x = (float)-posDelta.X * m_vecMouseSpeed.X;
            float y = (float)posDelta.Y * m_vecMouseSpeed.Y;

            Microsoft.Xna.Framework.Vector3 rotation = m_camera.LookAtTargetRotation;
            rotation += new Microsoft.Xna.Framework.Vector3(y, x, 0.0f);
            m_camera.SetLookAtTargetRotation(rotation);

        }

        private void UpdateCameraDistance(Point posDelta)
        {
            float y = (float)-posDelta.Y * m_fMouseDollySpeed;
            float cameraDistance = m_camera.DistanceToTarget * y + m_camera.DistanceToTarget;

            m_camera.SetDistanceToTarget(cameraDistance);
        }

        #region IInputListener Members

        public void OnKeyDown(object ob, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public void OnKeyUp(object ob, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public void OnMouseMoved(object ob, System.Windows.Forms.MouseEventArgs e)
        {
            if (m_bMouseLeftButtonDown == true)
            {
                Point posDelta = new Point();
                posDelta.X = e.Location.X - m_ptLastMousePos.X;
                posDelta.Y = e.Location.Y - m_ptLastMousePos.Y;

                UpdateCamera(posDelta);

                m_ptLastMousePos.X += posDelta.X;
                m_ptLastMousePos.Y += posDelta.Y;

            }
            else if (m_bMouseRightButtonDown == true)
            {
                Point posDelta = new Point();
                posDelta.X = e.Location.X - m_ptLastMousePos.X;
                posDelta.Y = e.Location.Y - m_ptLastMousePos.Y;

                UpdateCameraDistance(posDelta);

                m_ptLastMousePos.X += posDelta.X;
                m_ptLastMousePos.Y += posDelta.Y;

            }
        }

        public void OnMouseDown(object ob, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Right:
                    m_ptLastMousePos = e.Location;
                    m_bMouseRightButtonDown = true;
                    break;
                case System.Windows.Forms.MouseButtons.Left:
                    m_ptLastMousePos = e.Location;
                    m_bMouseLeftButtonDown = true;
                    break;
                default:
                    break;

            }
        }

        public void OnMouseUp(object ob, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Right:
                    m_bMouseRightButtonDown = false;
                    break;
                case System.Windows.Forms.MouseButtons.Left:
                    m_bMouseLeftButtonDown = false;
                    break;
                default:
                    break;

            }
        }

        #endregion

    }
}
