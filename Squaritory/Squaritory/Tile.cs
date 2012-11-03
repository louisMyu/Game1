using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squaritory.Shapes;

namespace Squaritory
{
    class Tile
    {
        public enum TileType
        {
            Warrior,
            Archer,
            Mage,
            Empty
        }

        public TileType UnitType
        {
            get;
            set;
        }

        public Color TileColor
        {
            get;
            set;
        }

        public Shape TileShape
        {
            get;
            set;
        }

        public SpawnPoint UnitSpawnPoint
        {
            get;
            set;
        }

        public float XCoord
        {
            get;
            set;
        }

        public float YCoord
        {
            get;
            set;
        }

        public Tile()
        {

        }

        public Tile(Color color)
        {
            TileColor = color;
        }
    }
}
