using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    class Program
    {
        
        //static bool IsConsistent(bool[] subset)
        //{
        //    for (int i = 0; i < 12; i++)
        //    {
        //        if
        //    }
        //}

        //static IEnumerable<bool[]> Subsets(int N)
        //{
        //    var max = 1 << N;
        //    for (long number = 0; number < max; number++)
        //    {
        //        var mask = 1;
        //        var subset = new bool[N];
        //        for (int j = N - 1; j >= 0; j--)
        //        {
        //            subset[j] = (number & mask) != 0;
        //            mask <<= 1;
        //        }
        //        yield return subset.ToArray();
        //    }
        //}

        static Dictionary<int, int[]> blockRoads = new Dictionary<int, int[]>{{1,new int[]{8,10}},
                                                                           {2,new int[]{4,5,6,9,11,12}},
                                                                           {3,new int[]{5,6,7,8,11,12}},
                                                                           {4,new int[]{2,12}},
                                                                           {5,new int[]{2,3,7,8,9,12}},
                                                                           {6,new int[]{2,3,8,9,10,11}},
                                                                           {7,new int[]{3,5}},
                                                                           {8,new int[]{3,5,6,10,11,12}},
                                                                           {9,new int[]{1,2,5,6,11,12}},
                                                                           {10,new int[]{6,8}},
                                                                           {11,new int[]{1,2,3,6,8,9}},
                                                                           {12,new int[]{2,3,4,5,8,9}}};

        //static int FindMaxIndex(int[] amount, int[] velocity)
        //{
        //    return 1;
        //    //
        //}

        static void DrivePlease(int[] amount, int[] velocity)
        {
            int[] roadsStatus = Enumerable.Range(0, 12).Select(x => 1).ToArray();
            while (roadsStatus.Contains(1))
            {
                int openRoad = roadsStatus.Select((x,i)=>new Tuple<int,int>(x,i))
                                          .Where(x=>roadsStatus[x.Item2]==1)
                                          .Select(x=>new Tuple<int,int>(amount[x.Item2]-velocity[x.Item2],x.Item2))
                                          .OrderByDescending(x=>x.Item1)
                                          .First()
                                          .Item2;
                int[] closedRoads = null;
                Program.blockRoads.TryGetValue(openRoad + 1, out closedRoads);
                closedRoads = closedRoads.Select(x => x - 1).ToArray();
                if (roadsStatus.Select((x, i) => new Tuple<int, int>(x, i))
                               .Where(x => x.Item1 == 2)
                               .Select(x => x.Item2)
                               .Intersect(closedRoads)
                               .ToArray()
                               .Length == 0)
                    roadsStatus[openRoad] = 2;
                else
                {
                    roadsStatus[openRoad] = 0;
                    continue;
                }
                foreach (int i in closedRoads.ToArray())
                {
                    roadsStatus[i] = 0;
                }
            }
            for (int i = 0; i < amount.Length; i++)
            {
                if (roadsStatus[i] == 2)
                    amount[i] -= velocity[i];
                if (amount[i] < 0)
                    amount[i] = 0;
            }
        }

        static void Main(string[] args)
        {
           int[] amount = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            int[] velocity = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < 10; i++)
            {
                DrivePlease(amount, velocity);
            }
            Console.WriteLine(amount.Max());
        }
    }
}
