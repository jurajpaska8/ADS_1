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
            //OptimalBinaryTree binaryTree = new OptimalBinaryTree("p_values", "q_values", sortedDicOver50k, false) ;
            //double cost = 0;
            //int[,] rootsMattrix = binaryTree.ComputeOptimalTreeCostSuccessful(out cost);
            //int rootMattrixIndex = sortedDicOver50k.Count - 1;


            //List<int> order = binaryTree.GetOrderOfAddingKeys(rootsMattrix, 151, 1);


            //// build tree
            //BinaryTreeFinal binaryTreeFinal = new BinaryTreeFinal();
            //binaryTreeFinal.BuildFromOrder(sortedDicOver50k.Keys.ToArray(), order.ToArray());

            //binaryTreeFinal.Find("the");
            //binaryTreeFinal.Find("a");
            //binaryTreeFinal.Find("of");
            //binaryTreeFinal.Find("and");

            //binaryTreeFinal.PrintGivenLevel(binaryTreeFinal.Root, 1);
            //binaryTreeFinal.PrintGivenLevel(binaryTreeFinal.Root, 2);
            //binaryTreeFinal.PrintGivenLevel(binaryTreeFinal.Root, 3);
            //Console.WriteLine("End");
            int n = 7;
            double[] p = new double[] { -10, 0.15, 0.10, 0.05, 0.10, 0.2};
            double[] q = new double[] { 0.05, 0.10, 0.05, 0.05, 0.05, 0.1 };
            OptimalBinaryTree obt = new OptimalBinaryTree(p, q, n);
            double c = 0;
            int[,] roots = obt.ComputeOptimalTreeCostSuccessful(out c);
            List<int> orderMatrix = obt.GetOrderOfAddingKeys(roots, 5, 1);

            BinaryTreeFinal binaryTreeFinal = new BinaryTreeFinal();
            binaryTreeFinal.BuildFromOrder(new string[] { "1", "2", "3", "4", "5"}, orderMatrix.ToArray());

            Console.WriteLine("End");
        }
    }
}