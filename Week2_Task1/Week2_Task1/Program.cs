using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Task1
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
            var numbersCount = int.Parse(lines[0]);
            var numbers = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var castedNumbers = Array.ConvertAll<string, Decimal>(numbers, Decimal.Parse);

            MergeSort(castedNumbers);

            foreach (var item in castedNumbers)
            {
                _sb.Append(item);
                _sb.Append(" ");
            }

            _sb.Append(Environment.NewLine);
            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }

        private static void MergeSort(decimal[] castedNumbers)
        {
            MergeSortFunc(castedNumbers, 0, castedNumbers.Length - 1);
        }

        private static void MergeSortFunc(decimal[] ar, int l, int r)
        {
            //Console.WriteLine($"{l} {r}");

            if (l == r)
            {
                return;
            }

            var mid = (r - l) / 2;
            var nR = l + mid;
            var nL = nR + 1;

            MergeSortFunc(ar, l, nR);
            MergeSortFunc(ar, nL, r);

            Merge(ar, l, mid, r);
        }

        private static void Merge(decimal[] ar, int l, int mid, int r)
        {
            var sortLenght = r - l + 1;
            var tempAr = new decimal[sortLenght];
            var tempLeft = l;
            var tempRight = tempLeft + mid + 1;

            for (var i = 0; i < sortLenght; i++)
            {
                if (tempLeft <= l + mid && tempRight <= r && ar[tempLeft] <= ar[tempRight])
                {
                    tempAr[i] = ar[tempLeft];
                    tempLeft++;
                }
                else if(tempLeft <= l + mid && tempRight <= r && ar[tempLeft] > ar[tempRight])
                {
                    tempAr[i] = ar[tempRight];
                    tempRight++;
                }
                else if(tempLeft <= l + mid)
                {
                    tempAr[i] = ar[tempLeft];
                    tempLeft++;
                }
                else if(tempRight <= r)
                {
                    tempAr[i] = ar[tempRight];
                    tempRight++;
                }
            }

            _sb.AppendFormat("{0} {1} {2} {3}", l + 1, r + 1, tempAr[0], tempAr[tempAr.Length - 1]);
            _sb.AppendLine();

            for (var i = 0; i < sortLenght; i++)
            {
                ar[l] = tempAr[i];
                l++;
            }
        }
    }
}
