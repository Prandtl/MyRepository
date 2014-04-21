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

        public static void Swap(this GameField.Tile a, GameField.Tile b)
        {
            var t = new GameField.Point(a.position.X, a.position.Y);
            a.position = b.position;
            b.position = t;
        }
    }

    public class GameField
    {
        public Tile[] Field;
        public Tile zeroTile;

        public GameField()
        {
            Field = new Tile[]{new Tile(0,0," "),
                               new Tile(1,0,"1"),
                               new Tile(2,0,"2"),
                               new Tile(0,1,"3"),
                               new Tile(1,1,"4"),
                               new Tile(2,1,"5"),
                               new Tile(0,2,"6"),
                               new Tile(1,2,"7"),
                               new Tile(2,2,"8")};
            zeroTile=Field[0];
        }

        public Tile[] GetNeighbours(Tile tile)
        {
            var a = new Point[]{new Point(-1,0),
                                new Point(1,0),
                                new Point(0,1),
                                new Point(0,-1)}.Select(x => x.Add(tile.position))
                                                .Where(x => (x.X >= 0 && x.X < 3) && (x.Y >= 0 && x.Y < 3));
            return Field.Where(x => a.Contains(x.position)).ToArray();
        }

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x,int y)
            {
                X = x;
                Y = y;
            }

            
        }
        public class Tile
        {
            public Point position { get; set; }
            public string value;

            public Tile(int x,int y, string val)
            {
                position = new Point(x, y);
                value = val;
            }
        }

    }
}
