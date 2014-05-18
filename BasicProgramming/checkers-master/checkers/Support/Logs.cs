using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Checkers
{
    public static class Logs //это логи. в них ничего писать нельзя. открывать через sublime или notepad++
    {
        static StreamWriter file;
        static string logs = "";
        public static void AddToLog(string str)
        {
            logs += str;
            logs += '\n';
        }
        public static void Done()
        {
            file = File.CreateText("Logs.txt");
            file.Write(logs);
            file.Close();
        }
    }
}
