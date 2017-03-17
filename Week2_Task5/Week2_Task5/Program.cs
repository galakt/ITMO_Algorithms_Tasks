using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Task5
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
            var K = castedNumbers0[1];

            var numbers1 = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var castedNumbers1 = Array.ConvertAll<string, int>(numbers1, int.Parse);

            if (K == 1)
            {
                _sb.Append("YES");
                _sb.AppendLine();
            }
            else
            {
                var res = IsPugaloMergeSortPossible(castedNumbers1, K);
                _sb.Append(res ? "YES" : "NO");
                _sb.AppendLine();
            }
            
            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }

        private static bool IsPugaloMergeSortPossible(int[] castedNumbers, int k)
        {
            var flag = true;

            for (int i = 0; i < castedNumbers.Length - 1; i++)
            {
                var min = i;
                for (int j = i + 1; j < castedNumbers.Length; j++)
                {
                    if (castedNumbers[j] < castedNumbers[min])
                    {
                        min = j;
                    }
                }

                if ((min - i) % k != 0)
                {
                    flag = false;
                    break;
                }

                var tmp = castedNumbers[i];
                castedNumbers[i] = castedNumbers[min];
                castedNumbers[min] = tmp;

            }

            return flag;
            //var r = 1;

            //var sortedArray = new int[castedNumbers.Length];
            //for (int i = 0; i < castedNumbers.Length; i++)
            //{
            //    sortedArray[i] = castedNumbers[i];
            //}

            //Array.Sort(sortedArray);

            //var flag = true;

            //for (int i = 0; i < castedNumbers.Length; i++)
            //{
            //    if (sortedArray[i] == castedNumbers[i])
            //    {
            //        continue;
            //    }

            //    var tempIndex = i + k;
            //    if (tempIndex >= castedNumbers.Length)
            //    {
            //        flag = false;
            //        break;
            //    }
            //    while (tempIndex < castedNumbers.Length)
            //    {
            //        if (sortedArray[i] == castedNumbers[tempIndex])
            //        {
            //            break;
            //        }

            //        tempIndex += k;
            //        if (tempIndex >= castedNumbers.Length)
            //        {
            //            flag = false;
            //            break;
            //        }
            //    }
            //}

            //var r = 1;
        }

        //private static void MergeSortFunc(int[] ar, int l, int r, int[] tempAr, int k)
        //{
        //    if (r - l < k)
        //    {
        //        return;
        //    }

        //    var midDelta = (r - l) / 2;
        //    var nR = l + midDelta;
        //    var nL = nR + 1;

        //    MergeSortFunc(ar, l, nR, tempAr, k);
        //    MergeSortFunc(ar, nL, r, tempAr, k);

        //    Merge(ar, l, midDelta, r, tempAr, k);
        //}

        //private static void Merge(int[] ar, int l, int mid, int r, int[] tempAr, int k)
        //{
        //    var flag = false;
        //    var sortLenght = r - l + 1;
        //var tempLeft = l;
        //var tempRight = tempLeft + mid + 1;

        //for (var i = 0; i < sortLenght; i++)
        //{
        //    if (tempLeft <= l + mid && tempRight <= r && ar[tempLeft] <= ar[tempRight])
        //    {
        //        tempAr[i] = ar[tempLeft];
        //        tempLeft++;
        //    }
        //    else if (tempLeft <= l + mid && tempRight <= r && ar[tempLeft] > ar[tempRight])
        //    {
        //        tempAr[i] = ar[tempRight];
        //        tempRight++;
        //    }
        //    else if (tempLeft <= l + mid)
        //    {
        //        tempAr[i] = ar[tempLeft];
        //        tempLeft++;
        //    }
        //    else if (tempRight <= r)
        //    {
        //        tempAr[i] = ar[tempRight];
        //        tempRight++;
        //    }
        //}

        //    for (var i = 0; i < sortLenght; i++)
        //    {
        //        ar[l] = tempAr[i];
        //        l++;
        //    }
        //}
    }
}
