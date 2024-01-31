using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MG2
{
    public class Player : Character, ICollidable
    {
        protected Texture2D texture;
        protected Camera camera;
        protected const float speedModifier = 0.7071F;
        protected float diagSpeed;
        protected float curSpeed;
        protected Color colour = Color.White;
        private Manager<Projectile> projectileManager;
        private HealthBar healthBar;

        KeyboardState kb;
        MouseState mouseOld;
        MouseState mouse;
        GamePadState gp;
        public int Hp
        {
            get { return hp; }
            set
            {
                if (value < 0)
                {
                    hp = 0;
                }
                else if (value > maxHp)
                {
                    hp = maxHp;
                }
                else
                {
                    hp = value;
                }
            }
        }
        public Player(Texture2D texture, Texture2D texture2)
        {
            this.texture = texture;
            X = 0;
            Y = 0;
            speed = 300F;
            maxHp = 10;
            hp = maxHp;
            attack = 5;
            diagSpeed = speed * speedModifier;
            hp = maxHp;
            hitbox = new Rectangle((int)X, (int)Y, 100, 100);
            projectileManager = new Manager<Projectile>();
            healthBar = new HealthBar(texture2, maxHp, hp, this);
        }
        public void GetCamera(Camera camera)
        {
            this.camera = camera;
        }
        public override void Update(float deltaTime)
        {
            kb = Keyboard.GetState();
            mouseOld = mouse;
            mouse = Mouse.GetState();
            gp = GamePad.GetState(PlayerIndex.One);
            if (hp > 0)
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
            }


            if (mouse.LeftButton == ButtonState.Pressed && mouseOld.LeftButton == ButtonState.Released)
            {
                direction = new Vector2(mouse.X + camera.X - X, mouse.Y + camera.Y - Y);
                direction.Normalize();
                projectileManager.Spawn(new Projectile(X, Y, texture, direction));
            }
            projectileManager.Update(deltaTime);
            healthBar.Update(hp, maxHp);

        }
        public override void Draw(BetterRender betterRenderer)
        {
            betterRenderer.RenderRelativeToCamera(texture, X, Y, hitbox.Width, hitbox.Height);
            projectileManager.Draw(betterRenderer);
            healthBar.Draw(betterRenderer);
        }
        public Rectangle GetHitbox()
        {
            return hitbox;
        }
        public void OnCollision()
        {
            hp -= 1;
        }
        public bool ToBeRemoved()
        {
            throw new NotImplementedException();
        }
        public Manager<Projectile> GetProjectileManager()
        {
            return projectileManager;
        }
    }
}
