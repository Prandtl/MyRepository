using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzlingPuzzle
{
    public static class ExtensionMethods
    {
        public static GameField.Point Add(this GameField.Point a, GameField.Point b)
        {
            return new GameField.Point(a.X + b.X, a.Y + b.Y);
        }

        public static GameField.Point Sub(this GameField.Point a, GameField.Point b)
        {
            return new GameField.Point(a.X - b.X, a.Y - b.Y);
        }

        public static bool CheckEquality(this int[][] a, int[][] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].SequenceEqual(b[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class GameField
    {
        public int[][] Field;
        public Point zeroTile;

        public static int[][] initialState = new int[][]{new int[]{0,1,2},
                                                  new int[]{3,4,5},
                                                  new int[]{6,7,8}};

        public GameField(int[][] field)
        {
            Field = field;
            zeroTile = GetPosition(field, 0);
        }

        public static Point GetPosition(int[][] field, int val)
        {
            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[i].Length; j++)
                {
                    if (field[i][j] == val)
                        return new GameField.Point(i, j);
                }
            }
            throw new System.Exception("no value like this in the field!");
        }

        public int[][] Swap(Point a, Point b)
        {

            int[][] Swapped = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                Swapped[i] = (int[])Field[i].Clone();
            }
            Swapped[a.X][a.Y] = Field[b.X][b.Y];
            Swapped[b.X][b.Y] = Field[a.X][a.Y];
            return Swapped;

        }

        public Point[] GetNeighbours()
        {

            var a = new Point[]{new Point(1,0),
                               new Point(-1,0),
                               new Point(0,1),
                               new Point(0,-1)};
            a.Select(x => x.Add(zeroTile))
             .Where(point => (point.X >= 0 && point.X < 3) && (point.Y >= 0 && point.Y < 3))
             .ToArray();

            return a;
        }



        public class Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

    }
}
