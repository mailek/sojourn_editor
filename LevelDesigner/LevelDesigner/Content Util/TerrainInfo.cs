using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDesigner.Content_Util
{
    class TerrainInfo
    {
        private LevelTerrain m_terrain = null;

        public TerrainInfo(LevelTerrain lt)
        {
            m_terrain = lt;
        }

        public bool ContainsPointXZ(Vector3 pt)
        {
            float rectTop = m_terrain.ZScale / 2.0f;
            float rectBottom = -m_terrain.ZScale / 2.0f;
            float rectLeft = -m_terrain.XScale / 2.0f;
            float rectRight = m_terrain.ZScale / 2.0f;

            Vector3 localPoint = pt - m_terrain.Position;
            if (localPoint.X <= rectRight && localPoint.X >= rectLeft)
            {
                if (localPoint.Z <= rectTop && localPoint.Z >= rectBottom)
                {
                    return true;
                }
            }

            return false;
        }

        public float? GetTerrainHeightAtXZ(float x, float z)
        {
            float? ret = null;

            if (!ContainsPointXZ(new Vector3(x, 0.0f, z)))
            {
                return ret;
            }

            Vector3 clampPointWorking = new Vector3();
            clampPointWorking.X = x;
            clampPointWorking.Z = z;

            clampPointWorking -= m_terrain.Position;
            clampPointWorking.Z = -clampPointWorking.Z;
            clampPointWorking += m_terrain.Scaling.Scales / 2.0f;

            clampPointWorking /= m_terrain.Scaling.Scales;
            clampPointWorking *= new Vector3(m_terrain.HeightMapDimensions.X, 1.0f, m_terrain.HeightMapDimensions.Y);

            int cellColumn = (int)Math.Floor(clampPointWorking.X);
            int cellRow = (int)Math.Floor(clampPointWorking.Z);

            float xDist = clampPointWorking.X - (float)cellColumn;
            float zDist = clampPointWorking.Z - (float)cellRow;

            if (xDist + zDist < 1.0f)
            {
                // top left triangle
                float y = (MathHelper.Lerp(m_terrain.GetHeightMapEntry(cellColumn, cellRow), m_terrain.GetHeightMapEntry(cellColumn + 1, cellRow), xDist)
                    + MathHelper.Lerp(m_terrain.GetHeightMapEntry(cellColumn, cellRow), m_terrain.GetHeightMapEntry(cellColumn, cellRow + 1), zDist))
                    / 2.0f;

                ret = (y * m_terrain.YScale) + m_terrain.Position.Y;
            }
            else
            {
                // bottom right triangle
                float y = (MathHelper.Lerp(m_terrain.GetHeightMapEntry(cellColumn, cellRow+1), m_terrain.GetHeightMapEntry(cellColumn+1, cellRow+1), xDist)
                    + MathHelper.Lerp(m_terrain.GetHeightMapEntry(cellColumn+1, cellRow), m_terrain.GetHeightMapEntry(cellColumn+1, cellRow+1), zDist))
                    / 2.0f;

                ret = (y * m_terrain.YScale) + m_terrain.Position.Y;
            }

            return ret;
        }
    }
}
