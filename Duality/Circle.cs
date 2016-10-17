using SharpDX;

namespace Duality
{
    /// <summary>
    /// Represents a 2D circle.
    /// </summary>
    public struct Circle
    {
        private Vector2 center;

        private static Circle _empty = default(Circle);

        /// <summary>
        /// Diameter of this circle.
        /// </summary>
        public float Diameter;

        /// <summary>
        /// Center position of this circle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// Top-Left location of this circle.
        /// </summary>
        public Vector2 Location
        {
            get { return new Vector2(Center.X - (Diameter / 2), Center.Y - (Diameter / 2)); }
        }

        /// <summary>
        /// Empty circle.
        /// </summary>
        public static Circle Empty
        {
            get { return _empty; }
        }

        /// <summary>
        /// Tells whether this circle is empty or not.
        /// </summary>
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

        /// <summary>
        /// Moves this circle.
        /// </summary>
        /// <param name="amount">Vector2 with x and y values to move this circle by.</param>
        public void Offset(Vector2 amount)
        {
            Center += amount;
        }

        /// <summary>
        /// Increases this circle's size from the center.
        /// </summary>
        /// <param name="amount">Value to increase the size of this circle by.</param>
        public void Inflate(float amount)
        {
            Center -= new Vector2(amount);
            Diameter += amount * 2;
        }

        /// <summary>
        /// Determines whether this circle contains a specified point.
        /// </summary>
        /// <param name="x">X value of point to check.</param>
        /// <param name="y">Y value of point to check.</param>
        /// <returns></returns>
        public bool Contains(float x, float y)
        {
            Vector2 v = Vector2.Clamp(center, new Vector2(x, y), new Vector2(x, y));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared >= 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }

        /// <summary>
        /// Determines whether this circle contains a specified point.
        /// </summary>
        /// <param name="value">Vector2 point to check.</param>
        /// <returns></returns>
        public bool Contains(Vector2 value)
        {
            Vector2 v = Vector2.Clamp(Center, value, value);

            Vector2 direction = center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared >= 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }

        /// <summary>
        /// Determines whether this circle intersects a rectangle.
        /// </summary>
        /// <param name="value">Rectangle to check intersection.</param>
        /// <returns></returns>
        public bool Intersects(RectangleF value)
        {
            Vector2 v = Vector2.Clamp(Center, value.TopLeft, value.BottomRight);

            Vector2 direction = center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared >= 0) && (distanceSquared < (Diameter / 2) * (Diameter / 2)));
        }
    }
}
