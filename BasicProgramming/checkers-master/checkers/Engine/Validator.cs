using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Validator
    {
        private HashSet<Point> checkersTODelete = new HashSet<Point>();
        public void IsCorrectMove(List<Move> moves, Checker[,] field, Color playerColor)
        {
            if (moves == null || (moves != null && moves.Count == 0))
            {
                Logs.AddToLog("Не сходил вообще");
                Logs.Done();
                throw new NotImplementedException("Player lost");
            }
            var firstMove = true;
            var result = true;
            var start = new Point(-1,-1);
            var mv = GetBindingMoves(field, playerColor);
            var attack = mv.Count != 0;
            foreach (var turn in moves)
                if (result && InField(new Point(turn.From.X, turn.From.Y)) && InField(new Point(turn.To.X, turn.To.Y)) && field[turn.From.X, turn.From.Y] != null)
                {
                    if (start.X == -1 && start.Y == -1)
                        start = turn.From;
                    if (start != turn.From)
                    {
                        Logs.AddToLog("Пытался сходить не той пешкой, которой ходил вначале");
                        Logs.Done();
                        throw new NotImplementedException("Пытался сходить не той пешкой, которой ходил вначале");
                    }
                    var bindingMoves = GetBindingMoves(field, playerColor);
                    if (bindingMoves.Count == 0 && !firstMove)
                    {
                        Logs.AddToLog("Попытка двойных ходов");
                        Logs.Done();
                        throw new NotImplementedException("Попытка двойных ходов");
                    }
                    if (bindingMoves.Count != 0 && !bindingMoves.Contains(turn))
                    {
                        Logs.AddToLog("Были обязательные ходы, но ход не был сделан");
                        Logs.Done();
                        throw new NotImplementedException("Были обязательные ходы, но ход не был сделан");
                    }
                    if (!field[turn.From.X, turn.From.Y].IsQueen)
                        result &= IsCheckerTurnCorrect(field, playerColor, turn);
                    else
                        result &= IsQuennTurnCorrect(field, playerColor, turn);
                    if (result)
                    {
                        MakeMove(field, turn);
                        firstMove = false;
                        start = turn.To;
                    }

                }
                else result = false;
            if (!result)
            {
                Logs.AddToLog("Некорректные ходы");
                Logs.Done();
                throw new NotImplementedException("Некорректные ходы");
            }
            var finalTest = GetBindingMoves(field, playerColor);
            if (IsInHashSet(finalTest, start) && attack)
            {
                Logs.AddToLog("Атака была не завершена");
                Logs.Done();
                throw new NotImplementedException("Атака была не завершена");
            }
            foreach (var e in checkersTODelete)
                field[e.X, e.Y] = null;
            checkersTODelete.Clear();
            return;
        }

        private bool IsInHashSet(HashSet<Move> hash, Point hero)
        {
            foreach (var e in hash)
                if (e.From == hero) return true;
            return false;
        }

        private bool IsCheckerTurnCorrect(Checker[,] field, Color playerColor, Move turn)
        {
            if (field[turn.From.X, turn.From.Y].Color != playerColor)
                return false;
            var dx = new int[2];
            var dy = new int[2];
            if (playerColor == Color.White)
            {
                dx = new int[] { 1, -1 };
                dy = new int[] { -1, -1 };
            }
            else
            {
                dx = new int[] { -1, 1 };
                dy = new int[] { 1, 1 };
            }
            for (var i = 0; i < 2; i++)
                if (InField(new Point(turn.From.X + dx[i], turn.From.Y + dy[i])))
                    if (field[turn.From.X + dx[i], turn.From.Y + dy[i]] == null &&
                        turn.From.X + dx[i] == turn.To.X && turn.From.Y + dy[i] == turn.To.Y)
                            return true;//ламповая проверка на возожность хода 
            
            dx = new int[] { 2, -2, 2, -2 };
            dy = new int[] { -2, -2, 2, 2 };
            for (var i = 0; i < 4; i++)
                if (InField(new Point(turn.From.X + dx[i], turn.From.Y + dy[i])))
                    if (field[turn.From.X + dx[i], turn.From.Y + dy[i]] == null)
                    {
                        var decr = GetNextFreePlace(dx[i], dy[i]);
                        if (field[turn.From.X + decr.X, turn.From.Y + decr.Y] != null &&
                            field[turn.From.X + decr.X, turn.From.Y + decr.Y].Color != playerColor)
                                return true;
                    }
            return false;
        }

        private bool IsQuennTurnCorrect(Checker[,] field, Color playerColor, Move turn)
        {
            var possibleAttack = Bind(field, new Point(turn.From.X, turn.From.Y), playerColor);
            return possibleAttack.Contains(turn) || IsRightQueenMove(turn, field, playerColor);
        }

        private bool InField(Point pos)
        {
            return pos.X < 8 && pos.X >= 0 && pos.Y < 8 && pos.Y >= 0;
        }

        public HashSet<Move> GetBindingMoves(Checker[,] field, Color playerColor) 
        {
            var ans = new HashSet<Move>();
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                    if (field[i, j] != null && field[i, j].Color == playerColor)
                        for (var di = -1; di < 2; di += 2)
                            for (var dj = -1; dj < 2; dj += 2)
                            {
                                var from = new Point(i, j);
                                var enemy = new Point(i + di, j + dj);
                                var free = new Point(i + di * 2, j + dj * 2);
                                if (InField(enemy) &&
                                    field[enemy.X, enemy.Y] != null &&
                                    field[enemy.X, enemy.Y].Color != playerColor &&
                                    InField(free) && field[enemy.X, enemy.Y].Color != Color.Beated &&
                                    field[free.X, free.Y] == null)
                                    ans.Add(new Move(from, free));
                            }
            AddToHash(ans, AddBindingForQueens(field, playerColor));
            return ans;
        }

        public void AddToHash(HashSet<Move> source, HashSet<Move> other)
        {
            foreach (var e in other)
                source.Add(e);
        }

        private bool IsRightQueenMove(Move move, Checker[,] field, Color playerColor)
        {
            var dx = new int[] { 1, -1, 1, -1 };
            var dy = new int[] { -1, -1, 1, 1 };
            var x = move.From.X;
            var y = move.From.Y;
            for (var i = 0; i < 4; i++)
            {
                var enemyFound = false;
                for (var delta = 1; delta < 8; delta++)
                    if (!enemyFound)
                    if (InField(new Point(x + dx[i] * delta, y + dy[i] * delta)))
                    {
                        if (field[x + dx[i] * delta, y + dy[i] * delta] != null)
                        {
                            enemyFound = true;
                            continue;
                        }
                        if (move.To.X == x + dx[i] * delta && move.To.Y == y + dy[i] * delta)
                            return true;
                    }
            }
            return false;
        }

        private HashSet<Move> Bind(Checker[,] field, Point pos, Color playerColor)
        {
            var dx = new int[] { 1, -1, 1, -1 };
            var dy = new int[] { -1, -1, 1, 1 };
            var x = pos.X;
            var y = pos.Y;
            var ans = new HashSet<Move>();
            for (var i = 0; i < 4; i++)
            {
                var noBlock = true;
                var enemyFound = false;
                for (var delta = 1; delta < 8; delta++)
                    if (InField(new Point(x + dx[i] * delta, y + dy[i] * delta)))
                    {
                        if (InField(new Point(x + dx[i] * (delta + 1), y + dy[i] * (delta + 1))))
                        {
                            if (field[x + dx[i] * delta, y + dy[i] * delta] != null
                                && field[x + dx[i] * (delta + 1), y + dy[i] * (delta + 1)] != null)
                                if (field[x + dx[i] * delta, y + dy[i] * delta].Color != playerColor
                                    && field[x + dx[i] * delta, y + dy[i] * delta].Color != Color.Beated
                                    && field[x + dx[i] * (delta + 1), y + dy[i] * (delta + 1)].Color != Color.Beated)
                                    noBlock = false;
                            if (field[x + dx[i] * delta, y + dy[i] * delta] != null && field[x + dx[i] * delta, y + dy[i] * delta].Color == Color.Beated)
                                noBlock = false;
                            if (field[x + dx[i] * delta, y + dy[i] * delta] != null && field[x + dx[i] * delta, y + dy[i] * delta].Color == playerColor)
                                noBlock = false;
                        }
                        if (field[x + dx[i] * delta, y + dy[i] * delta] != null)
                            if (InField(new Point(x + dx[i] * (delta + 1), y + dy[i] * (delta + 1))))
                                if (((field[x + dx[i] * delta, y + dy[i] * delta].Color != playerColor
                                    && field[x + dx[i] * delta, y + dy[i] * delta].Color != Color.Beated)) && noBlock
                                    && field[x + dx[i] * (delta + 1), y + dy[i] * (delta + 1)] == null)
                                {
                                    ans.Add(new Move(new Point(x, y), new Point(x + dx[i] * (delta + 1), y + dy[i] * (delta + 1))));
                                    enemyFound = true;
                                }
                        if (enemyFound && field[x + dx[i] * delta, y + dy[i] * delta] == null && noBlock)
                        {
                            ans.Add(new Move(new Point(x, y), new Point(x + dx[i] * (delta), y + dy[i] * (delta))));
                        }
                    }
            }
            return ans;
        }

        private Tuple<int,int> GetDelta(Move move)
        {
            var dx = move.To.X > move.From.X ? 1 : -1;
            var dy = move.To.Y > move.From.Y ? 1 : -1;
            return new Tuple<int, int>(dx, dy);
        }
        public HashSet<Move> AddBindingForQueens(Checker[,] field, Color color)
        {
            var ans = new HashSet<Move>();
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                    if (field[i, j] != null && field[i, j].Color == color && field[i, j].IsQueen)
                        AddToHash(ans, Bind(field, new Point(i, j), color));
            return ans;
        }

        private Point GetNextFreePlace(int x, int y)
        {
            var dx = x > 0 ? x - 1 : x + 1;
            var dy = y > 0 ? y - 1 : y + 1;
            return new Point(dx, dy);
        }

        public void MakeMove(Checker[,] field, Move move)
        {
            var delta = GetDelta(move);
            var dx = delta.Item1;
            var dy = delta.Item2;
            var x = move.From.X;
            var y = move.From.Y;
            var checker = new Checker(field[x,y].Color, field[x,y].IsQueen);
            var color = field[x, y].Color;
            field[x, y] = null;
            for (var i = 0; i < 8; i++)
                if (InField(new Point(x + dx * i, y + dy * i)))
                {
                    if (move.To.X == x + dx * i && move.To.Y == y + dy * i)
                    {
                        field[x + dx * i, y + dy * i] = checker;
                        if (y + dy * i == 0 && checker.Color == Color.White) field[x + dx * i, y + dy * i] = new Checker(checker.Color, true);
                        if (y + dy * i == 7 && checker.Color == Color.Black) field[x + dx * i, y + dy * i] = new Checker(checker.Color, true);
                        break;
                    }

                    if (field[x + dx * i, y + dy * i] != null && field[x + dx * i, y + dy * i].Color != color)
                    {
                        checkersTODelete.Add(new Point(x + dx * i, y + dy * i));
                        field[x + dx * i, y + dy * i] = new Checker(Color.Beated, false);
                    }
                }
        }
    }
}
