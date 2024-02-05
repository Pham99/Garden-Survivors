using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Camera : GameObject
    {
        private GameObject target;
        public Camera(GameObject target)
        {
            X = 0; Y = 0;
            this.target = target;
        }
        public override void Draw(BetterRender betterRender)
        {
            throw new NotImplementedException();
        }
        public void Update(int width, int height)
        {
            X = target.X - (width / 2) + (target.hitbox.Width / 2);
            Y = target.Y - (height / 2) + (target.hitbox.Height / 2);
        }
        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
        public override bool ToBeRemoved()
        {
            throw new NotImplementedException();
        }
    }
}
