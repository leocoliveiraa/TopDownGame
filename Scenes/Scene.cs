using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Input;

namespace TopDownGame.Scenes
{
    public abstract class Scene
    {
        public abstract void Update(GameTime gameTime, InputManager input);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void LoadContent();
        public abstract void UnloadContent();
    }
}