using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle
{
    public class WordleBot: IWordleBot
    {
        public WordleBot()
        {
        }

        public List<GuessResult> Guesses { get; set; }

        public string GenerateGuess()
        {
            throw new NotImplementedException();
        }

        private List<string> GeneratePossibleGuesses()
        {
            string filePath = "../../../data/english_words_full.txt";
            Console.WriteLine(filePath);
            var lines = new List<string>();
            var fiveLetterWords = new List<string>();

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    //if (line == "" || line[0] == '#')
                    //{
                    //	continue;
                    //}

                    lines.Add(line);
                }
            }
            lines.ForEach(delegate (string word)
            {
                if (word.Length == 5)
                {
                    fiveLetterWords.Add(word);
                }
            });
            return fiveLetterWords;
        }
    }
}
