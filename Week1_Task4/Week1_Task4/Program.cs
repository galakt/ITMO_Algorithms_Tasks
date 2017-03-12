using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1_Task4
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

            var numbers = lines[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var workers = new Worker[arrSize];

            var ci = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ".";

            for (int i = 0; i < numbers.Length; i++)
            {
                workers[i] = new Worker(id: i + 1, salary: decimal.Parse(numbers[i], ci));
            }
            
            for (int i = 1; i <= workers.Length - 1; i++)
            {
                var j = i;
                while (j > 0 && workers[j] < workers[j - 1])
                {
                    var tmp = workers[j];
                    workers[j] = workers[j - 1];
                    workers[j - 1] = tmp;
                    j--;
                }
            }

            var mid = arrSize / 2;
            _sb.AppendFormat("{0} {1} {2}", workers[0].Id, workers[mid].Id, workers[arrSize - 1].Id);
            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }

        public struct Worker : IComparable<Worker>
        {
            private int _id;
            private decimal _salary;

            public Worker(int id, decimal salary)
            {
                _id = id;
                _salary = salary;
            }

            public int Id { get { return _id; } }

            public decimal Salary { get { return _salary; } }

            public int CompareTo(Worker other)
            {
                if(this.Salary > other.Salary)
                {
                    return 1;
                }
                else if(this.Salary < other.Salary)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            public static bool operator >(Worker operand1, Worker operand2)
            {
                return operand1.CompareTo(operand2) == 1;
            }

            // Define the is less than operator.
            public static bool operator <(Worker operand1, Worker operand2)
            {
                return operand1.CompareTo(operand2) == -1;
            }

            // Define the is greater than or equal to operator.
            public static bool operator >=(Worker operand1, Worker operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            // Define the is less than or equal to operator.
            public static bool operator <=(Worker operand1, Worker operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }
        }
    }
}
