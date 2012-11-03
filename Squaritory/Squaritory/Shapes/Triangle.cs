using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory.Shapes
{
    public class Triangle : Shape
    {

        public Triangle()
            : this(null, Vector3.Zero)
        { 

        }

        public Triangle(GraphicsDevice graphicsDevice, Vector3 location)
        {
            if(graphicsDevice != null)
            {
                m_BasicEffect = new BasicEffect(graphicsDevice);
                m_StartingLocation = location;
                m_Location = m_StartingLocation;
                m_World = Matrix.CreateTranslation(m_Location);
                InitializeVertices(graphicsDevice);
                m_VertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), TRIANGLE_VERTICES, BufferUsage.WriteOnly);
            }
            else
            {
                throw new NullReferenceException("GraphicsDevice can't be null!");
            }
        }

        private void InitializeVertices(GraphicsDevice graphicsDevice)
        {
            m_Vertices = new Vector3[TRIANGLE_VERTICES];
            m_Vertices[0] = new Vector3(0, (float) Math.Sin(MathHelper.ToRadians(60)), 0);
            m_Vertices[1] = new Vector3(+0.5f, 0, 0);
            m_Vertices[2] = new Vector3(-0.5f, 0, 0);          
        }

        public override void Draw(GraphicsDevice graphicsDevice, Matrix view, Matrix projection)
        {
            m_BasicEffect.World = m_World;
            m_BasicEffect.View = view;
            m_BasicEffect.Projection = projection;
            m_BasicEffect.VertexColorEnabled = true;

            VertexPositionColor[] vertexPositionColorArray = GetVertexPositionColorArray(TRIANGLE_VERTICES);
            m_VertexBuffer.SetData<VertexPositionColor>(vertexPositionColorArray);
            graphicsDevice.SetVertexBuffer(m_VertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in m_BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }

        }

        public override void RotateX(float angle)
        {
            m_World = Matrix.CreateRotationX(MathHelper.ToRadians(angle)) * m_World;
        }

        public override void RotateY(float angle)
        {
            m_World = Matrix.CreateRotationY(MathHelper.ToRadians(angle)) * m_World;
        }

        public override void RotateZ(float angle)
        {
            m_World = Matrix.CreateRotationZ(MathHelper.ToRadians(angle)) * m_World;
        }

        public override void Translate(Vector3 translation)
        {
            m_Location += translation;
            m_World = Matrix.CreateTranslation(m_Location);
        }

        public override void ResetShapeLocation()
        {
            m_Location = m_StartingLocation;
            m_World = Matrix.CreateTranslation(m_Location);
        }

        public override void Scale(float scale)
        {
            m_World = Matrix.CreateScale(scale) * m_World;
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

        public override void Draw(GraphicsDevice graphicsDevice, Matrix view, Matrix projection, Color color)
        {
            throw new NotImplementedException();
        }
    }
}
