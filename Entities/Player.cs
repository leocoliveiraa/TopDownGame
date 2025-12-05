using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Input;
using TopDownGame.Utils;
using TopDownGame.Maps;

namespace TopDownGame.Entities
{
    public class Player : Entity
    {
        private Texture2D _texture;
        private float _maxSpeed = Constants.PLAYER_SPEED;
        private float _acceleration = 800f;
        private float _friction = 600f;
        private Vector2 _velocity;
        private Vector2 _direction;

        public Player(Texture2D texture, Vector2 startPosition)
        {
            _texture = texture;
            Position = startPosition;
            _velocity = Vector2.Zero;
            UpdateBounds(Constants.PLAYER_SIZE, Constants.PLAYER_SIZE);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void HandleInput(InputManager input, GameTime gameTime, Tilemap tilemap)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 inputDirection = input.GetMovementInput();
            
            if (inputDirection != Vector2.Zero)
            {
                _direction = inputDirection;
                
                // Acelera na direção do input
                _velocity += inputDirection * _acceleration * deltaTime;
                
                // Limita a velocidade máxima
                if (_velocity.Length() > _maxSpeed)
                {
                    _velocity.Normalize();
                    _velocity *= _maxSpeed;
                }
            }
            else
            {
                // Aplica fricção quando não há input
                if (_velocity.Length() > 0)
                {
                    float frictionAmount = _friction * deltaTime;
                    if (_velocity.Length() <= frictionAmount)
                    {
                        _velocity = Vector2.Zero;
                    }
                    else
                    {
                        Vector2 frictionDir = _velocity;
                        frictionDir.Normalize();
                        _velocity -= frictionDir * frictionAmount;
                    }
                }
            }

            // Aplica movimento se houver velocidade
            if (_velocity != Vector2.Zero)
            {
                Vector2 desiredPosition = Position + _velocity * deltaTime;
                
                if (!tilemap.CheckCollision(desiredPosition, Bounds.Width, Bounds.Height))
                {
                    Position = desiredPosition;
                    UpdateBounds(Constants.PLAYER_SIZE, Constants.PLAYER_SIZE);
                }
                else
                {
                    _velocity = Vector2.Zero; // Para ao colidir
                }
            }
        }

        // Placeholder para futuras animações
        private void UpdateAnimation()
        {
            // TODO: Implementar animações
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                Position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                Constants.LAYER_ENTITIES
            );
        }
    }
}