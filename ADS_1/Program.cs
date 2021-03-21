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
            int rootMattrixIndex = 151;
            int startIndex = 1;


            List<int> order = binaryTreeContext.GetOrderOfAddingKeys(rootsMattrix, rootMattrixIndex, startIndex);


            // build tree
            BinaryTree binaryTree = new BinaryTree();
            binaryTree.BuildFromOrder(sortedDicOver50k.Keys.ToArray(), order.ToArray());

            binaryTree.Find("the");
            binaryTree.Find("a");
            binaryTree.Find("of");
            binaryTree.Find("and");

            binaryTree.Find("another");
            binaryTree.Find("even");
            binaryTree.Find("must");
            binaryTree.Find("door");
            binaryTree.Find("after");
            binaryTree.Find("year");


            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("******** " + i + ". Level *********");
                binaryTree.PrintGivenLevel(binaryTree.Root, i);
            }

            int p1 = binaryTree.pocet_porovnani("thea");
            int p2 = binaryTree.pocet_porovnani("ab");
            int p3 = binaryTree.pocet_porovnani("ofa");
            int p4 = binaryTree.pocet_porovnani("anda");
            int p5 = binaryTree.pocet_porovnani("aa");
            int p6 = binaryTree.pocet_porovnani("musta");
            Console.WriteLine("End");
        }
    }
}