using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Enemy : Character, ICollidable
    {
        private Texture2D texture;
        private Player target;
        private bool remove = false;
        private HealthBar healthBar;
        public Enemy(Player target, Texture2D texture, Texture2D texture2, int x, int y)
        {
            this.maxHp = 5;
            this.hp = maxHp;
            this.texture = texture;
            this.target = target;
            X = x;
            Y = y;
            speed = 150F;
            hitbox = new Rectangle((int)X, (int)Y, 100, 100);
            healthBar = new HealthBar(texture2, maxHp, hp, this);
        }
        public override void Update(float deltaTime)
        {
            direction = new Vector2(X - target.X, Y - target.Y);
            direction.Normalize();

            X += speed * -direction.X * deltaTime;
            Y += speed * -direction.Y * deltaTime;
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);

            healthBar.Update(hp, maxHp);

            if (hp <= 0)
            {
                remove = true;
            }
        }
        public bool ToBeRemoved()
        {
            return remove;
        }
        public void OnCollision()
        {
            hp -= 1;
        }

        public override void Draw(BetterRender betterRender)
        {
            betterRender.RenderRelativeToCamera(texture, X, Y, hitbox.Width, hitbox.Height);
            healthBar.Draw(betterRender);
        }

        public Rectangle GetHitbox()
        {
            return hitbox;
        }
    }
}
