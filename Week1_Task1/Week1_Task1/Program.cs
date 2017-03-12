using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1_Task1
{
    class Program
    {
        private const string InFileName = "input.txt";
        private const string OutFileName = "output.txt";

        static void Main(string[] args)
        {
            var inputStr = System.IO.File.ReadAllText(path: System.IO.Path.Combine(Environment.CurrentDirectory, InFileName));
            var res = 0;
            foreach (var item in inputStr.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries))
            {
                res += int.Parse(item);
            }

            System.IO.File.WriteAllText(path: System.IO.Path.Combine(Environment.CurrentDirectory, OutFileName), contents: res.ToString());
        }
    }
}
