using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Input;
using TopDownGame.Utils;
using TopDownGame.Maps;
using TopDownGame.Graphics;
using System.Collections.Generic;

namespace TopDownGame.Entities
{
    public class Player : Entity
    {
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;
        private string _currentAnimationName;
        private float _speed = Constants.PLAYER_SPEED;
        private Vector2 _lastMovement;
        private float _scale = 3f; // Escala para aumentar o tamanho do personagem

        public Player(Dictionary<string, Animation> animations, Vector2 startPosition)
        {
            _animations = animations;
            Position = startPosition;
            
            // Define animação inicial como Idle
            SetAnimation("idle");
            
            // Define o tamanho do player baseado no primeiro frame da animação com escala
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

        public void HandleInput(InputManager input, GameTime gameTime, Tilemap tilemap)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 movement = input.GetMovementInput();
            
            if (movement != Vector2.Zero)
            {
                _lastMovement = movement;
                Vector2 desiredPosition = Position + movement * _speed * deltaTime;
                
                // Tenta mover nos dois eixos
                if (!tilemap.CheckCollision(desiredPosition, Bounds.Width, Bounds.Height))
                {
                    Position = desiredPosition;
                }
                else
                {
                    // Se não conseguir, tenta mover só em X
                    Vector2 moveX = new Vector2(Position.X + movement.X * _speed * deltaTime, Position.Y);
                    if (!tilemap.CheckCollision(moveX, Bounds.Width, Bounds.Height))
                    {
                        Position = moveX;
                    }
                    
                    // Tenta mover só em Y
                    Vector2 moveY = new Vector2(Position.X, Position.Y + movement.Y * _speed * deltaTime);
                    if (!tilemap.CheckCollision(moveY, Bounds.Width, Bounds.Height))
                    {
                        Position = moveY;
                    }
                }
                
                UpdateBounds((int)(_currentAnimation.FrameWidth * _scale), 
                            (int)(_currentAnimation.FrameHeight * _scale));
                SetAnimation("walk");
            }
            else
            {
                SetAnimation("idle");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentAnimation == null) return;

            SpriteEffects flip = SpriteEffects.None;
            
            // Inverte o sprite se estiver movendo para a esquerda
            if (_lastMovement.X < 0)
            {
                flip = SpriteEffects.FlipHorizontally;
            }

            spriteBatch.Draw(
                _currentAnimation.SpriteSheet,
                Position,
                _currentAnimation.GetCurrentFrameRectangle(),
                Color.White,
                0f,
                Vector2.Zero,
                _scale,
                flip,
                Constants.LAYER_ENTITIES
            );
        }
    }
}