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
        private Random random = new Random();
        private Manager<Enemy> enemyManager;
        private EnemyFactory enemyFactory;
        private Camera camera;
        private string enemyName;
        public Spawner(Manager<Enemy> enemyManager, Camera camera, EnemyFactory enemyFactory, int cooldown, string enemyName)
        {
            this.enemyManager = enemyManager;
            this.camera = camera;
            this.enemyFactory = enemyFactory;
            this.cooldown = cooldown;
            this.enemyName = enemyName;
        }

        public void Spawn()
        {
            if (cooldownTimer == 0)
            {
                int xOffset = (int)camera.X;
                int yOffset = (int)camera.Y;
                x = random.Next(-500 + xOffset, 1920 + xOffset);
                y = random.Next(-200 + yOffset, 0 + yOffset);
                int choice = (random.Next(0, 4));
                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        y += 1400;
                        break;
                    case 2:
                        int temp = x;
                        x = y;
                        y = temp;
                        break;
                    case 3:
                        int temp2 = x;
                        x = y;
                        y = temp2;
                        x += 2400;
                        break;

                }
                if (random.Next(0,2) == 0)
                {
                    y += 1400;
                }
                enemyManager.Add(enemyFactory.GetEnemy(enemyName, x, y));
                cooldownTimer = cooldown;
            }
            else
            {
                cooldownTimer--;
            }
        }
    }
}
