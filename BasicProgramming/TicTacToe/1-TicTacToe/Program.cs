using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        public static HashSet<int[]> ended=new HashSet<int[]>();

        public static int[][] combinations = new int[][] {new int[]{ 1, 2, 3 }, 
                                                new int[]{ 4, 5, 6 },
                                                new int[]{ 7, 8, 9 },
                                                new int[]{ 1, 4, 7 },
                                                new int[]{ 2, 5, 8 },
                                                new int[]{ 3, 6, 9 },
                                                new int[]{ 1, 5, 9 },
                                                new int[]{ 3, 5, 7 }};
        public static int xWin = 0;
        public static int oWin = 0;
        public static int draw = 0;


        static void Swap(ref int x1, ref int x2)
        {
            int t = x1;
            x2 = x1;
            x2 = t;
        }

        static IEnumerable<int[]> GetPermutations(int N)
        {
            int[] permutation = Enumerable.Range(1, N).ToArray();
            while (true)
            {
                yield return permutation;
                int j;
                for (j = N - 2; j >= 0; j--)
                {
                    if (permutation[j] < permutation[j + 1]) break;
                    if (j == 0) yield break;
                }
                int minIndex = j + 1;
                for (int i = j + 1; i < N; i++)
                {
                    if (permutation[i] < permutation[minIndex] && permutation[i] > permutation[j])
                        minIndex = i;
                }
                Swap(ref permutation[j], ref permutation[minIndex]);
                var copy = permutation.Reverse().ToArray();
                int t = 0;
                for (int i = j + 1; i < N; i++, t++)
                {
                    permutation[i] = copy[t];
                }
            }
        }

        static bool Includes(int[] first,int[] second)
        {
            foreach (var e in second)
            {
                if (!first.Contains(e)) return false;
            }
            return true;
        }

        static int FindTheEndOfCombination(int[] permutation, int[] combination)
        {
            
            int maxIndex=0;
            foreach (var element in combination)
            {
                int index = Array.FindIndex(permutation, x => (x == element));
                if (index > maxIndex)
                    maxIndex = index;
            }
            return maxIndex;
        }

        static void FindTheWinner(int[] permutation)
        {
            var combinations = new Dictionary<int, int[]>();
            var xPermutation = permutation.Where((x, index) => index % 2 != 1).ToArray();
            var oPermutation = permutation.Where((x, index) => index % 2 == 1).ToArray();
            foreach (var combination in Program.combinations)
            {
                if (!Includes(permutation, combination)) continue;
                int t;
                if ((t = FindTheEndOfCombination(xPermutation, combination)) != 0)
                    combinations.Add(t * 2, permutation);
                if ((t = FindTheEndOfCombination(oPermutation, combination)) != 0)
                    combinations.Add(t * 2 + 1, permutation);
            }
            if (combinations == null) 
            {
                Program.draw++;
                return;
            }
            var keys = combinations.Keys.ToList();
            keys.Sort();
            int winningKey=keys[0];
            if (keys[0]%2!=0)
                Program.xWin++;
            else Program.oWin++;
            var winningCombination=new List<int>();
            for (int i=0;i<=winningKey;i++)
            {
                winningCombination[i] = permutation[i];
            }
            Program.ended.Add(winningCombination.ToArray());
            return;
        }

        static bool BeenBefore(int[] permutation)
        {
            for (int i=5;i<9;i++)
            {
                int[]subSet=permutation.Where((x,j)=>j<i).ToArray();
                if (Program.ended.Contains(subSet))
                    return true;
            }
            return false;
        }

        static void Main()
        {
           foreach(var permutation in GetPermutations(9))
           {
               Console.WriteLine("{0} {1} {2}", Program.xWin, Program.oWin, Program.draw);
               if (BeenBefore(permutation)) continue;
               FindTheWinner(permutation);
           }
        }
    }
}
