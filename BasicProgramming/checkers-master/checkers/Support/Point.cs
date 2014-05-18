using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        public static bool operator ==(Point a, Point b)
        {
            if ((object)a == null)
                return (object)b == null;
            return a.Equals(b);
        }
        public override bool Equals(object a)
        {
            if (a == null || !(a is Point))
                return false;
            var point = (Point)a;
            return X == point.X && Y == point.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
