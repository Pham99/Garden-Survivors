using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MG2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BetterRender betterRenderer;
        private Manager<Enemy> enemyManager;
        private EnemyFactory enemyFactory;
        private Spawner spawner;
        private Spawner spawner2;
        private Spawner spawner3;
        private Camera camera;
        private Player player;
        private RepeatingBackground _background;
        private bool pause = false;

        int frameCount = 0;
        float frameTime = 0.0f;
        float fps = 0.0f;

        int h;
        int w;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1200;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowAltF4 = true;
            IsFixedTimeStep = false;
            Window.AllowUserResizing = true;
            //TargetElapsedTime = TimeSpan.FromMilliseconds(11.111);
        }

        protected override void Initialize()
        {
            base.Initialize();

            player = new Player(Assets.Textures["vicksyLevel"]);
            enemyManager = new Manager<Enemy>();
            enemyFactory = new EnemyFactory(player);
            camera = new Camera(player);
            player.GetCamera(camera);
            spawner = new Spawner(enemyManager, camera, enemyFactory, 4, "vicksyInsane");
            spawner2 = new Spawner(enemyManager, camera, enemyFactory, 8, "vicksyGa");
            spawner3 = new Spawner(enemyManager, camera, enemyFactory, 12, "vickSUS");
            betterRenderer = new BetterRender(_spriteBatch, camera);
            _background = new RepeatingBackground(Assets.Textures["grassBG"], player);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Textures.Add("vicksyLevel", Content.Load<Texture2D>(@"sprites/vicksyLevel"));
            Assets.Textures.Add("vicksyInsane", Content.Load<Texture2D>(@"sprites/vicksyInsaneOG"));
            Assets.Textures.Add("rectangle", Content.Load<Texture2D>(@"sprites/rect"));
            Assets.Textures.Add("grassBG", Content.Load<Texture2D>(@"sprites/grassBG"));
            Assets.Textures.Add("vicksyGa", Content.Load<Texture2D>(@"sprites/vicksyGa"));
            Assets.Textures.Add("vickSUS", Content.Load<Texture2D>(@"sprites/vickSUS"));
            Assets.Fonts.Add("arial", Content.Load<SpriteFont>("Arial"));

            //bullet = new Texture2D(GraphicsDevice, 1, 1);
            //bullet.SetData(new[] { Color.White });
        }
        protected override void Update(GameTime gameTime)
        {
            w = GraphicsDevice.Viewport.Bounds.Width;
            h = GraphicsDevice.Viewport.Bounds.Height;
            //inputs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                pause = !pause;
            }

            if (!pause)
            {
            player.Update(deltaTime);
            camera.Update(w, h);

            spawner.Spawn();
            spawner2.Spawn();
            spawner3.Spawn();
            enemyManager.Update(deltaTime);
            
            Collider.Collide(enemyManager, player.GetProjectileManager());
            Collider.Collide(enemyManager, player);
            }
            
            UpdateFPS(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _background.Draw(betterRenderer);
            player.Draw(betterRenderer);
            enemyManager.Draw(betterRenderer);

            //debug stuff
            _spriteBatch.DrawString(Assets.Fonts["arial"], $"{player.X} {player.Y}", new Vector2(0, 40), Color.Black);
            DrawFPS();
            _spriteBatch.DrawString(Assets.Fonts["arial"], $"{player.Hp}", new Vector2(0, 20), Color.White);
            _spriteBatch.DrawString(Assets.Fonts["arial"], $"{w}, {h}", new Vector2(0, 60), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void UpdateFPS(GameTime gameTime)
        {
            frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCount++;

            // Update FPS every second
            if (frameTime >= 1.0f)
            {
                fps = frameCount / frameTime;
                frameCount = 0;
                frameTime = 0.0f;
            }
        }
        private void DrawFPS()
        {
            // Draw FPS in the top-left corner
            _spriteBatch.DrawString(Content.Load<SpriteFont>("Arial"), $"FPS: {fps}", new Vector2(10, 10), Color.White);
        }
    }
}