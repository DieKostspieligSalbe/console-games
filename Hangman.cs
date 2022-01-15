using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    public delegate bool ExceptionErrorHandler(string message);
    public class Hangman
    {
        private Dictionary<int, string> wordsDict = new();
        private char[] lettersToGuess;
        private List<char> letters;
        public string Word { get; private set; }
        private ExceptionErrorHandler? exceptionOnLoad;
        public void RegisterExceptionErrorHandler(ExceptionErrorHandler method) //this is just for practice
        {
            exceptionOnLoad = method;
        }

        public void LoadDictFromFile(string path, out bool? success)
        {
            int counter = 0;
            StreamReader stream = null;
            try
            {
                stream = new StreamReader(path);
                while (stream.ReadLine() != null)
                {
                    wordsDict.TryAdd(counter++, stream.ReadLine());
                }
                success = true;
            }
            catch (FileNotFoundException)
            {
                success = exceptionOnLoad?.Invoke("File doesn't exist");
            }
            catch (Exception ex)
            {
                success = exceptionOnLoad?.Invoke($"Something went wrong when reading the file: {ex}");
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
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
