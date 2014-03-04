using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVS
{
    class Clones
    {
        private List<int> Clone = new List<int>() {0};

        private List<Element> Actions = new List<Element>() { new Element() { Previous = 0, Next = -1, Program = 0 } };

        private class Element
        {
            public int Previous { get; set; }
            public int Next { get; set; }
            public int Program { get; set; }
        }

        public void Learn(int inputNumber, int program)
        {
            int number = inputNumber - 1;
            Actions.Add(new Element() { Previous = Clone[number], Next = -1, Program = program });
            Actions[Clone[number]].Next = Actions.Count - 1;
            Clone[number] = Actions.Count - 1;
        }

        public void RollBack(int inputNumber)
        {
            int number = inputNumber - 1;
            Clone[number] = Actions[Clone[number]].Previous;
        }

        public void Relearn(int inputNumber)
        {
            int number = inputNumber - 1;
            Clone[number] = Actions[Clone[number]].Next;
        }

        public void CloneTheClone(int inputNumber)
        {
            int number = inputNumber - 1;
            Element original=Actions[Clone[number]];
            Actions.Add(new Element() { Previous = original.Previous, Program = original.Program, Next = original.Next });
            Clone.Add(Actions.Count - 1);
        }

        public int Check(int inputNumber)
        {
            int number = inputNumber - 1;
            return Actions[Clone[number]].Program;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int n = int.Parse(s[0]);
            int m = int.Parse(s[1]);
            Clones clones=new Clones();
            for (int i = 0; i < n; i++)
            {
                int program;
                s = Console.ReadLine().Split(' ');
                if (s[0] == "learn")
                    clones.Learn(int.Parse(s[1]), int.Parse(s[2]));
                if (s[0] == "rollback")
                    clones.RollBack(int.Parse(s[1]));
                if (s[0] == "relearn")
                    clones.Relearn(int.Parse(s[1]));
                if (s[0] == "clone")
                    clones.CloneTheClone(int.Parse(s[1]));
                if (s[0] == "check")
                {
                    program = clones.Check(int.Parse(s[1]));
                    if (program == 0) Console.WriteLine("basic");
                    else Console.WriteLine(program);
                }
            }
        }
    }
}
