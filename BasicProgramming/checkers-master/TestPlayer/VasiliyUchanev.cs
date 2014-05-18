using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlayer
{
    public class TestPlayer : IPlayer 
        //тут собсно будет ваш код. 
        //в пределах этого класса можете писать что угодно, 
        //а все изменения за пределами него
        //будут недействительны во время турнира
    {
        public void Initialize(Color color)
        {
            Color = color;
        }
        Color Color;
        public List<Move> MakeTurn(Checker[,] field)
        {
            Func<Point, bool> InField = (point => point.X < 8 && point.X >= 0 && point.Y < 8 && point.Y >= 0);
            var answer = new List<Move>();
            var valid = new Validator();
            var way = (Color == Color.White) ? -1 : 1;
            var bindingMoves = GetEvaluatedBindingMoves(field, Color);
            if (bindingMoves.Count() > 0)
            {

                bindingMoves.OrderByDescending(x => x.Value);
                var tempMap = new Checker[8, 8];
                for (var i = 0; i < 8; i++)
                    for (var j = 0; j < 8; j++)
                        if (field[i, j] != null)
                            tempMap[i, j] = new Checker(field[i, j].Color, field[i, j].IsQueen);
                answer.Add(bindingMoves.First().Key);
                valid.MakeMove(tempMap, answer[0]);
                var from = answer[0].To;
                var array = GetEvaluatedBindingMoves(tempMap, Color).Where(x => x.Key.From == from)
                                                                    .OrderByDescending(x => x.Value)
                                                                    .ToArray();
                var counter = array.Length;
                while (counter > 0)
                {
                    var move = new Move(from, array.First().Key.To);
                    answer.Add(move);
                    valid.MakeMove(tempMap, move);
                    from = move.To;
                    array = GetEvaluatedBindingMoves(tempMap, Color).Where(x => x.Key.From == from)
                                                                    .OrderByDescending(x => x.Value)
                                                                    .ToArray();
                    counter = array.Length;
                }
                return answer;

            }
            var dictOfEvaluatedPossibleMoves = new Dictionary<Move, int>();
            var testMove = new Move(new Point(0, 0), new Point(0, 0));
            for (var i = 0; i < 8; i++) // составляем список всех возможных фигур, которые могут ходить
                for (var j = 0; j < 8; j++)
                    if (field[i, j] != null && field[i, j].Color == Color)
                    {
                        if (InField(new Point(i + 1, j + way)) && field[i + 1, j + way] == null)
                        {
                            testMove = new Move(new Point(i, j), new Point(i + 1, j + way));
                            dictOfEvaluatedPossibleMoves.Add(testMove, EvaluateMove(field, testMove));
                        }
                        if (InField(new Point(i - 1, j + way)) && field[i - 1, j + way] == null)
                        {
                            testMove = new Move(new Point(i, j), new Point(i - 1, j + way));
                            dictOfEvaluatedPossibleMoves.Add(testMove, EvaluateMove(field, testMove));
                        }
                        if (InField(new Point(i + 1, j - way)) && field[i + 1, j - way] == null && field[i, j].IsQueen)
                        {
                            testMove = new Move(new Point(i, j), new Point(i + 1, j - way));
                            dictOfEvaluatedPossibleMoves.Add(testMove, EvaluateMove(field, testMove));
                        }
                        if (InField(new Point(i - 1, j - way)) && field[i - 1, j - way] == null && field[i, j].IsQueen)
                        {
                            testMove = new Move(new Point(i, j), new Point(i - 1, j - way));
                            dictOfEvaluatedPossibleMoves.Add(testMove, EvaluateMove(field, testMove));
                        }
                    }
            dictOfEvaluatedPossibleMoves.OrderByDescending(x => x.Value);
            if (dictOfEvaluatedPossibleMoves.Count > 0) //если в этом списке что-то есть -- добавляем рандомный эл-т и заканчиваем ход
            {
                var rand = Game.Rand.Next(0, dictOfEvaluatedPossibleMoves.Count);
                var move = dictOfEvaluatedPossibleMoves.First().Key;
                answer.Add(move);
                return answer;
            }
            return null;
        }

        Dictionary<Move, int> GetEvaluatedBindingMoves(Checker[,] field, Color color)
        {
            var valid = new Validator();
            var dict = new Dictionary<Move, int>();
            foreach (var move in valid.GetBindingMoves(field, color))
            {
                dict.Add(move, EvaluateMove(field, move));
            }
            return dict;
        }

        public int EvaluateMove(Checker[,] field, Move move)
        {
            var tempMap = new Checker[8, 8];

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (field[i, j] != null)
                    {
                        tempMap[i, j] = new Checker(field[i, j].Color, field[i, j].IsQueen);
                    }
                }
            }
            var valid = new Validator();

            valid.MakeMove(tempMap, move);
            var res = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tempMap[i, j] != null)
                    {
                        if (tempMap[i, j].Color == Color.Beated)
                        {
                            if (field[i, j].Color != Color)
                                res++;
                            else
                                res--;
                        }
                    }
                }
            }
            
            return res;
        }
    }
}
