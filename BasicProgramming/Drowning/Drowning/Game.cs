using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Drowning
{
    public enum Direction { Forward, Left, Right }
    class Game : Form
    {
        bool InMenu = true;
        public Timer timer;
        public PlayerPlane playerPlane { get; set; }
        public List<EnemyPlane> enemyPlanes { get; set; }
        public List<EnemyBullet> enemyBullets { get; set; }
        World world { get; set; }
        Dictionary<string, Image> Images;
        public bool IsOver;
        Spawner spawner { get; set; }
        PictureBox picBox = new PictureBox();

        public Game()
        {
            Images = Directory.EnumerateFiles(Application.StartupPath + "/images/", "*.png")
                        .ToDictionary(Path.GetFileNameWithoutExtension, Image.FromFile);

            this.ClientSize = new Size(new Point(1024,700));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += timerTick;
            timer.Start();

            
            Paint+=Menu_Paint;
            
            picBox.Image=Images["MenuButton"];
            picBox.Size = picBox.Image.Size;
            picBox.Location = new Point(300, 300);
            Controls.Add(picBox);
            picBox.Click +=picBox_Click;

            ResizeRedraw = true;
            DoubleBuffered = true;
            KeyPreview = true;

        }

        void picBox_Click(object sender, EventArgs e)
        {
            StartGame(this);
            Paint -= Menu_Paint;
            InMenu = false;
            picBox.Dispose();
        }

        void Menu_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(Images["MenuBack"],0,0);
        }


        static void StartGame(Game game)
        {
            game.playerPlane = new PlayerPlane(new Point(game.Size.Width / 2 - 20, game.Size.Height - 150));
            game.playerPlane.bullets = new List<PlayerBullet>();
            game.playerPlane.CanShoot = true;

            game.enemyPlanes = new List<EnemyPlane>();
            game.enemyBullets = new List<EnemyBullet>();
            game.spawner = new Spawner();

            game.KeyDown += new KeyEventHandler(game.playerPlane.Plane_KeyDown);
            game.KeyUp += new KeyEventHandler(game.playerPlane.Plane_KeyUp);

            game.world = new World();

            game.Paint += game.World_Paint;
            game.Paint += game.Game_Paint;
            game.Paint += game.EnemyPlane_Paint;
            game.Paint += game.Plane_Paint;
            game.Paint += game.Info_Paint;
        }
        void World_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            if (!IsOver)
            {
                graphics.FillRectangle(Brushes.Aqua, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
            if (IsOver)
                graphics.DrawImage(Images["GameOverScreen"], 0, 0);
            
        }

        void Info_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            if (!IsOver)
            {
                graphics.DrawImage(Images["InfoBar"],0,0);
                graphics.FillRectangle(Brushes.BlanchedAlmond, new Rectangle(650, 15, 110 * playerPlane.HP / 100, 20));

                graphics.FillRectangle(Brushes.BlanchedAlmond, new Rectangle(900, 15, 100 * world.HP / 200, 20));
                var font = new Font("Arial", 16);
                
                graphics.DrawString(world.Score.ToString(), font, Brushes.BlanchedAlmond, new Point(105, 10));

            }
        }

        void Game_Paint(object sender, PaintEventArgs e)
        {
            if (!IsOver)
            {
                var graphics = e.Graphics;
                foreach (var bullet in playerPlane.bullets)
                {
                    graphics.FillRectangle(Brushes.Black, bullet.Location.X, bullet.Location.Y, 2, 4);
                }
                foreach (var bullet in enemyBullets)
                {
                    graphics.FillRectangle(Brushes.Black, bullet.Location.X, bullet.Location.Y, 2, 4);
                }
            }   
        }

        public void Plane_Paint(object sender, PaintEventArgs e)
        {
            if (!IsOver)
            {
                var graphics = e.Graphics;

                graphics.DrawImage(Images["Ally"], playerPlane.fuselage);
                graphics.DrawImage(Images["AllyWing"], playerPlane.leftWing);
                graphics.DrawImage(Images["AllyWingRight"], playerPlane.rightWing);
            }
        }

        public void EnemyPlane_Paint(object sender, PaintEventArgs e)
        {
            if (!IsOver&&!InMenu)
            {
                var graphics = e.Graphics;
                foreach (var plane in enemyPlanes)
                {
                    graphics.DrawImage(Images["Enemy"], plane.fuselage);
                    graphics.DrawImage(Images["EnemyWing"], plane.leftWing);
                    graphics.DrawImage(Images["EnemyWingRight"], plane.rightWing);
                }
            }
        }

        void timerTick(object sender, EventArgs e)
        {
            if (!IsOver&&!InMenu)
            {
                for (int i = 0; i < playerPlane.bullets.Count; i++)
                {
                    playerPlane.bullets[i].Location = new Point(playerPlane.bullets[i].Location.X, playerPlane.bullets[i].Location.Y - 5);
                    if (playerPlane.bullets[i].Location.Y - 10 < 0)
                    {
                        playerPlane.bullets.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < enemyBullets.Count; i++)
                {
                    enemyBullets[i].Location = new Point(enemyBullets[i].Location.X, enemyBullets[i].Location.Y + 5);
                    if (enemyBullets[i].Location.Y - 10 < 0)
                    {
                        enemyBullets.RemoveAt(i);
                        i--;
                    }
                }
                playerPlane.Act(this);
                for (int i = 0; i < enemyPlanes.Count; i++)
                {
                    enemyPlanes[i].Act(this);
                    if (enemyPlanes[i].HP <= 0)
                    {
                        enemyPlanes.RemoveAt(i);
                        world.Score += 20;
                        continue;
                    }
                    if (enemyPlanes[i].Location.Y > this.Size.Height + 20)
                    {
                        world.HP -= 20;
                        enemyPlanes.RemoveAt(i);
                        continue;
                    }
                }
                spawner.Act(this);
                if (world.HP < 0)
                    IsOver = true;
            }

            Invalidate();
        }

        public void PressAnyKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            IsOver = false;

            StartGame(this);

            this.KeyDown -= new KeyEventHandler(this.PressAnyKey);
        }
    }
}