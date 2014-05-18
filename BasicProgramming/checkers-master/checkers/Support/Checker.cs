using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public enum Color {Black, White, Beated};
    public class Checker
    {
        public Checker(Color color, bool isQueen)
        {
            this.Color = color;
            this.IsQueen = isQueen;
        }
        public bool IsQueen
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            private set;
        }
        public static bool operator ==(Checker a, Checker b)
        {
            if ((object)a == null) 
                return (object)b == null;
            return a.Equals(b);
        }
        public override bool Equals(object a)
        {
            if (a == null || !(a is Checker))
                return false;
            var checker = (Checker)a;
            return IsQueen == checker.IsQueen && Color == checker.Color;
        }
        public static bool operator !=(Checker a, Checker b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
