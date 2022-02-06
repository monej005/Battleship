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
        int guessesLimit = 20;
        bool play = true;
        int totalHot = 2;
        int totalWarm = 4;

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
                    if (((x + a) <= fieldsize) && ((x + a) >= 0) && (actualBoard[x + a, y] != 'X') && (actualBoard[x + a, y] != 'H')) actualBoard[x + a, y] = 'H';

                    if (((x + a) <= fieldsize) && ((y - 2) >= 0) && ((x + a) >= 0) && (actualBoard[x + a, y - 2] != 'X') && (actualBoard[x + a, y - 2] != 'H')) actualBoard[x + a, y - 2] = 'H';
                    if (((x + a) <= fieldsize) && ((y - 1) >= 0) && ((x + a) >= 0) && (actualBoard[x + a, y - 1] != 'X') && (actualBoard[x + a, y - 1] != 'H')) actualBoard[x + a, y - 1] = 'H';

                    if (((y + 1) <= fieldsize) && ((x + a) <= fieldsize) && ((x + a) >= 0) && (actualBoard[x + a, y + 1] != 'X') && (actualBoard[x + a, y + 1] != 'H')) actualBoard[x + a, y + 1] = 'H';
                    if (((y + 2) <= fieldsize) && ((x + a) <= fieldsize) && ((x + a) >= 0) && (actualBoard[x + a, y + 2] != 'X') && (actualBoard[x + a, y + 2] != 'H')) actualBoard[x + a, y + 2] = 'H';
                }

                for (int b = -4; b <= totalWarm; b++)
                {
                    if (((x + b) <= fieldsize) && ((x + b) >= 0) && (actualBoard[x + b, y] != 'X') && (actualBoard[x + b, y] != 'H')) actualBoard[x + b, y] = 'W';
                    
                    if (((x + b) <= fieldsize) && ((y - 4) >= 0) && ((x + b) >= 0) && (actualBoard[x + b, y - 4] != 'X') && (actualBoard[x + b, y - 4] != 'H')) actualBoard[x + b, y - 4] = 'W';
                    if (((x + b) <= fieldsize) && ((y - 3) >= 0) && ((x + b) >= 0) && (actualBoard[x + b, y - 3] != 'X') && (actualBoard[x + b, y - 3] != 'H')) actualBoard[x + b, y - 3] = 'W';
                    if (((x + b) <= fieldsize) && ((y - 2) >= 0) && ((x + b) >= 0) && (actualBoard[x + b, y - 2] != 'X') && (actualBoard[x + b, y - 2] != 'H')) actualBoard[x + b, y - 2] = 'W';
                    if (((x + b) <= fieldsize) && ((y - 1) >= 0) && ((x + b) >= 0) && (actualBoard[x + b, y - 1] != 'X') && (actualBoard[x + b, y - 1] != 'H')) actualBoard[x + b, y - 1] = 'W';

                    if (((y + 1) <= fieldsize) && ((x + b) <= fieldsize) && ((x + b) >= 0) && (actualBoard[x + b, y + 1] != 'X') && (actualBoard[x + b, y + 1] != 'H')) actualBoard[x + b, y + 1] = 'W';
                    if (((y + 2) <= fieldsize) && ((x + b) <= fieldsize) && ((x + b) >= 0) && (actualBoard[x + b, y + 2] != 'X') && (actualBoard[x + b, y + 2] != 'H')) actualBoard[x + b, y + 2] = 'W';
                    if (((y + 3) <= fieldsize) && ((x + b) <= fieldsize) && ((x + b) >= 0) && (actualBoard[x + b, y + 3] != 'X') && (actualBoard[x + b, y + 3] != 'H')) actualBoard[x + b, y + 3] = 'W';
                    if (((y + 4) <= fieldsize) && ((x + b) <= fieldsize) && ((x + b) >= 0) && (actualBoard[x + b, y + 4] != 'X') && (actualBoard[x + b, y + 4] != 'H')) actualBoard[x + b, y + 4] = 'W';
                }
             
            }
            else i--;
                
        }
        
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
                        if (shipHits == shipPieces)
                        {
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
                    Console.WriteLine("You've had too many guesses, play again?(y/n)");
                    string input = Console.ReadLine().ToUpper();
                    if (input == "Y")
                    {
                        play = true;
                        break;
                    }
                    else if (input == "N")
                    {
                        play = false;
                        break;
                    }
                    Console.Write("Not valid input");
                    if (!play)
                    {
                        break;
                    }

                }
            }
            catch
            {
                Console.WriteLine("Bad input.");
            }

        }

        Console.WriteLine($"You won with {guesses} guesses!");

    }
}