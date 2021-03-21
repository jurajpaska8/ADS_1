using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ADS_1.code
{
    class OptimalBinaryTreeContext
    {
        // probability of key finding - p[0] is redudant -> due to index
        // [1..n]
        double[] p;
        // probability of dummy key finding 
        // [0..n]
        double[] q;
        // max index in p or q
        int n = 0;

        /// <summary>
        /// Build probabilities p, q and index n according to sorted dictionaries. Values p[] are from keys, q[] are from gaps between keys.
        /// If write binary is set to true, than we can store this arrays to binary files with pName for p and qName for q.
        /// </summary>
        /// <param name="keysDict"></param>
        /// <param name="allWords"></param>
        /// <param name="writeBinary"></param>
        /// <param name="pName"></param>
        /// <param name="qName"></param>
        public OptimalBinaryTreeContext(SortedDictionary<string, int> keysDict, SortedDictionary<string, int> allWords,
                                        bool writeBinary = false, string pName = "p_values", string qName = "q_values")
        {
            // compute sum of frequencies
            int sum = DictionaryHandler.SumOfValues(allWords);

            // make table
            p = new double[keysDict.Count + 1];

            // make table
            q = new double[keysDict.Count + 1];

            // set max index
            n = p.Length - 1;

            // first item in p will be empty
            p[0] = double.NegativeInfinity;
            // q[0] will contain frequencies of word lexicographically lower than first word from keys
            q[0] = DictionaryHandler.GetRelativeFrequencyOfGapFromAll(allWords.Keys.First(), keysDict, allWords, sum);

            // compute all freq
            int ctr = 1;

            foreach (string w in keysDict.Keys.ToArray())
            {
                p[ctr] = DictionaryHandler.GetRelativeFrequencyOfWord(w, allWords, sum);
                q[ctr] = DictionaryHandler.GetRelativeFrequencyOfGapFromAll(w + "a", keysDict, allWords, sum);
                ctr++;
            }

            if (writeBinary)
            {
                // write p as binary
                byte[] pBytes = new byte[p.Length * sizeof(double)];
                Buffer.BlockCopy(p, 0, pBytes, 0, pBytes.Length);
                System.IO.File.WriteAllBytes(pName, pBytes);

                // write q as binary
                byte[] qBytes = new byte[q.Length * sizeof(double)];
                Buffer.BlockCopy(q, 0, qBytes, 0, qBytes.Length);
                System.IO.File.WriteAllBytes(qName, qBytes);
            }
        }

        public OptimalBinaryTreeContext(double[] p, double[] q, int n)
        {
            this.p = p;
            this.q = q;
            this.n = n;
        }

        public OptimalBinaryTreeContext(string pPath, string qPath, bool onlyP)
        {
            // if we want to build tree without dummy keys - then remove first value - it is redudant
            if (onlyP)
            {
                // without first dummy freq
                byte[] pbytes = System.IO.File.ReadAllBytes(pPath);
                p = new double[pbytes.Length / 8 - 1];
                for (int i = 0; i < p.Length; i++)
                    p[i] = BitConverter.ToDouble(pbytes, (i + 1) * 8);
                return;
            }

            // read p from binary file
            byte[] pReadBytes = System.IO.File.ReadAllBytes(pPath);
            p = new double[pReadBytes.Length / 8];
            for (int i = 0; i < p.Length; i++)
                p[i] = BitConverter.ToDouble(pReadBytes, i * 8);

            // read q from binary file
            byte[] qReadBytes = System.IO.File.ReadAllBytes(qPath);
            q = new double[qReadBytes.Length / 8];
            for (int i = 0; i < q.Length; i++)
                q[i] = BitConverter.ToDouble(qReadBytes, i * 8);

            // set max index
            n = p.Length - 1;
        }

        public int[,] ComputeOptimalTreeCostSuccessful(out double cost)
        {
            // first element of p is redudant ... so it contains n - 1 keys and redudant member at 0 index
            int len = p.Length;
            // last index
            int n = len - 1;
            int[,] roots = new int[len + 1, len + 1];
            double[,] e= new double[len + 1, len + 1];
            double[,] w = new double[len + 1, len + 1];

            // For a single key, cost is equal to frequency of the key 
            for (int i = 1; i <= n + 1; i++)
            {
                roots[i, i - 1] = i;
                e[i, i - 1] = q[i - 1];
                w[i, i - 1] = q[i - 1];

            }

            // Now we need to consider chains of length 2, 3, ... . 
            // L is chain length. 
            for (int L = 1; L <= n; L++)
            {

                // i is row number in cost[][] 
                for (int i = 1; i <= n - L + 1; i++)
                {

                    // Get column number j from row number i and  
                    // chain length L 
                    int j = i + L - 1;
                    e[i, j] = int.MaxValue;
                    w[i, j] = w[i, j - 1] + p[j] + q[j];

                    // Try making all keys in interval keys[i..j] as root 
                    for (int r = i; r <= j; r++)
                    {

                        // c = cost when keys[r] becomes root of this subtree 
                        double c = e[i, r - 1] + e[r + 1, j] + w[i, j];
                        if (c < e[i, j])
                        {
                            e[i, j] = c;
                            roots[i, j] = r;
                        }
                    }
                }
            }
            cost = e[1, n - 1];
            return roots;
        }

        /// <summary>
        /// Returns order of keys in order to build optimal tree.
        /// </summary>
        /// <param name="rootMattrix"></param>
        /// <param name="rootIndex"></param>
        /// <returns></returns>
        public void GetOrderOfAddingKeys(int[,] rootMattrix, int start, int end, List<int> order)
        {
            int root = rootMattrix[start, end];
            order.Add(root);
            Console.WriteLine("(" + start + "," + end + ") = " + root);
            if (start != root)
            {
                GetOrderOfAddingKeys(rootMattrix, start, root - 1, order);
            }
            if(end != root)
            {
                GetOrderOfAddingKeys(rootMattrix, root + 1, end, order);
            }
        }

        /// <summary>
        /// Returns list of indexes, how should be optimal tree created.
        /// </summary>
        /// <param name="rootMattrix"></param>
        /// <param name="rootIndex"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public List<int> GetOrderOfAddingKeys(int[,] rootMattrix, int rootIndex, int startIndex = 0)
        {
            List<int> order = new List<int> { };
            GetOrderOfAddingKeys(rootMattrix, startIndex, rootIndex, order);
            return order;
        }
    }
}
