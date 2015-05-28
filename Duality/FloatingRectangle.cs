using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Duality
{
    public struct FloatingRectangle
    {
        private float x, y, width, height, left = 0, right = 0, top = 0, bottom = 0;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Left
        {
            get { return X; }
        }

        public float Right
        {
            get { return X + Width; }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Bottom
        {
            get { return Y + Height; }
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = value.X; Y = value.Y; }
        }

        public FloatingRectangle(float X, float Y, float width, float height)
        {
            this.x = X;
            this.y = Y;
            this.width = width;
            this.height = height;
        }

        public bool Intersect(FloatingRectangle floatingRectangle)
        {
            if ((this.Bottom > floatingRectangle.Top && this.Bottom < floatingRectangle.Bottom) || (this.Top < floatingRectangle.Bottom && this.Top > floatingRectangle.Top) || this.Top == floatingRectangle.Top || this.Bottom == floatingRectangle.Bottom)
            {
                if (this.Right > floatingRectangle.Left || this.Left < floatingRectangle.Right)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
