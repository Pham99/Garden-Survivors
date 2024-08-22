using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class ExpOrb : Collectable
    {
        public ExpOrb(float x, float y) : base(x, y)
        {
            texture = Assets.Textures["ExpOrb"];
        }
    }
}
