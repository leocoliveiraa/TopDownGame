using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TopDownGame.Utils;

namespace TopDownGame.Maps
{
    public class Tilemap
    {
        private int[,] _tiles;
        private Tile[] _tileSet;
        private Texture2D _tilesetTexture;
        
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Tilemap(ContentManager content, GraphicsDevice graphicsDevice)
        {
            LoadTileset(graphicsDevice);
            LoadMap();
        }

        private void LoadTileset(GraphicsDevice graphicsDevice)
        {
            // Cria textura placeholder do tileset
            _tilesetTexture = new Texture2D(graphicsDevice, 48, 16);
            Color[] data = new Color[48 * 16];
            
            // Tile 0: Verde (grama)
            for (int i = 0; i < 16 * 16; i++)
                data[i] = new Color(34, 139, 34);
            
            // Tile 1: Cinza (parede)
            for (int i = 16 * 16; i < 32 * 16; i++)
                data[i] = new Color(128, 128, 128);
            
            // Tile 2: Azul (água)
            for (int i = 32 * 16; i < 48 * 16; i++)
                data[i] = new Color(30, 144, 255);
            
            _tilesetTexture.SetData(data);

            _tileSet = new Tile[]
            {
                new Tile(0, false, new Rectangle(0, 0, 16, 16)),
                new Tile(1, true, new Rectangle(16, 0, 16, 16)),
                new Tile(2, false, new Rectangle(32, 0, 16, 16)),
            };
        }

        private void LoadMap()
        {
            Width = 20;
            Height = 15;
            _tiles = new int[Width, Height];

            // Preenche todo o mapa com grama (sem blocos)
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _tiles[x, y] = 0; // Apenas grama
                }
            }
        }

        public bool CheckCollision(Vector2 position, int width, int height)
        {
            int left = (int)position.X / Constants.TILE_SIZE;
            int right = (int)(position.X + width - 1) / Constants.TILE_SIZE;
            int top = (int)position.Y / Constants.TILE_SIZE;
            int bottom = (int)(position.Y + height - 1) / Constants.TILE_SIZE;

            if (left < 0 || right >= Width || top < 0 || bottom >= Height)
                return true;

            return _tileSet[_tiles[left, top]].IsSolid ||
                   _tileSet[_tiles[right, top]].IsSolid ||
                   _tileSet[_tiles[left, bottom]].IsSolid ||
                   _tileSet[_tiles[right, bottom]].IsSolid;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int tileId = _tiles[x, y];
                    Tile tile = _tileSet[tileId];

                    spriteBatch.Draw(
                        _tilesetTexture,
                        new Vector2(x * Constants.TILE_SIZE, y * Constants.TILE_SIZE),
                        tile.SourceRectangle,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        Constants.LAYER_TILES
                    );
                }
            }
        }
    }
}