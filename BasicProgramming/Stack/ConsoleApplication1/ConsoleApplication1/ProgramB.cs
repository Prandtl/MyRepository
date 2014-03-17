//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication1
//{
//    class Program
//    {
//        static int HaveX(char[,] field, int i, int j)
//        {
//            int width = field.GetLength(1);
//            int height = field.GetLength(0);
//            int amount = 0;
//            if (i > 1)
//            {
//                if (field[i - 1, j] == 'x')
//                    amount++;
//            }
//            if (i < height - 1)
//            {
//                if (field[i + 1, j] == 'x')
//                    amount++;
//            }
//            if (j > 1)
//            {
//                if (field[i, j - 1] == 'x')
//                    amount++;
//            }
//            if (j < width - 1)
//            {
//                if (field[i, j + 1] == 'x')
//                    amount++;
//            }
//            return amount;
//        }

//        public class Point
//        {
//            public int X { get; set; }
//            public int Y { get; set; }

//            public static  Point Sub( Point a, this Point b)
//            {
//                return new Point() { X = a.X - b.X, Y = a.Y - b.Y };
//            }
//            public static Point Add( Point a, this Point b)
//            {
//                return new Point() { X = a.X + b.X, Y = a.Y + b.Y };
//            }
//        }

//        static Point Next(char[,] field, Point point, Point firstPoint)
//        {
//            int width = field.GetLength(1);
//            int height = field.GetLength(0);
//            int i = point.X;
//            int j = point.Y;

//            if (i > 1)
//            {
//                if (field[i - 1, j] == 'x')
//                    if (i - 1 != firstPoint.X && j != firstPoint.Y)
//                        return new Point() { X = i - 1, Y = j };
//            }
//            if (i < height - 1)
//            {
//                if (field[i + 1, j] == 'x')
//                    if (i + 1 != firstPoint.X && j != firstPoint.Y)
//                        return new Point() { X = i + 1, Y = j };
//            }
//            if (j > 1)
//            {
//                if (field[i, j - 1] == 'x')
//                    if (i != firstPoint.X && j - 1 != firstPoint.Y)
//                        return new Point() { X = i, Y = j - 1 };
//            }
//            if (j < width - 1)
//            {
//                if (field[i, j + 1] == 'x')
//                    if (i != firstPoint.X && j + 1 != firstPoint.Y)
//                        return new Point() { X = i, Y = j + 1 };
//            }
//            return new Point() { X = 0, Y = 0 };
//        }

//        static void Main(string[] args)
//        {
//            string[] s = Console.ReadLine().Split(' ');
//            int height = int.Parse(s[0]);
//            int width = int.Parse(s[1]);
//            char[,] field = new char[height, width];
//            for (int i = 0; i < height; i++)
//            {
//                s[0] = Console.ReadLine();
//                for (int j = 0; j < width; j++)
//                {
//                    field[i, j] = s[0][j];
//                }
//            }
//            Point first = new Point(){X=-1,Y=-1};
//            for (int i = 0; i < height; i++)
//            {
                
//                for (int j = 0; j < width; j++)
//                {
//                    if (HaveX(field,i,j)==1)
//                        first = new Point(){X=i,Y=j};
//                }
//            }
//            var directions = new Point[] { new Point { X = -1, Y = 0 },
//                                           new Point { X = 1, Y = 0 },
//                                           new Point { X = 0, Y = 1 },
//                                           new Point { X = 0, Y = -1 } };
            
//            var points = new Stack<Point>();
//            foreach (var e in directions)
//            {
//                Point x=Point.Add(first,e);
//                if (field[e.X, e.Y] == 'x')
//                {
//                    points.Push(x);
//                    break;
//                }
//            }
//            Point before = first;

//            Point direction = Point.Sub(first,points.Peek());
//            int amount = 2;
//            int maxAmount = 2;
//            while (points.Count != 0)
//            {
//                Point point = points.Pop();
//                Point next = Next(field, point, before);
//                if (direction.Equals(Point.Sub(point, next)))
//                {
//                    maxAmount = Math.Max(amount, maxAmount);
//                }
//                else
//                {
//                    direction = Point.Sub(point, next);
//                    amount = 2;
//                }
//                points.Push(next);
//                before=
                    
//            }
//        }
//    }
//}
