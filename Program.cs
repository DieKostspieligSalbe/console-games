using System;

namespace NumbersGuess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NumberGuess();
        }
        static void NumberGuess()
        {
            Console.ForegroundColor = ConsoleColor.White;
            bool isMachine;
            bool isGuesser = false;
        enemyChoosingPoint:
            Console.WriteLine("\nDo you want to play against a friend or machine?");
            Console.WriteLine("1. Friend   2. Machine");
            ConsoleKeyInfo enemyInput = Console.ReadKey();
            switch (enemyInput.KeyChar)
            {
                case '1':
                    isMachine = false;
                    goto playOptions;
                case '2':
                    isMachine = true;
                    break;
                default:
                    Console.WriteLine("\nInvalid input");
                    goto enemyChoosingPoint;
            }
        guessChoosingPoint:
            Console.WriteLine("\nDo you want to guess or give a number for your enemy to guess?");
            Console.WriteLine("1. I want to guess    2. I want to give a number to guess");
            ConsoleKeyInfo guessInput = Console.ReadKey();
            switch (guessInput.KeyChar)
            {
                case '1':
                    isGuesser = true;
                    break;
                case '2':
                    isGuesser = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid input");
                    goto guessChoosingPoint;
            }
        playOptions:
            if (isMachine && isGuesser)
            {
                int counter = 1;
                string keyword;
                int machineNumber = MachineNumber();
                Console.WriteLine("\nSo the machine prepared a number for you. Can you guess it?");
                while (counter <= 5)
                {
                numberFromMachine:
                    bool targetIsValid = int.TryParse(Console.ReadLine(), out int assumption);
                    if (targetIsValid && assumption <= 100 && assumption >= 0)
                    {
                        if (assumption == machineNumber)
                        {
                            Console.WriteLine($"Congratulations! You've guessed {assumption} correctly!");
                            goto wantToPlayAgain;
                        }
                        else
                        {
                            keyword = assumption > machineNumber ? "bigger" : "smaller";
                            Console.WriteLine($"Unfortunately, your guess {assumption} wasn't correct. A little hint: your number is {keyword} than the right one");
                            Console.WriteLine($"{5 - counter} attempts left");
                            counter++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your input is invalid, please try again");
                        goto numberFromMachine;
                    }
                    if (counter > 5)
                    {
                        Console.WriteLine($"You lost! The right number was {machineNumber}");
                    }
                }
                goto wantToPlayAgain;
            }
            else if (isMachine && !isGuesser)
            {
            numberToMachineInput:
                Console.WriteLine("\nWhat number from 0 to 100 do you want to give to the machine?");
                bool targetIsValid = int.TryParse(Console.ReadLine(), out int target);
                if (targetIsValid && target <= 100 && target >= 0)
                {
                    bool result = MachineSearch(target);
                    Console.ForegroundColor = ConsoleColor.White;
                    if (result)
                    {
                        Console.WriteLine("Machine won!");
                    }
                    else
                    {
                        Console.WriteLine("Machine lost.");
                    }
                }
                else
                {
                    Console.WriteLine("Your input is invalid, please try again");
                    goto numberToMachineInput;
                }
                goto wantToPlayAgain;
            }
            else if (!isMachine)
            {
            twoPlayersNumberSet:
                Console.WriteLine("\nLet the first player set a number to guess:");
                bool targetIsValid = int.TryParse(Console.ReadLine(), out int target);
                if (targetIsValid && target <= 100 && target >= 0)
                {
                    Console.Clear();
                    int counter = 1;
                    string keyword;
                    Console.WriteLine("No worries! The console was cleared because the number was set. Now let the second player try to guess it:");
                    while (counter <= 5)
                    {
                    wrongGuesserInput:
                        bool guessIsValid = int.TryParse(Console.ReadLine(), out int assumption);
                        if (guessIsValid && assumption <= 100 && assumption >= 0)
                        {
                            if (assumption == target)
                            {
                                Console.WriteLine($"Congratulations! You've guessed {assumption} correctly!");
                                goto wantToPlayAgain;
                            }
                            else
                            {
                                keyword = assumption > target ? "bigger" : "smaller";
                                Console.WriteLine($"Unfortunately, your guess {assumption} wasn't correct. A little hint: your number is {keyword} than the right one");
                                Console.WriteLine($"{5 - counter} attempts left");
                                counter++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Your input is invalid, please try again");
                            goto wrongGuesserInput;
                        }
                        if (counter > 5)
                        {
                            Console.WriteLine($"You lost! The right number was {target}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Your input is invalid (possible less than 0 or bigger than 100), please try again");
                    goto twoPlayersNumberSet;
                }
                goto wantToPlayAgain;

            }

        wantToPlayAgain:
            Console.WriteLine("Do you want to play again?");
            Console.WriteLine("1. Yes    2. No");
            ConsoleKeyInfo newGameChoice = Console.ReadKey();
            switch (newGameChoice.KeyChar)
            {
                case '1':
                    Console.Clear();
                    goto enemyChoosingPoint;
                case '2':
                    Console.WriteLine("\nAight :(");
                    break;
                default:
                    Console.WriteLine("\nInvalid answer input");
                    goto wantToPlayAgain;
            }
            static int MachineNumber()
            {
                Random rnd = new Random();
                return rnd.Next(101);
            }

            static bool MachineSearch(int target)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                int counter = 1;
                int interMed;
                int rightPoint = 100;
                int leftPoint = 0;
                while (counter <= 5)
                {
                    Console.WriteLine($"The machine searches between {rightPoint} and {leftPoint}...");
                    Console.WriteLine($"Trying {interMed = leftPoint + (int)Math.Floor((double)((rightPoint - leftPoint) / 2))}");

                    if (interMed == target)
                    {
                        Console.WriteLine($"The machine guessed {target} correctly from the {counter} attempt");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"The machine continues guessing...{5 - counter} attempts left");
                        counter++;
                        if (interMed < target)
                        {
                            leftPoint = interMed;
                            Console.WriteLine($"Left search border is moved to {leftPoint}");
                        }
                        else
                        {
                            rightPoint = interMed;
                            Console.WriteLine($"Right search border is moved to {rightPoint}");
                        }
                    }
                }
                Console.WriteLine("The machine didn't manage to guess :(");
                return false;
            }
        }
    }
}

