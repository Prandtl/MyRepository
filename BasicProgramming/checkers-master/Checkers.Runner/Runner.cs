using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var way = Environment.CurrentDirectory;
            var ans = "";
            for (var i = 0; i < way.Length - 29; i++)
                ans += way[i];
            ans += "TestPlayer\\bin\\Debug\\";
            var assembly = Assembly.LoadFrom(ans + args[0]);
            Color color = args[1] == "White" ? Color.White : Color.Black;
            var player = assembly
                .GetTypes()
                .Where(z => z.GetInterfaces().Any(x => x == typeof(IPlayer)))
                .FirstOrDefault();
            var ctor = player
                .GetConstructor(new Type[] { });
            var playerObject = ctor
                .Invoke(new object[] { }) as IPlayer;
            playerObject.Initialize(color);
            while (true)
            {
                var str = Console.ReadLine();
                if (str == null)
                    Environment.Exit(0);
                var field = Serializer.StringToField(str);
                var moves = playerObject.MakeTurn(field);
                if (moves == null || moves.Count == 0)
                {
                    Console.WriteLine(color.ToString() + " LOSE");
                    Environment.Exit(0);
                }
                var answer = Serializer.MovesToString(moves);
                Console.WriteLine(answer);
            }
        }
    }
}
