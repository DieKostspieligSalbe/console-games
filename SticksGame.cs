using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SticksGame
{
    public class SticksGame
    {
        public event Action<string> SticksAction;
        private int sticks;
        public bool pickedLast = false; //true if human, false if computer
        public bool isOver = false;

        public SticksGame()
        {
            sticks = 10;
        }
        public SticksGame(int stickAmount)
        {
            if (stickAmount < 7)
            {
                sticks = 7;
            }
            else
            {
                sticks = stickAmount;
            }

        }

        public void MakeAMove(int sticksAmount)
        {
            if (sticksAmount > 3 && !pickedLast)
            {
                SticksAction?.Invoke($"You cannot take more than 3 sticks!");
                return;
            }
            if (sticksAmount < 1 && !pickedLast)
            {
                SticksAction?.Invoke($"You cannot take less than 1 stick!");
                return;
            }
            if (pickedLast) //computer turn
            {
                Random rand = new();     
                if (sticks <= 3)
                {
                    sticksAmount = rand.Next(1, sticks);
                    sticks -= sticksAmount;
                }
                else
                {
                    sticksAmount = rand.Next(1, 3);
                    sticks -= sticksAmount;
                }
            }
            else //player turn
            {
                if (sticksAmount >= sticks)
                {
                    SticksAction?.Invoke("All the sticks will be taken");
                    sticks = 0;
                }
                else
                {
                    sticks -= sticksAmount;
                }
            }
            SticksAction?.Invoke($"{sticksAmount} sticks were taken. {sticks} sticks are left");
            pickedLast = !pickedLast;
            SticksAction?.Invoke(PrintSticks());
            isOver = VictoryCheck();
        }

        private bool VictoryCheck()
        {
            if (sticks != 0)
            {
                return false;
            }
            string winner = pickedLast ? "Machine" : "Player";
            isOver = true;
            SticksAction?.Invoke($"No sticks left! {winner} wins!");
            return true;
        }

        private string PrintSticks()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sticks; i++)
            {
                sb.Append("| ");
            }
            return sb.ToString();
        }

        public void Print()
        {
            SticksAction?.Invoke(PrintSticks());
        }


    }
}
