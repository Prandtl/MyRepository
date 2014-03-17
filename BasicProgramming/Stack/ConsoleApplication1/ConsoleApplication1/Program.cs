using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int n = int.Parse(s[0]);
            int roadsAmount = int.Parse(s[1]);
            int mass = int.Parse(Console.ReadLine());
            Tuple<int, int, int>[] roads = Enumerable.Range(0, roadsAmount)
                                                  .Select(x =>
                                                  {
                                                      int[] e = Console.ReadLine().Split(' ').Select(z => int.Parse(z)).ToArray();
                                                      return new Tuple<int, int, int>(e[0], e[1], e[2]);
                                                  })
                                                  .ToArray();
            var roadsTo=new List<Tuple<int,int>>();

            foreach(var e in roads)
            {
                if (e.Item3 < mass)
                    continue;
                roadsTo.Add(new Tuple<int, int>(e.Item1, e.Item2));
                roadsTo.Add(new Tuple<int, int>(e.Item2, e.Item3));
            }

            var path = new Stack<int>();
            path.Push(1);
            bool flag=false;
            List<int> visited = new List<int>();
            while (path.Count != 0)
            {
                int road = path.Pop();
                foreach (var e in roadsTo)
                {
                    if (visited.Contains(e.Item1) || visited.Contains(e.Item2))
                        continue;
                    if (e.Item1 == road)
                    {
                        if (e.Item2 == n)
                            flag = true;
                        path.Push(e.Item2);
                    }
                    if (e.Item2 == road)
                    {
                        if (e.Item1 == n)
                            flag = true;
                        path.Push(e.Item1);
                    }
                    if (flag) break;

                }
                visited.Add(road);
                if (flag) break;
            }
            if (flag)
                Console.WriteLine("YES");
            else 
                Console.WriteLine("NO");
        }
    }
}
