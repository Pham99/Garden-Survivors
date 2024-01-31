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
        private Texture2D _texture;
        private Texture2D _texture2;
        private Texture2D background;
        private Manager<Enemy> enemyManager;
        private Spawner spawner;
        private Camera _camera;
        Player _player;
        Texture2D bullet;

        KeyboardState kb;
        MouseState mouseOld;
        MouseState mouse;
        GamePadState gamepad;

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
            mouse = Mouse.GetState();

            _player = new Player(Content.Load<Texture2D>(@"sprites/vicksyLevel"), _texture2);
            enemyManager = new Manager<Enemy>();
            _camera = new Camera(_player);
            _player.GetCamera(_camera);
            spawner = new Spawner(enemyManager, _player, _texture, _texture2, _camera);
            betterRenderer = new BetterRender(_spriteBatch, _camera);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>(@"sprites/vicksyInsaneOG");
            _texture2 = Content.Load<Texture2D>(@"sprites/rect");
            background = Content.Load<Texture2D>(@"sprites/grassBG");
            bullet = new Texture2D(GraphicsDevice, 1, 1);
            bullet.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            w = GraphicsDevice.Viewport.Bounds.Width;
            h = GraphicsDevice.Viewport.Bounds.Height;
            //inputs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseOld = mouse;
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();
            gamepad = GamePad.GetState(PlayerIndex.One); 

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _player.Update(deltaTime);
            _camera.Update(w, h);

            spawner.Spawn();
            enemyManager.Update(deltaTime);
            

            Collider.Collide(enemyManager, _player.GetProjectileManager());
            Collider.Collide(enemyManager, _player);

            
            UpdateFPS(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(0 - _camera.X, 0 - _camera.Y), Color.White);
            //_player.Draw(_spriteBatch);
            _player.Draw(betterRenderer);
            enemyManager.Draw(betterRenderer);

            _spriteBatch.DrawString(Content.Load<SpriteFont>("Arial"), $"{_camera.X} {_camera.Y}", new Vector2(0, 40), Color.Black);
            DrawFPS();
            _spriteBatch.DrawString(Content.Load<SpriteFont>("Arial"), $"{_player.Hp}", new Vector2(0, 20), Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("Arial"), $"{w}, {h}", new Vector2(0, 60), Color.Black);

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