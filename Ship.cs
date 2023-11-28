
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SeaBattle
{
    public class Ship
    {
        public void Create(char[,] gameBoard, int x, int y, int d, int size)
        {
            if (d == 2)
            {
                for (int i = x; i < x + size; i++)
                {
                    gameBoard[i, y] = '*'; // '*' представляет корабль на поле
                }
            }
            else if (d == 1)
            {
                for (int j = y; j < y + size; j++)
                {
                    gameBoard[x, j] = '*';
                }
            }
        }

    }
}

