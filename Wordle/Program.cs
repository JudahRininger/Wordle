using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            //var guessResult = new GuessResult("arise");

            //Console.WriteLine(guessResult);

            //string filePath = "../../../data/english_words_full.txt";

            //var bot = new WordleGame();
            //List<string> words = bot.GeneratePossibleGuesses();
            //foreach (string word in words)
            //{
            //    Console.WriteLine(word);
            //}

            int totalGuesses = 0;

            var secretWords = new string[] { "crane", "trash", "about", "plain", "chase", "scene"};

            foreach (var secretWord in secretWords)
            {
                var hBot = new WordleBot();

                Console.WriteLine("New Game!");
                var game = new WordleGame(secretWord);

                int guesses = game.Play(hBot);
                Console.WriteLine("Gameover.");
                Console.WriteLine($"Num Guesses: {guesses}");
                Console.WriteLine();

                totalGuesses += guesses;

            }
            //double averageGuesses = (double)totalGuesses / secretWords.Length;
            //Console.WriteLine();
            //Console.WriteLine($"Average Guesses: {averageGuesses}");

        }
    }
}
