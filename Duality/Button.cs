using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

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
        private bool leftHeld, rightHeld, clickable, hovered;
		private ButtonState leftOld = ButtonState.Released, rightOld = ButtonState.Released;
        private ButtonType type;
		private Color hoverColor = Color.White;
        private float diameter;
		private SpriteEffects spriteFlip = SpriteEffects.None;
		private string name;
        private Texture2D button0;

		/// <summary>
		/// Direction to flip button sprite.
		/// </summary>
		public SpriteEffects SpriteFlip
		{
			get { return spriteFlip; }
			set { spriteFlip = value; }
		}

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

		private event EventHandler entered;

		/// <summary>
		/// Event raised when the mouse enters the button's collision.
		/// </summary>
		public event EventHandler Entered
		{
			add { entered += value; }
			remove { entered -= value; }
		}

		private event EventHandler exited;

		/// <summary>
		/// Event raised when the mouse leaves the button's collision.
		/// </summary>
		public event EventHandler Exited
		{
			add { exited += value; }
			remove { exited -= value; }
		}

		public ButtonType Type
		{
			get { return type; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
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
                        Circle = new CircleF(center, diameter / 2);
                        return Circle.Center - new Point2(Circle.Radius, Circle.Radius);
                    case ButtonType.Rectangle:
                        return Collision.Position;
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

		public bool Hovered
		{
			get { return hovered; }
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

		public Color HoverColor
		{
			get { return hoverColor; }
			set { hoverColor = value; }
		}

		public Color DisplayColor { get; private set; }

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
        private CircleF circle;

        /// <summary>
        /// Circle structure.
        /// </summary>
        public CircleF Circle
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
        public Button(Vector2 position, float width, float height, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, bool clickable)
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
			DisplayColor = Color.White;
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
        public Button(Vector2 centerPosition, float circleDiameter, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, bool clickable)
        {
            collision = RectangleF.Empty;
            center = centerPosition;
            diameter = circleDiameter;
            circle = new CircleF(center, diameter / 2);
            mouseState = mouse;
            button0 = buttonNorm;
            button1 = buttonNorm;
            button2 = buttonHov;
            bNum = buttonNum;
            type = ButtonType.Circle;
            this.clickable = clickable;
			DisplayColor = Color.White;
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
        public Button(Vector2 centerPosition, int buttonNum, MouseState mouse, Texture2D buttonNorm, Texture2D buttonHov, bool clickable)
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
			DisplayColor = Color.White;
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
                    if (Collision.Contains(new Vector2(mouseState.X, mouseState.Y)))
                    {
						if (!hovered) OnEntered();
						if (clickable)
						{
							if (mouseState.LeftButton == ButtonState.Pressed && leftOld == ButtonState.Released)
							{
								OnLeftClicked();
							}
							else if (mouseState.RightButton == ButtonState.Pressed && rightOld == ButtonState.Released)
							{
								OnRightClicked();
							}
							leftHeld = mouseState.LeftButton == ButtonState.Pressed;
							rightHeld = mouseState.RightButton == ButtonState.Pressed; 
						}
                    }
                    else
                    {
						if (hovered) OnExited();
                    }
                    break;

                case ButtonType.Circle:
                    Circle = new CircleF(center, diameter / 2);
					double distance = Math.Pow(mouseState.X - Circle.Center.X, 2) + Math.Pow(mouseState.Y - Circle.Center.Y, 2);
                    if (/*Circle.Contains(new Vector2(mouseState.X, mouseState.Y))*/ distance <= Math.Pow(Circle.Radius, 2))
                    {
						if (!hovered) OnEntered();
						if (clickable)
						{
							if (mouseState.LeftButton == ButtonState.Pressed && leftOld == ButtonState.Released)
							{
								OnLeftClicked();
							}
							else if (mouseState.RightButton == ButtonState.Pressed && rightOld == ButtonState.Released)
							{
								OnRightClicked();
							}
							leftHeld = mouseState.LeftButton == ButtonState.Pressed;
							rightHeld = mouseState.RightButton == ButtonState.Pressed; 
						}
                    }
                    else
                    {
						if (hovered) OnExited();
					}
                    break;

                case ButtonType.Ellipse:
                    Ellipse = new Ellipse(button1.Width, button1.Height, center);
                    if (Ellipse.Contains(mouseState.X, mouseState.Y))
                    {
						if (!hovered) OnEntered();
						if (clickable)
						{
							if (mouseState.LeftButton == ButtonState.Pressed && leftOld == ButtonState.Released)
							{
								OnLeftClicked();
							}
							else if (mouseState.RightButton == ButtonState.Pressed && rightOld == ButtonState.Released)
							{
								OnRightClicked();
							}
							leftHeld = mouseState.LeftButton == ButtonState.Pressed;
							rightHeld = mouseState.RightButton == ButtonState.Pressed; 
						}
                    }
                    else
                    {
						if (hovered) OnExited();
                    }
                    break;

                default:
                    break;
            }

			leftOld = mouseState.LeftButton;
			rightOld = mouseState.RightButton;
		}

        private void OnLeftClicked()
        {
            leftClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnRightClicked()
        {
            rightClicked?.Invoke(this, EventArgs.Empty);
        }

		private void OnEntered()
		{
			button0 = button2;
			if (Clickable) DisplayColor = hoverColor;
			hovered = true;
			entered?.Invoke(this, EventArgs.Empty);
		}

		private void OnExited()
		{
			button0 = button1;
			DisplayColor = Color.White;
			hovered = false;
			leftHeld = false;
			rightHeld = false;
			exited?.Invoke(this, EventArgs.Empty);
		}
    }
}