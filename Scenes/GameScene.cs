using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Entities;
using TopDownGame.Graphics;
using TopDownGame.Input;
using TopDownGame.Maps;

namespace TopDownGame.Scenes
{
    public class GameScene : Scene
    {
        private ContentManager _content;
        private GraphicsDevice _graphicsDevice;
        
        private Player _player;
        private Tilemap _tilemap;
        private Camera2D _camera;

        public GameScene(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
        }

        public override void LoadContent()
        {
            _tilemap = new Tilemap(_content, _graphicsDevice);

            Texture2D playerTexture = CreatePlaceholderTexture(32, 32, Color.Red);
            _player = new Player(playerTexture, new Vector2(0, 0));

            _camera = new Camera2D(_graphicsDevice.Viewport);
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            _player.HandleInput(input, gameTime, _tilemap);
            _player.Update(gameTime);
            
            // Câmera fixa por enquanto para ver o player se movendo
            // _camera.Follow(_player.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                _camera.GetTransformMatrix()
            );

            // _tilemap.Draw(spriteBatch); // Desativado por enquanto
            _player.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void UnloadContent()
        {
        }

        private Texture2D CreatePlaceholderTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(_graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            texture.SetData(data);
            return texture;
        }
    }
}