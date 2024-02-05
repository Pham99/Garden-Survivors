using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public interface ICollidable
    {
        public Rectangle GetHitbox();
        public void OnCollision(ICollidable obj);
    }
}
