using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace MG2
{
    public class Bar : GameObject
    {
        private int value;
        private int maxValue;
        private int MaxWidth;
        private float statRatio;

        private Texture2D frameTexture;

        private Color colour = Color.White;
        private Color backgroundColor = Color.Black;
        private Func<float, Color> ColorFunc;

        private bool isFixed = true;
        private bool isOffset = false;
        private bool hasBackground = false;
        private bool hasChangingColours;
        private bool startFromZero = false;

        private Bar()
        {
            X = 0;
            Y = 0;
            hitbox.Width = 100;
            MaxWidth = 100;
            hitbox.Height = 10;
            texture = Assets.Textures["rectangle"];
            frameTexture = Assets.Textures["Frame"];
        }
        public void OnValueChanged(int value, int maxValue)
        {
            this.value = value;
            this.maxValue = maxValue;
        }
        public void OnPositionChanged(float x, float y)
        {
            X = x;
            Y = y;
        }
        public void SubscribeToValueChange(Stats stats)
        {
            stats.ValueChanged += OnValueChanged;
            maxValue = stats.MaxValue;
            if (startFromZero)
            {
                value = 0;
            }
            else
            {
                value = maxValue;
            }
        }
        public void SubscribeToPositionChange(GameObject gobj)
        {
            gobj.PositionChanged += OnPositionChanged;
            isFixed = false;
        }
        public void SubscribeToScaleWidthToResolution(object sender, ResolutionEventArgs e)
        {
            MaxWidth = e.Width;
        }
        public override void Draw(BetterRender betterRender)
        {
            float posY;
            if (isOffset)
            {
                posY = (int)Y + 100;
            }
            else
            {
                posY = Y;
            }
            statRatio = value / (float)maxValue;
            hitbox.Width = (int)(MaxWidth * statRatio);

            if (hasChangingColours)
            {
                colour = ColorFunc(statRatio);
            }

            if (isFixed)
            {
                if (hasBackground)
                {
                    betterRender.Render(texture, X, posY, MaxWidth, hitbox.Height, backgroundColor);

                }
                betterRender.Render(texture, X, posY, hitbox.Width, hitbox.Height, colour);
                betterRender.Render(frameTexture, X, posY, MaxWidth, hitbox.Height, Color.White);
            }
            else
            {
                if (hasBackground)
                {
                    betterRender.RenderRelativeToCamera(texture, X, posY, MaxWidth, hitbox.Height, backgroundColor);
                }
                betterRender.RenderRelativeToCamera(texture, X, posY, hitbox.Width, hitbox.Height, colour);
            }
        }
        public class BarBuilder
        {
            private Bar bar = new Bar();
            public BarBuilder() {}
            public BarBuilder SetPosition(float x, float y)
            {
                bar.X = x; bar.Y = y;
                return this;
            }
            public BarBuilder SetSize(int width, int height)
            {
                bar.MaxWidth = width;
                bar.hitbox.Width = width;
                bar.hitbox.Height = height;
                return this;
            }
            public BarBuilder SetStartFromZero()
            {
                bar.hitbox.Width = 0;
                bar.startFromZero = true;
                return this;
            }
            public BarBuilder SetValues(int value, int maxValue)
            {
                bar.value = value; bar.maxValue = maxValue;
                return this;
            }
            public BarBuilder SetColor(Color color)
            {
                bar.colour = color;
                bar.hasChangingColours = false;
                return this;
            }
            public BarBuilder SetCustomColorFunc(Func<float, Color> colorFunc)
            {
                bar.hasChangingColours = true;
                bar.ColorFunc = colorFunc;
                return this;
            }
            public BarBuilder SetBackground(bool hasBackground)
            {
                bar.hasBackground = hasBackground;
                return this;
            }
            public BarBuilder SetBackgroundColour(Color color)
            {
                bar.backgroundColor = color;
                return this;
            }
            public BarBuilder SetOffset()
            {
                bar.isOffset = true;
                return this;
            }
            public BarBuilder SetScaleWithResolution(ResolutionPublisher resolutionPublisher)
            {
                resolutionPublisher.ResolutionChanged += bar.SubscribeToScaleWidthToResolution;
                return this;
            }
            public void ResetBuild()
            {
                bar = new Bar();
            }
            public Bar Build()
            {
                Bar result = bar;
                ResetBuild();
                return result;
            }
        }
    }
}
