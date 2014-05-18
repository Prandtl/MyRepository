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
    public class Program
    {
        // эти две константы вы можете спокойно меня для удобства тестирования.
        public const int TimeOutOfMove = 100;  //задержка между ходами(для формочки).
        public const int BestOf = 9; //кол-во игр, по результатам которых будет определяться победитель дуэли. турнир будет проходить с числом 9.
        public static bool usingForm;
        public static int GamesCount = 1;
        public static int Winner; 
        static string firstPlayerFile;
        static string secondPlayerFile;
        static Form1 Window = new Form1();
        public static Thread thread;
        public static bool usingTimer;
        public static Stopwatch time;
        public static int MaxWhite;
        public static int MaxBlack;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 2 && args[2] == "true")
                usingForm = true;
            if (args.Length > 3 && args[3] == "timer")
                usingTimer = true;
            time = new Stopwatch();
            firstPlayerFile = args[0];
            secondPlayerFile = args[1];
            thread = new Thread(Gaming);
            thread.Start();
            if (usingForm)
                Application.Run(Window);
        }
        public static void Gaming()
        {
            Logs.AddToLog("Game #" + GamesCount + ". " + firstPlayerFile + "(White) versus " + secondPlayerFile + "(Black). Let the battle begin!");
            var movesCount = 0;
            var white = new MyRemotePlayer(firstPlayerFile, Color.White);
            var black = new MyRemotePlayer(secondPlayerFile, Color.Black);
            var validator = new Validator();
            var field = new Game().CreateMap();
            while (true)
            {
                movesCount++;
                if (movesCount > 150)
                {
                    Logs.AddToLog("i'm done. it's a draw");
                    if (GamesCount != BestOf)
                    {
                        GamesCount++;
                        var thr = new Thread(Gaming);
                        thr.Start();
                        Thread.CurrentThread.Abort();
                    }
                    else
                    {
                        Environment.Exit(0);
                        Logs.Done();
                    }
                }
                if (usingTimer && movesCount > 1)
                {
                    time.Reset();
                    time.Start();
                }
                validator.IsCorrectMove(white.MakeTurn(field), field, Color.White);
                if (usingTimer && movesCount > 1)
                {
                    time.Stop();
                    var ms = time.Elapsed.Milliseconds;
                    Logs.AddToLog(ms.ToString());
                    MaxWhite = Math.Max(MaxWhite, ms);
                }
                if (usingForm)
                {
                    Window.BeginInvoke(new Action<Checker[,]>(Window.Update), new object[] { field });
                    Thread.Sleep(TimeOutOfMove);
                }
                if (usingTimer && movesCount > 1)
                {
                    time.Reset();
                    time.Start();
                }
                validator.IsCorrectMove(black.MakeTurn(field), field, Color.Black);
                if (usingTimer && movesCount > 1)
                {
                    time.Stop();
                    var ms = time.Elapsed.Milliseconds;
                    Logs.AddToLog(ms.ToString());
                    MaxBlack = Math.Max(MaxBlack, ms);
                }
                if (usingForm)
                {
                    Window.BeginInvoke(new Action<Checker[,]>(Window.Update), new object[] { field });
                    Thread.Sleep(TimeOutOfMove);
                }
            }
        }
    }
}