using System.Collections.Generic;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace Duality
{
    /// <summary>
    /// Define a vector curve using Catmull Rom interpolation.
    /// </summary>
    public class Curve
    {
        private List<Vector2> points, reference;

        #region Properties
        /// <summary>
        /// Set of four points used to define curve.
        /// </summary>
        public List<Vector2> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector2 Start
        {
            get { return points[1]; }
            set { points[1] = value; }
        }

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector2 End
        {
            get { return points[2]; }
            set { points[2] = value; }
        }

        /// <summary>
        /// Reference set of interpolated points.
        /// </summary>
        public List<Vector2> Reference
        {
            get { return reference; }
            set { reference = value; }
        }
        #endregion

        /// <summary>
        /// Constructs a curve using four preset points.
        /// </summary>
        public Curve()
        {
            points = new List<Vector2>() { Vector2.Zero, new Vector2(50, 25), new Vector2(75, 100), new Vector2(150, 100) };
            Build();
        }

        /// <summary>
        /// Constructs a curve using provided set of four points.
        /// </summary>
        /// <param name="points">Set of points to use.</param>
        public Curve(List<Vector2> points)
        {
            this.points = points;
            Build();
        }

        /// <summary>
        /// Builds reference list of interpolated points. Explicitely for behind the scenes work.
        /// </summary>
        public void Build()
        {
            reference = new List<Vector2>();
            for (float i = 0; i < 1; i = i + .1f)
            {
                reference.Add(Vector2.CatmullRom(Points[0], Points[1], Points[2], Points[3], i));
            }
        }

        /// <summary>
        /// Draws this curve with provided texture at each interpolated point.
        /// </summary>
        /// <param name="spriteBatch">Spritebatch used for drawing.</param>
        /// <param name="tex">Texture to draw.</param>
        public void Draw(SpriteBatch spriteBatch, Texture2D tex)
        {
            for (float i = 0; i < 1; i = i + .01f)
            {
                Vector2 t = Vector2.CatmullRom(Points[0], Points[1], Points[2], Points[3], i);
                RectangleF rect = new RectangleF(t.X, t.Y, 2, 2);
                spriteBatch.Draw(tex, rect, Color.Black);
            }
        }
    }
}
