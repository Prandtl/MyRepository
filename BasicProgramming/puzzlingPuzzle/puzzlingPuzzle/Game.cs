using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace puzzlingPuzzle
{
    public partial class Game : Form
    {
        GameField field;
        public Timer timer;
        Dictionary<string, Image> Images;
        Button button;

        public Game()
        {
            this.ClientSize = new Size(64 * 3 + 128, 64 * 3);
            this.Text = "8-puzzle, nigga!";
            ResizeRedraw = true;
            DoubleBuffered = true;
            KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button = new Button();
            button.Text = "Solve";
            button.Size = new Size(100, 25);
            button.Location = new Point(64 * 3 + 14, 64 + 32 - 12);
            StartGame();
            Images = Directory.EnumerateFiles(Application.StartupPath + "/images/", "*.bmp")
                        .ToDictionary(Path.GetFileNameWithoutExtension, Image.FromFile);
        }


        Queue<int[][]> path;
        bool Solving = false;

        void button_Click(object sender, EventArgs e)
        {
            path = new Queue<int[][]>(A_Star.FindPath(field.Field, GameField.initialState));
            if (path.Count != 0)
            {
                Solving = true;
            }
            else
            {
                field.Field = GameField.initialState;
            }
        }

        void StartGame()
        {
            field = new GameField(new[]{new[]{0,6,5},new[]{2,1,8},new[]{4,3,7}});
            this.Paint += Game_Paint;
            //this.KeyDown += new KeyEventHandler(Game_KeyDown);

            this.Controls.Add(button);
            button.Click += button_Click;
            timer = new Timer();
            timer.Interval = 250;
            timer.Tick += timer_Tick;
            timer.Start();
            
        }

        Dictionary<Keys, GameField.Point> KeyVector = new Dictionary<Keys, GameField.Point> { {Keys.Up,new GameField.Point(1,0) },
                                                                                              {Keys.Down,new GameField.Point(-1,0)},
                                                                                              {Keys.Left,new GameField.Point(0,1)},
                                                                                              {Keys.Right,new GameField.Point(0,-1)}};

        void Game_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (KeyVector.Keys.Contains(e.KeyCode))
            {
                var zero = field.zeroTile;
                var willGo = KeyVector[e.KeyCode].Add(zero);
                if (field.GetNeighbours().Select(x => x.Add(field.zeroTile))
                                         .Where(point => (point.X >= 0 && point.X < 3) && (point.Y >= 0 && point.Y < 3))
                                         .Where(point => (point.X == willGo.X && point.Y == willGo.Y))
                                         .Count() != 0)
                {

                    //Console.WriteLine("{0}, {1}", zero.X, zero.Y);
                    //System.Threading.Thread.Sleep(500);
                    var moveFrom = field.zeroTile.Add(KeyVector[e.KeyCode]);
                    field.Field[zero.X][zero.Y] = field.Field[moveFrom.X][moveFrom.Y];
                    field.zeroTile = moveFrom;
                    field.Field[moveFrom.X][moveFrom.Y] = 0;
                }
                this.Text = field.zeroTile.X.ToString() + ", " + field.zeroTile.Y.ToString();
            }
        }

       
        void Game_Paint(object sender, PaintEventArgs e)
        {
            Font drawFont = new Font("Arial", 32);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            var graphics=e.Graphics;
            graphics.FillRectangle(Brushes.Aqua,0,0,192,192);
            for (int i = 0; i < field.Field.Length; i++)
            {
                for (int j = 0; j < field.Field[i].Length; j++)
                {
                    if (field.Field[i][j] != 0)
                    {
                        graphics.DrawImage(Images[field.Field[i][j].ToString()], new Point(j * 64 + 1, i * 64 + 1));
                    }

                }
            }
        }

        int counter=0;
        void timer_Tick(object sender, EventArgs e)
        {
            if (Solving)
            {
                counter++;
                if (counter == 1)
                {
                    if (path.Count == 0)
                        Solving = false;
                    else 
                        field.Field = path.Dequeue();
                    counter = 0;
                }
            }
            Invalidate();
        }
    }
}
