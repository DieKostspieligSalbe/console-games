using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class TicTacToe
    {
        char[] Field { get; set; } = new char[9] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
        bool WasCross { get; set; }
        public bool IsOver { get; private set; } = false;

        public bool Put(char sign, int cellNumber)
        {
            if (cellNumber > 8 || cellNumber < 0)
            {
                Console.WriteLine("That cell index doesn't exist");
                return false;
            }
            if (!(char.ToUpper(sign) == 'X' || char.ToUpper(sign) == 'O'))
            {
                Console.WriteLine("Invalid char input, please try again");
                return false;
            }
            if (char.IsWhiteSpace(Field[cellNumber]))
            {
                Field[cellNumber] = char.ToUpper(sign);
            }
            else
            {
                Console.WriteLine("No, there's already a symbol in the cell");
                Draw();
                return false;
            }
            Draw();        
            return true;
        }

        public void FreeMoveMaker()
        {
            Console.WriteLine("Choose your symbol and then a cell to put it, like this: X 2 or x2");
            if (InputParse(Console.ReadLine(), out char sign, out int cellNumber))
            {
                if (this.Put(sign, cellNumber))
                {
                    Console.WriteLine("The move was successfully made!");
                    VictoryOrOverflowCheck(); 
                }
            }
            else
            {
                Console.WriteLine("Your input was wrong, please try again. Remember");
                FreeMoveMaker();
            }
        }

        public void MovesInTurns()
        {
            char sign;
            bool nowCross;
            if (WasCross)
            {
                nowCross = false;
                sign = 'O';
            }
            else
            {
                nowCross = true;
                sign = 'X';
            }
            Console.WriteLine($"Now {sign} makes a move. Print a cell number to put it there");
            if (InputParse(Console.ReadLine(), out int cellNumber))
            {
                if (this.Put(sign, cellNumber))
                {
                    Console.WriteLine("The move was successfully made!");
                    WasCross = nowCross;
                    VictoryOrOverflowCheck(); 
                }
            }
            else
            {
                Console.WriteLine("Your input was wrong, please try again. Remember");
                MovesInTurns();
            }

        }
        private bool InputParse(string str, out char sign, out int cellNumber)
        {          
            sign = ' ';
            cellNumber = 0;
            str = str.Trim();
            try
            {
                sign = str[0];
                if (str.Length == 2)
                {
                    cellNumber = int.Parse(str[1].ToString()) - 1;
                }
                else if (str.Length == 3)
                {
                    cellNumber = int.Parse(str[2].ToString()) - 1;
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }
         private bool InputParse(string str, out int cellNumber)
        {
            cellNumber = 0;
            str = str.Trim();
            if (int.TryParse(str, out int result))
            {
                cellNumber = result - 1;
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Draw()
        {
            Console.WriteLine($" {Field[0]} | {Field[1]} | {Field[2]}");
            Console.WriteLine("----------");
            Console.WriteLine($" {Field[3]} | {Field[4]} | {Field[5]}");
            Console.WriteLine("----------");
            Console.WriteLine($" {Field[6]} | {Field[7]} | {Field[8]}");
        }
        public void DrawFirstTime()
        {
            Console.WriteLine($" 1 | 2 | 3");
            Console.WriteLine("----------");
            Console.WriteLine($" 4 | 5 | 6");
            Console.WriteLine("----------");
            Console.WriteLine($" 7 | 8 | 9");
        }
        public bool VictoryOrOverflowCheck() 
        {
            char sign;
            if (WasCross)
            {         
                sign = 'X';
            }
            else
            {
                sign = 'O';
            }
            bool diagonalWin = (!char.IsWhiteSpace(Field[0]) && Field[0].Equals(Field[4]) && Field[4].Equals(Field[8])) || (!char.IsWhiteSpace(Field[2]) && Field[2].Equals(Field[4]) && Field[4].Equals(Field[6]));
            bool horizontalWin = (!char.IsWhiteSpace(Field[0]) && Field[0].Equals(Field[1]) && Field[1].Equals(Field[2])) || (!char.IsWhiteSpace(Field[3]) && Field[3].Equals(Field[4]) && Field[4].Equals(Field[5])) || (!char.IsWhiteSpace(Field[6]) && Field[6].Equals(Field[7]) && Field[7].Equals(Field[8]));
            bool verticalWin = (!char.IsWhiteSpace(Field[0]) && Field[0].Equals(Field[3]) && Field[3].Equals(Field[6])) || (!char.IsWhiteSpace(Field[1]) && Field[1].Equals(Field[4]) && Field[4].Equals(Field[7])) || (!char.IsWhiteSpace(Field[2]) && Field[2].Equals(Field[5]) && Field[5].Equals(Field[8]));
            {
                if (diagonalWin || horizontalWin || verticalWin)
                {
                    Console.WriteLine($"Congratulations! {sign} won!");
                    IsOver = true;
                    return true;
                }       
            }
            if (!Field.Contains(' '))
            {
                IsOver = true;
                Console.WriteLine("The field is now full and it's a draw");
                return true;
            }
            return false;
        }
    }
}
