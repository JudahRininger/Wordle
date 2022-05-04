using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle
{
    public class WordleBot: IWordleBot
    {
        public List<GuessResult> Guesses { get; set; }
        private List<string> PossibleGuesses { get; set; }
        private Dictionary<int, List<char>> IncorrectLetters { get; set; }
        public WordleBot()
        {
            PossibleGuesses = GeneratePossibleGuesses();
            IncorrectLetters = new Dictionary<int, List<char>>();
            IncorrectLetters[0] = new List<char>();
            IncorrectLetters[1] = new List<char>();
            IncorrectLetters[2] = new List<char>();
            IncorrectLetters[3] = new List<char>();
            IncorrectLetters[4] = new List<char>();
            Guesses = new List<GuessResult>();
        }


        public string GenerateGuess()
        {
            string guess = "";
            if(Guesses.Count == 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(PossibleGuesses.Count);
                guess = PossibleGuesses[index];
                return guess;
            }
            else
            {
                GuessResult results = Guesses[Guesses.Count - 1];
                for (int i = 0; i < 5; i++)
                {
                    if(results.Guess[i].LetterResult == LetterResult.Incorrect)
                    {
                        IncorrectLetters[i].Add(results.Guess[i].Letter);
                    }
                }
                var filteredGuesses = new List<string>();
                foreach (var str in PossibleGuesses)
                {
                    string word = str;
                    int proc = 0;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (results.Guess[i].LetterResult == LetterResult.Correct)
                        {
                            if (word[i] == results.Guess[i].Letter)
                            {
                                proc++;
                                word = setCharAt(word, i, '-');
                            }
                        }
                        else if (results.Guess[i].LetterResult == LetterResult.Misplaced)
                        {
                            if (results.Guess[i].Letter != word[i])
                            {
                                bool proc2 = true;
                                for (int e = 0; e < str.Length; e++)
                                {
                                    if (results.Guess[i].Letter == word[e] && proc2)
                                    {
                                        if (word[e] != results.Guess[e].Letter)
                                        {
                                            proc++;
                                            word = setCharAt(word, e, '-');
                                            proc2 = false;
                                        }
                                    }
                                }
                            }
                        }
                        else if (results.Guess[i].LetterResult == LetterResult.Incorrect)
                        {
                            if(!IncorrectLetters[i].Contains(word[i]))
                            {
                                proc++;
                            }
                        }
                    }
                    if (proc == 5)
                    {
                        filteredGuesses.Add(str);
                    }
                }
                PossibleGuesses = filteredGuesses;
                Random rnd = new Random();
                int index = rnd.Next(filteredGuesses.Count);
                if(filteredGuesses.Count > 0)
                {
                    guess = filteredGuesses[index];
                }
                else
                {
                    guess = "fails";
                }
                return guess;

            }

        }
        private string setCharAt(string str, int index, char chr)
        {
            if (index > str.Length - 1) { return str; }
            else
            {
                return str.Substring(0, index) + chr + str.Substring(index + 1);
            }
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
