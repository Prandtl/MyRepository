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

        static int FindMaxIndex(int[] amount, int[] velocity)
        {
            int max = amount.Max();
            int maxVelocity =0;
            int maxIndex=-1;
            for (int i = 0; i < amount.Length; i++)
            {
                if (amount[i] == max && velocity[i] > maxVelocity)
                {
                    max = amount[i];
                    maxVelocity = velocity[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        static void DrivePlease(int[] amount, int[] velocity)
        {
            int[] roadsStatus = Enumerable.Range(0, 12).Select(x => 1).ToArray();
            while (roadsStatus.Contains(1))
            {
                int openRoad = FindMaxIndex(amount.Where((x,index)=>(roadsStatus[index]==1)).ToArray(), velocity);
                int ones = 0;
                for (int i = 0; i < roadsStatus.Length; i++)
                {
                    if (ones == openRoad)
                        openRoad = i;
                    if (roadsStatus[i] == 1)
                        ones++;
                }
                roadsStatus[openRoad] = 2;
                int[] closedRoads = null;
                Program.blockRoads.TryGetValue(openRoad,out closedRoads);
                foreach (int i in closedRoads.Select(x => x - 1).ToArray())
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
