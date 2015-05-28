using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Duality
{
    public struct FloatingRectangle
    {
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float X, Y, Width, Height;

        private static FloatingRectangle _empty = default(FloatingRectangle);

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

        public Vector2 Location
        {
            get { return new Vector2(X, Y); }
            set { X = value.X; Y = value.Y; }
        }

        public Vector2 Center
        {
            get { return new Vector2(X + (Width / 2), Y + (Height / 2)); }
        }

        public static FloatingRectangle Empty
        {
            get { return FloatingRectangle._empty; }
        }

        public bool IsEmpty
        {
            get { return this.Width == 0 && this.Height == 0 && this.X == 0 && this.Y == 0; }
        }

        public FloatingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Offset(Vector2 amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        public void Offset(Point amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public void Offset(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        public bool Contains(float x, float y)
        {
            return Left <= x && x < Right && Top <= y && y < Bottom;
        }

        public bool Contains(int x, int y)
        {
            return Left <= x && x < Right && Top <= y && y < Bottom;
        }

        public bool Contains(Vector2 value)
        {
            return Left <= value.X && value.X < Right && Top <= value.Y && value.Y < Bottom;
        }

        public bool Contains(Point value)
        {
            return Left <= value.X && value.X < Right && Top <= value.Y && value.Y < Bottom;
        }

        public void Contains(ref Vector2 value, out bool result)
        {
            result = (Left <= value.X && value.X < Right && Top <= value.Y && value.Y < Bottom);
        }

        public void Contains(ref Point value, out bool result)
        {
            result = (Left <= value.X && value.X < Right && Top <= value.Y && value.Y < Bottom);
        }

        public bool Contains(FloatingRectangle value)
        {
            return Left <= value.X && value.Right <= Right && Top <= value.Y && value.Bottom <= Bottom;
        }

        public bool Contains(Rectangle value)
        {
            return Left <= value.X && value.Right <= Right && Top <= value.Y && value.Bottom <= Bottom;
        }

        public void Contains(ref FloatingRectangle value, out bool result)
        {
            result = (Left <= value.X && value.Right <= Right && Top <= value.Y && value.Bottom <= Bottom);
        }

        public void Contains(ref Rectangle value, out bool result)
        {
            result = (Left <= value.X && value.Right <= Right && Top <= value.Y && value.Bottom <= Bottom);
        }

        public bool Intersects(FloatingRectangle value)
        {
            return value.Left < Right && Left < value.Right && value.Top < Bottom && Top < value.Bottom;
        }

        public bool Intersects(Rectangle value)
        {
            return value.Left < Right && Left < value.Right && value.Top < Bottom && Top < value.Bottom;
        }

        public void Intersects(ref FloatingRectangle value, out bool result)
        {
            result = (value.Left < Right && Left < value.Right && value.Top < Bottom && Top < value.Bottom);
        }

        public void Intersects(ref Rectangle value, out bool result)
        {
            result = (value.Left < Right && Left < value.Right && value.Top < Bottom && Top < value.Bottom);
        }

        public static FloatingRectangle Intersect(FloatingRectangle value1, FloatingRectangle value2)
        {
            float num = value1.Right;
            float num2 = value2.Right;
            float num3 = value1.Bottom;
            float num4 = value2.Bottom;
            float num5 = (value1.X > value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y > value2.Y) ? value1.Y : value2.Y;
            float num7 = (num < num2) ? num : num2;
            float num8 = (num3 < num4) ? num3 : num4;
            FloatingRectangle result;
            if (num7 > num5 && num8 > num6)
            {
                result.X = num5;
                result.Y = num6;
                result.Width = num7 - num5;
                result.Height = num8 - num6;
            }
            else
            {
                result.X = 0;
                result.Y = 0;
                result.Width = 0;
                result.Height = 0;
            }
            return result;
        }

        public static void Intersect(ref FloatingRectangle value1, ref FloatingRectangle value2, out FloatingRectangle result)
        {
            float num = value1.Right;
            float num2 = value2.Right;
            float num3 = value1.Bottom;
            float num4 = value2.Bottom;
            float num5 = (value1.X > value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y > value2.Y) ? value1.Y : value2.Y;
            float num7 = (num < num2) ? num : num2;
            float num8 = (num3 < num4) ? num3 : num4;
            if (num7 > num5 && num8 > num6)
            {
                result.X = num5;
                result.Y = num6;
                result.Width = num7 - num5;
                result.Height = num8 - num6;
                return;
            }
            result.X = 0;
            result.Y = 0;
            result.Width = 0;
            result.Height = 0;
        }

        public static FloatingRectangle Union(FloatingRectangle value1, FloatingRectangle value2)
        {
            float num = value1.Right;
            float num2 = value2.Right;
            float num3 = value1.Bottom;
            float num4 = value2.Bottom;
            float num5 = (value1.X < value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y < value2.Y) ? value1.Y : value2.Y;
            float num7 = (num > num2) ? num : num2;
            float num8 = (num3 > num4) ? num3 : num4;
            FloatingRectangle result;
            result.X = num5;
            result.Y = num6;
            result.Width = num7 - num5;
            result.Height = num8 - num6;
            return result;
        }

        public static void Union(ref FloatingRectangle value1, ref FloatingRectangle value2, out FloatingRectangle result)
        {
            float num = value1.Right;
            float num2 = value2.Right;
            float num3 = value1.Bottom;
            float num4 = value2.Bottom;
            float num5 = (value1.X < value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y < value2.Y) ? value1.Y : value2.Y;
            float num7 = (num > num2) ? num : num2;
            float num8 = (num3 > num4) ? num3 : num4;
            result.X = num5;
            result.Y = num6;
            result.Width = num7 - num5;
            result.Height = num8 - num6;
        }

        public bool Equals(FloatingRectangle other)
        {
            return Left == other.Left && Top == other.Top && Width == other.Width && Height == other.Height;
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is FloatingRectangle)
            {
                result = Equals((FloatingRectangle)obj);
            }
            return result;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Width:{2} Height:{3}}}", new object[]
            {
                X.ToString(currentCulture),
                Y.ToString(currentCulture),
                Width.ToString(currentCulture),
                Height.ToString(currentCulture)
            });
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Width.GetHashCode() + Height.GetHashCode();
        }

        public static bool operator ==(FloatingRectangle a, FloatingRectangle b)
        {
            return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(FloatingRectangle a, FloatingRectangle b)
        {
            return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
        }
    }
}