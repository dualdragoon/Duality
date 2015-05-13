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
        /// <summary>
        /// Backing store for Center.
        /// </summary>
        private Vector2 center;

        /// <summary>
        /// Center position of the circle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// Diameter of the circle.
        /// </summary>
        public float Diameter;

        /// <summary>
        /// Constructs a new circle.
        /// </summary>
        public Circle(Vector2 position, float diameter)
        {
            center = position;
            Diameter = diameter;
        }

        /// <summary>
        /// Determines if a circle intersects a rectangle.
        /// </summary>
        /// <returns>True if the circle and rectangle overlap. False otherwise.</returns>
        public bool Intersects(Rectangle rectangle)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
                                    MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

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
