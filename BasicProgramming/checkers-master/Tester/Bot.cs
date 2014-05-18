using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public class Bot : IPlayer
    {
        public List<Move> MakeTurn(Checker[,] field)
        {
            Func<Point, bool> InField = (point => point.X < 8 && point.X >= 0 && point.Y < 8 && point.Y >= 0);
            var answer = new List<Move>();
            var valid = new Validator();
            var way = (Color == Color.White) ? -1 : 1;
            var listOfMyCheckersWhoCanMove = new List<Move>();
            var bindingMoves = valid.GetBindingMoves(field, Color);
            if (bindingMoves.Count > 0)
            {
                var tempMap = new Checker[8, 8];
                for (var i = 0; i < 8; i++)
                    for (var j = 0; j < 8; j++)
                        if (field[i, j] != null)
                            tempMap[i, j] = new Checker(field[i, j].Color, field[i, j].IsQueen);
                answer.Add(bindingMoves.ToArray()[Program.Rand.Next(0, bindingMoves.Count)]);
                valid.MakeMove(tempMap, answer[0]);
                var from = answer[0].To;
                var array = valid.GetBindingMoves(tempMap, Color).Where(x => x.From == from).ToArray();
                var counter = array.Length;
                while (counter > 0)
                {
                    var rand = Program.Rand.Next(0, counter);
                    var move = new Move(from, array[rand].To);
                    answer.Add(move);
                    valid.MakeMove(tempMap, move);
                    from = move.To;
                    array = valid.GetBindingMoves(tempMap, Color).Where(x => x.From == from).ToArray();
                    counter = array.Length;
                }
                return answer;
            }
            for (var i = 0; i < 8; i++) // составляем список всех возможных фигур, которые могут ходить
                for (var j = 0; j < 8; j++)
                    if (field[i, j] != null && field[i, j].Color == Color)
                    {
                        if (InField(new Point(i + 1, j + way)) && field[i + 1, j + way] == null)
                            listOfMyCheckersWhoCanMove.Add(new Move(new Point(i, j), new Point(i + 1, j + way)));
                        if (InField(new Point(i - 1, j + way)) && field[i - 1, j + way] == null)
                            listOfMyCheckersWhoCanMove.Add(new Move(new Point(i, j), new Point(i - 1, j + way)));
                        if (InField(new Point(i + 1, j - way)) && field[i + 1, j - way] == null && field[i, j].IsQueen)
                            listOfMyCheckersWhoCanMove.Add(new Move(new Point(i, j), new Point(i + 1, j - way)));
                        if (InField(new Point(i - 1, j - way)) && field[i - 1, j - way] == null && field[i, j].IsQueen)
                            listOfMyCheckersWhoCanMove.Add(new Move(new Point(i, j), new Point(i - 1, j - way)));
                    }
            if (listOfMyCheckersWhoCanMove.Count > 0) //если в этом списке что-то есть -- добавляем рандомный эл-т и заканчиваем ход
            {
                var rand = Program.Rand.Next(0, listOfMyCheckersWhoCanMove.Count);
                var move = listOfMyCheckersWhoCanMove[rand];
                answer.Add(move);
                return answer;
            }
            MessageBox.Show(Color.ToString() + " lose");
            Environment.Exit(0);
            return null;
        }

        public void Initialize(Color color)
        {
            Color = color;
        }
        Color Color;
    }
}
