using System;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HangmanGame();
        }
        static void HangmanGame()
        {
            Console.WriteLine("Welcum to the game of Hangman!");
            var hang = new Hangman();
            int counter = 0;
            hang.LoadDictFromFile("words.txt"); //this file has russian words
            hang.GetRandomWord();
            Console.WriteLine("We chose a random word for you and decrypted it below:");
            Console.WriteLine(hang.GetCurrentState());
            char input = ' ';
            while (counter < 6)
            {
                Console.WriteLine("Print a letter to find out if the word has it");
                try
                {
                    input = Console.ReadLine()[0];
                    bool goodGuess = hang.GuessAttempt(char.ToLower(input), out string word, out bool gameOver);
                    Console.WriteLine(word);
                    if (goodGuess)
                    {
                        Console.WriteLine("Nice, you guessed a letter right!");
                    }
                    else
                    {
                        Console.WriteLine("No, it doesn't exist there :(");
                    }
                    if (gameOver)
                    {
                        Console.WriteLine("Congratulations! You've guessed everything right");
                    }
                    Console.WriteLine();
                    counter++;
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong with your input");
                }             
            }
            if (counter == 6)
            {
                Console.WriteLine($"You didn't manage to guess the word :( It was {hang.Word}");
            }
            PlayAgain();
            
        }
        static bool PlayAgain()
        {
            Console.WriteLine("Do you want to play again?");
            Console.WriteLine("1. Yes    2. No");
            ConsoleKeyInfo newGameChoice = Console.ReadKey();
            switch (newGameChoice.KeyChar)
            {
                case '1':
                    Console.Clear();
                    HangmanGame();
                    return true;
                case '2':
                    Console.WriteLine("\nAight :(");
                    return false;
                default:
                    Console.WriteLine("\nInvalid answer input");
                    PlayAgain();
                    return false;
            }
        }
    }
}
