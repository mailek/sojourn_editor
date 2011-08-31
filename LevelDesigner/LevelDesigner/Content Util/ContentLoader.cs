using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using WinFormsContentLoading;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace LevelDesigner
{
    class ContentLoader
    {
        static ContentLoader m_singletonInstance = null;
        static int m_refCnt = 0;
        public static String m_contentPath ="";

        ContentBuilder builder = new ContentBuilder();

        public ContentLoader()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../Content");
            m_contentPath = Path.GetFullPath(relativePath);
        }

        public Model LoadModel(IServiceProvider deviceService, String modelFileName, String modelName)
        {
            Model model = null;

//            ContentBuilder builder = new ContentBuilder();
            builder.Clear();

            builder.Add(modelFileName, modelName, null, "ModelProcessor");
            String errors = builder.Build();

            if (!String.IsNullOrEmpty(errors))
            {
                MessageBox.Show(errors);
                return model;
            }

            /* Load the model from the content folder */
            ContentManager cm = new ContentManager(deviceService, builder.OutputDirectory);
            model = cm.Load<Model>(modelName);

            if (model == null)
            {
                throw new System.Exception();
            }

            return model;
        }

        public Texture2D LoadTexture(IServiceProvider deviceService, String textureFileName, String textureName)
        {
            Texture2D texture = null;

            builder.Clear();
            builder.Add(textureFileName, textureName, null, "TextureProcessor");

            String errors = builder.Build();

            if (!String.IsNullOrEmpty(errors))
            {
                MessageBox.Show(errors);
                return texture;
            }

            /* Load the model from the content folder */
            ContentManager cm = new ContentManager(deviceService, builder.OutputDirectory);
            texture = cm.Load<Texture2D>(textureName);

            if (texture == null)
            {
                throw new System.Exception();
            }

            return texture;
        }

        public void Release(bool disposing)
        {
            if (m_refCnt == 1)
            {
                if (disposing)
                {
                    m_singletonInstance = null;
                }
            }

            m_refCnt--;
        }

        public static ContentLoader AddRef()
        {
            if (m_singletonInstance == null)
            {
                m_singletonInstance = new ContentLoader();
            }

            m_refCnt++;

            return m_singletonInstance;
        }
    }
}
