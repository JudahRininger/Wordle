using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle
{
    public class WordleGame
    {
        public string SecretWord { get; set; }
        public int MaxGuesses { get; set; } = 100;
        private List<string> PossibleGuesses { get; set; }
        

        public WordleGame(string secretWord = "arise")
        {
            PossibleGuesses = GeneratePossibleGuesses();
            SecretWord = secretWord;

        }

        public int Play(IWordleBot bot)
        {
            int guessNumber;
            for (guessNumber = 1; guessNumber < MaxGuesses; guessNumber++)
            {
                string guess = bot.GenerateGuess();
                Console.WriteLine($"guess {guessNumber}: {guess}");

                GuessResult guessResult = CheckGuess(guess);
                bot.Guesses.Add(guessResult);
                Console.WriteLine(guessResult);

                if (IsCorrect(guessResult))
                {
                    return guessNumber;
                }
            }

            return guessNumber;
        }

        // TODO
        public GuessResult CheckGuess(string guess)
        {
            var guessResult = new GuessResult(guess);

            var foundArray = new int[guess.Length];

            string secretWordTemp = SecretWord;

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == secretWordTemp[i] && foundArray[i] != 1)
                {
                    guessResult.Guess[i].LetterResult = LetterResult.Correct;
                    secretWordTemp = setCharAt(secretWordTemp, i, '-');
                    foundArray[i] = 1;
                }
                else
                {
                    if (secretWordTemp.Contains(guess[i]))
                    {
                        bool proc = true;
                        for (int e = 0; e < guess.Length; e++)
                        {
                            if(guess[i] == secretWordTemp[e] && proc)
                            {
                                if(guess[e] != secretWordTemp[e])
                                {
                                    guessResult.Guess[i].LetterResult = LetterResult.Misplaced;
                                    secretWordTemp = setCharAt(secretWordTemp, e, '-');
                                    proc = false;
                                }
                            }
                        }
                        if (proc)
                        {
                            guessResult.Guess[i].LetterResult = LetterResult.Incorrect;
                        }
                    }
                    else
                    {
                        guessResult.Guess[i].LetterResult = LetterResult.Incorrect;
                    }
                }
            }

            return guessResult;
        }
        private string setCharAt(string str, int index, char chr)
        {
            if (index > str.Length - 1) { return str; }
            else
            {
            return str.Substring(0, index) + chr + str.Substring(index + 1);
            }
        }


        private bool IsCorrect(GuessResult guessResult)
        {
            foreach (var letterGuess in guessResult.Guess)
            {
                if (letterGuess.LetterResult != LetterResult.Correct)
                {
                    return false;
                }
            }

            return true;
        }
        public List<string> GeneratePossibleGuesses()
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