using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public class Stats
    {
        public event Action<int, int> ValueChanged;
        public event Action MaxValueReached;
        private int _value;
        public int MaxValue { get; set; }
        
        public Stats(int maxHp)
        {
            MaxValue = maxHp;
            _value = maxHp;
        }
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                if (_value < 0)
                {
                    _value = 0;
                }
                else if (_value > MaxValue)
                {
                    _value = MaxValue;
                    MaxValueReached?.Invoke();
                }
                ValueChanged?.Invoke(_value, MaxValue);
            }
        }
    }
}
