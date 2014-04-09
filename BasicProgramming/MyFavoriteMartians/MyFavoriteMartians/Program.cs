using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFavoriteMartians
{
    class Program
    {
        static List<int>[] ReadFromConsole()
        {
            var n = int.Parse(Console.ReadLine());
            var childrens = new List<int>[n];
            childrens.Select(x => new List<int>());
            Enumerable.Range(0, n)
                      .ToList()
                      .ForEach(x => childrens[x] = Console.ReadLine()
                                                          .Split(' ')
                                                          .Select(z => int.Parse(z))
                                                          .Where(z => z != 0)
                                                          .Select(z => z - 1)
                                                          .ToList());
            return childrens;
        }
        static void Main(string[] args)
        {
            var childrens = ReadFromConsole();
            var childrensAmount = new int[childrens.Count()];
            childrensAmount= childrensAmount.Select(x => childrens[x].Count).ToArray();
            var parentsAmount = new int[childrens.Count()];
            parentsAmount.Select(x => 0);
            childrens.ToList()
                     .ForEach(x => x.ForEach(z => parentsAmount[z]++));

            var generation = new int[childrens.Count()];
            generation.Select(x => 0);
            var queue = new Queue<int>();
            for (int i = 0; i < parentsAmount.Length; i++)
            {
                if (parentsAmount[i] == 0)
                    queue.Enqueue(i);
            }

            SBFS(queue, generation, childrens);

            Output(generation);
            
        }

        static void SBFS(Queue<int> queue, int[] generation, List<int>[] childrens)
        {
            var a = queue.Dequeue();
            foreach (var e in childrens[a])
            {
                queue.Enqueue(e);
                generation[e] = generation[a] + 1;
            }
            if (queue.Count != 0)
                SBFS(queue, generation, childrens);
        }

        static void Output(int[] generation)
        {
            Console.WriteLine(Enumerable.Range(1, generation.Length)
                            .Select(x => Tuple.Create(x, generation[x - 1]))
                            .OrderBy(x => x.Item2)
                            .Select(x => x.Item1.ToString())
                            .Aggregate((x, e) => x + " " + e));
        }
        
    }
}
