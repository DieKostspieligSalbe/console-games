using System;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TicTacToeGame();
            Console.ReadLine();
        }
        static void TicTacToeGame()
        {
            Console.WriteLine("Welcum! There's your Tic Tac Toe field with cell numbers:");
            var tictac = new TicTacToe();
            tictac.DrawFirstTime();
            while (!tictac.isOver)
            {
                tictac.MovesInTurns();
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
                    TicTacToeGame();
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
