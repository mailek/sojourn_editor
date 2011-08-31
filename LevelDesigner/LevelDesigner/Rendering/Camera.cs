using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    class Camera
    {
        public enum ECameraMode
        {
            eInvalid,
            //eLookAtLeft,
            //eLookAtUp,
            //eLookAtFront,
            eLookAtFree,
            eFreeRoam
        };

        private Matrix viewMatrix;
        private Matrix projMatrix;
        private Vector3 m_cameraPosition;
        //private Vector3 vecForward;
        //private Vector3 vecUp;
        private ECameraMode m_cameraMode = ECameraMode.eInvalid;

        /* look at camera */
        private ILookAtTarget m_objLookAtTarget = null;
        private float m_lookAtDistance = 5.0f;
        private RotationEuler m_lookAtRotation = new RotationEuler();

        private GraphicsDeviceService m_device = null;

        private const float MIN_LOOKATDISTANCE = 0.00001f;
        private const float MAX_LOOKATDISTANCE = 1000000f;

        public Vector3 LookAtTargetRotation
        {
            get { return m_lookAtRotation.Euler_Degrees; }
        }

        public void SetLookAtTargetRotation(Vector3 rotation)
        {
            m_lookAtRotation.Euler_Degrees = rotation;
        }

        public ECameraMode CameraMode 
        {
            get { return m_cameraMode; }
        }

        public void ChangeCameraMode(ECameraMode mode)
        {
            m_lookAtRotation.Euler_Degrees = new Vector3(0.0f, 0.0f, 0.0f);

            if (m_cameraMode != mode)
            {
                //if (mode == ECameraMode.eLookAtFree)
                //{
        
                //}

                m_cameraMode = mode;
                RecalculateXfms();
            }
        }

        public ILookAtTarget LookAtTarget
        {
            get { return m_objLookAtTarget; }
        }

        public void SetLookAtTarget(ILookAtTarget target)
        {
            System.Diagnostics.Debug.Assert(m_cameraMode == ECameraMode.eLookAtFree);

            m_objLookAtTarget = target;
        }

        public Vector3 CameraPosition
        {
            get { return m_cameraPosition; }
        }

        public void SetCameraPosition(Vector3 pos)
        {
            System.Diagnostics.Debug.Assert(m_cameraMode == ECameraMode.eFreeRoam);

            if (m_cameraMode == ECameraMode.eFreeRoam)
            {
                m_cameraPosition = pos;
                RecalculateXfms();
            }
        }

        public float DistanceToTarget
        {
            get { return m_lookAtDistance; }
        }

        public void SetDistanceToTarget(float distance)
        {
            System.Diagnostics.Debug.Assert(m_cameraMode == ECameraMode.eLookAtFree);

            if (m_cameraMode == ECameraMode.eLookAtFree)
            {
                distance = Math.Max(distance, MIN_LOOKATDISTANCE);
                distance = Math.Min(distance, MAX_LOOKATDISTANCE);
                m_lookAtDistance = distance;
                RecalculateXfms();
            }
        }

        public Camera(GraphicsDeviceService device)
        {
            m_device = device;
            m_lookAtRotation.Order = RotationEuler.EOrder.eOrder_XZY;
            RecalculateXfms();
        }

        public void RecalculateXfms()
        {
            float aspectRatio = m_device.Device.Viewport.AspectRatio;

            // Look At Camera
            if (m_cameraMode == ECameraMode.eLookAtFree)
            {
                if (m_objLookAtTarget == null)
                    return;

                // calculate the camera's position in target's local space

                // start directly behind target, at target distance
                Vector3 cameraLocal = new Vector3(0.0f, 0.0f, -m_lookAtDistance);

                // apply rotation to position
                cameraLocal = Vector3.Transform(cameraLocal, m_lookAtRotation.GetRotationMatrix());

                if (m_objLookAtTarget != null)
                {
                    m_cameraPosition = m_objLookAtTarget.GetPosition3D() + cameraLocal;

                    viewMatrix = Matrix.CreateLookAt(m_cameraPosition, m_objLookAtTarget.GetPosition3D(), Vector3.Up);

                }
            }
            else if (m_cameraMode == ECameraMode.eFreeRoam)
            {
            }
            
            
            projMatrix = Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, aspectRatio,
                                                                1.0f, 1000.0f);
        }

        public Matrix GetViewMatrix()
        {
            return viewMatrix;
        }

        public Matrix GetProjectionMatrix()
        {
            return projMatrix;
        }
    }
}
