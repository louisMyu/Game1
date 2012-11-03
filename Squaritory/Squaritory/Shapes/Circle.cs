using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory.Shapes
{
    /// <summary>
    /// This class defines the Hexagon shape.
    /// </summary>
    class Circle : Shape
    {
        /// <summary>
        /// Number of vertices that comprise a hexagon.
        /// </summary>
        public const int CIRCLE_VERTICES = 181;

        /// <summary>
        /// Number of edges that comprise a hexagon.
        /// </summary>
        public const int CIRCLE_EDGES = 180;

        /// <summary>
        /// Number of indices that comprise a hexagon.
        /// </summary>
        public const int CIRCLE_INDICES = 540;

        public float Radius
        {
            get;
            set;
        }

        //public float HexSideLength
        //{
        //    get { return Vector3.Distance(m_Vertices[0], m_Vertices[1]); }
        //}

        //public float HexWidth
        //{
        //    get { return Vector3.Distance(m_Vertices[1], m_Vertices[5]); }
        //}

        //public float HexHeight
        //{
        //    get { return Vector3.Distance(m_Vertices[0], m_Vertices[3]); }
        //}

        private VertexPositionColor[] m_VertexPositionColorArray;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Circle()
            : this(null, Vector3.Zero, 1.0f)
        {

        }

        /// <summary>
        /// Overloaded constructor.  Initializes object member variables.
        /// </summary>
        /// <param name="graphicsDevice">Graphics device.  For use with drawing.</param>
        /// <param name="location">World location.</param>
        public Circle(GraphicsDevice graphicsDevice, Vector3 location, float radius)
        {
            Radius = radius;

            // Initialize members.
            if (graphicsDevice != null)
            {
                m_BasicEffect = new BasicEffect(graphicsDevice);
                m_StartingLocation = location;
                m_Location = m_StartingLocation;
                m_World = Matrix.CreateTranslation(m_Location);
                InitializeVertices(graphicsDevice);
                InitializeIndices(graphicsDevice);

                m_VertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), CIRCLE_VERTICES, BufferUsage.WriteOnly);
                m_IndexBuffer = new IndexBuffer(graphicsDevice, typeof(short), m_Indices.Length, BufferUsage.WriteOnly);
                m_VertexPositionColorArray = GetVertexPositionColorArray(CIRCLE_VERTICES);
            }
            else
            {
                throw new NullReferenceException("GraphicsDevice can't be null!");
            }
        }

        /// <summary>
        /// Creates the vertex buffer for a hexagon shape.
        /// </summary>
        /// <param name="graphicsDevice">Graphics device.  For use with drawing.</param>
        private void InitializeVertices(GraphicsDevice graphicsDevice)
        {
            m_Vertices = new Vector3[CIRCLE_VERTICES];

            m_Vertices[0] = Vector3.Zero;
            for (int i = 1; i < CIRCLE_EDGES+1; i++)
            {
                float angle = i * 360 / CIRCLE_EDGES;
                Vector3 vertexPosition = Vector3.Transform(new Vector3(0.0f, Radius, 0.0f), Matrix.CreateRotationZ(MathHelper.ToRadians(angle)));
                m_Vertices[i] = vertexPosition;
            }
        }

        private void InitializeIndices(GraphicsDevice graphicsDevice)
        {
            m_Indices = new short[CIRCLE_INDICES];
            for (int i = 0; i < CIRCLE_EDGES; i++)
            {
                m_Indices[i * 3] = 0;
                m_Indices[(i * 3) + 1] = (short)(i + 1);
                m_Indices[(i * 3) + 2] = (short)(i + 2);
            }
            m_Indices[(CIRCLE_INDICES) - 1] = 1;
        }



        public override void Draw(GraphicsDevice graphicsDevice, Matrix view, Matrix projection, Color color)
        {
            m_BasicEffect.World = m_World;
            m_BasicEffect.View = view;
            m_BasicEffect.Projection = projection;
            m_BasicEffect.VertexColorEnabled = true;

            VertexPositionColor[] vertexPositionColorArray = new VertexPositionColor[CIRCLE_VERTICES];
            for (int i = 0; i < CIRCLE_VERTICES; i++)
            {
                vertexPositionColorArray[i] = new VertexPositionColor(m_Vertices[i], color);
            }
            m_VertexBuffer.SetData<VertexPositionColor>(vertexPositionColorArray);
            graphicsDevice.SetVertexBuffer(m_VertexBuffer);
            m_IndexBuffer.SetData(m_Indices);
            graphicsDevice.Indices = m_IndexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in m_BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, CIRCLE_VERTICES, 0, CIRCLE_INDICES / TRIANGLE_VERTICES);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection)
        {
            m_BasicEffect.World = m_World;
            m_BasicEffect.View = view;
            m_BasicEffect.Projection = projection;
            m_BasicEffect.VertexColorEnabled = true;

            m_VertexBuffer.SetData<VertexPositionColor>(m_VertexPositionColorArray);
            graphicsDevice.SetVertexBuffer(m_VertexBuffer);
            m_IndexBuffer.SetData(m_Indices);
            graphicsDevice.Indices = m_IndexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in m_BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, CIRCLE_VERTICES, 0, CIRCLE_INDICES / TRIANGLE_VERTICES);
            }
        }

        public override void RotateX(float angle)
        {
            throw new NotImplementedException();
        }

        public override void RotateY(float angle)
        {
            throw new NotImplementedException();
        }

        public override void RotateZ(float angle)
        {
            throw new NotImplementedException();
        }

        public override void Translate(Microsoft.Xna.Framework.Vector3 translation)
        {
            m_Location += translation;
            m_World = Matrix.CreateTranslation(m_Location);
        }

        public override void TranslateX(float distance)
        {
            m_Location.X += distance;
            m_World = Matrix.CreateTranslation(m_Location);
        }
        public override void TranslateY(float distance)
        {
            m_Location.Y += distance;
            m_World = Matrix.CreateTranslation(m_Location);
        }

        public override void TranslateZ(float distance)
        {
            m_Location.Z += distance;
            m_World = Matrix.CreateTranslation(m_Location);
        }

        public override void ResetShapeLocation()
        {
            m_Location = m_StartingLocation;
            m_World = Matrix.CreateTranslation(m_Location);
        }

        public override void Scale(float scale)
        {
            throw new NotImplementedException();
        }
    }
}
