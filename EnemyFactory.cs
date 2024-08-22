using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class EnemyFactory
    {
        private Manager<Enemy> enemyManager;
        private BarDirector barDirector;
        private Dictionary<string, EnemyFlyWeight> enemyFlyweights = new Dictionary<string, EnemyFlyWeight>();

        public EnemyFactory(Player player, Manager<Collectable> colMan, Manager<Enemy> enemyManager, BarDirector barDirector)
        {
            this.enemyManager = enemyManager;
            this.barDirector = barDirector;
            enemyFlyweights.Add("vicksyInsane",new EnemyFlyWeight(Assets.Textures["EnemySlime"], player, 2, 2, 100, colMan));
            enemyFlyweights.Add("vicksyGa", new EnemyFlyWeight(Assets.Textures["EnemyShroom"], player, 1, 10, 175, colMan));
            enemyFlyweights.Add("vickSUS", new EnemyFlyWeight(Assets.Textures["EnemyFlower"], player, 3, 1, 70, colMan));
        }
        public Enemy Create(string name, int x, int y)
        {
            EnemyFlyWeight flyWeight = enemyFlyweights[name];
            Enemy enemy = new Enemy(flyWeight, x, y);
            Bar healthBar = barDirector.CreateHealthBar();
            healthBar.SubscribeToValueChange(enemy.stats);
            healthBar.SubscribeToPositionChange(enemy);
            enemyManager.Add(enemy);
            return enemy;
        }
    }
}
