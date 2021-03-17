using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ADS_1.code
{
    class FileHandler
    {
        public string Path { get; set; }
        public FileHandler(string path)
        {
            Path = path;
        }

        public string[] readLines()
        {
            string[] lines = System.IO.File.ReadAllLines(Path);
            return lines;
        }
        public Dictionary<string, int> transformLinesIntoDic(string[] lines)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            foreach (string line in lines)
            {
                // parse frequention from line
                int freq = int.Parse(Regex.Match(line, @"\d+").Value);
                // parse word from line
                string word = Regex.Match(line, @"\D+").Value.Trim();
                dic.Add(word, freq);
            }
            return dic;
        }

        /// <summary>Returns Dictionary of words, which has all frequencies higher 
        ///     than value freq. Allert: Alters original dictionary.
        /// </summary>
        public Dictionary<string, int> getWordsWithFrequentionHigherThanFreq(Dictionary<string, int> dic, int freq)
        {
            foreach (KeyValuePair<string, int> entry in dic)
            { 
                if(entry.Value <= freq)
                {
                    dic.Remove(entry.Key);
                }
            }
            return dic;
        }


    }
}
