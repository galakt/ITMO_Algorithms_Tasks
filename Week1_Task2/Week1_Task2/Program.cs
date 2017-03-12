using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1_Task2
{
    class Program
    {
        private const string InFileName = "input.txt";
        private const string OutFileName = "output.txt";

        static void Main(string[] args)
        {
            var inFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, InFileName);
            var outFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, OutFileName);

            var numbers = System.IO.File.ReadAllText(inFilePath).Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var castedNumbers = Array.ConvertAll<string, Decimal>(numbers, Decimal.Parse);

            var res = castedNumbers[0] + castedNumbers[1] * castedNumbers[1];

            System.IO.File.WriteAllText(path: outFilePath, contents: res.ToString());
        }
    }
}
