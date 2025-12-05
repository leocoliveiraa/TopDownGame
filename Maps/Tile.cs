using Microsoft.Xna.Framework;

namespace TopDownGame.Maps
{
    public class Tile
    {
        public int Id { get; set; }
        public bool IsSolid { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public Tile(int id, bool isSolid, Rectangle sourceRect)
        {
            Id = id;
            IsSolid = isSolid;
            SourceRectangle = sourceRect;
        }
    }
}