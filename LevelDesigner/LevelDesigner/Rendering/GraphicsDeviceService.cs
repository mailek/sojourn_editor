using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LevelDesigner
{
    class GraphicsDeviceService : IGraphicsDeviceService
    {
        static GraphicsDeviceService singleton = null;
        static int refCnt = 0; 
        
        GraphicsDevice device = null;
        PresentationParameters prms = null;

        public GraphicsDevice Device
        {
            get { return device; }
        }

        GraphicsDeviceService(IntPtr wndHandle, int width, int height)
        {
            prms = new PresentationParameters();

            prms.BackBufferWidth = Math.Max(width, 1);
            prms.BackBufferHeight = Math.Max(height, 1);
            prms.BackBufferFormat = SurfaceFormat.Color;
            prms.AutoDepthStencilFormat = DepthFormat.Depth24;
            prms.EnableAutoDepthStencil = true;
            prms.DeviceWindowHandle = wndHandle;
            prms.PresentationInterval = PresentInterval.Immediate;
            prms.IsFullScreen = false;

            device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                                                DeviceType.Hardware,
                                                wndHandle,
                                                prms);

            if (DeviceCreated != null)
            {
                DeviceCreated(this, EventArgs.Empty);
            }
        }

        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
            {
                DeviceResetting(this, EventArgs.Empty);
            }

            prms.BackBufferWidth = width;
            prms.BackBufferHeight = height;

            device.Reset(prms);

            if (DeviceReset != null)
            {
                DeviceReset(this, EventArgs.Empty);
            }
        }

        public static GraphicsDeviceService AddRef(IntPtr wndHandle, int width, int height)
        {
            if(refCnt == 0)
            {
                singleton = new GraphicsDeviceService(wndHandle, width, height );
            }

            refCnt++;

            return singleton;
        }

        public void Release(bool disposing)
        {
            if (refCnt == 1)
            {
                if (DeviceDisposing != null)
                {
                    DeviceDisposing(this, EventArgs.Empty);
                }

                device.Dispose();
                device = null;
            }

            refCnt--;
        }

        /* IGraphicsDeviceService */
        public event EventHandler DeviceCreated;
        public event EventHandler DeviceDisposing;
        public event EventHandler DeviceReset;
        public event EventHandler DeviceResetting;

        public GraphicsDevice GraphicsDevice
        {
            get { return device; }
        }

    }
}
