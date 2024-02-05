using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MG2
{
    public class RepeatingBackground
    {
        private Texture2D texture;
        private Player player;

        public RepeatingBackground(Texture2D texture, Player player)
        {
            this.texture = texture;
            this.player = player;
        }
        public void Draw(BetterRender betterRender)
        {
            int x = GameMath.CalculateFloor((int)player.X, texture.Width);
            int y = GameMath.CalculateFloor((int)player.Y, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x, y, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x + texture.Width, y, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x - texture.Width, y, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x, y + texture.Height, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x, y - texture.Height, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x + texture.Width, y - texture.Height, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x - texture.Width, y + texture.Height, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x + texture.Width, y + texture.Height, texture.Width, texture.Height);
            betterRender.RenderRelativeToCamera(texture, x - texture.Width, y - texture.Height, texture.Width, texture.Height);
        }
    }
}
