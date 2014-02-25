using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
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
                yield return subset.ToArray();
            }
        }

        static void Main(string[] args)
        {
            int[] amount = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            int[] velocity = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

        }
    }
}
