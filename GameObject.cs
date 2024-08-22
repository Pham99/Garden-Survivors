using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MG2
{
    public abstract class GameObject
    {
        protected Texture2D texture;
        private Texture2D hitboxTexture = Assets.Textures["rectangle"];
        private bool remove = false;
        public event Action<float, float> PositionChanged;
        public Rectangle hitbox;
        private float x;
        private float y;
        public virtual float X
        {
            get => x;
            set
            {
                x = value;
                PositionChanged?.Invoke(X, Y);
            }
        }
        public virtual float Y
        {
            get => y;
            set
            {
                y = value;
                PositionChanged?.Invoke(X, Y);
            }
        }

        public virtual void Update(float deltaTime) { }
        public virtual void Draw(BetterRender betterRender) { }
        public virtual bool ToBeRemoved() { return remove; }
        public void DrawHitbox(BetterRender betterRender)
        {
            betterRender.RenderRelativeToCamera(hitboxTexture, hitbox.X, hitbox.Y, hitbox.Width, hitbox.Height, new Color(Color.Red, 0.5f));
        }
    }
}
