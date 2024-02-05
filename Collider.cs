using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public static class Collider
    {
        public static void Collide<T1,T2>(Manager<T1> collidable1, Manager<T2> collidable2) where T1 : GameObject, ICollidable where T2 : GameObject, ICollidable
        {
            foreach(var item1 in collidable1.list) 
            {
                foreach( var item2 in collidable2.list)
                {
                    if (item1.GetHitbox().Intersects(item2.GetHitbox()) && item1 != item2)
                    {
                        item1.OnCollision(item2);
                        item2.OnCollision(item1);
                    }
                }
            }
        }
        public static void Collide<T>(Manager<T> collidable1, Player player) where T : GameObject, ICollidable
        {
            foreach (var item1 in collidable1.list)
            {
                if (item1.GetHitbox().Intersects(player.GetHitbox()))
                {
                    item1.OnCollision(player);
                    player.OnCollision(item1);
                }
            }
        }
    }
}
