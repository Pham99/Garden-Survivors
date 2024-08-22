using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Collectable : GameObject, ICollidable
    {
        Color color = Color.White;
        bool remove = false;

        public Collectable(float x, float y)
        {
            texture = Assets.Textures["rectangle"];
            this.X = x;
            this.Y = y;
            hitbox.X = (int)this.X;
            hitbox.Y = (int)this.Y;
            hitbox.Width = 16;
            hitbox.Height = 16;
        }
        public override void Draw(BetterRender betterRender)
        {
            betterRender.RenderRelativeToCamera(texture, X, Y, hitbox.Width, hitbox.Height, color);
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

        public override void Update(float deltaTime)
        {
            
        }
    }
}
