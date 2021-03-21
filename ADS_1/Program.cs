using System;
using System.Collections.Generic;
using System.Linq;
using ADS_1.code;

namespace ADS_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // get file
            FileHandler fileHandler = new FileHandler("C:/UserData/Z0045C9C/OneDrive - Siemens Healthineers/ing/2sem/ads/ADS_1/ADS_1/data/dictionary.txt");
            // read lines
            string[] lines = fileHandler.ReadLines();
            // transform lines into <string, int> dictionary
            Dictionary<string, int> dic = fileHandler.TransformLinesIntoDic(lines);
            // get words with freq. higher than 50k
            Dictionary<string, int> dicOver50k = DictionaryHandler.GetWordsWithFrequentionHigherThanFreq(new Dictionary<string, int>(dic), 50_000);
            //get original Dictionary as sorted one
            var sortedDic = new SortedDictionary<string, int>(dic);
            //get shrunked Dictionary as sorted one
            var sortedDicOver50k = new SortedDictionary<string, int>(dicOver50k);



            //OptimalBinaryTreeContext binaryTreeContext = new OptimalBinaryTreeContext(sortedDicOver50k, sortedDic, true, "p_values", "q_values");
            OptimalBinaryTreeContext binaryTreeContext = new OptimalBinaryTreeContext("p_values", "q_values", false);
            double cost = 0;
            int[,] rootsMattrix = binaryTreeContext.ComputeOptimalTreeCostSuccessful(out cost);
            int rootMattrixIndex = sortedDicOver50k.Count - 1;


            List<int> order = binaryTreeContext.GetOrderOfAddingKeys(rootsMattrix, 151, 1);


            // build tree
            BinaryTree binaryTree = new BinaryTree();
            binaryTree.BuildFromOrder(sortedDicOver50k.Keys.ToArray(), order.ToArray());

            binaryTree.Find("the");
            binaryTree.Find("a");
            binaryTree.Find("of");
            binaryTree.Find("and");

            for(int i = 1; i <= 10; i++)
            {
                Console.WriteLine("******** " + i + ". Level *********");
                binaryTree.PrintGivenLevel(binaryTree.Root, i);
            }

            int p1 = binaryTree.PocetPorovnani("thea");
            int p2 = binaryTree.PocetPorovnani("ab");
            int p3 = binaryTree.PocetPorovnani("ofa");
            int p4 = binaryTree.PocetPorovnani("anda");
            int p5 = binaryTree.PocetPorovnani("aa");
            Console.WriteLine("End");
        }
    }
}