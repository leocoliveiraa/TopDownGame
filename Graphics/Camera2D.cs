using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownGame.Graphics
{
    public class Camera2D
    {
        public Vector2 Position { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        private readonly Viewport _viewport;

        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;
            Zoom = 1f;
            Rotation = 0f;
            Position = Vector2.Zero;
        }

        public Matrix GetTransformMatrix()
        {
            // Sem transformação por enquanto - câmera fixa
            return Matrix.Identity;
        }

        public void Follow(Vector2 targetPosition)
        {
            Position = targetPosition;
        }
    }
}