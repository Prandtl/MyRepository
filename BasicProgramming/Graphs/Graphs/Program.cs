using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = CreateGraphFromFile("wiktionary-links.txt");
            //var graph = CreateGraphFromFile("test-graph.txt");
            //foreach (var k in graph.Keys)
            //{
            //    Console.WriteLine(k);
            //    Console.WriteLine(graph[k].Aggregate((x, i) => x + i));
            //    Console.WriteLine();
            //}
            bool flag=true;
            while (flag)
            {
                switch (Console.ReadLine())
                {
                    case ("components analyse"):
                        ComponentsAnalyse(graph);
                        break;
                    case("quit"):
                        flag = false;
                        break;
                    case("robustness test"):
                        var components = GetComponents(graph);
                        for (int i = 0; i < 1000; i++)
                        {
                            var res = RobustnessTest(graph,components);
                            if (i % 25 == 0)
                                Console.WriteLine("{0}\t{1}", res.Item1, res.Item2);
                        }
                        graph = CreateGraphFromFile("wiktionary-links.txt");
                        break;
                    default:
                        Console.WriteLine("wrong order");
                        break;
                }
            }
            
        }

        static int DestroyVertice(Dictionary<string, List<string>> graph,List<HashSet<string>> components)
        {
            int amount = graph.Last().Value.Count;
            var e = graph.Last().Key;
            graph.Remove(graph.Last().Key);
            foreach (var m in graph.Values)
            {
                m.Remove(e);
            }
            foreach (var m in components)
            {
                m.Remove(e);
            }
            return amount;
        }

        static Tuple<int, int> RobustnessTest(Dictionary<string, List<string>> graph, List<HashSet<string>> components)
        {
            int amount=DestroyVertice(graph,components);
            return Tuple.Create(amount, components.First().Count);
        }

        static void ComponentsAnalyse(Dictionary<string, List<string>> graph)
        {
            var components = GetComponents(graph);

            Console.WriteLine("Components are made");
            foreach (var e in components.OrderByDescending(x => x.Count).Take(20))
            {
                var m = e.OrderByDescending(x => graph[x].Count);
                Console.WriteLine("{0}\t {1}\t {2:00.00}%", e.Count, m.First(), (double)e.Count * 100 / graph.Keys.Count);

            }
        }

        static Dictionary<string, List<string>> CreateGraphFromFile(string path)
        {
            string s;
            var graph = new Dictionary<string, List<string>>();
            var sr = new StreamReader(path);
            while ((s = sr.ReadLine()) != null)
            {
                var pair = s.Split(',');
                if (!graph.Keys.Contains(pair[0]))
                    graph.Add(pair[0], new List<string>());
                if (!graph.Keys.Contains(pair[1]))
                    graph.Add(pair[1], new List<string>());
                graph[pair[0]].Add(pair[1]);
                graph[pair[1]].Add(pair[0]);
            }
            Console.WriteLine("Graph from file {0} is created!", path);
            return graph;
        }

        static List<HashSet<string>> GetComponents(Dictionary<string, List<string>> graph)
        {
            var components = new List<HashSet<string>>();
            var e = graph.Keys.ToArray();
            while (e.Count() != 0)
            {
                var component = BFS(graph, e.First());
                e = e.Except(component).ToArray();
                components.Add(component);
            }
            return components;
        }

        static HashSet<string> BFS(Dictionary<string, List<string>> graph, string startingPoint)
        {
            var component = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(startingPoint);
            component.Add(startingPoint);
            while (queue.Count != 0)
            {
                var e=queue.Dequeue();
                if (graph.Keys.Contains(e))
                {
                    foreach (var vertice in graph[e])
                    {
                        if (!component.Contains(vertice))
                        {
                            component.Add(vertice);
                            queue.Enqueue(vertice);
                        }
                    }
                }
            }
            return component;
        }
    }
}
