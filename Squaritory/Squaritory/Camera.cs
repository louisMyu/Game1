using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Squaritory
{
    class Camera
    {
        private Matrix m_View;
        private Vector3 m_Location;
        private Vector3 m_LookAt;
        private Vector3 m_Up;

        public Matrix View
        {
            get { return m_View; }
        }

        public Camera()
        {
            m_Location = new Vector3(0, 0, 10);
            m_LookAt = new Vector3(0, 0, 0);
            m_Up = Vector3.Up;

            m_View = Matrix.CreateLookAt(m_Location, m_LookAt, m_Up);
        }

        public Matrix Move(Vector3 delta)
        {
            m_Location += delta;
            m_LookAt += delta;

            return Matrix.CreateLookAt(m_Location, m_LookAt, m_Up);
        }  
    }
}
