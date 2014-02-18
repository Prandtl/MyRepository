using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Swap(int[] array,int x1, int x2)
        {
            int t = array[x2];
            array[x2] = array[x1];
            array[x1] = t;
        }

        static int[] GenerateArray(int N)
        {
            return Enumerable.Range(1, N)
                             .ToArray();
        }

        static IEnumerable<int[]> GetPermutations(int N)
        {
            int[] permutation = GenerateArray(N);
            while (true)
            {
                yield return permutation;
                int j = 0;
                for (j = N - 2; j >= 0; j--)
                {
                    if (permutation[j] < permutation[j + 1]) break;
                    if (j == 0) yield break;
                }

                int min = 0;
                int minIndex = 0;
                for (int i = j+1; i < N; i++)
                {
                    if ((permutation[i] < min && permutation[i] > permutation[j]) || (min == 0))
                    {
                        min = permutation[i];
                        minIndex = i;
                    }
                }
                Swap(permutation, j, minIndex);
                var copy = permutation.Reverse().ToArray();
                int t = 0;
                for (int i = j + 1; i < N; i++)
                {
                    permutation[i] = copy[t];
                    t++;
                }
            }
        }

        static void Main()
        {
           foreach (var e in GetPermutations(4))
            {
                foreach (var k in e)
                    Console.Write(k + " ");
                Console.WriteLine();
            
            }
        }

        
    }
}
