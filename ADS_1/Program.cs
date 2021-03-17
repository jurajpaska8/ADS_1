using System;
using System.Collections.Generic;
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

            // return sum of all frequencies in over 50k dictionary
            var freqSumSortedOver50k = DictionaryHandler.SumOfValues(sortedDicOver50k);
            var freqSumOver50k = DictionaryHandler.SumOfValues(dicOver50k);

            // return sum of all frequencies in over 50k dictionary
            var freqSumSortedOriginal = DictionaryHandler.SumOfValues(sortedDic);
            var freqSumOriginal = DictionaryHandler.SumOfValues(dic);


            // freq of the
            double freq_the = DictionaryHandler.GetRelativeFrequencyOfWord("the", sortedDicOver50k, freqSumSortedOver50k);
            double freq_108 = DictionaryHandler.GetRelativeFrequencyOfWord(108, sortedDicOver50k, freqSumSortedOver50k);

            // test gap frequencies
            double q_all_sorted = DictionaryHandler.GetRelativeFrequencyOfGap(-1, sortedDicOver50k.Count, sortedDicOver50k, freqSumSortedOver50k);

            double q_all_sorted_1_n = DictionaryHandler.GetRelativeFrequencyOfGap(0, sortedDicOver50k.Count - 1, sortedDicOver50k, freqSumSortedOver50k);
            double q_all_sorted_words = DictionaryHandler.GetRelativeFrequencyOfGap("a", "your", sortedDicOver50k, freqSumSortedOver50k);

            //print stats
            Console.WriteLine(freq_the);
            Console.WriteLine("Lines in file: " + lines.Length);
            Console.WriteLine("KeyPairs in original dic: " + dic.Count);
            Console.WriteLine("KeyPairs in shrunked dic: " + dicOver50k.Count);
        }
    }
}