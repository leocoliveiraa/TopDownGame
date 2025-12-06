using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Graphics;
using TopDownGame.Utils;
using System.Collections.Generic;

namespace TopDownGame.Entities
{
    public class Enemy : Entity
    {
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;
        private string _currentAnimationName;
        private float _scale;

        public Enemy(Dictionary<string, Animation> animations, Vector2 startPosition, float scale = 3f)
        {
            _animations = animations;
            Position = startPosition;
            _scale = scale;
            
            // Define animação inicial como Idle
            SetAnimation("idle");
            
            // Define o tamanho baseado no primeiro frame da animação com escala
            if (_currentAnimation != null)
            {
                UpdateBounds((int)(_currentAnimation.FrameWidth * _scale), 
                            (int)(_currentAnimation.FrameHeight * _scale));
            }
        }

        private void SetAnimation(string animationName)
        {
            if (_animations.ContainsKey(animationName) && _currentAnimationName != animationName)
            {
                _currentAnimationName = animationName;
                _currentAnimation = _animations[animationName];
                _currentAnimation.Reset();
            }
        }

        public override void Update(GameTime gameTime)
        {
            _currentAnimation?.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentAnimation == null) return;

            Rectangle sourceRect = _currentAnimation.GetCurrentFrameRectangle();
            
            spriteBatch.Draw(
                _currentAnimation.SpriteSheet,
                Position,
                sourceRect,  // Usando explicitamente o sourceRect
                Color.White,
                0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                Constants.LAYER_ENTITIES
            );
        }
    }
}
