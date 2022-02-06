using System;
using System.Linq;

class MainClass
{
    static void Main(string[] args)
    {
        int fieldsize = 8;
        int shipPieces = 2;
        int shipHits = 0;
        int guesses = 0;
        int guessesLimit = 3;
        int totalHot = 2;
        int totalWarm = 4;
        bool gameOver = true;

        char[,] actualBoard = new char[(fieldsize + 1), (fieldsize + 1)];
        char[,] board = new char[fieldsize, fieldsize];

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int a = 0; a < board.GetLength(1); a++)
            {
                board[i, a] = '.';
                actualBoard[i, a] = '.';
            }
        }

        Random random = new Random();
        
        for (int i = 0; i < shipPieces; i++)
        {
            int x = random.Next(0, fieldsize);
            int y = random.Next(0, fieldsize);
            if (actualBoard[x, y] != 'X')
            {
                actualBoard[x, y] = 'X';

                for (int a = -2; a <= totalHot; a++)
                {
                    for (int b = -2; b <= totalHot; b++)
                    {
                        if (((y + b) <= fieldsize) && ((x + a) <= fieldsize) && ((x + a) >= 0) && ((y + b) >= 0) && (actualBoard[x + a, y + b] != 'X') && (actualBoard[x + a, y + b] != 'H')) actualBoard[x + a, y + b] = 'H';
                    }
                }

                for (int b = -4; b <= totalWarm; b++)
                {
                    for (int c = -4; c <= totalWarm; c++) 
                    {
                        if (((y + c) <= fieldsize) && ((x + b) <= fieldsize) && ((x + b) >= 0) && ((y + c) >= 0) && (actualBoard[x + b, y + c] != 'X') && (actualBoard[x + b, y + c] != 'H')) actualBoard[x + b, y + c] = 'W';
                    }
                }
             
            }
            else i--;
                
        }

        Console.WriteLine("Hello! Welcome to Battleships, you'll be asked to enter two numbers which act as co-ordinates on the AI's grid. You'll be given clues about how well you are doing as you go along. Good luck!");

        while (true)
        {
            try
            {
                Console.Write("Enter a Number: ");
                string colInput = Console.ReadLine();
                int row = Int32.Parse(colInput) - 1;

                Console.Write("Enter a Number: ");
                string rowInput = Console.ReadLine();
                int col = Int32.Parse(rowInput) - 1;

                if (board[col, row] == '.')
                {
                    guesses++;
                    if (actualBoard[col, row] == 'X')
                    {
                        board[col, row] = 'X';
                        actualBoard[col, row] = 'D';
                        Console.WriteLine("Sunk!");
                        shipHits++;
                        for (int i = 0; i < fieldsize; i++)
                        {
                            for (int j = 0; j < fieldsize; j++)
                            {
                                if (actualBoard[i, j] == 'H' || actualBoard[i, j] == 'W')
                                {
                                    actualBoard[i, j] = '.';
                                }
                            }
                        }
                        for (int i = 0; i < fieldsize; i++)
                        {
                            for (int j = 0; j < fieldsize; j++) 
                            {
                                if (actualBoard[i, j] == 'X')
                                {

                                    for (int a = -2; a <= totalHot; a++)
                                    {
                                        for (int b = -2; b <= totalHot; b++)
                                        {
                                            if (((j + b) <= fieldsize) && ((i + a) <= fieldsize) && ((i + a) >= 0) && ((j + b) >= 0) && (actualBoard[i + a, j + b] != 'X') && (actualBoard[i + a, j + b] != 'H')) actualBoard[i + a, j + b] = 'H';
                                        }
                                    }
                                    for (int b = -4; b <= totalWarm; b++)
                                    {
                                        for (int c = -4; c <= totalWarm; c++)
                                        {
                                            if (((j + c) <= fieldsize) && ((i + b) <= fieldsize) && ((i + b) >= 0) && ((j + c) >= 0) && (actualBoard[i + b, j + c] != 'X') && (actualBoard[i + b, j + c] != 'H')) actualBoard[i + b, j + c] = 'W';
                                        }
                                    }
                                }
                            }
                        }
                        if (shipHits == shipPieces)
                        {
                            gameOver = false;
                            break;
                        }
                    }
                    else
                    {
                        board[col, row] = 'O';
                        if (actualBoard[col, row] == 'H') Console.WriteLine("Hot");
                        if (actualBoard[col, row] == 'W') Console.WriteLine("Warm");
                        if (actualBoard[col, row] == '.') Console.WriteLine("Miss");
                    }
                }
                if (guesses >= guessesLimit)
                {
                    Console.WriteLine("You've had too many guesses, Better luck next time!");
                    string input = Console.ReadLine().ToUpper();
                    break;
                }
            }
            catch
            {
                Console.WriteLine("Bad input.");
            }
        }
        if (!gameOver) Console.WriteLine($"You won with {guesses} guesses!");
    }
}