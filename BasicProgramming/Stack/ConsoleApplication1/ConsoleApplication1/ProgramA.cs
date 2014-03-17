//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string s = Console.ReadLine();

//            if (s.Length % 1 == 1)
//                Console.WriteLine("Impossible");
//            else
//            {
//                char[] sorted = s.OrderBy(x => (int)x).ToArray();

//                var frequency = new Dictionary<char, int>();
//                char letter = sorted[0];
//                int amount = 0;
//                for (int i = 0; i < sorted.Length; i++)
//                {
//                    if (sorted[i] != letter)
//                    {
//                        frequency.Add(letter, amount);
//                        letter = sorted[i];
//                        amount = 0;
//                    }
//                    amount++;
//                    if (i == sorted.Length - 1)
//                    {
//                        frequency.Add(letter, amount);
//                    }
//                }
//                string firstHalf = "";
//                string secondHalf = "";
//                bool flag = true;
//                foreach (var e in frequency.OrderByDescending(x=>x.Key))
//                {
//                    if (e.Value + firstHalf.Length <= s.Length / 2)
//                    {
//                        for (int i = 0; i < e.Value; i++)
//                        {
//                            firstHalf += e.Key;
//                        }
//                    }
//                    else
//                    {
//                        if (e.Value + secondHalf.Length <= s.Length / 2)
//                        {
//                            for (int i = 0; i < e.Value; i++)
//                            {
//                                secondHalf += e.Key;
//                            }
//                        }
//                        else
//                        {
//                            int n = e.Value - s.Length / 2 + firstHalf.Length;
//                            for (int i = 0; i < n; i++)
//                            {
//                                firstHalf += e.Key;
//                            }
//                            for (int i = 0; i < e.Value - n; i++)
//                            {
//                                secondHalf += e.Key;
//                            }


//                        }
//                    }
//                }
//                if (!flag)
//                    Console.WriteLine("Impossible");
//                else
//                    Console.WriteLine(firstHalf + secondHalf);
//            }

//        }
//    }
//}
