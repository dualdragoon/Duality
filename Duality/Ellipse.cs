using System;
using SharpDX;
using SharpDX.Toolkit.Input;

namespace Duality
{
    /// <summary>
    /// Represents a 2D ellipse.
    /// </summary>
    public struct Ellipse
    {
        private static Ellipse _empty = default(Ellipse);

        private enum EllipseDirection { Vertical, Horizontal, }

        private EllipseDirection direction;
        private float width, height, fociDistance, majorAxis, minorAxis;
        private Vector2 center, posFoci, negFoci;
        
        /// <summary>
        /// Center position of the ellipse.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public Vector2 Location
        {
            get { return new Vector2(center.X - (width / 2), center.Y - (height / 2)); }
        }

        public static Ellipse Empty
        {
            get { return _empty; }
        }

        public bool IsEmpty
        {
            get { return Center == Vector2.Zero && width == 0 && height == 0; }
        }

        /// <summary>
        /// Constructs a new ellipse.
        /// </summary>
        /// <param name="width">Width at thickest part of ellipse.</param>
        /// <param name="height">Height at tallest part of ellipse.</param>
        /// <param name="center">Center of ellipse.</param>
        public Ellipse(float width, float height, Vector2 center)
        {
            this.width = width;
            this.height = height;
            this.center = center;

            direction = (width < height) ? EllipseDirection.Vertical : EllipseDirection.Horizontal;

            switch (direction)
            {
                case EllipseDirection.Vertical:
                    majorAxis = height;
                    minorAxis = width;
                    fociDistance = (float)Math.Sqrt(Math.Pow(height / 2, 2) - Math.Pow((width / 2), 2));
                    posFoci = center + new Vector2(0, fociDistance);
                    negFoci = center - new Vector2(0, fociDistance);
                    break;

                case EllipseDirection.Horizontal:
                    majorAxis = width;
                    minorAxis = height;
                    fociDistance = (float)Math.Sqrt(Math.Pow(width / 2, 2) - Math.Pow((height / 2), 2));
                    posFoci = center + new Vector2(fociDistance, 0);
                    negFoci = center - new Vector2(fociDistance, 0);
                    break;

                default:
                    majorAxis = 0;
                    minorAxis = 0;
                    fociDistance = 0;
                    posFoci = Vector2.Zero;
                    negFoci = Vector2.Zero;
                    break;
            }
        }

        public void Offset(Vector2 amount)
        {
            Center += amount;

            switch (direction)
            {
                case EllipseDirection.Vertical:
                    posFoci = center + new Vector2(0, fociDistance);
                    negFoci = center - new Vector2(0, fociDistance);
                    break;

                case EllipseDirection.Horizontal:
                    posFoci = center + new Vector2(fociDistance, 0);
                    negFoci = center - new Vector2(fociDistance, 0);
                    break;

                default:
                    break;
            }
        }

        public bool Contains(float x, float y)
        {
            Vector2 v = Vector2.Clamp(center, new Vector2(x, y), new Vector2(x, y));

            Vector2 posDirection = posFoci - v;
            float posDistanceSquared = posDirection.LengthSquared();

            Vector2 negDirection = negFoci - v;
            float negDistanceSquared = negDirection.LengthSquared();

            return ((posDistanceSquared > 0 && negDistanceSquared > 0) && ((float)Math.Sqrt(posDistanceSquared) + (float)Math.Sqrt(negDistanceSquared) <= majorAxis));
        }

        public bool Contains(Vector2 value)
        {
            Vector2 v = Vector2.Clamp(center, value, value);

            Vector2 posDirection = posFoci - v;
            float posDistanceSquared = posDirection.LengthSquared();

            Vector2 negDirection = negFoci - v;
            float negDistanceSquared = negDirection.LengthSquared();

            return ((posDistanceSquared > 0 && negDistanceSquared > 0) && ((float)Math.Sqrt(posDistanceSquared) + (float)Math.Sqrt(negDistanceSquared) <= majorAxis));
        }

        public bool Intersects(RectangleF value)
        {
            Vector2 v = Vector2.Clamp(center, value.TopLeft, value.BottomRight);

            Vector2 posDirection = posFoci - v;
            float posDistanceSquared = posDirection.LengthSquared();

            Vector2 negDirection = negFoci - v;
            float negDistanceSquared = negDirection.LengthSquared();

            return ((posDistanceSquared > 0 && negDistanceSquared > 0) && ((float)Math.Sqrt(posDistanceSquared) + (float)Math.Sqrt(negDistanceSquared) <= majorAxis));
        }

        public bool Intersects(MouseState mouse, float windowWidth, float windowHeight)
        {
            Vector2 v = Vector2.Clamp(Center, new Vector2(mouse.X * windowWidth, mouse.Y * windowHeight), new Vector2(mouse.X * windowWidth, mouse.Y * windowHeight));

            Vector2 posDirection = posFoci - v;
            float posDistanceSquared = posDirection.LengthSquared();

            Vector2 negDirection = posFoci - v;
            float negDistanceSquared = negDirection.LengthSquared();

            return ((posDistanceSquared > 0 && negDistanceSquared > 0) && ((float)Math.Sqrt(posDistanceSquared) + (float)Math.Sqrt(negDistanceSquared) <= majorAxis));
        }
    }
}
