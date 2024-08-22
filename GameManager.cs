using System;

namespace MG2
{
    public class GameMananger
    {
        private Stats exp;
        public int DifficultryScale { get; private set; } = 1;
        public event Action<int> LevelUp;
        public Stats Exp
        {
            get
            {
                return exp;
            }

            private set { exp = value; }
        }

        public GameMananger()
        {
            Exp = new Stats(10);
            Exp.Value = 0;
            Exp.MaxValueReached += OnMaxExp;
        }
        private void OnMaxExp()
        {
            Exp.MaxValue += 5;
            exp.Value = 0;
            DifficultryScale++;
            LevelUp?.Invoke(DifficultryScale);
        }
    }
}