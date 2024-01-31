using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MG2
{
    internal class HealthBar
    {
        private Character character;
        private Vector2 position;
        private Texture2D texture;
        private int maxStat;
        private int stat;
        private Rectangle shape;
        private float statRatio;

        public HealthBar(Texture2D texture, int maxStat, int stat, Character character)
        {
            this.texture = texture;
            this.maxStat = maxStat;
            this.stat = stat;
            this.character = character;
            this.shape = new Rectangle((int)character.X, (int)character.Y + 500, 100, 10);
        }
        public void Update(int hp, int maxhp)
        {
            position = new Vector2(character.X, character.Y + 100);
            shape.X = (int)character.X;
            shape.Y = (int)character.Y;

            statRatio = hp / (float)maxhp;

            shape.Width = Convert.ToInt32(100 * statRatio);
        }

        public void Draw(BetterRender betterRender)
        {
            Color colour = new Color(Math.Max(0.0f, 2.0f - 2.0f * statRatio), Math.Min(1.0f, 2.0f * statRatio), 0F);
            betterRender.RenderRelativeToCamera(texture, position.X, position.Y, shape.Width, shape.Height, colour);
        }
    }
}
