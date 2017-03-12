using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1_Task3
{
    class Program
    {
        private const string InFileName = "input.txt";
        private const string OutFileName = "output.txt";
        private static readonly StringBuilder _sb = new StringBuilder();

        static void Main(string[] args)
        {
            var inFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, InFileName);
            var outFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, OutFileName);

            var lines = System.IO.File.ReadAllLines(inFilePath);

            var arrSize = int.Parse(lines[0]);
            var numbers = Array.ConvertAll(lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            var swapLog = new int[arrSize];
            swapLog[0] = 1;

            for (int i = 1; i <= numbers.Length - 1; i++)
            {
                var j = i;
                while (j > 0 && numbers[j] < numbers[j-1])
                {
                    var tmp = numbers[j];
                    numbers[j] = numbers[j - 1];
                    numbers[j - 1] = tmp;
                    j--;
                }
                swapLog[i] = j + 1;
            }

            foreach (var item in swapLog)
            {
                _sb.Append(item);
                _sb.Append(" ");
            }

            _sb.Append(Environment.NewLine);

            foreach (var item in numbers)
            {
                _sb.Append(item);
                _sb.Append(" ");
            }

            _sb.Append(Environment.NewLine);

            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }
    }
}
