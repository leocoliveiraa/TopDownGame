using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TopDownGame.Input
{
    public class InputManager
    {
        private KeyboardState _currentKeyState;
        private KeyboardState _previousKeyState;

        public void Update()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key) => _currentKeyState.IsKeyDown(key);
        public bool IsKeyPressed(Keys key) => _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);

        public Vector2 GetMovementInput()
        {
            Vector2 movement = Vector2.Zero;

            if (IsKeyDown(Keys.W)) movement.Y -= 1;
            if (IsKeyDown(Keys.S)) movement.Y += 1;
            if (IsKeyDown(Keys.A)) movement.X -= 1;
            if (IsKeyDown(Keys.D)) movement.X += 1;

            if (movement != Vector2.Zero)
                movement.Normalize();

            return movement;
        }
    }
}