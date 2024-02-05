using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public abstract class GameObject
    {
        public Rectangle hitbox;
        public float X { get; set; }
        public float Y { get; set; }

        public abstract void Update(float deltaTime);
        public abstract void Draw(BetterRender betterRender);
        public abstract bool ToBeRemoved();
    }
}
