using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipGame
{
    class Program
    {
        static IEnumerable<bool[]> Subsets(int N)
        {
            var max = 1 << N;
            for (long number = 0; number < max; number++)
            {
                var mask = 1;
                var subset = new bool[N];
                for (int j = N - 1; j >= 0; j--)
                {
                    subset[j] = (number & mask) != 0;
                    mask <<= 1;
                }
                yield return subset;

            }
        }

        static bool IsTheWin(int[] field)
        {
            return field.All(x => x == field[0]);
        }

        static void FlipPieces(int[] field, int point)
        {
            field[point] ^= 1;
            if (point - 4 >= 0)
                field[point - 4] ^= 1;
            if (point + 4 < 16)
                field[point + 4] ^= 1;
            if (point % 4 != 0)
                field[point - 1] ^= 1;
            if (point % 4 != 3)
                field[point + 1] ^= 1;
        }

        static void Main(string[] args)
        {
            StringBuilder input = new StringBuilder();
            for (int i = 0; i < 4; i++)
                input.Append(Console.ReadLine());
            int[] field = input.ToString().Select(x => (x == 'b') ? 0 : 1).ToArray();
            int[] startingField = field.ToArray();
            int minFlips=20;
            foreach (var subset in Subsets(16))
            {
                int n = 0;
                for (int i = 0; i < subset.Length; i++)
                {
                    if (subset[i])
                    {
                        FlipPieces(field, i);
                        n++;
                    }
                }
                if (n < minFlips && IsTheWin(field)) minFlips = n;
                field = startingField.ToArray();
            }

            if (minFlips > 16) Console.WriteLine("Impossible");
            else Console.WriteLine(minFlips);
            
        }
    }
}
