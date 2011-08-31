using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.ObjectModel;
using WinFormsContentLoading;

namespace LevelDesigner
{
    class RenderQuad : ISceneObject
    {
        public enum ETextureMapMode
        {
            eWorldScale, /* repeats the texture for each 1.0f game unit */
            eStretchFull /* texture is stretched to fit whole quad */
        };

        public enum EQuadCorner : int
        {
            eUpperLeftCorner,
            eUpperRightCorner,
            eLowerRightCorner,
            eLowerLeftCorner
        };

        Vector3 m_up;
        Vector3 m_normal;
        Vector3 m_position;
        Vector3 m_left;
        float m_height;
        float m_width;
        Vector2 m_textureScale;
        String m_name;

        VertexPositionTexture[] m_vertices;
        VertexDeclaration m_vertexDecl;
        int[] m_indices;
        GraphicsDevice m_device = null;
        ReadOnlyCollection<Effect> m_effects;
        BasicEffect m_effect = null;
        Texture2D m_texture = null;

        public RenderQuad(IServiceProvider deviceService, 
            Vector3 pos, 
            Vector3 norm, 
            Vector3 worldUp, 
            float width, 
            float height, 
            ETextureMapMode textureMode,
            String name)
        {
            IGraphicsDeviceService service = deviceService.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)) as IGraphicsDeviceService;
            m_device = service.GraphicsDevice;
            m_name = name;
            m_position = pos;
            m_normal = Vector3.Normalize(norm);
            m_up = Vector3.Normalize(worldUp);
            m_left = Vector3.Cross(m_normal, m_up);
            if (m_left.LengthSquared() == 0.0f)
            {
                m_left = new Vector3(1.0f, 0.0f, 0.0f);
            }
            m_width = width;
            m_height = height;

            switch(textureMode)
            {
                case ETextureMapMode.eWorldScale:
                    {
                        m_textureScale = new Vector2(width, height); 
                        break;
                    }
                default:
                    {
                        m_textureScale = new Vector2(1.0f, 1.0f);
                        break;
                    }
            }

            /* texture coordinates */
            Vector2 textureUL = new Vector2(0.0f, 0.0f);
            Vector2 textureUR = new Vector2(1.0f, 0.0f) * m_textureScale;
            Vector2 textureLL = new Vector2(0.0f, 1.0f) * m_textureScale;
            Vector2 textureLR = new Vector2(1.0f, 1.0f) * m_textureScale;

            /* create texture & material */
            m_effect = new BasicEffect(m_device, null);
            List<Effect> list = new List<Effect>(1);
            list.Add(m_effect);
            m_effects = new ReadOnlyCollection<Effect>(list);

            ContentLoader loader = ContentLoader.AddRef();
            m_texture = loader.LoadTexture(deviceService, ContentLoader.m_contentPath+"//grid.dds", "grid");
            loader.Release(true);

            m_effect.Texture = m_texture;
            m_effect.TextureEnabled = true;


            /* A---------B
             * |        /|
             * |      /  |
             * |    /    |
             * |  /      |
             * |/        |
             * D---------C */

            /* vertices */
            Vector3[] corners;
            CalculateCorners(out corners);
            
            m_vertices = new VertexPositionTexture[4];
            m_vertices[(int)EQuadCorner.eUpperLeftCorner].Position = corners[(int)EQuadCorner.eUpperLeftCorner];      /* A */
            m_vertices[(int)EQuadCorner.eUpperLeftCorner].TextureCoordinate = textureUL;
            m_vertices[(int)EQuadCorner.eUpperRightCorner].Position = corners[(int)EQuadCorner.eUpperRightCorner];    /* B */
            m_vertices[(int)EQuadCorner.eUpperRightCorner].TextureCoordinate = textureUR;
            m_vertices[(int)EQuadCorner.eLowerRightCorner].Position = corners[(int)EQuadCorner.eLowerRightCorner];    /* C */
            m_vertices[(int)EQuadCorner.eLowerRightCorner].TextureCoordinate = textureLR;
            m_vertices[(int)EQuadCorner.eLowerLeftCorner].Position = corners[(int)EQuadCorner.eLowerLeftCorner];      /* D */
            m_vertices[(int)EQuadCorner.eLowerLeftCorner].TextureCoordinate = textureLL;

            /* m_indices */
            m_indices = new int[6];
            m_indices[0] = (int)EQuadCorner.eLowerLeftCorner; /* D */
            m_indices[1] = (int)EQuadCorner.eUpperRightCorner; /* B */
            m_indices[2] = (int)EQuadCorner.eUpperLeftCorner; /* A */

            m_indices[3] = (int)EQuadCorner.eLowerLeftCorner; /* D */
            m_indices[4] = (int)EQuadCorner.eLowerRightCorner; /* C */
            m_indices[5] = (int)EQuadCorner.eUpperRightCorner; /* B */

            /* vertex type def */
            m_vertexDecl = new VertexDeclaration(m_device, VertexPositionTexture.VertexElements);

        }

        public void CalculateCorners(out Vector3[] corners)
        {
            corners = new Vector3[4];
            //Vector3 uppercenter = (m_up * m_height / 2) + m_position;
            //corners[(int)EQuadCorner.eUpperLeftCorner] = uppercenter + (m_left * m_width / 2.0f);
            //corners[(int)EQuadCorner.eUpperRightCorner] = uppercenter - (m_left * m_width / 2);
            //corners[(int)EQuadCorner.eLowerRightCorner] = corners[(int)EQuadCorner.eUpperRightCorner] - (m_up * m_height);
            //corners[(int)EQuadCorner.eLowerLeftCorner] = corners[(int)EQuadCorner.eUpperLeftCorner] - (m_up * m_height);

            corners[(int)EQuadCorner.eUpperLeftCorner] = new Vector3(-m_width / 2.0f, 0.0f, m_height / 2.0f);
            corners[(int)EQuadCorner.eUpperRightCorner] = new Vector3(m_width / 2.0f, 0.0f, m_height / 2.0f);
            corners[(int)EQuadCorner.eLowerRightCorner] = new Vector3(m_width / 2.0f, 0.0f, -m_height / 2.0f);
            corners[(int)EQuadCorner.eLowerLeftCorner] = new Vector3(-m_width / 2.0f, 0.0f, -m_height / 2.0f);

        }

        #region ISceneObject Members

        public virtual void Draw()
        {
            m_device.VertexDeclaration = this.VertexDecl;

            m_effect.Begin();
            foreach (EffectPass pass in m_effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                m_device.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, m_vertices, 0, 4, m_indices, 0, 2);

                pass.End();
            }

            m_effect.End();
        }

        public ReadOnlyCollection<Effect> Effects
        {
            get { return m_effects; }
        }

        public VertexDeclaration VertexDecl
        {
            get { return m_vertexDecl; }
        }

        public Matrix WorldXfm
        {
            get { return Matrix.CreateTranslation(m_position); }
        }

        public string GetID
        {
            get { return m_name; }
        }

        public BoundingSphere BoundingSphere
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ILookAtTarget Members

        public Vector3 GetPosition3D()
        {
            return m_position;
        }

        #endregion

    }
}
