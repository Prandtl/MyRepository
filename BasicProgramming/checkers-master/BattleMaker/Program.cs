using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace Checkers
{
    public class ChallengeObjects
    {
        public List<Tuple<string, string>> Fighters;
        public Dictionary<string, int> Results;
        public ChallengeObjects()
        {
            //provides fighters combinations
            Fighters = GetFighters();
            FiltrateFighters();
            Results = GetResults();
        }

        public void FiltrateFighters()
        {
            var pretendents = new Dictionary<string, int>();
            foreach (var e in Fighters)
                if (!pretendents.ContainsKey(e.Item1))
                    pretendents[e.Item1] = 0;
            var temp = pretendents.ToDictionary(x => x.Key, y => y.Value);
            foreach (var e in temp)
            {
                try
                {
                    var process = new Process();
                    process.StartInfo.FileName = "Checkers.Tournament.exe";
                    process.StartInfo.Arguments = e.Key + " " + "testPlayer.dll";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    var winner = process.StandardOutput.ReadLine()[0];
                    if (winner == 'W')
                        pretendents[e.Key]++;
                }
                catch
                {
                    pretendents[e.Key] = -10;
                }
            }
            Fighters = Fighters.Where(x => pretendents.ContainsKey(x.Item1) && pretendents.ContainsKey(x.Item2))
                               .ToList();
            //добавить ограничение по победам
            // фильтруем некорректные дллс
        }

        private Dictionary<string, int> GetResults()
        {
            var results = new Dictionary<string, int>(); //где целые - количество побед
            foreach (var e in Fighters)
                if (!results.ContainsKey(e.Item1))
                    results[e.Item1] = 0;
            foreach (var e in Fighters)
            {
                var process = new Process();
                process.StartInfo.FileName = "Checkers.Tournament.exe";
                process.StartInfo.Arguments = e.Item1 + " " + e.Item2;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                var winner = process.StandardOutput.ReadLine()[0];
                if (winner == 'W')
                    results[e.Item1]++;
                else if (winner == 'B')
                    results[e.Item2]++;
            }
            return results;
        }

        private List<Tuple<string, string>> GetFighters()
        {
            var dlls = Directory.GetFiles("DLLS").ToList();
            var result = new List<Tuple<string, string>>();
            var temp = "fighter";
            var used = new HashSet<Tuple<string, string>>();
            foreach (var first in dlls)
            {
                temp = first;
                foreach (var second in dlls)
                {
                    if (temp != second )
                    {
                        result.Add(new Tuple<string, string>(temp, second));
                        used.Add(new Tuple<string,string>(temp, second));
                        used.Add(new Tuple<string,string>(second, temp));
                    }
                }
            }
            return result;
        }

        public Dictionary<string, int> GetSortedResults()
        {
            return Results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
        }

    }

    class Program1
    {
        static void Main(string[] args)
        {
            
            var challenge = new ChallengeObjects();
            foreach (var e in challenge.GetSortedResults())
                Console.WriteLine(e.Key + " won times:  " + e.Value);
            Console.ReadKey();
        }
    }
}
