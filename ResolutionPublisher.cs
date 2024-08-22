using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class ResolutionPublisher
    {
        private int _width;
        private int _height;

        public event EventHandler<ResolutionEventArgs> ResolutionChanged;

        public int Width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnResolutionChanged();
                }
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnResolutionChanged();
                }
            }
        }
        public void UpdateResolution(int width, int height)
        {
            Width = width;
            Height = height;
        }
        private void OnResolutionChanged()
        {
            ResolutionChanged?.Invoke(this, new ResolutionEventArgs(Width, Height));
        }
    }
}
