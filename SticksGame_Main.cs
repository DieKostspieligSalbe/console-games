using System;

namespace SticksGame
{
    internal class SticksGame_Main
    {
        static void Main(string[] args)
        {
            Sticks();
        }

        static void Sticks()
        {
            SticksGame sticks = new();
            sticks.SticksAction += StickActionHandler;
            Console.WriteLine("Welcum to the game of sticks!");
            Console.WriteLine("Each turn take 1-3 sticks until they are all gone! Last one who makes a pick, loses.");
            sticks.Print();
            while (!sticks.isOver)
            {
                if (!sticks.pickedLast) 
                {
                    Console.WriteLine("Print the number of sticks you want to take:");
                    bool correctSticks = int.TryParse(Console.ReadLine(), out int sticksNumber);
                    if (!correctSticks)
                    {
                        Console.WriteLine("Your input was invalid, please try again");
                        continue;
                    }
                    sticks.MakeAMove(sticksNumber);
                }  
                else
                {
                    sticks.MakeAMove(0);
                }            
            }
            PlayAgain();
        }

        static void StickActionHandler(string message)
        {
            Console.WriteLine(message);
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
                    Sticks();
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
