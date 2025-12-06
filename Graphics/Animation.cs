using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownGame.Graphics
{
    public class Animation
    {
        public Texture2D SpriteSheet { get; private set; }
        public int FrameCount { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public float FrameTime { get; private set; }
        public bool IsLooping { get; set; }

        private float _timer;
        private int _currentFrame;

        public Animation(Texture2D spriteSheet, int frameCount, float frameTime, bool isLooping = true)
        {
            SpriteSheet = spriteSheet;
            FrameCount = frameCount;
            FrameTime = frameTime;
            IsLooping = isLooping;
            
            FrameWidth = spriteSheet.Width / frameCount;
            FrameHeight = spriteSheet.Height;
            
            _timer = 0f;
            _currentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= FrameTime)
            {
                _timer = 0f;
                _currentFrame++;

                if (_currentFrame >= FrameCount)
                {
                    if (IsLooping)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        _currentFrame = FrameCount - 1;
                    }
                }
            }
        }

        public void Reset()
        {
            _currentFrame = 0;
            _timer = 0f;
        }

        public Rectangle GetCurrentFrameRectangle()
        {
            return new Rectangle(
                _currentFrame * FrameWidth,
                0,
                FrameWidth,
                FrameHeight
            );
        }

        public bool IsFinished()
        {
            return !IsLooping && _currentFrame == FrameCount - 1;
        }
    }
}
