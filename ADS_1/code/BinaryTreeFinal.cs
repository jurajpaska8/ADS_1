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

    class BinaryTreeFinal
    {
        public Node Root { get; set; }

        public bool Add(string value, int order = -1)
        {
            Node before = null, after = this.Root;

            while (after != null)
            {
                before = after;
                if (string.Compare(value, after.Word) < 0) //Is new node in left tree? 
                    after = after.LeftNode;
                else if (string.Compare(value, after.Word) > 0) //Is new node in right tree?
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

            if (this.Root == null)//Tree ise empty
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

        public void BuildFromOrder(string[] keys, int[] keysOrder)
        {
            if (keys.Length != keysOrder.Length)
            {
                Console.WriteLine("Bad inputs in BuildFromOrder");
                return;
            }
            for(int i = 0; i < keysOrder.Length; i++)
            {
                Add(keys[keysOrder[i]], keysOrder[i]);
            }
        }
    }
}
