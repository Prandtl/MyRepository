//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DirtySearch
//{

//    class Map
//    {
//        private const int WALL = 0;
//        private int[][] Cells;
//        public Tuple<int, int> Computer;
//        public Tuple<int, int> Fridge;

//        public void LoadFromConsole()
//        {
//            var size = ReadPointFromConsole();
//            Computer = ReadPointFromConsole();
//            Fridge = ReadPointFromConsole();
//            Cells = Enumerable.Range(0, size.Item2 + 1)
//                .Select(y => (Console.ReadLine() ?? "").Select(ch => ch - '0').ToArray())
//                .ToArray();
//        }

//        public int GetCell(Tuple<int, int> p)
//        {
//            return Inside(p) ? Cells[p.Item2][p.Item1] : WALL;
//        }

//        public IEnumerable<Tuple<int, int>> GetFreeNeighbours(Tuple<int, int> p)
//        {
//            for (int dx = -1; dx <= 1; dx++)
//                for (int dy = -1; dy <= 1; dy++)
//                {
//                    if (dx == 0 && dy == 0) continue;
//                    var n = Tuple.Create(p.Item1 + dx, p.Item2 + dy);
//                    if (GetCell(n) != WALL) yield return n;
//                }
//        }

//        private bool Inside(Tuple<int, int> p)
//        {
//            return p.Item2 >= 0 && p.Item2 < Cells.Length && p.Item1 >= 0 && p.Item1 < Cells[0].Length;
//        }


//        private static Tuple<int, int> ReadPointFromConsole()
//        {
//            var parts = (Console.ReadLine() ?? "").Split(' ');
//            return Tuple.Create(int.Parse(parts[1]) - 1, int.Parse(parts[0]) - 1);
//        }
//    }
    
//    class Program
//    {
        
        
//        static void Main(string[] args)
//        {

//            var map = new Map();
//            map.LoadFromConsole();
//            var ans=Search(map);
//            Console.WriteLine("{0} {1}", ans.Item1, ans.Item2);
//        }

//        static Tuple<int, int> Search(Map map)
//        {
//            Queue<Tuple<int, int, int>> queue = new Queue<Tuple<int, int, int>>();
//            queue.Enqueue(Tuple.Create(map.Computer.Item1, map.Computer.Item2, 1));
//            var visited = new HashSet<Tuple<int, int>>();
//            while (queue.Count != 0)
//            {
                
//                var element = queue.Dequeue();
//                var coordinates = Tuple.Create(element.Item1, element.Item2);
//                if (map.Fridge.Equals(coordinates))
//                    return Tuple.Create(element.Item3, 0);
//                foreach(var e in map.GetFreeNeighbours(Tuple.Create(element.Item1,element.Item2)))
//                {
//                    if (!visited.Contains(e))
//                    {
//                        queue.Enqueue(Tuple.Create(e.Item1, e.Item2, element.Item3 + 1));

//                        visited.Add(coordinates);
//                    }
//                }
//            }
//            return Tuple.Create(0, 0);
//        }
        
//    }
//}
