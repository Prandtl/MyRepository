using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiard
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            int n = int.Parse(Console.ReadLine());
            var input = new int[n];
            for (int i = 0; i < n; i++)
                input[i] = int.Parse(Console.ReadLine());
            var ballsOut = new Stack<int>(input.Reverse());
            var firstBall = ballsOut.Pop();
            var inside = new Stack<int>(Enumerable.Range(1, firstBall - 1));
            var onTable = new Stack<int>(Enumerable.Range(firstBall + 1, n - firstBall).Reverse());
            while (ballsOut.Count != 0)
            {
                if (inside.Count == 0)
                {
                    if (onTable.Count != 0)
                    {
                        inside.Push(onTable.Pop());
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                var neededBall = ballsOut.Pop();
                if (inside.Peek() > neededBall)
                {
                    flag = false;
                    break;
                }
                while (inside.Peek() < neededBall)
                {
                    if (onTable.Count != 0)
                    {
                        inside.Push(onTable.Pop());
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag) break;
                if (inside.Peek() == neededBall)
                {
                    inside.Pop();
                }
            }
            Console.WriteLine(flag ? "Not a proof" : "Cheater");
        }
    }
}
