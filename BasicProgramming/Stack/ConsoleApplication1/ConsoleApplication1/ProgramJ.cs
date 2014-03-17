//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication1
//{
//    class Program
//    {
//        static int Sum(int a)
//        {
//            int sum = 0;
//            while (a / 10 != 0)
//            {
//                sum += a % 10;
//                a = a / 10;
//            }
//            return sum + a;
//        }
//        static void Main(string[] args)
//        {
//            string[] s = Console.ReadLine().Split(' ').ToArray();
//            int n = int.Parse(s[0]);
//            int k = int.Parse(s[1]);
//            int[] sorted = Enumerable.Range(1, n).Select(x => Sum(x)).ToArray();
//        }
//    }
//}
