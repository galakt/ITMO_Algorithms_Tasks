using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Task2
{
    class Program
    {
        private const string InFileName = "input.txt";
        private const string OutFileName = "output.txt";
        private static readonly StringBuilder _sb = new StringBuilder();
        private static Int64 inversionCount = 0;

        static void Main(string[] args)
        {
            var inFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, InFileName);
            var outFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, OutFileName);

            var lines = System.IO.File.ReadAllLines(inFilePath);
            var numbersCount = int.Parse(lines[0]);
            var numbers = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var castedNumbers = Array.ConvertAll<string, Decimal>(numbers, Decimal.Parse);

            MergeSort(castedNumbers);

            _sb.Append(inversionCount);
            _sb.AppendLine();

            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }

        private static void MergeSort(decimal[] castedNumbers)
        {
            var tempAr = new decimal[castedNumbers.Length];
            MergeSortFunc(castedNumbers, 0, castedNumbers.Length - 1, tempAr);
        }

        private static void MergeSortFunc(decimal[] ar, int l, int r, decimal[] tempAr)
        {
            if (l == r)
            {
                return;
            }

            var mid = (r - l) / 2;
            var nR = l + mid;
            var nL = nR + 1;

            MergeSortFunc(ar, l, nR, tempAr);
            MergeSortFunc(ar, nL, r, tempAr);

            Merge(ar, l, mid, r, tempAr);
        }

        private static void Merge(decimal[] ar, int l, int mid, int r, decimal[] tempAr)
        {
            var sortLenght = r - l + 1;
            var tempLeft = l;
            var tempRight = tempLeft + mid + 1;

            for (var i = 0; i < sortLenght; i++)
            {
                if (tempLeft <= l + mid && tempRight <= r && ar[tempLeft] <= ar[tempRight])
                {
                    tempAr[i] = ar[tempLeft];
                    tempLeft++;
                }
                else if (tempLeft <= l + mid && tempRight <= r && ar[tempLeft] > ar[tempRight])
                {
                    tempAr[i] = ar[tempRight];
                    inversionCount += l + mid - tempLeft + 1;
                    tempRight++;
                }
                else if (tempLeft <= l + mid)
                {
                    tempAr[i] = ar[tempLeft];
                    tempLeft++;
                }
                else if (tempRight <= r)
                {
                    tempAr[i] = ar[tempRight];
                    tempRight++;
                }
            }

            for (var i = 0; i < sortLenght; i++)
            {
                ar[l] = tempAr[i];
                l++;
            }
        }
    }
}
