using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory.Shapes
{
    public abstract class Shape
    {
        /// <summary>
        /// Number of vertices that comprise a triangle.
        /// </summary>
        protected const int TRIANGLE_VERTICES = 3;

        protected VertexBuffer m_VertexBuffer;
        protected IndexBuffer m_IndexBuffer;
        protected Vector3[] m_Vertices;
        protected short[] m_Indices;
        protected Matrix m_World;
        protected Vector3 m_Location;
        protected Vector3 m_StartingLocation;
        protected BasicEffect m_BasicEffect;
        static Random rand = new Random();

        public Vector3 Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }

        public Shape()
        {

        }

        public Shape(GraphicsDevice graphicsDevice, Vector3 location)
        {
            if (graphicsDevice != null)
            {
                m_BasicEffect = new BasicEffect(graphicsDevice);
                m_StartingLocation = location;
                m_Location = m_StartingLocation;
                m_World = Matrix.CreateTranslation(m_Location);                
            }
        }

        public VertexPositionColor[] GetVertexPositionColorArray(int numShapeVertices)
        {
            VertexPositionColor[] vertexPositionColorArray = new VertexPositionColor[numShapeVertices];
            
            Color color = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), 255);

            for (int i = 0; i < numShapeVertices; i++)
            {
                vertexPositionColorArray[i] = new VertexPositionColor(m_Vertices[i], color);
            }

            return vertexPositionColorArray;
        }

        public virtual void SetStartingLocation(Vector3 startingLoc)
        {
            m_StartingLocation = startingLoc;
            this.Translate(startingLoc);
        }

        public abstract void Draw(GraphicsDevice graphicsDevice, Matrix view, Matrix projection);
        public abstract void Draw(GraphicsDevice graphicsDevice, Matrix view, Matrix projection, Color color);

        public abstract void RotateX(float angle);

        public abstract void RotateY(float angle);

        public abstract void RotateZ(float angle);

        public abstract void Translate(Vector3 translation);

        public abstract void TranslateX(float distance);
        public abstract void TranslateY(float distance);
        public abstract void TranslateZ(float distance);

        public abstract void ResetShapeLocation();

        public abstract void Scale(float scale);
    }
}
