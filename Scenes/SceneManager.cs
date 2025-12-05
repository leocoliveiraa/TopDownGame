using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Input;

namespace TopDownGame.Scenes
{
    public class SceneManager
    {
        private Scene _currentScene;

        public void LoadScene(Scene newScene)
        {
            _currentScene?.UnloadContent();
            _currentScene = newScene;
            _currentScene.LoadContent();
        }

        public void Update(GameTime gameTime, InputManager input)
        {
            _currentScene?.Update(gameTime, input);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScene?.Draw(spriteBatch);
        }
    }
}