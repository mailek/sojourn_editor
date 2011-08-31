using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelDesigner.Data
{
    public class LevelEventArgs : System.EventArgs
    {
        public LevelObject lo = null;
        public LevelObjectType lot = null;
        public LevelTerrain terr = null;

        LevelObject LevelObject
        {
            get { return lo; }
            set { lo = value; }
        }

        LevelObjectType LevelObjectType
        {
            get { return lot; }
            set { lot = value; }
        }

        LevelTerrain LevelTerrain
        {
            get { return terr; }
            set { terr = value; }
        }
    }

    public delegate void ObjectInstanceAddedEventHandler(object sender, LevelEventArgs e);
    public delegate void ObjectInstanceRemovedEventHandler(object sender, LevelEventArgs e);
    public delegate void ObjectTypeAddedEventHandler(object sender, LevelEventArgs e);
    public delegate void ObjectTypeRemovedEventHandler(object sender, LevelEventArgs e);
    public delegate void ObjectTerrainAddedEventHandler(object sender, LevelEventArgs e);
    public delegate void ObjectTerrainRemovedEventHandler(object sender, LevelEventArgs e);


    interface ILevelEvents
    {
        // Summary:
        //     The event that occurs when an object instance is added to the level definition.
        event ObjectInstanceAddedEventHandler ObjectInstanceAdded;

        // Summary:
        //     The event that occurs when an object instance is removed from the level definition.
        event ObjectInstanceRemovedEventHandler ObjectInstanceRemoved;

        // Summary:
        //     The event that occurs when an object type is added to the level's object palette.
        event ObjectTypeAddedEventHandler ObjectTypeAdded;

        // Summary:
        //     The event that occurs when an object type is removed from the level's object palette.
        event ObjectTypeRemovedEventHandler ObjectTypeRemoved;

        // Summary:
        //     The event that occurs when an terrain chunk is added to the level terrain definition.
        event ObjectTerrainAddedEventHandler ObjectTerrainAdded;

        // Summary:
        //     The event that occurs when an terrain chunk is removed from the level terrain definition.
        event ObjectTerrainRemovedEventHandler ObjectTerrainRemoved;
    }
}
