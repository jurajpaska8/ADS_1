using System;
using System.Collections.Generic;
using ADS_1.code;

namespace ADS_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileHandler fileHandler = new FileHandler("C:/UserData/Z0045C9C/OneDrive - Siemens Healthineers/ing/2sem/ads/ADS_1/ADS_1/data/dictionary.txt");
            string[] lines = fileHandler.readLines();
            Dictionary<string, int> dic = fileHandler.transformLinesIntoDic(lines);
            Dictionary<string, int> dicOver50k = fileHandler.getWordsWithFrequentionHigherThanFreq(dic, 50_000);
            Console.WriteLine(lines.Length);
            Console.WriteLine(dic.Count);
            Console.WriteLine(dicOver50k.Count);
        }
    }
}
