using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Level
    {
        private Player player;
        private Manager<Enemy> enemyManager;
        private Manager<Collectable> collectableManager;
        private EnemyFactory enemyFactory;
        private Manager<Bar> healthBarManager2;
        private Manager<Bar> healthBarManager;
        private Manager<Projectile> projectileManager;
        private Spawner spawner;
        private Spawner spawner2;
        private Spawner spawner3;
        private Camera camera;
        private RepeatingBackground background;
        private GameMananger gameManager;
        private HUD Hud;
        private Gun gun;

        public Level(Player player, Camera camera, ResolutionPublisher resolutionPublisher)
        {
            gameManager = new GameMananger();
            this.player = player;
            player.GetGameManager(gameManager);
            this.camera = camera;

            healthBarManager = new Manager<Bar>();
            healthBarManager2 = new Manager<Bar>();
            enemyManager = new Manager<Enemy>();
            collectableManager = new Manager<Collectable>();
            projectileManager = new Manager<Projectile>();
            

            Bar.BarBuilder barBuilder = new Bar.BarBuilder();
            BarDirector barDirector = new BarDirector(barBuilder, healthBarManager2, resolutionPublisher);
            Bar expbar = barDirector.CreateExpBar();
            expbar.SubscribeToValueChange(gameManager.Exp);
            barDirector.BarManager = healthBarManager;
            Bar playerHPBar = barDirector.CreateHealthBar();
            playerHPBar.SubscribeToValueChange(player.hp);
            playerHPBar.SubscribeToPositionChange(player);
            Hud = new HUD(gameManager);
            gun = new Gun(projectileManager, 0.3f);
            player.GetGun(gun);

            enemyFactory = new EnemyFactory(player, collectableManager, enemyManager, barDirector);
            spawner = new Spawner(camera, enemyFactory, 90, "vicksyInsane", gameManager);
            spawner2 = new Spawner(camera, enemyFactory, 90, "vicksyGa", gameManager);
            spawner3 = new Spawner(camera, enemyFactory, 90, "vickSUS", gameManager);
            background = new RepeatingBackground(Assets.Textures["grassBG"], player);

        }
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(deltaTime);
            camera.Update();

            spawner.Spawn();
            spawner2.Spawn();
            spawner3.Spawn();

            enemyManager.Update(deltaTime);
            collectableManager.Update(deltaTime);
            gun.Update(deltaTime);
            projectileManager.Update(deltaTime);

            Collider.Collide(enemyManager, projectileManager);
            Collider.Collide(enemyManager, player);
            Collider.Collide(collectableManager, player);
        }
        public void Draw(BetterRender betterRenderer)
        {
            background.Draw(betterRenderer);
            player.Draw(betterRenderer);
            enemyManager.Draw(betterRenderer);
            projectileManager.Draw(betterRenderer);
            collectableManager.Draw(betterRenderer);
            healthBarManager.Draw(betterRenderer);
            healthBarManager2.Draw(betterRenderer);
            Hud.Draw(betterRenderer);

        }
        public void DrawDebug(BetterRender betterRenderer)
        {
            player.DrawHitbox(betterRenderer);
            enemyManager.DrawHitbox(betterRenderer);
            collectableManager.DrawHitbox(betterRenderer);
            projectileManager.DrawHitbox(betterRenderer);
        }
    }
}
