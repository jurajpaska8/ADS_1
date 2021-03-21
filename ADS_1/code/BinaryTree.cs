using System;

namespace ADS_1.code
{
    public class Node 
    { 
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public string Word = "";
        public double freq = double.NegativeInfinity;
        public int order = -1;
    };

    class BinaryTree
    {
        public Node Root { get; set; }

        public bool Add(string value, int order = -1, int freq = -1)
        {
            Node before = null, after = this.Root;

            while (after != null)
            {
                before = after;
                // Is new node in left tree? 
                if (string.Compare(value, after.Word) < 0) 
                    after = after.LeftNode;
                // Is new node in right tree?
                else if (string.Compare(value, after.Word) > 0) 
                    after = after.RightNode;
                else
                {
                    //Exist same value
                    return false;
                }
            }

            Node newNode = new Node();
            newNode.Word = value;
            newNode.order = order;
            newNode.freq = freq;

            // If Tree is empty
            if (this.Root == null)
                this.Root = newNode;
            else
            {
                if (string.Compare(value, before.Word) < 0)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }

            return true;
        }

        public Node Find(string word)
        {
            int level = 1;
            return this.Find(word, this.Root, level);
        }

        public Node Find(string word, Node parent, int level)
        {
            if (parent != null)
            {
                if (word.Equals(parent.Word)) 
                {
                    Console.WriteLine(level + ". level, word = " + word);
                    return parent;
                }
                level += 1;
                if (word.CompareTo(parent.Word) < 0)
                    return Find(word, parent.LeftNode, level);
                else
                    return Find(word, parent.RightNode, level);
            }

            return null;
        }

        /// <summary>
        /// Returns number of comparison 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public int PocetPorovnani(string word)
        {
            int level = 1;
            return this.PocetPorovnani(word, this.Root, level);
        }

        public int PocetPorovnani(string word, Node parent, int level)
        {
            if (parent != null)
            {
                if (word.Equals(parent.Word))
                {
                    Console.WriteLine(level + ". level, word = " + word);
                    return level;
                }
                level += 1;
                if (word.CompareTo(parent.Word) < 0)
                    return PocetPorovnani(word, parent.LeftNode, level);
                else
                    return PocetPorovnani(word, parent.RightNode, level);
            }

            return level;
        }

        /// <summary>
        /// Function will add keys according to order given in keysOrder array. 
        /// So it means that keysOrder should be permutation
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="keysOrder"></param>
        public void BuildFromOrder(string[] keys, int[] keysOrder)
        {
            if (keys.Length != keysOrder.Length)
            {
                Console.WriteLine("Bad inputs in BuildFromOrder");
                return;
            }
            for(int i = 0; i < keysOrder.Length; i++)
            {
                Add(keys[keysOrder[i] - 1], keysOrder[i] - 1);
            }
        }


        /// <summary>
        ///  Print nodes at a given level
        /// </summary>
        /// <param name="root"></param>
        /// <param name="level"></param>
        public void PrintGivenLevel(Node root, int level)
        {
            if (root == null)
                return;
            if (level == 1)
                Console.WriteLine(root.Word);
            else if (level > 1)
            {
                PrintGivenLevel(root.LeftNode, level - 1);
                PrintGivenLevel(root.RightNode, level - 1);
            }
        }
    }
}
