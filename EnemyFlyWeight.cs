using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class EnemyFlyWeight
    {
        public Texture2D texture {  get; init; }
        private Player target;
        private Random random = new Random();
        public int maxHp { get; init; }
        public int attack { get; private set; }
        public int speed { get; private set; }
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        public Manager<Collectable> collectable { get; private set; }
        public EnemyFlyWeight(Texture2D texture, Player target, int maxHp, int attack, int speed, Manager<Collectable> collectable, int sizeX = 100, int sizeY = 100)
        {
            this.texture = texture;
            this.target = target;
            this.maxHp = maxHp;
            this.attack = attack;
            this.speed = speed;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.collectable = collectable;
        }
        public void Update(float deltaTime, Enemy unique)
        {
            Vector2 direction = new Vector2(unique.X - target.X, unique.Y - target.Y);
            direction.Normalize();

            unique.X += speed * -direction.X * deltaTime;
            unique.Y += speed * -direction.Y * deltaTime;
            unique.hitbox.X = (int)unique.X;
            unique.hitbox.Y = (int)unique.Y;

            if (unique.stats.Value <= 0)
            {
                unique.remove = true;
            }
        }
        public void OnCollision(ICollidable obj, Enemy unique)
        {
            if (obj is Projectile proj)
            {
                unique.stats.Value -= proj.Attack;
            }
        }
        public void ToBeRemoved(Enemy unique)
        {
            if (unique.remove)
            {
                if(random.Next(0, 100) < 10)
                {
                    collectable.Add(new HealthPack(unique.hitbox.Center.X, unique.hitbox.Center.Y));
                }
                else
                {
                    collectable.Add(new ExpOrb(unique.hitbox.Center.X, unique.hitbox.Center.Y));
                }
            }

        }
        public void Draw(BetterRender betterRender, Enemy unique)
        {
            betterRender.RenderRelativeToCamera(texture, unique.X, unique.Y, unique.hitbox.Width, unique.hitbox.Height);
        }
    }
}
