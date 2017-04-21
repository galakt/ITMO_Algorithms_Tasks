using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6_Task3
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
            if (N == 0)
            {
                System.IO.File.WriteAllText(path: outFilePath, contents: "0");
            }
            else
            {
                var r = GetMaxDepthIter(lines, 1); //GetMaxDepthReq(lines, 1);
                System.IO.File.WriteAllText(path: outFilePath, contents: r.ToString());
            }
        }

        private static int GetMaxDepthReq(string[] nodes, int nodeIndex)
        {
            if (nodeIndex == 0)
            {
                return 0;
            }

            var nodeParams = Array.ConvertAll(nodes[nodeIndex].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            var l = GetMaxDepthReq(nodes, nodeParams[1]);
            var r = GetMaxDepthReq(nodes, nodeParams[2]);

            return Math.Max(l, r) + 1;
        }

        private static int GetMaxDepthIter(string[] nodes, int nodeIndex)
        {
            var q = new Queue<string>();
            
            q.Enqueue(nodes[nodeIndex]);
            var height = 0;

            while (true)
            {
                var levelNodeCount = q.Count;
                if (levelNodeCount == 0)
                {
                    return height;
                }
                height++;

                while (levelNodeCount > 0)
                {
                    var currentNode = q.Dequeue();
                    var parsedCurrentNode = Array.ConvertAll(currentNode.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

                    if (parsedCurrentNode[1] != 0)
                    {
                        q.Enqueue(nodes[parsedCurrentNode[1]]);
                    }

                    if (parsedCurrentNode[2] != 0)
                    {
                        q.Enqueue(nodes[parsedCurrentNode[2]]);
                    }

                    levelNodeCount--;
                }
            }
        }
    }
}
