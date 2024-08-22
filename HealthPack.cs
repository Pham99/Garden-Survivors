using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class HealthPack : Collectable
    {
        public HealthPack(float x, float y) : base(x, y)
        {
            texture = Assets.Textures["HealthPack"];
            hitbox.Width = 32;
            hitbox.Height = 32;
        }
    }
}
