using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MG2
{
    public class Projectile : GameObject, ICollidable
    {
        private float speed = 1000F;
        private Texture2D texture;
        private Vector2 direction;
        public bool remove = false;

        public Projectile(float x, float y, Texture2D texture, Vector2 direction)
        {
            X = x;
            Y = y;
            this.hitbox = new Rectangle((int)X, (int)Y, 10, 10);
            this.texture = texture;
            this.direction = direction;
        }

        public override void Update(float deltaTime)
        {
            X += direction.X * speed * deltaTime;
            Y += direction.Y * speed * deltaTime;
            hitbox.X = (int)X;
            hitbox.Y = (int)Y;
            if(Math.Abs(X) > 1920 || Math.Abs(Y) > 1200)
            {
                remove = true;
            }
        }
        public override void Draw(BetterRender betterRender)
        {
            betterRender.RenderRelativeToCamera(texture, X, Y, 10, 10, Color.Yellow);
        }
        public Rectangle GetHitbox()
        {
            return this.hitbox;
        }
        public void OnCollision()
        {
            remove = true;
        }
        public bool ToBeRemoved()
        {
            return remove;
        }
    }
}
