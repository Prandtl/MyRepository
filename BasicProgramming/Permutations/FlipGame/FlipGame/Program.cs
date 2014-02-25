using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipGame
{
    class Program
    {
        static bool IsTheWin(int[] field)
        {
            for (int i = 1; i < field.Length; i++)
                if (field[i] != field[i - 1]) return false;
            return true;
        }

        static void Flip(ref int piece)
        {
            if (piece == 1)
            {
                piece = 0;
                return;
            }
            piece = 1;
            return;
        }

        static void FlipPieces(int[] field, int pointx, int pointy)
        {
            int point = pointx - 1 + 4 * (pointy - 1);
            Flip(ref field[point]);
            if (point - 4 >= 0)
                Flip(ref field[point - 4]);
            if (point + 4 < 16)
                Flip(ref field[point + 4]);
            if (point % 4 != 0)
                Flip(ref field[point - 1]);
            if (point % 4 != 3)
                Flip(ref field[point + 1]);
        }

        static void Main(string[] args)
        {
            StringBuilder input = new StringBuilder();
            for (int i = 0; i < 4; i++)
                input.Append(Console.ReadLine());
            int[] field = input.ToString().Select(x => (x == 'b') ? 0 : 1).ToArray();
            for (int i = 0; i < field.Length; i++)
            {
                if (i % 4 == 0) Console.WriteLine();
                Console.Write(field[i]);
            }

            FlipPieces(field, 1, 3);

            for (int i = 0; i < field.Length; i++)
            {
                if (i % 4 == 0) Console.WriteLine();
                Console.Write(field[i]);
            }

            
        }
    }
}
