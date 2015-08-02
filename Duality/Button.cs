using SharpDX;
using SharpDX.Toolkit.Input;
using SharpDX.Toolkit.Graphics;

namespace Duality.Interaction
{
    public struct Button
    {
        private enum ButtonType { Rectangle, Circle };

        private bool buttonState { get; set; }
        private float diameter { get; set; }
        private float windowWidth { get; set; }
        private float windowHeight { get; set; }
        private int bNum { get; set; }
        private ButtonType type { get; set; }
        private MouseState mouseState { get; set; }
        private Texture2D button0 { get; set; }

        /// <summary>
        /// Backing store for Collision.
        /// </summary>
        private Rectangle collision;

        /// <summary>
        /// Rectangle structure.
        /// </summary>
        public Rectangle Collision
        {
            get { return collision; }
            set { collision = value; }
        }

        /// <summary>
        /// Backing store for Circle.
        /// </summary>
        private Circle circle;

        /// <summary>
        /// Circle structure.
        /// </summary>
        public Circle Circle
        {
            get { return circle; }
            set { circle = value; }
        }

        /// <summary>
        /// Backing Stores for textures.
        /// </summary>
        private Texture2D button1, button2;

        /// <summary>
        /// Set Unpressed Button Texture.
        /// </summary>
        public Texture2D UnpressedButton
        {
            set { button1 = value; }
        }

        /// <summary>
        /// Set Hovered Button Texture.
        /// </summary>
        public Texture2D HoveredButton
        {
            set { button2 = value; }
        }

        /// <summary>
        /// Backing Store for Center of circle.
        /// </summary>
        private Vector2 center;

        /// <summary>
        /// Center of circle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// Creates a new button for the menu.
        /// </summary>
        /// <param name="position">Position of top left corner.</param>
        /// <param name="width">Width of button in pixels.</param>
        /// <param name="height">Height of button in pixels.</param>
        /// <param name="buttonNum">Number button uses to identify.</param>
        /// <param name="mouse">Mouse state for detection.</param>
        /// <param name="buttonNorm">Ordinary button state.</param>
        /// <param name="buttonHov">Hovered button state.</param>
        public Button(Vector2 position, int width, int height, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, float windowWidth, float windowHeight)
            : this()
        {
            center = Vector2.Zero;
            collision = new Rectangle((int)position.X, (int)position.Y, width, height);
            mouseState = mouse;
            button1 = buttonNorm;
            button2 = buttonHov;
            bNum = buttonNum;
            type = ButtonType.Rectangle;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        /// <summary>
        /// Creates a new circular button for the menu.
        /// </summary>
        /// <param name="centerPosition">The center position of the circle.</param>
        /// <param name="cirlceRadius">The radius of the circle.</param>
        /// <param name="buttonNum">Number button uses to identify.</param>
        /// <param name="mouse">Mouse state for detection.</param>
        /// <param name="buttonNorm">Ordinary button state.</param>
        /// <param name="buttonHov">Hovered button state.</param>
        public Button(Vector2 centerPosition, float circleDiameter, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, float windowWidth, float windowHeight)
            : this()
        {
            collision = Rectangle.Empty;
            center = centerPosition;
            diameter = circleDiameter;
            mouseState = mouse;
            button1 = buttonNorm;
            button2 = buttonHov;
            bNum = buttonNum;
            type = ButtonType.Circle;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public bool getButtonState()
        {
            switch (type)
            {
                case ButtonType.Rectangle:
                    if (Collision.Contains(mouseState.X * windowWidth, mouseState.Y * windowHeight))
                    {
                        button0 = button2;
                        if (mouseState.LeftButton.Pressed)
                        {
                            buttonState = true;
                        }
                    }
                    else
                    {
                        button0 = button1;
                        buttonState = false;
                    }
                    return buttonState;

                case ButtonType.Circle:
                    Circle = new Circle(center, diameter);
                    if (Circle.Intersects(mouseState, windowWidth, windowHeight))
                    {
                        button0 = button2;
                        if (mouseState.LeftButton.Pressed)
                        {
                            buttonState = true;
                        }
                    }
                    else
                    {
                        button0 = button1;
                        buttonState = false;
                    }
                    return buttonState;

                default:
                    return buttonState;
            }
        }

        public int getButtonNum()
        {
            return bNum;
        }

        public Vector2 getPosition()
        {
            switch (type)
            {
                case ButtonType.Circle:
                    Circle = new Circle(center, diameter);
                    return Circle.Location;
                case ButtonType.Rectangle:
                    return new Vector2(Collision.X, Collision.Y);
                default:
                    return Vector2.Zero;
            }
        }

        public Texture2D getTexture()
        {
            return button0;
        }
    }
}