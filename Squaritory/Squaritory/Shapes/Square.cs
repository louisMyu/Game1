using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory.Shapes
{
    class Square : Shape
    {
        public const int SQUARE_VERTICES = 4;
        public const int SQUARE_EDGES = 6;
        public const int SQUARE_INDICES = 6;

        public float SquareSideLength
        {
            get { return Vector3.Distance(m_Vertices[0], m_Vertices[1]); }
        }

        public float SquareWidth
        {
            get { return Vector3.Distance(m_Vertices[0], m_Vertices[1]); }
        }

        public float SquareHeight
        {
            get { return Vector3.Distance(m_Vertices[1], m_Vertices[2]); }
        }

        public Square()
            : this(null, Vector3.Zero)
        {

        }

        public Square(GraphicsDevice graphicsDevice, Vector3 location)
            : base(graphicsDevice, location)
        {
            InitializeVertices(graphicsDevice);
            InitializeIndices(graphicsDevice);

            m_VertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), SQUARE_VERTICES, BufferUsage.WriteOnly);
            m_IndexBuffer = new IndexBuffer(graphicsDevice, typeof(short), m_Indices.Length, BufferUsage.WriteOnly);
        }        

        private void InitializeVertices(GraphicsDevice graphicsDevice)
        {
            m_Vertices = new Vector3[SQUARE_VERTICES];
            m_Vertices[0] = new Vector3(1.0f, 1.0f, 0);
            m_Vertices[1] = new Vector3(-1.0f, 1.0f, 0);
            m_Vertices[2] = new Vector3(-1.0f, -1.0f, 0);
            m_Vertices[3] = new Vector3(1.0f, -1.0f, 0);
        }

        private void InitializeIndices(GraphicsDevice graphicsDevice)
        {
            m_Indices = new short[SQUARE_INDICES];
            m_Indices[0] = 0; m_Indices[1] = 1; m_Indices[2] = 2;
            m_Indices[3] = 0; m_Indices[4] = 2; m_Indices[5] = 3;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection, Color color)
        {
            m_BasicEffect.World = m_World;
            m_BasicEffect.View = view;
            m_BasicEffect.Projection = projection;
            m_BasicEffect.VertexColorEnabled = true;

            VertexPositionColor[] vertexPositionColorArray = new VertexPositionColor[SQUARE_VERTICES];
            for (int i = 0; i < SQUARE_VERTICES; i++)
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
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, SQUARE_VERTICES, 0, SQUARE_INDICES / TRIANGLE_VERTICES);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override void TranslateY(float distance)
        {
            throw new NotImplementedException();
        }

        public override void TranslateZ(float distance)
        {
            throw new NotImplementedException();
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
