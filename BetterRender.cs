using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class BetterRender
    {
        public SpriteBatch spriteBatch { get; private set; }
        private Camera camera;
        public BetterRender(SpriteBatch spriteBatch, Camera camera)
        {
            this.spriteBatch = spriteBatch;
            this.camera = camera;
        }

        public void Render(Texture2D texture, Vector2 position) 
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        public void Render(Texture2D texture, float x, float y, int width, int height, Color color)
        {
            spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, width, height), color);
        }
        public void RenderRelativeToCamera(Texture2D texture, float x, float y, int width, int height)
        {
            spriteBatch.Draw(texture, new Rectangle((int)x - (int)camera.X, (int)y - (int)camera.Y, width, height), Color.White);
        }
        public void RenderRelativeToCamera(Texture2D texture, float x, float y, int width, int height, Color colour)
        {
            spriteBatch.Draw(texture, new Rectangle((int)x - (int)camera.X, (int)y - (int)camera.Y, width, height), colour);
        }
        public void RenderWithRotationRelativeToCamera(Texture2D texture, float x, float y, Rectangle? rect, float rotation, Vector2 origin, Vector2 scale)
        {
            spriteBatch.Draw(texture, new Vector2((int)x - (int)camera.X, (int)y - (int)camera.Y), rect, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
        public void DrawFontInMiddle(SpriteFont font, string text, float verticalOffset, Color color)
        {
            Vector2 stringWidth = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2((camera.width / 2) - (stringWidth.X / 2), verticalOffset), color);
        }
    }
}
