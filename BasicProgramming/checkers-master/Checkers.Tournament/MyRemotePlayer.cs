using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Checkers
{
    public class MyRemotePlayer
    {
        Process process;
        public MyRemotePlayer(string dllName, Color color)
        {
            process = new Process();
            process.StartInfo.FileName = "Checkers.Runner.exe";
            process.StartInfo.Arguments = dllName + " " + color.ToString();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = false;
            process.Start();
        }

        public List<Move> MakeTurn(Checker[,] field)
        {
            var fieldInString = Serializer.FieldToString(field);
            process.StandardInput.WriteLine(fieldInString);
            var movesInString = process.StandardOutput.ReadLine();
            Logs.AddToLog(process.StartInfo.Arguments.Split(' ')[1] + "'s turn: " + movesInString);
            if (movesInString == "White LOSE" || movesInString == "Black LOSE")
            {
                if (movesInString == "White LOSE")
                    Program.Winner--;
                else
                    Program.Winner++;
                Console.WriteLine(movesInString);
                Logs.AddToLog(movesInString);
                if (Program.GamesCount != Program.BestOf)
                {
                    Program.GamesCount++;
                    var thr = new Thread(Program.Gaming);
                    thr.Start();
                    Thread.CurrentThread.Abort();
                }
                else
                {
                    if (Program.Winner > 0)
                    {
                        var res = "WHITE WON THE GAME!!!";
                        Logs.AddToLog(res);
                        Console.WriteLine(res);
                        if (Program.usingTimer)
                            Console.WriteLine("White's longest move: {0}ms. Black's longest move: {1}ms.", Program.MaxWhite, Program.MaxBlack);
                    }
                    if (Program.Winner < 0)
                    {
                        var res = "BLACK WON THE GAME!!!";
                        Logs.AddToLog(res);
                        Console.WriteLine(res);
                        if (Program.usingTimer)
                            Console.WriteLine("White's longest move: {0}ms. Black's longest move: {1}ms.", Program.MaxWhite, Program.MaxBlack);
                    }
                    if (Program.Winner == 0)
                    {
                        var res = "DRAW!";
                        Logs.AddToLog(res);
                        Console.WriteLine(res);
                        if (Program.usingTimer)
                            Console.WriteLine("White's longest move: {0}ms. Black's longest move: {1}ms.", Program.MaxWhite, Program.MaxBlack);
                    }
                    Logs.Done();
                    Environment.Exit(0);
                }
            }
            return Serializer.StringToMoves(movesInString);
        }
    }
}
