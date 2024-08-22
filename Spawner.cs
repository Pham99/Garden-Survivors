using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Spawner
    {
        private int x;
        private int y;
        private int cooldownTimer = 0;
        private int cooldown;
        private int thickness = 300;
        private int padding = 100;
        private Random random = new Random();
        private EnemyFactory enemyFactory;
        private Camera camera;
        private string enemyName;
        public Spawner(Camera camera, EnemyFactory enemyFactory, int cooldown, string enemyName, GameMananger gameMananger)
        {
            this.camera = camera;
            this.enemyFactory = enemyFactory;
            this.cooldown = cooldown;
            this.enemyName = enemyName;
            gameMananger.LevelUp += OnLevelUp;
        }

        public void Spawn()
        {
            if (cooldownTimer == 0)
            {
                int xOffset = (int)camera.X;
                int yOffset = (int)camera.Y;
                if (random.Next(2) == 0)
                {
                    //vertical spawn area
                    x = random.Next(xOffset - thickness - padding, xOffset - padding);
                    y = random.Next(yOffset - thickness, yOffset + camera.height + thickness);
                    if (random.Next(2) == 0)
                    {
                        x += thickness + camera.width + padding;
                    }
                }
                else
                {
                    //horizontal spawn area
                    x = random.Next(xOffset, xOffset + camera.width);
                    y = random.Next(yOffset - thickness - padding, yOffset - padding);
                    if (random.Next(2) == 0)
                    {
                        y += thickness + camera.height + padding;
                    }
                }

                enemyFactory.Create(enemyName, x, y);
                cooldownTimer = cooldown;
            }
            else
            {
                cooldownTimer--;
            }
        }
        public void OnLevelUp(int level)
        {
            cooldown = cooldown - (int)Math.Log2(level * 10);
        }
    }
}
