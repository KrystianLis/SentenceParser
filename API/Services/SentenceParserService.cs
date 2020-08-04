using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class SentenceParserService : ISentenceParserService
    {
        public IEnumerable<string> GetWordsFromText(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException();
            }

            string trimInput = input.Trim();
            string[] words = System.Text.RegularExpressions.Regex.Split(trimInput, @"\s{1,}");

            return words.AsEnumerable();
        }
    }
}
