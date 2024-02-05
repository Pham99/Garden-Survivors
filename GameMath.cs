using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG2
{
    public static class GameMath
    {
        public static int CalculateFloor(int x, int y)
        {
            int quotient = x / y; // Integer division
            int result = quotient * y; // Rounds down to the nearest multiple of y

            // Adjust result to be the floor of the nearest multiple
            if (x < 0 && x % y != 0)
            {
                result -= y;
            }

            return result;
        }
    }
}
