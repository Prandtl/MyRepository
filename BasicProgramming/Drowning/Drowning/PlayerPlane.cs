using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drowning
{
    class PlayerPlane
    {
        public Point Location;

        public Rectangle fuselage;
        public Rectangle leftWing;
        public Rectangle rightWing;

        public int HP { get; set; }

        public Direction PlaneDirection { get; set; }

        public PlayerPlane(Point StartingLoc)
        {
            Location = StartingLoc;

            fuselage = new Rectangle(Location, new Size(40, 100));
            leftWing = new Rectangle(Location.X - 55, Location.Y + 25, 55, 25);
            rightWing = new Rectangle(Location.X + 40, Location.Y + 25, 55, 25);

            HP = 100;
        }

        void ChangeLocation()
        {
            switch (PlaneDirection)
            {
                case Direction.Left:
                    fuselage = new Rectangle(Location, new Size(40, 100));
                    leftWing = new Rectangle(Location.X - 40, Location.Y + 25, 40, 25);
                    rightWing = new Rectangle(Location.X + 40, Location.Y + 25, 40, 25);
                    break;
                case Direction.Forward:
                    fuselage = new Rectangle(Location, new Size(40, 100));
                    leftWing = new Rectangle(Location.X - 55, Location.Y + 25, 55, 25);
                    rightWing = new Rectangle(Location.X + 40, Location.Y + 25, 55, 25);
                    break;
                case Direction.Right:
                    fuselage = new Rectangle(Location, new Size(40, 100));
                    leftWing = new Rectangle(Location.X - 40, Location.Y + 25, 40, 25);
                    rightWing = new Rectangle(Location.X + 40, Location.Y + 25, 40, 25);
                    break;
            }

            
        }
        


        public void Plane_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && Location.X>65)
            {
                Location.X-=5;
                PlaneDirection = Direction.Left;
            }
            if (e.KeyCode == Keys.D && Location.X < Game.ActiveForm.ClientSize.Width-105)
            {
                Location.X += 5;
                PlaneDirection = Direction.Right;
            }
            if (e.KeyCode == Keys.W && Location.Y > 240)
            {
                Location.Y -= 2;
                PlaneDirection = Direction.Forward;
            }
            if (e.KeyCode == Keys.S && Location.Y < Game.ActiveForm.ClientSize.Height-110)
            {
                Location.Y += 2;
                PlaneDirection = Direction.Forward;
            }

            if (e.KeyCode == Keys.Space && CanShoot)
            {
                bullets.Add(new PlayerBullet(new Point(Location.X - 30, Location.Y + 25)));
                bullets.Add(new PlayerBullet(new Point(Location.X + 70, Location.Y + 25)));
                CanShoot = false;
            }
            ChangeLocation();

        }

        public void Plane_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            PlaneDirection = Direction.Forward;
            ChangeLocation();
        }

        public List<PlayerBullet> bullets { get; set; }

        public bool CanShoot { get; set; }

        int counter = 0;
        public void Act(Game game)
        {
            if (CanShoot == false)
            {
                counter++;
                if (counter % 10 == 0)
                {
                    counter = 0;
                    CanShoot = true;
                }
            }

            var bullets = game.enemyBullets;
            for (int i = 0; i < bullets.Count; i++)
            {
                if (fuselage.IntersectsWith(new Rectangle(bullets[i].Location.X, bullets[i].Location.Y, 2, 4)))
                {
                    HP -= 15;
                    game.enemyBullets.RemoveAt(i);
                    continue;
                }
                if (leftWing.IntersectsWith(new Rectangle(bullets[i].Location.X, bullets[i].Location.Y, 2, 4)))
                {
                    HP -= 10;
                    game.enemyBullets.RemoveAt(i);
                    continue;
                }
                if (rightWing.IntersectsWith(new Rectangle(bullets[i].Location.X, bullets[i].Location.Y, 2, 4)))
                {
                    HP -= 10;
                    game.enemyBullets.RemoveAt(i);
                    continue;
                }
                if (HP <= 0)
                {
                    game.IsOver = true;
                    game.KeyDown += new KeyEventHandler(game.PressAnyKey);
                }
            }
        }

    }

    public class PlayerBullet
    {
        public Point Location { get; set; }

        public PlayerBullet(Point point)
        {
            Location = point;
        }
    }
}
