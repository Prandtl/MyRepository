using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public class Program
    {
        public static Random Rand = new Random();
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new MyForm(new Game().CreateMap()));
        }
    }
}
