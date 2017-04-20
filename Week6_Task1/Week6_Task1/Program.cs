using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6_Task1
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
            var N = int.Parse(lines[0]);

            var numbers1 = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var a = Array.ConvertAll<string, int>(numbers1, int.Parse);

            var M = int.Parse(lines[2]);

            var numbers2 = lines[3].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var tasks = Array.ConvertAll<string, int>(numbers2, int.Parse);

            foreach (var task in tasks)
            {
                FindStartEnd(a, task);
            }

            var s = _sb.ToString();
            System.IO.File.WriteAllText(path: outFilePath, contents: s);
        }

        private static void FindStartEnd(int[] numbers, int searchItem)
        {
            var start = FindFirstIndex(numbers, 0, numbers.Length, searchItem);
            var end = FindLastIndex(numbers, 0, numbers.Length, searchItem);
            
            _sb.AppendFormat("{0} {1}",start == -1 ? -1 : start + 1, end == -1 ? -1 : end + 1);
            _sb.AppendLine();
        }

        private static int FindFirstIndex(int[] numbers, int start, int end, int searchItem)
        {
            if (start >= end)
            {
                return -1;
            }
            var midIndex = start + (end - start) / 2;
            var midItem = numbers[midIndex];

            if (midItem == searchItem)
            {
                if (midIndex == 0 || numbers[midIndex - 1] < searchItem)
                {
                    return midIndex;
                }
            }

            if (searchItem > midItem)
            {
                return FindFirstIndex(numbers, midIndex == start ? midIndex + 1 : midIndex, end, searchItem);
            }
            else
            {
                return FindFirstIndex(numbers, start, midIndex, searchItem);
            }
        }

        private static int FindLastIndex(int[] numbers, int start, int end, int searchItem)
        {
            if (start >= end)
            {
                return -1;
            }
            var midIndex = start + (end - start) / 2;
            var midItem = numbers[midIndex];

            if (midItem == searchItem)
            {
                if (midIndex == numbers.Length -1 || numbers[midIndex + 1] > searchItem)
                {
                    return midIndex;
                }
            }

            if (searchItem >= midItem)
            {
                return FindLastIndex(numbers, midIndex == start ? midIndex + 1 : midIndex, end, searchItem);
            }
            else
            {
                return FindLastIndex(numbers, start, midIndex, searchItem);
            }
        }
    }
}
