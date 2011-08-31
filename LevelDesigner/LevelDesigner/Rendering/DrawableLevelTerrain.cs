using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;

namespace LevelDesigner.Rendering
{
    class DrawableLevelTerrain : ISceneObject
    {
        VertexPositionTexture[] m_vertices = null;
        int[] m_indices = null;
        ReadOnlyCollection<Effect> m_effects = null;
        Texture2D m_texture = null;
        GraphicsDevice m_device = null;
        VertexDeclaration m_vertexDecl = null;

        LevelTerrain m_terrain;

        public DrawableLevelTerrain(LevelTerrain terrain)
        {
            m_terrain = terrain;
        }

        public void Init(IServiceProvider deviceService)
        {
            m_terrain.Position = new Vector3(0.0f, 0.0f, 0.0f);
            IGraphicsDeviceService service = deviceService.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)) as IGraphicsDeviceService;
            m_device = service.GraphicsDevice;
            m_vertexDecl = new VertexDeclaration(m_device, VertexPositionTexture.VertexElements);

            BasicEffect effect = new BasicEffect(m_device, null);
            effect.EnableDefaultLighting();
            /* create texture & material */
            effect = new BasicEffect(m_device, null);
            List<Effect> list = new List<Effect>(1);
            list.Add(effect);
            m_effects = new ReadOnlyCollection<Effect>(list);

            ContentLoader loader = ContentLoader.AddRef();
            m_texture = loader.LoadTexture(deviceService, ContentLoader.m_contentPath + "//groundTexture.dds", "ground");
            loader.Release(true);

            effect.Texture = m_texture;
            effect.TextureEnabled = true;

            CreateTerrainMeshFromMap();
        }

        public void CreateTerrainMeshFromMap()
        {
            // create the vertices
            m_vertices = new VertexPositionTexture[m_terrain.HeightMap.Length];
            m_indices = new int[(m_terrain.HeightMapDimensions.Y - 1) * (m_terrain.HeightMapDimensions.X - 1) * 6];

            for (int row = 0; row < m_terrain.HeightMapDimensions.Y; row++)
            {
                for (int col = 0; col < m_terrain.HeightMapDimensions.X; col++)
                {
                    int index = row * m_terrain.HeightMapDimensions.X + col;

                    // rescale the int height to a value between 0.0-1.0
                    float yValue = (float)m_terrain.GetHeightMapEntry(col, row);
                    Vector3 pos = new Vector3((float)col / (float)(m_terrain.HeightMapDimensions.X-1), yValue, (float)row / (float)(m_terrain.HeightMapDimensions.Y-1));

                    // center the mesh in the x and z axes
                    pos += new Vector3(-0.5f, 0.0f, -0.5f);
                    pos.Z = -pos.Z;
                    m_vertices[index].Position = pos;

                    m_vertices[index].TextureCoordinate = 20 * new Vector2((float)col / (float)(m_terrain.HeightMapDimensions.X - 1), 
                        (float)row / (float)(m_terrain.HeightMapDimensions.Y - 1));

                }

            }

            // create indices
            for (int i = 0; i < m_terrain.HeightMapDimensions.X - 1; i++)
            {
                for (int j = 0; j < m_terrain.HeightMapDimensions.Y - 1; j++)
                {
                    int index = 6 * (i * (m_terrain.HeightMapDimensions.Y - 1) + j);

                    // top triangle
                    m_indices[index + 0] = j + m_terrain.HeightMapDimensions.X * (i + 1);
                    m_indices[index + 1] = j + m_terrain.HeightMapDimensions.X * i;
                    m_indices[index + 2] = j + 1 + m_terrain.HeightMapDimensions.X * i;

                    // bottom triangle
                    m_indices[index + 3] = j + m_terrain.HeightMapDimensions.X * (i + 1);
                    m_indices[index + 4] = j + 1 + m_terrain.HeightMapDimensions.X * i;
                    m_indices[index + 5] = j + 1 + m_terrain.HeightMapDimensions.X * (i + 1);
                }
            }
        }

        #region ISceneObject Members

        public void Draw()
        {
            m_device.VertexDeclaration = this.VertexDecl;

            foreach (BasicEffect effect in m_effects)
            {
                effect.Begin();
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    m_device.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, m_vertices, 0, m_vertices.Length, m_indices, 0, m_indices.Length / 3);

                    pass.End();
                }

                effect.End();
            }
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
            get { return Matrix.CreateScale(m_terrain.Scaling.Scales) * Matrix.CreateTranslation(m_terrain.Position); }
        }

        public string GetID
        {
            get { return m_terrain.Name; }
        }

        public BoundingSphere BoundingSphere
        {
            get { return new BoundingSphere();/*throw new NotImplementedException();*/ }
        }

        #endregion

        #region ILookAtTarget Members

        public Vector3 GetPosition3D()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
