using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Checkers
{
    public class MyForm : Form
    {
        const int ElementSize = 64;
        List<Move> moves;
        List<Point> playerMoves = new List<Point>();
        Player white = new Player();
        Bot black = new Bot();
        int tickCount;
        Checker[,] field;
        Point turn = new Point(-1, -1);
        Validator validator;

        public MyForm(Checker[,] field)
        {
            black.Initialize(Color.Black);
            white.Initialize(Color.White);
            //to make a turn mark your fields to go one by one and press "T". To simulate a game
            //press space button. Happy gaming, your BandW404 team!
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.field = field;
            validator = new Validator();
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(ElementSize * 8, ElementSize * 8);
            DoubleBuffered = true;
            Text = "Checkers";
            tickCount = 0;
            this.KeyDown += MyForm_KeyDown;
            this.MouseClick += MyForm_MouseClick;
        }

        void MyForm_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var x = e.X / 64;
            var y = e.Y / 64;
            playerMoves.Add(new Point(x, y));
        }

        void MyForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (tickCount > 200)
                {
                    MessageBox.Show("draw, too long game");
                    Environment.Exit(0);
                }
                if (tickCount % 2 == 0)
                {
                    moves = white.MakeTurn(field);
                    validator.IsCorrectMove(moves, field, Color.White);
                }
                else
                {
                    moves = black.MakeTurn(field);
                    validator.IsCorrectMove(moves, field, Color.Black);
                }
                tickCount++;
                Invalidate();
            }

            if (e.KeyCode == Keys.T)
            {
                var temp = new Point(-1, -1);
                var pMoves = new List<Move>();
                for (var i = 0; i < playerMoves.Count; i++)
                {
                    if (temp.X == -1)
                    {
                        temp = playerMoves[i];
                        continue;
                    }
                    else
                    {
                        pMoves.Add(new Move(temp, playerMoves[i]));
                        temp = playerMoves[i];
                    }
                }
                validator.IsCorrectMove(pMoves, field, Color.White);
                moves = black.MakeTurn(field);
                validator.IsCorrectMove(moves, field, Color.Black);
                playerMoves = new List<Point>();
                turn.X = -1;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var pen = new Pen(Brushes.Gold, 5);
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                {
                    e.Graphics.FillRectangle(
                        (i + j) % 2 != 0 ? Brushes.Gray : Brushes.Wheat,
                        i * ElementSize,
                        j * ElementSize,
                        (i + 1) * ElementSize,
                        (j + 1) * ElementSize);
                    if (field[i, j] != null)
                    {
                        e.Graphics.FillEllipse(
                            field[i, j].Color == Color.White ? Brushes.White : Brushes.Black,
                            i * ElementSize + 5,
                            j * ElementSize + 5,
                            ElementSize - 10,
                            ElementSize - 10);
                        if (field[i, j].IsQueen)
                            e.Graphics.DrawEllipse(pen,
                            i * ElementSize + 5,
                            j * ElementSize + 5,
                            ElementSize - 10,
                            ElementSize - 10);
                    }
                }
        }
    }
}