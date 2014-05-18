using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    static class LinqExtention
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var e in list)
                action(e);
        }
    }

    public class Game
    {
        public static Random Rand = new Random();
        public Checker[,] CreateMap()
        {
            var field = new Checker[8,8];
            for (var i = 0; i < 4; i++)
                for (var j = 0; j < 3; j++)
                    if (j == 1)
                    {
                        field[i * 2 + 1, 7 - j] = new Checker(Color.White, false);
                        field[i * 2, j] = new Checker(Color.Black, false);
                    }
                    else
                    {
                        field[i * 2, 7 - j] = new Checker(Color.White, false);
                        field[i * 2 + 1, j] = new Checker(Color.Black, false);
                    }
            return field;
        }
    }
}
