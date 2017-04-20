using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3_Task1
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

            var n = int.Parse(lines[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]);
            var m = int.Parse(lines[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1]);

            var c = new int[n * m];
            var result = new int[n * m];

            var numbersA = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var a = Array.ConvertAll<string, int>(numbersA, int.Parse);

            var numbersB = lines[2].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var b = Array.ConvertAll<string, int>(numbersB, int.Parse);
            
            var ci = 0;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    c[ci] = a[i] * b[j];
                    ci++;
                }
            }

            var histo = new Dictionary<int,int>();

            for (int i = 0; i < c.Length; i++)
            {
                if (histo.ContainsKey(c[i]))
                {
                    histo[c[i]] += 1;
                }
                else
                {
                    histo.Add(c[i], 1);
                }
            }

            var total = 0;
            foreach (var keyValuePair in histo.OrderBy(p=> p.Key))
            {
                var count = keyValuePair.Value;
                histo[keyValuePair.Key] = total;
                total += count;
            }

            for (int i = 0; i < c.Length; i++)
            {
                result[histo[c[i]]] = c[i];
                histo[c[i]] += 1;
            }


            var summ = 0;
            var ti = 0;
            while (ti < result.Length)
            {
                summ += result[ti];
                ti += 10;
            }

            System.IO.File.WriteAllText(path: outFilePath, contents: summ.ToString());
        }
    }
}
