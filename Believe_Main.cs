using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BelieveOrNot
{
    internal class Believe_Main
    {
        static void Main(string[] args)
        {
            BelieveOrNotGame("Questions.csv");
        }
        static void BelieveOrNotGame(string filePath)
        {
            var thesisList = FileRead(filePath);
            int attemptCounter = 0;          

            Console.WriteLine("Welcum to the Believe It Or Not game!");
            Console.WriteLine("We will give you statements: try to guess, if they are true or not!");
            while (attemptCounter < 3)
            {
                Thesis thesis = ThesisPicker(ref thesisList);
                ThesisDialogue(thesis);
                PlayAgain(filePath);
            }
        }

        static List<Thesis> FileRead(string filePath)
        {
            return File.ReadAllLines(filePath).Select(line => Thesis.ThesisLineParser(line)).ToList();
        }
        static Thesis ThesisPicker(ref List<Thesis> thesisList)
        {
            Random rand = new();
            int randomNum = rand.Next(0, thesisList.Count);
            Thesis pickedThesis = thesisList[randomNum];
            thesisList.Remove(pickedThesis);
            return pickedThesis;
        }
        static void ThesisDialogue(Thesis thesis)
        {
            ThesisResult answer = ThesisResult.No;
            bool validAnswer = false;

            Console.WriteLine("\n" + thesis.Question);
            Console.WriteLine("1. Yes   2. No");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case '1':
                    answer = ThesisResult.Yes;
                    validAnswer = true;
                    break;
                case '2':
                    answer = ThesisResult.No;
                    validAnswer = true;
                    break;
                default:
                    Console.WriteLine("\nYour input was wrong, please try again");
                    validAnswer = false;
                    break;
            }
            if (!validAnswer)
            {
                ThesisDialogue(thesis);
            }
            else
            {
                if (answer == thesis.Answer)
                {
                    Console.WriteLine("\nYou're right!");
                }
                else
                {
                    Console.WriteLine("\nNo, you're wrong!");
                    Console.WriteLine(thesis.Comment);
                }
            }
        }
        static bool PlayAgain(string path)
        {
            Console.WriteLine("Do you want to play again?");
            Console.WriteLine("1. Yes    2. No");
            ConsoleKeyInfo newGameChoice = Console.ReadKey();
            switch (newGameChoice.KeyChar)
            {
                case '1':
                    Console.Clear();
                    BelieveOrNotGame(path);
                    return true;
                case '2':
                    Console.WriteLine("\nAight :(");
                    return false;
                default:
                    Console.WriteLine("\nInvalid answer input");
                    PlayAgain(path);
                    return false;
            }
        }
    }
}
