using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MG2
{
    public class Player : GameObject, ICollidable
    {
        protected Camera camera;
        protected const float speedModifier = 0.7071F;
        protected float diagSpeed;
        protected float curSpeed;
        protected Color colour = Color.White;
        public Stats hp = new Stats(100);
        private GameMananger gameManager;
        private Gun gun;

        protected int maxHp = 10;
        protected int attack = 5;
        protected float speed = 150F;
        protected Vector2 direction = new Vector2(0, 0);
        public bool IsAlive = true;

        KeyboardState kb;
        MouseState mouseOld;
        MouseState mouse;
        GamePadState gp;

        public Player()
        {

            texture = Assets.Textures["PlayerGnome"];
            X = 0;
            Y = 0;
            speed = 300F;
            maxHp = 100;
            attack = 5;
            diagSpeed = speed * speedModifier;
            hitbox = new Rectangle((int)X, (int)Y, 100, 100);
        }
        public void GetCamera(Camera camera)
        {
            this.camera = camera;
        }
        public void GetGameManager(GameMananger gameManager)
        {
            this.gameManager = gameManager;
        }
        public void GetGun(Gun gun)
        {
            this.gun = gun;
        }
        public override void Update(float deltaTime)
        {
            kb = Keyboard.GetState();
            mouseOld = mouse;
            mouse = Mouse.GetState();
            gp = GamePad.GetState(PlayerIndex.One);
            if (hp.Value > 0)
            {
                curSpeed = speed;
                if (kb.GetPressedKeyCount() > 1)
                {
                    curSpeed = diagSpeed;
                }
                if (kb.IsKeyDown(Keys.S)) { Y = Y + 1 * curSpeed * deltaTime; }
                if (kb.IsKeyDown(Keys.D)) { X = X + 1 * curSpeed * deltaTime; }
                if (kb.IsKeyDown(Keys.W)) { Y = Y + -1 * curSpeed * deltaTime; }
                if (kb.IsKeyDown(Keys.A)) { X = X + -1 * curSpeed * deltaTime; }

                hitbox.X = (int)X;
                hitbox.Y = (int)Y;

                Vector2 movement = gp.ThumbSticks.Left;
                X += movement.X * speed * deltaTime;
                Y += -movement.Y * speed * deltaTime;
                if (mouse.LeftButton == ButtonState.Pressed && mouseOld.LeftButton == ButtonState.Released || mouse.RightButton == ButtonState.Pressed)
                {
                    direction = new Vector2(mouse.X - camera.width / 2, mouse.Y - camera.height / 2);
                    direction.Normalize();
                    gun.Shoot(X + 50, Y + 50, direction);
                }
            }
            else
            {
                IsAlive = false;
            }
        }
        public override void Draw(BetterRender betterRenderer)
        {
            betterRenderer.RenderRelativeToCamera(texture, X, Y, hitbox.Width, hitbox.Height);
        }
        public Rectangle GetHitbox()
        {
            return hitbox;
        }
        public void OnCollision(ICollidable obj)
        {
            if (obj is Enemy)
            {
                hp.Value -= 1;
            }
            else if (obj is ExpOrb)
            {
                gameManager.Exp.Value += 1;
            }
            else if (obj is HealthPack)
            {
                hp.Value += 10;
            }
        }
        public override bool ToBeRemoved()
        {
            throw new NotImplementedException();
        }
    }
}
