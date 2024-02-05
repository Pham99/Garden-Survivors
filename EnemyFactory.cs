using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class EnemyFactory
    {
        private Dictionary<string, EnemyFlyWeight> enemyFlyweights = new Dictionary<string, EnemyFlyWeight>();

        public EnemyFactory(Player player) 
        {
            enemyFlyweights.Add("vicksyInsane",new EnemyFlyWeight(Assets.Textures["vicksyInsane"], player, 10, 2, 150));
            enemyFlyweights.Add("vicksyGa", new EnemyFlyWeight(Assets.Textures["vicksyGa"], player, 1, 1, 300));
            enemyFlyweights.Add("vickSUS", new EnemyFlyWeight(Assets.Textures["vickSUS"], player, 500, 1000, 100));      
        }
        public Enemy GetEnemy(string name, int x, int y)
        {
            return new Enemy(enemyFlyweights[name], x, y);
        }
    }
}
