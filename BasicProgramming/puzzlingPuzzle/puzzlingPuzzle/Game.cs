using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzlingPuzzle
{
    public partial class Game : Form
    {
        GameField field;
        public Timer timer;

        public Game()
        {
            this.ClientSize = new Size(64 * 3 + 128, 64 * 3);
            this.Text = "8-puzzle, nigga!";
            ResizeRedraw = true;
            DoubleBuffered = true;
            KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            StartGame();
        }

        void StartGame()
        {
            field = new GameField();

            this.Paint += Game_Paint;

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += timer_Tick;
            timer.Start();
            this.KeyDown += new KeyEventHandler(Game_KeyDown);
            
        }

        void Game_KeyDown(object sender, KeyEventArgs e)
        {
            var neighbours = field.GetNeighbours(field.zeroTile);
            var a=neighbours.Where(x=>x.position==field.zeroTile.position.Add(keyVector[e.KeyCode]));
            if (a.Count() != 0)
            {
                var movingTile=a.First();
                movingTile.Swap(field.zeroTile);
            }
                
        }

        Dictionary<Keys, GameField.Point> keyVector = new Dictionary<Keys, GameField.Point>{{Keys.Down,new GameField.Point(0,-1)},
                                                                                            {Keys.Up,new GameField.Point(0,1)},
                                                                                            {Keys.Left,new GameField.Point(1,0)},
                                                                                            {Keys.Right,new GameField.Point(-1,0)}}; 

        //void Game_KeyPress(object sender, KeyPressEventArgs e)
        //{
            
            
        //}

        void Game_Paint(object sender, PaintEventArgs e)
        {
            Font drawFont = new Font("Arial", 32);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            var graphics=e.Graphics;
            graphics.FillRectangle(Brushes.Aqua,0,0,192,192);
            foreach (var tile in field.Field)
            {
                if (tile.value != " ")
                {
                    graphics.FillRectangle(Brushes.BurlyWood, tile.position.X * 64 + 1, tile.position.Y * 64 + 1, 62, 62);
                    graphics.DrawString(tile.value, drawFont, drawBrush, new PointF(tile.position.X * 64 + 12, tile.position.Y * 64+5));
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
