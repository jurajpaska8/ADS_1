using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADS_1.code
{
    class DictionaryHandler
    {

        /// <summary>
        /// Returns Dictionary of words, which has all frequencies higher 
        /// than value freq. Allert: Alters original dictionary.
        /// </summary>
        public static Dictionary<string, int> GetWordsWithFrequentionHigherThanFreq(Dictionary<string, int> dic, int freq)
        {
            foreach (KeyValuePair<string, int> entry in dic)
            {
                if (entry.Value <= freq)
                {
                    dic.Remove(entry.Key);
                }
            }
            return dic;
        }
        /// <summary>
        /// Returns sum of all values in dictionary
        /// </summary>
        /// <param name="dic">dictionary</param>
        /// <returns>sum of values</returns>
        public static int SumOfValues(IDictionary<string, int> dic)
        {
            return dic.Sum(v => v.Value);
        }

        /// <summary>
        /// Returns relative frequency of given word in passed dictionary. 
        /// Sum of frequencies is passed due to computional complexity. 
        /// It is better to compute it once and then just pass as argument.
        /// </summary>
        /// <param name="word">wanted word</param>
        /// <param name="dic">dictionary</param>
        /// <param name="allFreq">sum of frequencies from dictionary. </param>
        /// <returns></returns>
        public static double GetRelativeFrequencyOfWord(string word, IDictionary<string, int> dic, int allFreq)
        {
            if (dic.TryGetValue(word, out int val))
            {
                return (double)val / allFreq;
            }
            return 0;
        }
        /// <summary>
        /// Returns relative frequency of word with given index in passed dictionary. 
        /// Sum of frequencies is passed due to computional complexity. 
        /// It is better to compute it once and then just pass as argument.
        /// Use of SortedDictionary is recomended. 
        /// </summary>
        /// <param name="idx">index of wanted word</param>
        /// <param name="dic">dictionary</param>
        /// <param name="allFreq">sum of frequencies from dictionary. </param>
        public static double GetRelativeFrequencyOfWord(int idx, IDictionary<string, int> dic, int allFreq)
        {
            string word = dic.ElementAtOrDefault(idx).Key;

            if(word == null)
            {
                return 0;
            }
            return GetRelativeFrequencyOfWord(word, dic, allFreq);
        }

        /// <summary>
        /// Sum all frequencies of words between word on idxBottomWord index and word on idxUpWord (both excluding).
        /// </summary>
        /// <param name="idxBottomWord"></param>
        /// <param name="idxUpWord"></param>
        /// <param name="dic"></param>
        /// <param name="allFreq"></param>
        /// <returns>sum of frequencies</returns>
        public static double GetRelativeFrequencyOfGap(int idxBottomWord, int idxUpWord, SortedDictionary<string, int> dic, int allFreq)
        {
            double sum = 0;
            for(int i = idxBottomWord + 1; i < idxUpWord; i++)
            {
                if(i%1000==0) Console.WriteLine(i);
                sum += GetRelativeFrequencyOfWord(i, dic, allFreq);
            }
            return sum;
        }

        /// <summary>
        /// Sum all frequencies of words between wordBottom and wordUp (both excluding).
        /// </summary>
        /// <param name="wordBottom"></param>
        /// <param name="wordUp"></param>
        /// <param name="dic"></param>
        /// <param name="allFreq"></param>
        /// <returns>um of frequencies</returns>
        public static double GetRelativeFrequencyOfGap(string wordBottom, string wordUp, SortedDictionary<string, int> dic, int allFreq)
        {
            // find indexes of words
            int idxBottomWord = dic.Keys.ToList().IndexOf(wordBottom);
            int idxUpWord = dic.Keys.ToList().IndexOf(wordUp);
            if(idxBottomWord >= 0 && idxUpWord >= 0)
            {
                return GetRelativeFrequencyOfGap(idxBottomWord, idxUpWord, dic, allFreq);
            }

            return 0;
        }

        public static bool IsWordInDictionary(string word, IDictionary<string, int> dic)
        {
            return dic.ContainsKey(word);
        }
    }
}
