using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public static class Serializer
    {
        public static string FieldToString(Checker[,] field)
        {
            var answer = "";
            foreach (var value in field)
            {
                if (value == null)
                {
                    answer+='0';
                    continue;
                }
                answer += (value.Color == Color.Black ? 1 : 0) + (value.IsQueen ? 2 : 0) + 1;
            }
            return answer;
        }
        public static Checker[,] StringToField(string str)
        {
            var field = new Checker[8, 8];
            if (str.Length != 64)
                throw new ArgumentException("not correct argument string in method StringToField");
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                {
                    switch (str[i * 8 + j])
                    {
                        case '1':
                            field[i, j] = new Checker(Color.White, false);
                            break;
                        case '2':
                            field[i, j] = new Checker(Color.Black, false);
                            break;
                        case '3':
                            field[i, j] = new Checker(Color.White, true);
                            break;
                        case '4':
                            field[i, j] = new Checker(Color.Black, true);
                            break;
                    }
                }
            return field;
        }
        public static string MovesToString(List<Move> list)
        {
            var ans = "";
            foreach (var move in list)
                ans += move.From.X.ToString() + move.From.Y.ToString() + move.To.X.ToString() + move.To.Y.ToString() + "/";
            return ans;
        }
        public static List<Move> StringToMoves(string str)
        {
            var ans = new List<Move>();
            var moves = str.Split(new string[] {"/"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var move in moves)
                ans.Add(new Move(new Point(int.Parse(move[0].ToString()), int.Parse(move[1].ToString())), new Point(int.Parse(move[2].ToString()), int.Parse(move[3].ToString()))));
            return ans;
        }
    }
}
