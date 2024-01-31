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
        private int cooldown = 0;
        private Random random = new Random();
        private Manager<Enemy> enemyManager;
        private Player player;
        private Texture2D texture;
        private Texture2D texture2;
        private Camera camera;
        public Spawner(Manager<Enemy> enemyManager, Player player, Texture2D texture, Texture2D texture2, Camera camera)
        {
            this.enemyManager = enemyManager;
            this.player = player;
            this.texture = texture;
            this.texture2 = texture2;
            this.camera = camera;
        }

        public void Spawn()
        {
            if (cooldown == 0)
            {
                int xOffset = (int)camera.X;
                int yOffset = (int)camera.Y;
                x = random.Next(0 + xOffset, 1920 + xOffset);
                y = random.Next(-200 + yOffset, 0 + yOffset);
                if (random.Next(0,2) == 0)
                {
                    y += 1400;
                }
                enemyManager.Spawn(new Enemy(player, texture, texture2, x, y));
                cooldown = 120;
            }
            else
            {
                cooldown--;
            }
        }
    }
}
