using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsenbaevNumber
{
    static class Extentions
    {
        public static void AddSingle<T>(this List<T> list, T element)
        {
            if (!list.Contains(element))
                list.Add(element);
        }
    }
    class Program
    {
        static List<List<string>> ReadInput(Dictionary<string,List<string>> participants,Dictionary<string, int> iNumber)
        {
            int n = int.Parse(Console.ReadLine());
             return Enumerable.Range(0, n)
                                  .Select(x =>
                                  {
                                      var a = Console.ReadLine()
                                             .Split(' ')
                                             .ToList();
                                      a.ForEach(z =>
                                      {
                                          if (!participants.ContainsKey(z))
                                              participants.Add(z, new List<string>());
                                          if (!iNumber.ContainsKey(z))
                                              iNumber.Add(z, -1);
                                      });
                                      return a;
                                  })
                                  .ToList();
        }
        static void Main(string[] args)
        {
            var participants = new Dictionary<string,List<string>>();

            var iNumber = new Dictionary<string, int>();

            var teams = ReadInput(participants, iNumber);
            teams.ForEach(z =>
            {
                participants[z[0]].AddSingle(z[1]);
                participants[z[0]].AddSingle(z[2]);
                participants[z[1]].AddSingle(z[0]);
                participants[z[1]].AddSingle(z[2]);
                participants[z[2]].AddSingle(z[0]);
                participants[z[2]].AddSingle(z[1]);
            });




            if (iNumber.ContainsKey("Isenbaev"))
            {
                iNumber["Isenbaev"] = 0;
                BFS(iNumber, participants, "Isenbaev", new Queue<string>());
            }

            Output(iNumber);
            
        }

        static void Output(Dictionary<string, int> iNumber)
        {
            if (iNumber.ContainsKey("Isenbaev"))
            {
                iNumber.OrderBy(x => x.Key)
                    .ToList()
                    .ForEach(x =>
                    {
                        Console.Write(x.Key + " ");
                        if (x.Value == -1)
                            Console.WriteLine("undefined");
                        else
                            Console.WriteLine(x.Value);
                    });
            }
            else
            {
                iNumber.Keys
                       .OrderBy(x => x)
                       .ToList()
                       .ForEach(x => Console.WriteLine(x + " undefined"));
            }
        }
        static void BFS(Dictionary<string,int> iNum,Dictionary<string,List<string>> participants, string name,Queue<string> queue)
        {
            participants[name].Where(x=>iNum[x]==-1)
                              .ToList()
                              .ForEach(x =>
                              {
                                  queue.Enqueue(x);
                                  iNum[x] = iNum[name] + 1;
                              });
            if(queue.Count!=0)
            {
                var a=queue.Dequeue();
                BFS(iNum, participants, a, queue);
            }
        }
    }
}
