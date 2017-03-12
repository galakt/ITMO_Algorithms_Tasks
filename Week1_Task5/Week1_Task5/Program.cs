using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1_Task5
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

            var numbersAsStr = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var numbers = Array.ConvertAll(numbersAsStr, decimal.Parse);

            for (int i = 0; i < numbers.Length; i++)
            {
                var min = numbers[i];
                var swapIndex = -1;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if(numbers[j] < min)
                    {
                        min = numbers[j];
                        swapIndex = j;
                    }
                }

                if(swapIndex != -1)
                {
                    var tmp = numbers[i];
                    numbers[i] = numbers[swapIndex];
                    numbers[swapIndex] = tmp;

                    if (i > swapIndex)
                    {
                        _sb.AppendFormat("Swap elements at indices {0} and {1}.", swapIndex + 1, i + 1);
                        _sb.AppendLine();
                    }
                    else if (i < swapIndex)
                    {
                        _sb.AppendFormat("Swap elements at indices {0} and {1}.", i + 1, swapIndex + 1);
                        _sb.AppendLine();
                    }
                }
            }
            
            _sb.Append("No more swaps needed.");
            _sb.AppendLine();

            foreach (var item in numbers)
            {
                _sb.Append(item);
                _sb.Append(" ");
            }

            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }
    }
}
