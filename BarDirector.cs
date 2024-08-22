using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class BarDirector
    {
        private Bar.BarBuilder barBuilder;
        public Manager<Bar> BarManager { private get; set; }
        private ResolutionPublisher resolutionPublisher;

        public BarDirector(Bar.BarBuilder barBuilder, Manager<Bar> barManager, ResolutionPublisher resolutionPublisher)
        {
            this.barBuilder = barBuilder;
            this.BarManager = barManager;
            this.resolutionPublisher = resolutionPublisher;
        }
        public Bar CreateHealthBar() 
        {
            Bar result = barBuilder.SetCustomColorFunc(x => new Color(Math.Max(0.0f, 2.0f - 2.0f * x), Math.Min(1.0f, 2.0f * x), 0F))
                                    .SetValues(0, 100)
                                    .SetSize(100, 10)
                                    .SetOffset()
                                    .Build();
            BarManager.Add(result);
            return result;
        }
        public Bar CreateExpBar()
        {
            Bar result = barBuilder.SetColor(Color.Purple)
                                    .SetBackground(true)
                                    .SetPosition(0, 0)
                                    .SetSize(1000, 100)
                                    .SetStartFromZero()
                                    .SetScaleWithResolution(resolutionPublisher)
                                    .SetColor(Color.Purple)
                                    .Build();
            BarManager.Add(result);
            return result;
        }
    }
}
