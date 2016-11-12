using System;
using SharpDX;
using SharpDX.Toolkit.Input;
using SharpDX.Toolkit.Graphics;

namespace Duality.Interaction
{
    /// <summary>
    /// Defines the geometric type of a button.
    /// </summary>
    public enum ButtonType { Rectangle, Circle, Ellipse };

    /// <summary>
    /// Define a Button for user interaction.
    /// </summary>
    public class Button
    {
        private bool leftHeld, rightHeld, clickable;
        private ButtonType type;
        private float diameter, windowWidth, windowHeight;
        private Texture2D button0;

        private event EventHandler leftClicked;

        /// <summary>
        /// Event raised when button is left clicked.
        /// </summary>
        public event EventHandler LeftClicked
        {
            add { leftClicked += value; }
            remove { leftClicked -= value; }
        }

        private event EventHandler rightClicked;

        /// <summary>
        /// Event raised when button is right clicked.
        /// </summary>
        public event EventHandler RightClicked
        {
            add { rightClicked += value; }
            remove { rightClicked -= value; }
        }

        /// <summary>
        /// Position of button used for drawing.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                switch (type)
                {
                    case ButtonType.Circle:
                        Circle = new Circle(center, diameter);
                        return Circle.Location;
                    case ButtonType.Rectangle:
                        return Collision.Location;
                    case ButtonType.Ellipse:
                        return Ellipse.Location;
                    default:
                        return Vector2.Zero;
                }
            }
        }

        /// <summary>
        /// Flag raised when left clicked and held.
        /// </summary>
        public bool LeftHeld
        {
            get { return leftHeld; }
        }

        /// <summary>
        /// Flag raised when right clicked and held.
        /// </summary>
        public bool RightHeld
        {
            get { return rightHeld; }
        }
        
        /// <summary>
        /// Whether button is clickable or not.
        /// </summary>
        public bool Clickable
        {
            get { return clickable; }
            set
            { 
                clickable = value;
                leftHeld = false;
                rightHeld = false;
            }
        }

        /// <summary>
        /// Currently active texture used for drawing.
        /// </summary>
        public Texture2D Texture
        {
            get { return button0; }
        }

        private int bNum;

        /// <summary>
        /// Identification number used to differentiate between otherwise identical buttons.
        /// </summary>
        public int ButtonNum
        {
            get { return bNum; }
        }

        private MouseState mouseState;

        /// <summary>
        /// Backing store for Collision.
        /// </summary>
        private RectangleF collision;

        /// <summary>
        /// Rectangle structure.
        /// </summary>
        public RectangleF Collision
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
        /// Backing store for Ellipse.
        /// </summary>
        private Ellipse ellipse;

        /// <summary>
        /// Ellipse structure.
        /// </summary>
        public Ellipse Ellipse
        {
            get { return ellipse; }
            set { ellipse = value; }
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
        /// <param name="clickable">Whether button is clickable or not.</param>
        /// <param name="windowWidth">Width of viewport for scaling.</param>
        /// <param name="windowHeight">Height of viewport for scaling.</param>
        public Button(Vector2 position, float width, float height, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, bool clickable, float windowWidth, float windowHeight)
        {
            center = Vector2.Zero;
            collision = new RectangleF(position.X, position.Y, width, height);
            mouseState = mouse;
            button0 = buttonNorm;
            button1 = buttonNorm;
            button2 = buttonHov;
            bNum = buttonNum;
            type = ButtonType.Rectangle;
            this.clickable = clickable;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        /// <summary>
        /// Creates a new circular button for the menu.
        /// </summary>
        /// <param name="centerPosition">The center position of the circle.</param>
        /// <param name="circleDiameter">Diameter of the circle.</param>
        /// <param name="buttonNum">Number button uses to identify.</param>
        /// <param name="mouse">Mouse state for detection.</param>
        /// <param name="buttonNorm">Ordinary button state.</param>
        /// <param name="buttonHov">Hovered button state.</param>
        /// <param name="clickable">Whether button is clickable or not.</param>
        /// <param name="windowWidth">Width of viewport for scaling.</param>
        /// <param name="windowHeight">Height of viewport for scaling.</param>
        public Button(Vector2 centerPosition, float circleDiameter, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, bool clickable, float windowWidth, float windowHeight)
        {
            collision = RectangleF.Empty;
            center = centerPosition;
            diameter = circleDiameter;
            circle = new Circle(center, diameter);
            mouseState = mouse;
            button0 = buttonNorm;
            button1 = buttonNorm;
            button2 = buttonHov;
            bNum = buttonNum;
            type = ButtonType.Circle;
            this.clickable = clickable;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }


        /// <summary>
        /// Creates a new elliptical button for the menu.
        /// </summary>
        /// <param name="centerPosition">The center position of the ellipse.</param>
        /// <param name="buttonNum">Number button uses to identify.</param>
        /// <param name="mouse">Mouse state for detection.</param>
        /// <param name="buttonNorm">Ordinary button state.</param>
        /// <param name="buttonHov">Hovered button state.</param>
        /// <param name="clickable">Whether button is clickable or not.</param>
        /// <param name="windowWidth">Width of viewport for scaling.</param>
        /// <param name="windowHeight">Height of viewport for scaling.</param>
        public Button(Vector2 centerPosition, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, bool clickable, float windowWidth, float windowHeight)
        {
            collision = RectangleF.Empty;
            center = centerPosition;
            mouseState = mouse;
            button0 = buttonNorm;
            button1 = buttonNorm;
            button2 = buttonHov;
            bNum = buttonNum;
            type = ButtonType.Ellipse;
            this.clickable = clickable;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        /// <summary>
        /// Updates button to check for clicks.
        /// </summary>
        /// <param name="mouse"></param>
        public void Update(MouseState mouse)
        {
            mouseState = mouse;
            switch (type)
            {
                case ButtonType.Rectangle:
                    if (Collision.Contains(mouseState.X * windowWidth, mouseState.Y * windowHeight) && clickable)
                    {
                        button0 = button2;
                        if (mouseState.LeftButton.Pressed)
                        {
                            OnLeftClicked();
                        }
                        else if (mouseState.RightButton.Pressed)
                        {
                            OnRightClicked();
                        }
                        leftHeld = mouseState.LeftButton.Down;
                        rightHeld = mouseState.RightButton.Down;
                    }
                    else
                    {
                        leftHeld = false;
                        rightHeld = false;
                        button0 = button1;
                    }
                    break;

                case ButtonType.Circle:
                    Circle = new Circle(center, diameter);
                    if (Circle.Contains(mouseState.X * windowWidth, mouseState.Y * windowHeight) && clickable)
                    {
                        button0 = button2;
                        if (mouseState.LeftButton.Pressed)
                        {
                            OnLeftClicked();
                        }
                        else if (mouseState.RightButton.Pressed)
                        {
                            OnRightClicked();
                        }
                        leftHeld = mouseState.LeftButton.Down;
                        rightHeld = mouseState.RightButton.Down;
                    }
                    else
                    {
                        leftHeld = false;
                        rightHeld = false;
                        button0 = button1;
                    }
                    break;

                case ButtonType.Ellipse:
                    Ellipse = new Ellipse(button1.Width, button1.Height, center);
                    if (Ellipse.Contains(mouseState.X * windowWidth, mouseState.Y * windowHeight) && clickable)
                    {
                        button0 = button2;
                        if (mouseState.LeftButton.Pressed)
                        {
                            OnLeftClicked();
                        }
                        else if (mouseState.RightButton.Pressed)
                        {
                            OnRightClicked();
                        }
                        leftHeld = mouseState.LeftButton.Down;
                        rightHeld = mouseState.RightButton.Down;
                    }
                    else
                    {
                        leftHeld = false;
                        rightHeld = false;
                        button0 = button1;
                    }
                    break;

                default:
                    break;
            }
        }

        private void OnLeftClicked()
        {
            leftClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnRightClicked()
        {
            rightClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}