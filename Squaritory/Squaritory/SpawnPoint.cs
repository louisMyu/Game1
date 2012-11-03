using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squaritory.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory
{
    class SpawnPoint
    {
        public Vector3 Location
        {
            get;
            set;
        }

        private GraphicsDevice m_GraphicsDevice;

        public SpawnPoint()
            : this(Vector3.Zero, null)
        {

        }

        public SpawnPoint(Vector3 location, GraphicsDevice gd)
        {
            Location = location;
            m_GraphicsDevice = gd;
        }

        public Unit SpawnWarrior()
        {
            return new Unit_Warrior(Location, m_GraphicsDevice);
        }

        public Unit SpawnArcher()
        {
            return new Unit_Archer(Location, m_GraphicsDevice);
        }

        public Unit SpawnMage()
        {
            return new Unit_Mage(Location, m_GraphicsDevice);
        }
    }
}
