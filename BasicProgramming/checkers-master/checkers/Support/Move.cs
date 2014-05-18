using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Move
    {
        public Move(Point from, Point to)
        {
            this.From = from;
            this.To = to;
        }
        public Point From
        {
            get;
            set;
        }
        public Point To
        {
            get;
            set;
        }
        public static bool operator ==(Move a, Move b)
        {
            if ((object)a == null)
                return (object)b == null;
            return a.Equals(b);
        }
        public override bool Equals(object a)
        {
            if (a == null || !(a is Move))
                return false;
            var move = (Move)a;
            return From == move.From && To == move.To;
        }
        public static bool operator !=(Move a, Move b)
        {
            return !(a==b);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
