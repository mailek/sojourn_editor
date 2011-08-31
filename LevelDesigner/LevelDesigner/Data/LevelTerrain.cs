using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LevelDesigner
{
    public class LevelTerrain
    {
        Scale m_terrainScale;
        Translation m_translation;
        String m_name;
        Point m_heightMapDimensions;
        byte[] m_heightMap = null;

        const int heightValueSize = 256; /* currently supporting byte values only */

        [DescriptionAttribute("The world position of this terrain chunk.")]
        public Vector3 Position
        {
            get { return m_translation.Position; }
            set { m_translation.Position = value; }
        }

        [DescriptionAttribute("The raw height map array."),
        ReadOnlyAttribute(true)]
        public byte[] HeightMap
        {
            get { return m_heightMap; }
            set { m_heightMap = value; }
        }

        [DescriptionAttribute("The width/height dimensions of the height map."),
        ReadOnlyAttribute(true)]
        public Point HeightMapDimensions
        {
            get { return m_heightMapDimensions; }
            set { m_heightMapDimensions = value; }
        }

        [DescriptionAttribute("The terrain block name."),
        ReadOnlyAttribute(true)]
        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public Scale Scaling
        {
            get { return m_terrainScale; }
            set { m_terrainScale = value; }
        }

        public float XScale
        {
            get { return m_terrainScale.Scales.X; }
            set { m_terrainScale.scales.X = value;}
        }

        public float YScale
        {
            get { return m_terrainScale.Scales.Y; }
            set { m_terrainScale.scales.Y = value; }
        }

        public float ZScale
        {
            get { return m_terrainScale.Scales.Z; }
            set { m_terrainScale.scales.Z = value; }
        }

        public float GetHeightMapEntry(int col, int row)
        {
            int index = row * m_heightMapDimensions.X + col;
            return (float)m_heightMap[index] / heightValueSize;
        }

    }
}
