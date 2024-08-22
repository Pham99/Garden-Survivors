using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Manager<T> where T : GameObject
    {
        public List<T> list = new List<T>();
        public Manager() { }
        public void Add(T item)
        {
            list.Add(item);
        }
        public void Update(float deltaTime)
        {
            foreach (var obj in list)
            {
                obj.Update(deltaTime);
            }
            list.RemoveAll(t => t.ToBeRemoved() == true);
        }
        public void Draw(BetterRender betterRender)
        {
            foreach (var obj in list)
            {
                obj.Draw(betterRender);
            }
        }
        public void DrawHitbox(BetterRender betterRender)
        {
            foreach (var obj in list)
            {
                obj.DrawHitbox(betterRender);
            }
        }
    }
}
