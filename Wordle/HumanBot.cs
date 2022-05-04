using System;
using System.Collections.Generic;

namespace Wordle
{
    public class HumanBot : IWordleBot
    {
        public HumanBot()
        {
        }

        public List<GuessResult> Guesses { get; set; } = new List<GuessResult>();

        public string GenerateGuess()
        {
            Console.WriteLine("Enter a 5-letter guess");
            var guess = Console.ReadLine();

            return guess;
        }
    }
}
