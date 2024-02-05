using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MG2
{
    public class HealthBar
    {
        private Texture2D texture;
        private Rectangle shape;
        private float statRatio;

        public HealthBar(int width = 100, int height = 10)
        {
            texture = Assets.Textures["rectangle"];
            shape = new Rectangle(0, 0, width, height);
        }
        public void Draw(BetterRender betterRender, int hp, int maxhp, float x, float y)
        {
            shape.X = (int)x;
            shape.Y = (int)y + 100;
            statRatio = hp / (float)maxhp;
            shape.Width = (int)(100 * statRatio);
            Color colour = new Color(Math.Max(0.0f, 2.0f - 2.0f * statRatio), Math.Min(1.0f, 2.0f * statRatio), 0F);
            betterRender.RenderRelativeToCamera(texture, shape.X, shape.Y, shape.Width, shape.Height, colour);
        }
    }
}
