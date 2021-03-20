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

          

            //OptimalBinaryTree binaryTree = new OptimalBinaryTree(sortedDicOver50k, sortedDic);
            OptimalBinaryTree binaryTree = new OptimalBinaryTree("p_values", "q_values", sortedDicOver50k, true) ;
            double cost = 0;
            int[,] rootsMattrix = binaryTree.ComputeOptimalTreeCostSuccessful(out cost);
            int rootMattrixIndex = sortedDicOver50k.Count - 1;


            List<int> order = binaryTree.GetOrderOfAddingKeys(rootsMattrix, rootMattrixIndex);


            // build tree
            BinaryTreeFinal binaryTreeFinal = new BinaryTreeFinal();
            binaryTreeFinal.BuildFromOrder(sortedDicOver50k.Keys.ToArray(), order.ToArray());
            Console.WriteLine("End");


            //print stats
        }
    }
}