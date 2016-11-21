using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using Duality.Interaction;

namespace Duality
{
    /// <summary>
    /// Define a Scrolling Window for text display.
    /// </summary>
    public class ScrollingWindow
    {
        float height, scrollTopPos;
        Button downArrow, upArrow, scrollBar;
        MouseState state;
        List<string> strings = new List<string>();
        RectangleF size;
        SpriteFont font;
        Vector2 scale;

        /// <summary>
        /// Value between 0 and 1.
        /// </summary>
        float position = 0;

        /// <summary>
        /// Position of scroll bar between 0 and 1.
        /// </summary>
        public float Position
        {
            get { return position; }
            set
            {
                position = value;
                if (position < 0) position = 0;
                if (position > 1f) position = 1;
            }
        }

        /// <summary>
        /// Strings to be shown.
        /// </summary>
        public List<string> Strings
        {
            set { strings = value; }
        }

        private void LoadContent(Texture2D[] textures, Vector2[] sizes)
        {
            upArrow = new Button(new Vector2(size.X + size.Width - sizes[0].X, size.Y), sizes[0].X, sizes[0].Y, 0, state, textures[0], textures[1], true, scale.X, scale.Y);
            upArrow.LeftClicked += UpArrowPressed;

            scrollBar = new Button(new Vector2(size.X + size.Width - sizes[1].X, size.Y + sizes[0].Y), sizes[1].X, sizes[1].Y, 0, state, textures[2], textures[3], true, scale.X, scale.Y);

            downArrow = new Button(new Vector2(size.X + size.Width - sizes[2].X, size.Y + size.Height - sizes[2].Y), sizes[2].X, sizes[2].Y, 0, state, textures[4], textures[5], true, scale.X, scale.Y);
            downArrow.LeftClicked += DownArrowPressed;

            height = size.Height - (upArrow.Collision.Height + downArrow.Collision.Height * 2);
            scrollTopPos = size.Y + upArrow.Collision.Height;
        }

        /// <summary>
        /// Creates a scrollable window.
        /// </summary>
        /// <param name="strings">List of strings to be displayed in window.</param>
        /// <param name="textures">Split up. 2 per piece. In order: up arrow, scroll bar, down arrow.</param>
        /// <param name="sizes">Sizes of each piece. In order: up arrow, scroll bar, down arrow.</param>
        /// <param name="size">Rectangle used for size and position of window.</param>
        /// <param name="windowScale">Vector2 with width and height of gamescreen.</param>
        /// <param name="font">Font to use for strings.</param>
        /// <param name="mouse">Mouse state for checking the buttons.</param>
        public ScrollingWindow(List<string> strings, Texture2D[] textures, Vector2[] sizes, RectangleF size, Vector2 windowScale, SpriteFont font, MouseState mouse)
        {
            this.strings = strings;
            this.size = size;
            scale = windowScale;
            this.font = font;
            state = mouse;

            LoadContent(textures, sizes);
        }

        private void UpArrowPressed(object sender, EventArgs e)
        {
            Position -= .01f;
            scrollBar.Collision = new RectangleF(scrollBar.Collision.X, (Position * height) + scrollTopPos, scrollBar.Collision.Width, scrollBar.Collision.Height);
        }

        private void DownArrowPressed(object sender, EventArgs e)
        {
            Position += .01f;
            scrollBar.Collision = new RectangleF(scrollBar.Collision.X, (Position * height) + scrollTopPos, scrollBar.Collision.Width, scrollBar.Collision.Height);
        }

        /// <summary>
        /// Updates window to check for movement.
        /// </summary>
        /// <param name="mouse"></param>
        public void Update(MouseState mouse)
        {
            upArrow.Update(mouse);
            scrollBar.Update(mouse);
            downArrow.Update(mouse);

            if (mouse.WheelDelta > 0) UpArrowPressed(this, EventArgs.Empty);
            if (mouse.WheelDelta < 0) DownArrowPressed(this, EventArgs.Empty);

            if (scrollBar.LeftHeld)
            {
                Position = (mouse.Y * scale.Y - (scrollBar.Collision.Height / 2) - scrollTopPos) / height;
                scrollBar.Collision = new RectangleF(scrollBar.Collision.X, (Position * height) + scrollTopPos, scrollBar.Collision.Width, scrollBar.Collision.Height);
            }
        }

        /// <summary>
        /// Draws the window.
        /// </summary>
        /// <param name="spriteBatch">Spritebatch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(upArrow.Texture, upArrow.Collision, Color.White);
            spriteBatch.Draw(scrollBar.Texture, scrollBar.Collision, Color.White);
            spriteBatch.Draw(downArrow.Texture, downArrow.Collision, Color.White);

            for (int i = 0; i < strings.Count; i++)
            {
                spriteBatch.DrawString(font, strings[i], new Vector2(size.X + 10, size.Y + 10 + i * font.MeasureString("H").Y - Position * height), Color.Gold);
            }
        }
    }
}
