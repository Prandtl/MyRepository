//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication1
//{
//    class Program
//    {
//        static int GetDecimalMeaning(int[] number)
//        {
//            int max = number.Max();
//            int maxIndex=0;
//            for (int i = 0; i < number.Length; i++)
//            {
//                if (number[i] == max)
//                {
//                    maxIndex = i;
//                    break;
//                }
//            }
//            int right = 0;
//            int left=0;
//            if (maxIndex < number.Length)
//                right = GetDecimalMeaning(number.Where((x, i) => i > maxIndex).ToArray());
//            if (maxIndex > 0)
//                left = GetDecimalMeaning(number.Where((x, i) => i < maxIndex).ToArray());
//            return max + right - left;
//        }


//        static void Main(string[] args)
//        {
            
            
//        }
//    }
//}
