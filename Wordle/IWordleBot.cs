using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle
{
	public interface IWordleBot
	{
        public List<GuessResult> Guesses { get; set; }

        public string GenerateGuess();

        public bool IsValidWord(string word)
        {
            return true;
        }

    }
}

