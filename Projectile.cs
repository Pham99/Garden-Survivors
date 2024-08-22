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
        public int Attack {  get { return attack; } }

        public Projectile(float x, float y, Vector2 direction)
        {
            X = x - sizeX/2;
            Y = y - sizeY/2;
            hitbox = new Rectangle((int)X, (int)Y, sizeX, sizeY);
            this.texture = Assets.Textures["Bullet"];
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
        public override void Draw(BetterRender betterRender)
        {
            //betterRender.RenderRelativeToCamera(texture, X, Y, hitbox.Width, hitbox.Height, color);
            betterRender.RenderWithRotationRelativeToCamera(texture, X + 5, Y + 5, null, (float)Math.Atan2(direction.Y, direction.X), new Vector2(8, 8), Vector2.One);
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
