using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duality
{
    /// <summary>
    /// Represents a 2D circle.
    /// </summary>
    public struct Circle
    {
        private Vector2 center;

        private static Circle _empty = default(Circle);

        public float Diameter;

        /// <summary>
        /// Center position of the circle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public Vector2 Location
        {
            get { return new Vector2(Center.X - (Diameter / 2), Center.Y - (Diameter / 2)); }
        }

        public static Circle Empty
        {
            get { return Circle._empty; }
        }

        public bool IsEmpty
        {
            get { return Center == Vector2.Zero && Diameter == 0; }
        }

        /// <summary>
        /// Constructs a new circle.
        /// </summary>
        /// <param name="position">Center of circle.</param>
        /// <param name="diameter">Diameter of circle.</param>
        public Circle(Vector2 position, float diameter)
        {
            center = position;
            Diameter = diameter;
        }

        public void Offset(Vector2 amount)
        {
            Center += amount;
        }

        public void Inflate(float amount)
        {
            Center -= new Vector2(amount);
            Diameter += amount * 2;
        }

        public bool Contains(float x, float y)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, x, x),
                                    MathHelper.Clamp(Center.Y, y, y));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }

        public bool Contains(Vector2 value)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, value.X, value.X),
                                    MathHelper.Clamp(Center.Y, value.Y, value.Y));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }

        /// <summary>
        /// Determines if a circle intersects a rectangle.
        /// </summary>
        /// <returns>True if the circle and rectangle overlap. False otherwise.</returns>
        public bool Intersects(Rectangle value)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, value.Left, value.Right),
                                    MathHelper.Clamp(Center.Y, value.Top, value.Bottom));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }

        /// <summary>
        /// Determines if a circle intersects with a floating-point rectangle.
        /// </summary>
        /// <returns>True if the circle and rectangle overlap. False otherwise.</returns>
        public bool Intersects(FloatingRectangle value)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, value.Left, value.Right),
                                    MathHelper.Clamp(Center.Y, value.Top, value.Bottom));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }

        /// <summary>
        /// Determines if a circle contains the mouse.
        /// </summary>
        /// <returns>True if the mouse is within the circle. False otherwise</returns>
        public bool Intersects(MouseState mouse)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, mouse.X, mouse.X),
                                    MathHelper.Clamp(Center.Y, mouse.Y, mouse.Y));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }
    }
}
