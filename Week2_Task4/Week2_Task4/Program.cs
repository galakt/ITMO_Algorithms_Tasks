using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Task4
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
            var numbers0 = lines[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var castedNumbers0 = Array.ConvertAll<string, int>(numbers0, int.Parse);
            var N = castedNumbers0[0];
            var K1 = castedNumbers0[1];
            var K2 = castedNumbers0[2];

            var numbers1 = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var castedNumbers1 = Array.ConvertAll<string, int>(numbers1, int.Parse);
            var A = castedNumbers1[0];
            var B = castedNumbers1[1];
            var C = castedNumbers1[2];
            var generatedArray = new int[N];
            generatedArray[0] = castedNumbers1[3];
            generatedArray[1] = castedNumbers1[4];

            for (int i = 2; i < N; i++)
            {
                generatedArray[i] = A * generatedArray[i - 2] + B * generatedArray[i - 1] + C;
            }

            QSort(generatedArray, 0, generatedArray.Length - 1, K1, K2);

            for (int i = K1 - 1; i < K2; i++)
            {
                _sb.AppendFormat("{0} ", generatedArray[i]);
            }
            _sb.AppendLine();

            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }

        private static void QSort(int[] a, int l, int r, int k1, int k2)
        {
            int mid = a[l + (r-l)/2];
            int i = l;
            int j = r;

            if (k2 - 1 < l || k1 - 1 > r)
            {
                return;
            }

            while (i <= j)
            {
                while (a[i] < mid)
                {
                    i++;
                }
                while (a[j] > mid)
                {
                    j--;
                }
                if (i <= j)
                {
                    Swap(a, i, j);
                    i++;
                    j--;
                }
            }

            if (i < r)
            {
                QSort(a, i, r, k1, k2);
            }
            if (j > l)
            {
                QSort(a, l, j, k1, k2);
            }

        }

        private static void Swap(int[] a, int i, int j)
        {
            var tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }
    }
}
