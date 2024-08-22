using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class HUD : GameObject
    {
        private int levelNumber = 1;
        private string text;
        public HUD(GameMananger gameMananger)
        {
            gameMananger.LevelUp += OnLevelUp;
        }
        public void OnLevelUp(int level)
        {
            levelNumber = level;
        }
        public override void Draw(BetterRender betterRender)
        {
            text = $"Level: {levelNumber}";
            betterRender.DrawFontInMiddle(Assets.Fonts["arialBig"], text, 100, Color.White);
        }
    }
}
