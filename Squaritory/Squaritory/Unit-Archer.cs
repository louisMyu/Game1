using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squaritory.Shapes;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory
{
    class Unit_Archer : Unit
    {
        public Unit_Archer()
            : this(Vector3.Zero, null)
        {

        }

        public Unit_Archer(Vector3 location, GraphicsDevice gd)
            : base(location, gd)
        {
        }

        public override void DrawUnit(GraphicsDevice graphicsDevice, Matrix view, Matrix projection)
        {
            Model.Draw(graphicsDevice, view, projection, Color.DarkGreen);
        }
    }
}
