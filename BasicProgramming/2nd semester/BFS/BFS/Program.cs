using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{

    class Graph
    {
        public Dictionary<int, List<int>> Vertices;
        Random r = new Random();

        void CreateRandomGraph()
        {
            Vertices = new Dictionary<int, List<int>>();
            int n = r.Next(20) + 1;
            for (int i = 0; i < n; i++)
            {
                Vertices.Add(i, new List<int>());
            }
            for (int j = 0; j < n; j++)
            {
                int amount = r.Next(5);
                for (int i = 0; i < amount; i++)
                {
                    int neighbour = r.Next(n);
                    if (j != neighbour)
                    {
                        if (!Vertices[j].Contains(neighbour))
                            Vertices[j].Add(neighbour);
                        if (!Vertices[neighbour].Contains(j))
                            Vertices[neighbour].Add(j);
                    }
                }
            }

        }
        void InputGraph()
        {
            Console.WriteLine("Vertices Amount:");
            int n = int.Parse(Console.ReadLine());
            Vertices = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                Vertices.Add(i, Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList());
            }

        }
        public Graph(string graphCreator)
        {
            switch (graphCreator)
            {
                case "Console":
                    Console.WriteLine("Enter Manually");
                    InputGraph();
                    break;
                case "Random":
                    Console.WriteLine("Random graph will be created");
                    CreateRandomGraph();
                    break;
                case "C":
                    Console.WriteLine("Enter Manually");
                    InputGraph();
                    break;
                case "R":
                    Console.WriteLine("Random graph will be created");
                    CreateRandomGraph();
                    break;
                default:
                    Console.WriteLine("Wrong input: Creating Random");
                    CreateRandomGraph();
                    break;
            }
            Console.WriteLine("Got a Graph here!");
        }

        public void Visualise()
        {
            foreach (var key in Vertices.Keys)
            {
                if (Vertices[key].Count > 0)
                {
                    Console.WriteLine(Vertices[key].Select(x => x.ToString()).Aggregate((x, i) => x + ", " + i));
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
    }
    class BFS
    {
        List<int> Visited;
        Queue<int> Opened;

        Graph graph;

        public List<List<int>> Components;

        public BFS(Graph inpGraph)
        {
            graph = inpGraph;
        }

        public void FindComponents()
        {
            Components = new List<List<int>>();
            Opened = new Queue<int>();
            Visited = new List<int>();
            while (Visited.Count != graph.Vertices.Count)
            {
                var m=graph.Vertices.Keys.Where(x=>!Visited.Contains(x)).First();
                Opened.Enqueue(m);
                Visited.Add(m);
                List<int> Component = new List<int>();
                while (Opened.Count != 0)
                {
                    var v = Opened.Dequeue();
                    foreach (var n in graph.Vertices[v].Where(x => !Visited.Contains(x)))
                    {
                        Opened.Enqueue(n);
                        Visited.Add(n);
                        Component.Add(n);
                    }
                }
                Components.Add(Component);
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            string graphCreator = Console.ReadLine();
            var graph = new Graph(graphCreator);
            graph.Visualise();
            var bfs = new BFS(graph); 
            bfs.FindComponents();
            Console.WriteLine();
            Console.WriteLine(bfs.Components.Count);
            foreach (var component in bfs.Components)
            {
                Console.WriteLine(component.Select(x => x.ToString()).Aggregate((x, i) => x + ", " + i));
            }
        }
    }
}
