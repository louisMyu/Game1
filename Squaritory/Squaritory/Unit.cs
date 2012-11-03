using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squaritory.Shapes;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory
{
    public abstract class Unit
    {
        public Vector3 Location
        {
            get;
            set;
        }

        public Shape Model
        {
            get;
            set;
        }

        public Unit()
            :this(Vector3.Zero, null)
        {

        }

        public Unit(Vector3 location, GraphicsDevice graphicsDevice)
        {
            Location = location;
            Model = new Circle(graphicsDevice, location, 0.05f);
        }

        public abstract void DrawUnit(GraphicsDevice graphicsDevice, Matrix view, Matrix projection);

        public void MoveRandom(Random rand)
        {
            int modifier = rand.Next(-1, 2);
            float xDir = (float) rand.NextDouble() * 0.2f * modifier;

            modifier = rand.Next(-1, 2);
            float yDir = (float) rand.NextDouble() * 0.2f * modifier;

            Location = new Vector3(Location.X + xDir, Location.Y + yDir, 0.0f);
            Model.Translate(new Vector3(xDir, yDir, 0.0f));
        }
    }
}
