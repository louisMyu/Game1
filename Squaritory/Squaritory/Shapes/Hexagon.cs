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
    class Hexagon : Shape
    {
        
      
        /// <summary>
        /// Number of vertices that comprise a hexagon.
        /// </summary>
        public const int HEXAGON_VERTICES =     6;
      
        /// <summary>
        /// Number of edges that comprise a hexagon.
        /// </summary>
        public const int HEXAGON_EDGES =        6;

        /// <summary>
        /// Number of indices that comprise a hexagon.
        /// </summary>
        public const int HEXAGON_INDICES =      12;

        public float HexSideLength
        {
            get { return Vector3.Distance(m_Vertices[0], m_Vertices[1]); }
        }

        public float HexWidth
        {
            get { return Vector3.Distance(m_Vertices[1], m_Vertices[5]); }
        }

        public float HexHeight
        {
            get { return Vector3.Distance(m_Vertices[0], m_Vertices[3]); }
        }

        private VertexPositionColor[] m_VertexPositionColorArray;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Hexagon()
            : this(null, Vector3.Zero)
        { 

        }

        /// <summary>
        /// Overloaded constructor.  Initializes object member variables.
        /// </summary>
        /// <param name="graphicsDevice">Graphics device.  For use with drawing.</param>
        /// <param name="location">World location.</param>
        public Hexagon(GraphicsDevice graphicsDevice, Vector3 location)
        {
            // Initialize members.
            if(graphicsDevice != null)
            {
                m_BasicEffect = new BasicEffect(graphicsDevice);
                m_StartingLocation = location;
                m_Location = m_StartingLocation;
                m_World = Matrix.CreateTranslation(m_Location);
                InitializeVertices(graphicsDevice);
                InitializeIndices(graphicsDevice);

                m_VertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), HEXAGON_VERTICES, BufferUsage.WriteOnly);
                m_IndexBuffer = new IndexBuffer(graphicsDevice, typeof(short), m_Indices.Length, BufferUsage.WriteOnly);
                m_VertexPositionColorArray = GetVertexPositionColorArray(HEXAGON_VERTICES);
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
            m_Vertices = new Vector3[HEXAGON_VERTICES];            

            for (int i = 0; i < HEXAGON_EDGES; i++)
            {
                float angle = i * 360 / HEXAGON_EDGES;
                Vector3 vertexPosition = Vector3.Transform(Vector3.UnitY, Matrix.CreateRotationZ(MathHelper.ToRadians(angle)));
                m_Vertices[i] = vertexPosition;
            }            
        }

        private void InitializeIndices(GraphicsDevice graphicsDevice)
        {
            m_Indices = new short[HEXAGON_INDICES];
            m_Indices[0] = 0; m_Indices[1] = 1; m_Indices[2] = 5;
            m_Indices[3] = 1; m_Indices[4] = 2; m_Indices[5] = 5;
            m_Indices[6] = 2; m_Indices[7] = 4; m_Indices[8] = 5;
            m_Indices[9] = 2; m_Indices[10] = 3; m_Indices[11] = 4;           
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
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, HEXAGON_VERTICES, 0, HEXAGON_INDICES / TRIANGLE_VERTICES);
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

        public override void Draw(GraphicsDevice graphicsDevice, Matrix view, Matrix projection, Color color)
        {
            throw new NotImplementedException();
        }
    }
}
