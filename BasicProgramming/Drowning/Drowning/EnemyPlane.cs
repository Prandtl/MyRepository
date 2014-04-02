using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drowning
{
    class EnemyPlane
    {
        public Point Location;

        public Rectangle fuselage;
        public Rectangle leftWing;
        public Rectangle rightWing;

        public int HP { get; set; }

        public Direction PlaneDirection { get; set; }

        public EnemyPlane(Point StartingLoc)
        {
            Location = StartingLoc;

            fuselage = new Rectangle(Location, new Size(40, 100));
            leftWing = new Rectangle(Location.X - 55, Location.Y + 50, 55, 25);
            rightWing = new Rectangle(Location.X + 40, Location.Y + 50, 55, 25);

            HP = 10;
        }

        void ChangeLocation()
        {
            switch (PlaneDirection)
            {
                case Direction.Left:
                    fuselage = new Rectangle(Location, new Size(40, 100));
                    leftWing = new Rectangle(Location.X - 40, Location.Y + 50, 40, 25);
                    rightWing = new Rectangle(Location.X + 40, Location.Y + 50, 40, 25);
                    break;
                case Direction.Forward:
                    fuselage = new Rectangle(Location, new Size(40, 100));
                    leftWing = new Rectangle(Location.X - 55, Location.Y + 50, 55, 25);
                    rightWing = new Rectangle(Location.X + 40, Location.Y + 50, 55, 25);
                    break;
                case Direction.Right:
                    fuselage = new Rectangle(Location, new Size(40, 100));
                    leftWing = new Rectangle(Location.X - 40, Location.Y + 50, 40, 25);
                    rightWing = new Rectangle(Location.X + 40, Location.Y + 50, 40, 25);
                    break;
            }
        }



        int counter=0;
        bool canShoot;
        public void Act(Game game)
        {
            var bullets = game.playerPlane.bullets;
            for (int i = 0; i < bullets.Count; i++)
            {
                if (fuselage.IntersectsWith(new Rectangle(bullets[i].Location.X, bullets[i].Location.Y, 2, 4)))
                {
                    HP -= 15;
                    game.playerPlane.bullets.RemoveAt(i);
                    continue;
                }
                if (leftWing.IntersectsWith(new Rectangle(bullets[i].Location.X, bullets[i].Location.Y, 2, 4)))
                {
                    HP -= 10;
                    game.playerPlane.bullets.RemoveAt(i);
                    continue;
                }
                if (rightWing.IntersectsWith(new Rectangle(bullets[i].Location.X, bullets[i].Location.Y, 2, 4)))
                {
                    HP -= 10;
                    game.playerPlane.bullets.RemoveAt(i);
                    continue;
                }
            }

            Location.Y += 2;
            ChangeLocation();
            if (canShoot == false)
            {
                counter++;
                if (counter % 50 == 0)
                {
                    counter = 0;
                    canShoot = true;
                }
            }
            if (Math.Abs(Location.X - game.playerPlane.Location.X) < 100 && canShoot)
            {
                game.enemyBullets.Add(new EnemyBullet(new Point(Location.X - 30, Location.Y + 75)));
                game.enemyBullets.Add(new EnemyBullet(new Point(Location.X + 70, Location.Y + 75)));
                canShoot = false;
            }
            
        }
    }

    public class EnemyBullet
    {
        public Point Location { get; set; }

        public EnemyBullet(Point point)
        {
            Location = point;
        }
    }
}
