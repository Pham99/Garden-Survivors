using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Gun
    {
        Manager<Projectile> projectileManager;
        private float fireRate;
        private float fireRateTimer;
        public Gun(Manager<Projectile> projectileManager, float fireRate)
        {
            this.projectileManager = projectileManager;
            this.fireRate = fireRate;
        }
        public void Shoot(float x, float y, Vector2 direction)
        {
            if (fireRateTimer <= 0)
            {
                projectileManager.Add(new Projectile(x, y, direction));
                fireRateTimer = fireRate;
            }
        }
        public void Update(float deltaTime)
        {
            fireRateTimer -= deltaTime;
        }
    }
}
