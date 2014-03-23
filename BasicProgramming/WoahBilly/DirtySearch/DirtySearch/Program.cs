//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DirtySearch
//{
//    class DirtyQueueElement
//    {
//        public int Steps { get; set; }
//        public int Redressed { get; set; }
//        public Map.Point Point { get; set; }
//        public DirtyQueueElement(int steps, int redressed, Map.Point point)
//        {
//            this.Steps = steps;
//            this.Redressed = redressed;
//            this.Point = point;
//        }
//    }

//    class Map
//    {
//        public class Point
//        {
//            public int X { get; set; }
//            public int Y { get; set; }
            
//            public Point(int x, int y)
//            {
//                X = x;
//                Y = y;
//            }

//            public Point Add(Point a)
//            {
//                return new Point(this.X + a.X, this.Y + a.Y);
//            }
//        }

//        public int width;
//        public int height;
//        public int[][] Field { get; set; }


//        public Map(int inpHeight, int inpWidth)
//        {

//            height = inpHeight;
//            width = inpWidth;
//        }

//        private bool CanMove(Point point)
//        {
//            int x = point.X;
//            int y = point.Y;
//            if ((x < 0 || x > height) || (y < 0 || y > width))
//                return false;
//            if (Field[x][y] == 0)
//                return false;
//            return true;
//        }

//        public IEnumerable<Point> GetNeighbours(Point point)
//        {
//            return new Point[] { new Point(-1, 1),
//                                 new Point(0, -1),
//                                 new Point(1, 0),
//                                 new Point(0, 1),
//                                 new Point(-1, 0),
//                                 new Point(-1, -1),
//                                 new Point(1, -1),
//                                 new Point(1, 1)}
//                                .Select(x => x.Add(point))
//                                .Where(x => CanMove(x));
//        }
//    }
    
//    class Program
//    {
//        static Tuple<int, int> DirtySearch(Map map, Map.Point computer, Map.Point fridge)
//        {
//            var field = new bool[map.height, map.width];
//            var queue = new Queue<DirtyQueueElement>();
//            queue.Enqueue(new DirtyQueueElement(1, 0, computer));
//            int i = 0;
//            while(i<=Math.Max(Math.Max(computer.X,map.width-computer.X),Math.Max(computer.Y,map.height-computer.Y)))//smth strange here
//            {
//                while (queue.Count != 0)
//                {
//                    var element = queue.Dequeue();
//                    if (element.Point.Equals(fridge))
//                        return Tuple.Create(element.Steps, element.Redressed);
//                    field[element.Point.X, element.Point.Y] = true;
//                    foreach(Map.Point e in map.GetNeighbours(element.Point))
//                    {
//                        if (!field[e.X, e.Y])
//                        {
//                            if (map.Field[e.X][e.Y] == '2')
//                            {
//                                if (!(element.Redressed + 1 > i))
//                                {
//                                    queue.Enqueue(new DirtyQueueElement(element.Steps + 1, element.Redressed + 1, e));
//                                }
//                            }
//                            else
//                            {
//                                queue.Enqueue(new DirtyQueueElement(element.Steps + 1, element.Redressed, e));
//                            }
//                        }
//                    }
//                }
//                i++;
//            }
//            return Tuple.Create(0,0);
//        }
        
//        static void Main(string[] args)
//        {
//            string[] s = Console.ReadLine().Split(' ');
//            var map = new Map(int.Parse(s[0]), int.Parse(s[1]));
//            s = Console.ReadLine().Split(' ');
//            var computer = new Map.Point(int.Parse(s[1])-1,int.Parse(s[0])-1);
//            s = Console.ReadLine().Split(' ');
//            var fridge = new Map.Point(int.Parse(s[1])-1, int.Parse(s[0])-1);
//            map.Field = Enumerable.Range(0, map.height)
//                                    .Select(x => Console.ReadLine()
//                                           .Select(c => c - '0')
//                                           .ToArray())
//                                           .ToArray();
//            for (int i = 0; i < map.height; i++)
//            {
//                for (int j = 0; j < map.width; j++)
//                {
//                    Console.Write(map.Field[i][j]);
//                }
//                Console.WriteLine();
//            }

//            foreach (var e in map.GetNeighbours(new Map.Point( 1,4)))
//            {
//                Console.WriteLine("{0} {1}", e.X,e.Y);
//            }
            

//        }
//    }
//}
