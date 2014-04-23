using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using puzzlingPuzzle;


namespace ConsolePuzzle
{
    static class Program
    {
        static void Main()
        {
            GameSession session = new GameSession();
            session.Start();
            
        }

        
    }


    class GameSession
    {
        public void Start()
        {
            var field = new GameField(new int[][]{new int[]{1,4,2},new int[]{3,0,5},new int[]{6,7,8}});
            field.zeroTile = new GameField.Point(0, 0);
            //while (true)
            //{
            //    Visualise(field);
            //    Act(Console.ReadKey().Key,field);

            //    Console.Clear();
            //}

            var a = A_Star.FindPath(field.Field, GameField.initialState);
            foreach (var e in a)
            {
                Visualise(new GameField(e));
                Console.Clear();
                System.Threading.Thread.Sleep(500);
            }
        }
        
        Dictionary<ConsoleKey, GameField.Point> KeyVector = new Dictionary<ConsoleKey, GameField.Point> { {ConsoleKey.UpArrow,new GameField.Point(1,0) },
                                                                                                          {ConsoleKey.DownArrow,new GameField.Point(-1,0)},
                                                                                                          {ConsoleKey.LeftArrow,new GameField.Point(0,1)},
                                                                                                          {ConsoleKey.RightArrow,new GameField.Point(0,-1)}};

        public void Act(ConsoleKey key, GameField field)
        {
           
            if (KeyVector.Keys.Contains(key))
            {
                //foreach (var e in field.GetNeighbours().Select(x => x.Add(field.zeroTile))
                //                         .Where(point => (point.X >= 0 && point.X < 3) && (point.Y >= 0 && point.Y < 3)))
                //{
                //    Console.WriteLine("{0}, {1}", e.X, e.Y);
                //}
                //Console.WriteLine("{0}, {1}", KeyVector[key].X, KeyVector[key].Y);
                //System.Threading.Thread.Sleep(500);
                var zero = field.zeroTile;
                var willGo = KeyVector[key].Add(zero);
                if (field.GetNeighbours().Select(x => x.Add(field.zeroTile))
                                         .Where(point => (point.X >= 0 && point.X < 3) && (point.Y >= 0 && point.Y < 3))
                                         .Where(point => (point.X == willGo.X && point.Y == willGo.Y))
                                         .Count()!=0)
                {
                    
                    //Console.WriteLine("{0}, {1}", zero.X, zero.Y);
                    //System.Threading.Thread.Sleep(500);
                    var moveFrom = field.zeroTile.Add(KeyVector[key]);
                    field.Field[zero.X][zero.Y] = field.Field[moveFrom.X][moveFrom.Y];
                    field.zeroTile = moveFrom;
                    field.Field[moveFrom.X][moveFrom.Y] = 0;
                }
            }
        }



        static void Visualise(GameField field)
        {

            foreach (var line in field.Field)
            {
                foreach (var e in line)
                {
                    if (e != 0)
                    {
                        Console.Write(e.ToString() + ' ');
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();

            }
        }
    }
}

