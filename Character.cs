using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public abstract class Character : GameObject
    {
        protected int maxHp = 10;
        protected int hp;
        protected int attack = 5;
        protected float speed = 200F;
        protected Vector2 direction = new Vector2(0, 0);
    }
}
