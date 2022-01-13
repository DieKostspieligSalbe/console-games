using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    public class Hangman
    {
        private Dictionary<int, string> wordsDict = new();
        private char[] lettersToGuess;
        private List<char> letters;
        public string Word { get; private set; }
        public void LoadDictFromFile(string path)
        {
            int counter = 0;
            var stream = new StreamReader(path);
            try
            {
                while (stream.ReadLine() != null)
                {
                    wordsDict.TryAdd(counter++, stream.ReadLine());
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File doesn't exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong when reading the file: {ex}");
            }
            finally
            {
                stream.Close();
            }
        }
        public void GetRandomWord()
        {
            var rand = new Random();
            Word = wordsDict[rand.Next(wordsDict.Count + 1)];
            letters = Word.ToList();
            lettersToGuess = Word.ToCharArray();
        }
        public bool GuessAttempt(char ch, out string word, out bool gameOver)
        {
            bool rightGuess = false;
            if (letters.Contains(ch))
            {
                letters.RemoveAll(x => x == ch);
                rightGuess = true;
            }
            word = GetCurrentState();
            gameOver = VictoryCheck();
            return rightGuess;
        }

        public string GetCurrentState()
        {
            var str = new StringBuilder();
            for (int i = 0; i < lettersToGuess.Length; i++)
            {
                char placeholder = letters.Contains(lettersToGuess[i]) ? '-' : lettersToGuess[i];
                str.Append(placeholder);
            }
            return str.ToString();
        }

        private bool VictoryCheck()
        {
            if (letters.Count == 0)
            {
                return true;
            }
            return false;
        }

    }
}
