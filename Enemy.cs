using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Enemy : GameObject, ICollidable
    {
        private EnemyFlyWeight flyWeight;
        public Stats stats;
        public bool remove = false;
        public Enemy(EnemyFlyWeight flyWeight, int x, int y)
        {
            X = x;
            Y = y;
            this.flyWeight = flyWeight;
            stats = new Stats(flyWeight.maxHp);
            hitbox.Width = flyWeight.sizeX;
            hitbox.Height = flyWeight.sizeY;
        }

        public override void Update(float deltaTime)
        {
            flyWeight.Update(deltaTime, this);
        }
        public override void Draw(BetterRender betterRender)
        {
            flyWeight.Draw(betterRender, this);
        }
        public void OnCollision(ICollidable obj)
        {
            flyWeight.OnCollision(obj, this);
        }
        public override bool ToBeRemoved()
        {
            flyWeight.ToBeRemoved(this);
            return remove;

        }
        public Rectangle GetHitbox()
        {
            return hitbox;
        }
    }
}
