using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6_Task4
{
    class Program
    {
        private const string InFileName = "input.txt";
        private const string OutFileName = "output.txt";

        static void Main(string[] args)
        {
            var inFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, InFileName);
            var outFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, OutFileName);

            var lines = System.IO.File.ReadAllLines(inFilePath);

            var N = int.Parse(lines[0]);

            var binTree = GenerateTest42();//ParseLinesToTree(lines, 1, N);

            var r = binTree.GetHeightIter().ToString();
            System.IO.File.WriteAllText(path: outFilePath, contents: r);

            //if (N == 0)
            //{
            //    System.IO.File.WriteAllText(path: outFilePath, contents: "0");
            //}
            //else
            //{
            //    var r = GetMaxDepthIter(lines, 1);
            //    System.IO.File.WriteAllText(path: outFilePath, contents: r.ToString());
            //}
        }

        private static BinaryTree<int> GenerateTest42()
        {
            var bt = new BinaryTree<int> {Root = new Node<int>()};

            var last = bt.Root;
            for (int i = 0; i < 120000; i++)
            {
                last.Left = new Node<int>();
                last = last.Left;
            }

            return bt;
        }

        private static BinaryTree<int> ParseLinesToTree(string[] lines, int startIndex, int nodesCount)
        {
            if (nodesCount == 0)
            {
                return new BinaryTree<int>();
            }

            return new BinaryTree<int>
            {
                Root = ParseLineIntoNode(lines, startIndex)
            };
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

        private static Node<int> ParseLineIntoNode(string[] lines, int nodeIndex)
        {
            if (nodeIndex == 0)
            {
                return null;
            }

            var rootNodeParams = Array.ConvertAll(lines[nodeIndex].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            return new Node<int>
            {
                Data = rootNodeParams[0],
                Left = ParseLineIntoNode(lines, rootNodeParams[1]),
                Right = ParseLineIntoNode(lines, rootNodeParams[2])
            };
        }

#if DEBUG
        [DebuggerDisplay("Data = {" + nameof(Data) + "}")]
#endif
        public class Node<T>
        {
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }

            public T Data { get; set; }

            public Node()
            {
                Left = null;
                Right = null;
            }

            public Node(T data) : this()
            {
                this.Data = data;
            }

            public int GetSize()
            {
                //return 1 + (Left?.GetSize() ?? 0) + (Right?.GetSize() ?? 0);

                var s = 1;
                if (Left != null)
                {
                    s += Left.GetSize();
                }
                if (Right != null)
                {
                    s += Right.GetSize();
                }
                return s;
            }

            public int GetHeightReq()
            {
                //return 1 + Math.Max(Left?.GetHeightReq() ?? 0, Right?.GetHeightReq() ?? 0);

                var l = 0;
                var r = 0;
                if (Left != null)
                {
                    l = Left.GetHeightReq();
                }
                if (Right != null)
                {
                    r = Right.GetHeightReq();
                }
                
                return 1 + Math.Max(l, r);
            }
        }

        public class BinaryTree<T>
        {
            public Node<T> Root { get; set; }

            public BinaryTree()
            {
                Root = null;
            }

            public int GetSize()
            {
                if (Root == null)
                {
                    return 0;
                }

                return Root.GetSize();
            }

            public int GetHeightReq()
            {
                if (Root == null)
                {
                    return 0;
                }

                return Root.GetHeightReq();
            }

            public int GetHeightIter()
            {
                if (Root == null)
                {
                    return 0;
                }

                var q = new Queue<Node<T>>();
                
                q.Enqueue(Root);
                var h = 0;

                while (true)
                {
                    var levelNodeCount = q.Count;
                    if (levelNodeCount == 0)
                    {
                        return h;
                    }
                    h++;

                    while (levelNodeCount > 0)
                    {
                        var currentNode = q.Dequeue();
                        if (currentNode.Left != null)
                        {
                            q.Enqueue(currentNode.Left);
                        }
                        if (currentNode.Right != null)
                        {
                            q.Enqueue(currentNode.Right);
                        }

                        levelNodeCount--;
                    }
                }
            }
        }
    }
}
