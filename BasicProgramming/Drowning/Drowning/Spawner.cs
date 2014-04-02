using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drowning
{
    class Spawner
    {
        Random ran = new Random();
        int counter=0;
        public void Act(Game game)
        {

            if (game.enemyPlanes.Count < 4)
            {
                if (counter == 0)
                {
                    game.enemyPlanes.Add(new EnemyPlane(new Point(55 + ran.Next(876), -100)));
                    counter = ran.Next(200);
                }
            }
            if (counter > 0)
                counter--;
        }
    }
}
