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

        private Camera camera;
        private Player player;
        private ResolutionPublisher resolutionPublisher;
        private Level level;

        private bool paused = false;
        private bool debugModeOn = false;

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

            resolutionPublisher = new ResolutionPublisher();
            player = new Player();
            camera = new Camera(player, resolutionPublisher, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height);
            player.GetCamera(camera);

            level = new Level(player, camera, resolutionPublisher);

            betterRenderer = new BetterRender(_spriteBatch, camera);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Textures.Add("rectangle", Content.Load<Texture2D>(@"sprites/rect"));
            Assets.Textures.Add("grassBG", Content.Load<Texture2D>(@"sprites/grassBG"));
            Assets.Textures.Add("ExpOrb", Content.Load<Texture2D>(@"sprites/ExpOrb"));
            Assets.Textures.Add("HealthPack", Content.Load<Texture2D>(@"sprites/HealthPack"));
            Assets.Textures.Add("Frame", Content.Load<Texture2D>(@"sprites/frame"));
            Assets.Textures.Add("Bullet", Content.Load<Texture2D>(@"sprites/Bullet"));
            Assets.Textures.Add("PlayerGnome", Content.Load<Texture2D>(@"sprites/PlayerGnome"));
            Assets.Textures.Add("EnemyFlower", Content.Load<Texture2D>(@"sprites/EnemyFlower"));
            Assets.Textures.Add("EnemySlime", Content.Load<Texture2D>(@"sprites/EnemySlime"));
            Assets.Textures.Add("EnemyShroom", Content.Load<Texture2D>(@"sprites/EnemyShroom"));
            Assets.Fonts.Add("arial", Content.Load<SpriteFont>(@"fonts/Arial12"));
            Assets.Fonts.Add("arialBig", Content.Load<SpriteFont>(@"fonts/Arial48"));

            //bullet = new Texture2D(GraphicsDevice, 1, 1);
            //bullet.SetData(new[] { Color.White });
        }
        protected override void Update(GameTime gameTime)
        {
            w = GraphicsDevice.Viewport.Bounds.Width;
            h = GraphicsDevice.Viewport.Bounds.Height;

            resolutionPublisher.UpdateResolution(w, h);

            //inputs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                paused = !paused;
            }
            if (Keyboard.GetState().IsKeyDown (Keys.O))
            {
                debugModeOn = !debugModeOn;
            }

            if (!paused && player.IsAlive)
            {
                level.Update(gameTime);
            }
            
            UpdateFPS(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Immediate);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            level.Draw(betterRenderer);

            if (!player.IsAlive)
            {
                betterRenderer.DrawFontInMiddle(Assets.Fonts["arialBig"], "YOU DIED!", 700, Color.Red);
            }

            //debug stuff
            if (debugModeOn)
            {
                level.DrawDebug(betterRenderer);
                _spriteBatch.DrawString(Assets.Fonts["arial"], $"{player.X} {player.Y}", new Vector2(0, 40), Color.Black);
                DrawFPS();
                _spriteBatch.DrawString(Assets.Fonts["arial"], $"{player.hp.Value}", new Vector2(0, 20), Color.White);
                _spriteBatch.DrawString(Assets.Fonts["arial"], $"{w}, {h}", new Vector2(0, 60), Color.Black);
            }

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
            _spriteBatch.DrawString(Assets.Fonts["arial"], $"FPS: {fps}", new Vector2(10, 10), Color.White);
        }
    }
}