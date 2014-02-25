using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stones
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
            int n = int.Parse(Console.ReadLine());
            string input = Console.ReadLine();
            int[] weight = input.Split(' ').Select(x => int.Parse(x)).ToArray();
            int sum = weight.Sum();
            int minDifference = sum;
            foreach (var e in Subsets(weight.Length))
            {
                int subsetSum = 0;
                int t = 0;
                foreach (var c in e)
                {
                    if (c) subsetSum += weight[t];
                    t++;
                }

                if (Math.Abs(sum - 2 * subsetSum) < minDifference) minDifference = Math.Abs(sum - 2 * subsetSum);

            }
            Console.WriteLine(minDifference);
        }
    }
}
