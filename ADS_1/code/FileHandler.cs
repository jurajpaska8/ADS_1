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

        public string[] ReadLines()
        {
            string[] lines = System.IO.File.ReadAllLines(Path);
            return lines;
        }
        /// <summary>
        /// Lines have string form: "int: frequency string: word". Method splits lines into dictionary where word is key and the frequency is value.
        /// </summary>
        /// <param name="lines">array of lines cotainig word with its frequency</param>
        /// <returns>new dictionary with word:frequency mapping</returns>
        public Dictionary<string, int> TransformLinesIntoDic(string[] lines)
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
    }
}
