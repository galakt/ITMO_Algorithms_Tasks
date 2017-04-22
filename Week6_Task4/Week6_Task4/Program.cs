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
        private static readonly StringBuilder _sb = new StringBuilder();

        static void Main(string[] args)
        {
            var inFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, InFileName);
            var outFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, OutFileName);

            var lines = System.IO.File.ReadAllLines(inFilePath);

            var N = int.Parse(lines[0]);

            var binTree = ParseLinesToTree(lines, 1, N);

            var M = int.Parse(lines[N + 1]);
            var tasks = Array.ConvertAll(lines[N + 2].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            foreach (var task in tasks)
            {
                binTree.DeleteSubtreeByKey(task);
                _sb.Append(binTree.GetSize());
                _sb.AppendLine();
            }

            System.IO.File.WriteAllText(path: outFilePath, contents: _sb.ToString());
        }

        private static BinaryTree<int> GenerateTest42()
        {
            var bt = new BinaryTree<int> {Root = new Node<int>()};

            var last = bt.Root;
            for (int i = 0; i < 1200000; i++)
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
                Root = ParseLineIntoNode(lines, startIndex, null)
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

        private static Node<int> ParseLineIntoNode(string[] lines, int nodeIndex, Node<int> parent)
        {
            if (nodeIndex == 0)
            {
                return null;
            }

            var rootNodeParams = Array.ConvertAll(lines[nodeIndex].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            var node = new Node<int>
            {
                Data = rootNodeParams[0],
                Parent = parent
            };
            node.Left = ParseLineIntoNode(lines, rootNodeParams[1], node);
            node.Right = ParseLineIntoNode(lines, rootNodeParams[2], node);

            return node;
        }

#if DEBUG
        [DebuggerDisplay("Data = {" + nameof(Data) + "}")]
#endif
        public class Node<T> where T : IComparable<T>
        {
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }
            public Node<T> Parent { get; set; }

            private int? LastCalculatedSize { get; set; }

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
                if (LastCalculatedSize != null)
                {
                    return LastCalculatedSize.Value;
                }
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
                LastCalculatedSize = s;

                return LastCalculatedSize.Value;
            }

            public void RebuildSize()
            {
                var s = 1;
                if (Left != null)
                {
                    s += Left.GetSize();
                }
                if (Right != null)
                {
                    s += Right.GetSize();
                }
                LastCalculatedSize = s;

                if (Parent != null)
                {
                    Parent.RebuildSize();
                }
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

            public Node<T> DeleteSubtreeByKey(T key) 
            {
                if (key.CompareTo(Data) > 0) // Key > root.Data
                {
                    if (Right == null)
                    {
                        return null;
                    }

                    if (Right.Data.CompareTo(key) == 0)
                    {
                        var removingItem = Right;
                        Right = null;
                        RebuildSize();
                        
                        return removingItem;
                    }

                    return Right.DeleteSubtreeByKey(key);
                }
                else if (key.CompareTo(Data) < 0) // Key < root.Data
                {
                    if (Left == null)
                    {
                        return null;
                    }

                    if (Left.Data.CompareTo(key) == 0)
                    {
                        var removingItem = Left;
                        Left = null;
                        RebuildSize();

                        return removingItem;
                    }

                    return Left.DeleteSubtreeByKey(key);
                }
                else
                {
                    return null;
                }
            }
        }

        public class BinaryTree<T> where T : IComparable<T>
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

            public int GetHeightIter(int? estimatedSize)
            {
                if (Root == null)
                {
                    return 0;
                }

                Queue<Node<T>> q;
                if (estimatedSize != null)
                {
                    q = new Queue<Node<T>>(estimatedSize.Value);
                }
                else
                {
                    q = new Queue<Node<T>>();
                }

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

            public Node<T> DeleteSubtreeByKey(T key)
            {
                return Root.DeleteSubtreeByKey(key);
            }
        }
    }
}
