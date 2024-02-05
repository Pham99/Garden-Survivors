using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MG2
{
    public class Projectile : GameObject, ICollidable
    {
        private Texture2D texture;
        private Vector2 direction;
        private int attack = 1;
        private float speed = 1000F;
        private int sizeX = 10;
        private int sizeY = 10;
        private Color color = Color.Yellow;
        private float timeToLive = 5;
        private bool remove = false;

        public Projectile(float x, float y, Texture2D texture, Vector2 direction)
        {
            X = x;
            Y = y;
            hitbox = new Rectangle((int)X, (int)Y, sizeX, sizeY);
            this.texture = texture;
            this.direction = direction;
        }

        public override void Update(float deltaTime)
        {
            X += direction.X * speed * deltaTime;
            Y += direction.Y * speed * deltaTime;
            hitbox.X = (int)X;
            hitbox.Y = (int)Y;
            timeToLive -= deltaTime;
            if(timeToLive < 0)
            {
                remove = true;
            }
        }
        public int Attack {  get { return attack; } }
        public override void Draw(BetterRender betterRender)
        {
            betterRender.RenderRelativeToCamera(texture, X, Y, sizeX, sizeY, color);
        }
        public Rectangle GetHitbox()
        {
            return hitbox;
        }
        public void OnCollision(ICollidable obj)
        {
            remove = true;
        }
        public override bool ToBeRemoved()
        {
            return remove;
        }
    }
}
