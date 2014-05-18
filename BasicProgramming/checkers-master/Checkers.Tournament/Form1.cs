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
using System.Threading;

namespace Checkers
{
    public class Form1 : Form
    {
        const int ElementSize = 64;
        Checker[,] field;

        public Form1()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(ElementSize * 8, ElementSize * 8);
            DoubleBuffered = true;
            Text = "Checkers";
            field = new Game().CreateMap();
        }
        public void Update(Checker[,] field)
        {
            this.field = field;
            Invalidate();
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
