using System;
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
            var max = 1 << (N);
            for (long number = 0; number < max; number++)
            {
                var mask = 1;
                var subset = new bool[N];
                for (int j = N - 1; j >= 0; j--)
                {
                    subset[j] = (number & mask) != 0;
                    mask <<= 1;
                }
                yield return subset.ToArray();

            }
        }

        static bool IsTheWin(int[] field)
        {
            for (int i = 1; i < field.Length; i++)
                if (field[i] != field[i - 1]) return false;
            return true;
        }

        static void Flip(ref int piece)
        {
            if (piece == 1)
            {
                piece = 0;
                return;
            }
            piece = 1;
            return;
        }

        static void FlipPieces(int[] field, int point)
        {
            Flip(ref field[point]);
            if (point - 4 >= 0)
                Flip(ref field[point - 4]);
            if (point + 4 < 16)
                Flip(ref field[point + 4]);
            if (point % 4 != 0)
                Flip(ref field[point - 1]);
            if (point % 4 != 3)
                Flip(ref field[point + 1]);
        }

        static int[] Copy(int[] array)
        {
            int[] copy = new int[array.Length];
            for(int i=0;i<array.Length;i++)
                copy[i]=array[i];
            return copy;
        }

        static void Main(string[] args)
        {
            StringBuilder input = new StringBuilder();
            for (int i = 0; i < 4; i++)
                input.Append(Console.ReadLine());
            int[] field = input.ToString().Select(x => (x == 'b') ? 0 : 1).ToArray();
            int[] startingField=Copy(field);
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
                field = Copy(startingField);
            }

            if (minFlips > 16) Console.WriteLine("Impossible");
            else Console.WriteLine(minFlips);
            
        }
    }
}
