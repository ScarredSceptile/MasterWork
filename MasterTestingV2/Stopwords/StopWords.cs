using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StopWord
{
    public static class StopWords
    {
        public static string[] GetStopWords(string lang)
        {
            return LoadStopWords(lang);
        }

        private static string[] LoadStopWords(string lang)
        {
            string data;
            if (lang == "en")
                data = File.ReadAllText(@"E:\UniWork\MasterTestingV2\Stopwords\data\English.txt");
            else if (lang == "no")
                data = File.ReadAllText(@"E:\UniWork\MasterTestingV2\Stopwords\data\Norwegian.txt");
            else
            {
                Console.Error.WriteLine("Language not found!");
                return null;
            }

            var result = data.Split(new[] { "\r\n", "\r", "\n" },
                       StringSplitOptions.None);
            result = result.Where(x => !String.IsNullOrEmpty(x)).ToArray();
            return result;
        }
    }
}
