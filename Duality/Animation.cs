using SharpDX.Toolkit.Graphics;

namespace Duality.Graphics
{
    /// <summary>
    /// Represents an animated texture.
    /// </summary>
    /// <remarks>
    /// Currently, this class assumes that each frame of animation is
    /// as wide as each animation is tall. The number of frames in the
    /// animation are inferred from this.
    /// </remarks>
    public class Animation
    {
        /// <summary>
        /// All frames in the animation arranged horizontally.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;

        /// <summary>
        /// Duration of time to show each frame.
        /// </summary>
        public float FrameTime
        {
            get { return frameTime; }
        }
        float frameTime;

        /// <summary>
        /// When the end of the animation is reached, should it
        /// continue playing from the beginning?
        /// </summary>
        public bool IsLooping
        {
            get { return isLooping; }
        }
        bool isLooping;

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public int FrameCount
        {
            get
			{
				int tempCount = Texture.Width / FrameWidth;
				if (tempCount == 0) tempCount++;
				return tempCount;
			}
        }

        /// <summary>
        /// Gets the width of a frame in the animation.
        /// </summary>
        public int FrameWidth
        {
            get { return frameWidth; }
			set { frameWidth = value; }
        }
        int frameWidth;

        /// <summary>
        /// Gets the height of a frame in the animation.
        /// </summary>
        public int FrameHeight
        {
            get { return frameHeight; }
			set { frameHeight = value; }
        }
		int frameHeight;

        /// <summary>
        /// Constructs a new animation.
        /// </summary>        
        public Animation(Texture2D texture, float frameTime, bool isLooping, int frameWidth)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
            this.frameWidth = frameWidth;
			frameHeight = texture.Height;
        }
    }
}
