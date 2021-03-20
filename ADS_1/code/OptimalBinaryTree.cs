using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ADS_1.code
{
    class OptimalBinaryTree
    {
        string[] keys;
        double[] p;
        double[] q;

        // successfull searching
        double[,] successfullSearchingTable;

        public OptimalBinaryTree(SortedDictionary<string, int> keysDict, SortedDictionary<string, int> allWords)
        {
            // compute sum of frequencies
            int sum = DictionaryHandler.SumOfValues(allWords);

            // get keys
            keys = keysDict.Keys.ToArray();

            // make table
            p = new double[keysDict.Count + 1];
            q = new double[keysDict.Count + 1];

            // make table of sums - successful searching
            successfullSearchingTable = new double[keysDict.Count + 1, keysDict.Count + 1];

            // first item in p will be empty
            p[0] = double.NegativeInfinity;
            // q[0] will contain frequencies of word lexicographically lower than first word from keys
            q[0] = DictionaryHandler.GetRelativeFrequencyOfGapFromAll(allWords.Keys.First(), keysDict, allWords, sum);

            // compute all freq
            int ctr = 1;

            foreach (string w in keys)
            {
                p[ctr] = DictionaryHandler.GetRelativeFrequencyOfWord(w, allWords, sum);
                q[ctr] = DictionaryHandler.GetRelativeFrequencyOfGapFromAll(w + "a", keysDict, allWords, sum);
                ctr++;
            }

            double testSum = q[0];
            for (int i = 1; i < q.Length; i++)
            {
                testSum += q[i];
                testSum += p[i];
            }

            byte[] p_bytes = new byte[p.Length * sizeof(double)];
            Buffer.BlockCopy(p, 0, p_bytes, 0, p_bytes.Length);
            System.IO.File.WriteAllBytes("p_values", p_bytes);

            byte[] q_bytes = new byte[q.Length * sizeof(double)];
            Buffer.BlockCopy(q, 0, q_bytes, 0, q_bytes.Length);
            System.IO.File.WriteAllBytes("q_values", q_bytes);

        }

        public OptimalBinaryTree(string pPath, string qPath, SortedDictionary<string, int> keysDict, bool onlyP)
        {
            if (onlyP)
            {
                // without first dummy freq - skip first 8 bytes
                byte[] pbytes = System.IO.File.ReadAllBytes(pPath);
                p = new double[pbytes.Length / 8 - 1];
                for (int i = 0; i < p.Length; i++)
                    p[i] = BitConverter.ToDouble(pbytes, (i + 1) * 8);
                successfullSearchingTable = new double[p.Length + 1, p.Length + 1];
                keys = keysDict.Keys.ToArray();
                return;
            }
            byte[] p_read_bytes = System.IO.File.ReadAllBytes(pPath);
            p = new double[p_read_bytes.Length / 8];
            for (int i = 0; i < p.Length; i++)
                p[i] = BitConverter.ToDouble(p_read_bytes, i * 8);

            byte[] q_read_bytes = System.IO.File.ReadAllBytes(qPath);
            q = new double[q_read_bytes.Length / 8];
            for (int i = 0; i < q.Length; i++)
                q[i] = BitConverter.ToDouble(q_read_bytes, i * 8);

            double testSum = q[0];
            for (int i = 1; i < q.Length; i++)
            {
                testSum += q[i];
                testSum += p[i];
            }

            successfullSearchingTable = new double[p.Length, p.Length];
            keys = keysDict.Keys.ToArray();
        }

        public int[,] ComputeOptimalTreeCostSuccessful(out double cost)
        {
            int n = keys.Length;
            int[,] roots = new int[n + 1, n + 1];

            // For a single key, cost is equal to frequency of the key 
            for (int i = 0; i < n; i++)
            {
                roots[i, i] = i;
                successfullSearchingTable[i, i] = p[i];

            }

            // Now we need to consider chains of length 2, 3, ... . 
            // L is chain length. 
            for (int L = 2; L <= n; L++)
            {

                // i is row number in cost[][] 
                for (int i = 0; i <= n - L + 1; i++)
                {

                    // Get column number j from row number i and  
                    // chain length L 
                    int j = i + L - 1;
                    successfullSearchingTable[i, j] = int.MaxValue;

                    // Try making all keys in interval keys[i..j] as root 
                    for (int r = i; r <= j; r++)
                    {

                        // c = cost when keys[r] becomes root of this subtree 
                        double c = ((r > i) ? successfullSearchingTable[i, r - 1] : 0)
                                + ((r < j) ? successfullSearchingTable[r + 1, j] : 0) + sum(p, i, j);
                        if (c < successfullSearchingTable[i, j])
                        {
                            successfullSearchingTable[i, j] = c;
                            roots[i, j] = r;
                        }
                    }
                }
            }
            cost = successfullSearchingTable[0, n - 1];
            return roots;
        }

        // A utility function to get sum of array elements  
        // freq[i] to freq[j] 
        static double sum(double[] freq, int i, int j)
        {
            double s = 0;
            for (int k = i; k <= j; k++)
            {
                if (k >= freq.Length)
                    continue;
                s += freq[k];
            }
            return s;
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

        public List<int> GetOrderOfAddingKeys(int[,] rootMattrix, int rootIndex)
        {
            List<int> order = new List<int> { };
            GetOrderOfAddingKeys(rootMattrix, 0, rootIndex, order);
            return order;
        }
    }
}
