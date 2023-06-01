using System;
using System.Globalization;
using System.Linq;

namespace StopWord
{
    public static class StopWordsExtension
    {
        public static string RemoveStopWords(this string s, string lang)
        {

            return Remove(s, lang);
        }

        private static string Remove(string s, string lang)
        {
            var stopWordList = StopWords.GetStopWords(lang);
            s = s.Split(' ').Where(x => !stopWordList.Contains(x.ToLower())).DefaultIfEmpty().Aggregate((current, next) => current + " " + next);

            return s ?? String.Empty;
        }
    }
}
