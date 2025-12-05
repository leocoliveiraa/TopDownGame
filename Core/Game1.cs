using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Scenes;
using TopDownGame.Input;

namespace TopDownGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneManager _sceneManager;
        private InputManager _inputManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _inputManager = new InputManager();
            _sceneManager = new SceneManager();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var gameScene = new GameScene(Content, GraphicsDevice);
            _sceneManager.LoadScene(gameScene);
        }

        protected override void Update(GameTime gameTime)
        {
            _inputManager.Update();
            _sceneManager.Update(gameTime, _inputManager);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _sceneManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}