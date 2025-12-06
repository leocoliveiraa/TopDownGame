using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Entities;
using TopDownGame.Graphics;
using TopDownGame.Input;
using TopDownGame.Maps;
using System.Collections.Generic;

namespace TopDownGame.Scenes
{
    public class GameScene : Scene
    {
        private ContentManager _content;
        private GraphicsDevice _graphicsDevice;
        
        private Player _player;
        private List<Enemy> _enemies;
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

            // Carrega as texturas de animação do Soldier
            Texture2D idleTexture = _content.Load<Texture2D>("Sprites/Soldier-Idle");
            Texture2D walkTexture = _content.Load<Texture2D>("Sprites/Soldier-Walk");

            // Cria as animações
            // Idle: 600x100 = 6 frames de 100x100
            // Walk: 800x100 = 8 frames de 100x100
            var animations = new Dictionary<string, Animation>
            {
                { "idle", new Animation(idleTexture, 6, 0.12f, true) },
                { "walk", new Animation(walkTexture, 8, 0.08f, true) }
            };

            _player = new Player(animations, new Vector2(100, 100));

            // Carrega animação do Skeleton
            Texture2D skeletonIdleTexture = _content.Load<Texture2D>("Sprites/Skeleton Idle");
            
            // Skeleton Idle: 264x32, testando com 11 frames de 24x32 pixels cada
            var skeletonAnimations = new Dictionary<string, Animation>
            {
                { "idle", new Animation(skeletonIdleTexture, 11, 24, 32, 0.12f, true) }
            };

            // Cria lista de inimigos com um esqueleto
            _enemies = new List<Enemy>
            {
                new Enemy(skeletonAnimations, new Vector2(400, 200), 2f)
            };

            _camera = new Camera2D(_graphicsDevice.Viewport);
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            _player.HandleInput(input, gameTime, _tilemap);
            _player.Update(gameTime);
            
            // Atualiza inimigos
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
            
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
            
            // Desenha inimigos
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }

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