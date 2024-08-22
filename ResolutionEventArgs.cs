using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class ResolutionEventArgs : EventArgs
    {
        public int Width { get; }
        public int Height {  get; }
        public ResolutionEventArgs(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
