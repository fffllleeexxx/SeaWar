using SeaBattle;

char[,] board = new char[10, 10];
char[,] aiBoard = new char[10, 10];
char[,] aioBoardP = new char[10, 10];

string input;
int row;
int col;
int direction;

Ship ship = new Ship();

int fourIndex = 1;
int threeIndex = 2;
int twoIndex = 3;
int oneIndex = 4;

bool player = true;


while (CountOfShips(oneIndex, twoIndex, threeIndex, fourIndex))
{
    Console.WriteLine("Вам нужно расставить корабли. Напишите какой корабль хотите поставить");
    string type = Console.ReadLine();
    switch (type)
    {

        case "Однопалубный":
            if (oneIndex > 0)
            {
                Console.WriteLine("Напишите координату коробля:");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                Console.Clear();

                if (CanPlaceShip(row, col, 1, 1, board))
                {
                    ship.Create(board, row, col, 1, 1);
                    FillSurroundings(row, col, 1, 1, board);
                    oneIndex--;
                }
                else { Console.WriteLine("Нельзя повставить корабль"); }
                Print(board);
            }
            else { Console.WriteLine("Однопалубные кончились"); }
            break;

        case "Двупалубный":
            if (twoIndex > 0)
            {
                Console.WriteLine("Напишите первую координату и направление (либо вправо(1), либо вниз(2))");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                direction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CanPlaceShip(row, col, 2, direction, board))
                {
                    ship.Create(board, row, col, direction, 2);
                    FillSurroundings(row, col, 2, direction, board);
                    twoIndex--;
                }
                else { Console.WriteLine("Нельзя поставить корабль"); }

                Print(board);
            }
            else { Console.WriteLine("Двупалубные кончились"); }
            break;

        case "Трёхпалубный":
            if (threeIndex > 0)
            {
                Console.WriteLine("Напишите первую координату и направление (либо вправо(1), либо вниз(2)");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                direction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CanPlaceShip(row, col, 3, direction, board))
                {
                    ship.Create(board, row, col, direction, 3);
                    FillSurroundings(row, col, 3, direction, board);
                    threeIndex--;
                }
                else { Console.WriteLine("Нельзя повставить корабль"); }

                Print(board);
            }
            else { Console.WriteLine("Трёхпалубные кончились"); }
            break;

        case "Четырёхпалубный":
            if (fourIndex > 0)
            {
                Console.WriteLine("Напишите первую координату и направление (либо вправо(1), либо вниз(2)");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                direction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CanPlaceShip(row, col, 4, direction, board))
                {
                    ship.Create(board, row, col, direction, 4);
                    FillSurroundings(row, col, 4, direction, board);
                    fourIndex--;
                }
                else { Console.WriteLine("Нельзя повставить корабль"); }

                Print(board);

            }
            else { Console.WriteLine("Четырёхпалубные кончились"); }
            break;
    }
}

fourIndex = 1;
threeIndex = 2;
twoIndex = 3;
oneIndex = 4;
Random rnd = new Random();


while (CountOfShips(oneIndex, twoIndex, threeIndex, fourIndex))
{
    while (oneIndex > 0)
    {
        row = rnd.Next(10);
        col = rnd.Next(10);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, 1, 1);
            oneIndex--;
        }
    }
    while (twoIndex > 0)
    {
        row = rnd.Next(9);
        col = rnd.Next(9);
        direction = rnd.Next(1, 2);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, direction, 2);
            twoIndex--;
        }
    }
    while (threeIndex > 0)
    {
        row = rnd.Next(8);
        col = rnd.Next(8);
        direction = rnd.Next(1, 2);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, 1, 3);
            threeIndex--;
        }
    }
    while (fourIndex > 0)
    {
        row = rnd.Next(7);
        col = rnd.Next(7);
        direction = rnd.Next(1, 2);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, direction, 4);
            fourIndex--;
        }
    }
}

bool t = false;

while (true)
{
    while (player)
    {
        Console.WriteLine("Напишите, куда хотите выстрелить");
        input = Console.ReadLine().ToUpper();
        row = input[1] - '0';
        col = input[0] - 'A';
        if (Shoot(row, col, aiBoard) == true)
        {
            aioBoardP[row, col] = 'X';
            Print(aioBoardP);
            if (isWin(board) == true)
            {
                Console.WriteLine("Победил игрок");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            continue;
        }
        else { player = false; }
    }

    while (player == false)
    {
        row = rnd.Next(10);
        col = rnd.Next(10);
        if (Shoot(row, col, board) == true)
        {
            board[row, col] = 'X';
            Print(board);
            if (isWin(board) == true)
            {
                Console.WriteLine("Победил компьютер");
                break;
            }
            t = true;
            while (t)
            {
                row = rnd.Next(row - 1, row + 1);
                col = rnd.Next(col - 1, col + 1);
                if ((row >= 0 && row <= 9 && col >= 0 && col <= 9))
                {
                    if (Shoot(row, col, board) == true)
                    {
                        board[row, col] = 'X';
                        Print(board);
                        if (isWin(board) == true)
                        {
                            Console.WriteLine("Победил компьютер");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                        }
                        continue;
                    }
                    else { t = false; player = true; }
                }
                else { continue; }
            }
            continue;
        }
        else { player = true; }
    }
}




void Print(char[,] c)
{
    Console.WriteLine("   A B C D E F G H I J");
    Console.WriteLine("   -------------------");
    for (int i = 0; i < 10; i++)
    {
        Console.Write((i) + "| ");
        for (int j = 0; j < 10; j++)
        {

            Console.Write(c[i, j] + "  ");
        }
        Console.WriteLine();
    }
}

static bool CountOfShips(int x, int y, int z, int v)
{
    if (x == 0 && y == 0 && z == 0 && v == 0)
    {
        return false;
    }
    return true;
}

static bool CheckAdjacentShips(int x, int y, char[,] grid)
{
    for (int i = Math.Max(0, x - 1); i <= Math.Min(10 - 1, x + 1); i++)
    {
        for (int j = Math.Max(0, y - 1); j <= Math.Min(10 - 1, y + 1); j++)
        {
            if (grid[i, j] == '*' && (i != x || j != y))
            {
                return true;
            }
        }
    }
    return false;
}

bool Shoot(int row, int col, char[,] b)
{
    if (b[row, col] == ' ' || b[row, col] == '~')
    {
        b[row, col] = 'O'; // промах
        return false;
    }
    else if (b[row, col] == '*')
    {
        b[row, col] = 'X'; // попадание
        return true;
    }
    return false;
}

bool isWin(char[,] b)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if (b[i, j] == '*')
            {
                return false;
            }
        }
    }
    return true;
}

static bool CanPlaceShip(int x, int y, int size, int direction, char[,] gameBoard)
{
    if (direction == 2)
    {
        // Проверка, не выходит ли корабль за пределы поля
        if (x + size > 10)
            return false;

        // Проверка, не пересекается ли с другими кораблями
        for (int i = x; i < x + size; i++)
        {
            if (gameBoard[i, y] != '\0')
                return false;
        }
    }
    else if (direction == 1)
    {
        // Проверка, не выходит ли корабль за пределы поля
        if (y + size > 10)
            return false;

        // Проверка, не пересекается ли с другими кораблями
        for (int j = y; j < y + size; j++)
        {
            if (gameBoard[x, j] != '\0')
                return false;
        }
    }

    return true;
}

static void FillSurroundings(int x, int y, int size, int direction, char[,] gameBoard)
{
    for (int i = x - 1; i <= x + size; i++)
    {
        for (int j = y - 1; j <= y + 1; j++)
        {
            if (i >= 0 && i < 10 && j >= 0 && j < 10 && gameBoard[i, j] == '\0')
            {
                gameBoard[i, j] = '~';
            }
        }
    }

    if (direction == 2)
    {
        for (int j = y - 1; j <= y + 1; j++)
        {
            if (x - 1 >= 0 && x - 1 < 10 && j >= 0 && j < 10 && gameBoard[x - 1, j] == '\0')
            {
                gameBoard[x - 1, j] = '~';
            }

            if (x + size < 10 && x + size >= 0 && j >= 0 && j < 10 && gameBoard[x + size, j] == '\0')
            {
                gameBoard[x + size, j] = '~';
            }
        }
    }
    else if (direction == 1)
    {
        for (int i = x - 1; i <= x + 1; i++)
        {
            if (i >= 0 && i < 10 && y - 1 >= 0 && y - 1 < 10 && gameBoard[i, y - 1] == '\0')
            {
                gameBoard[i, y - 1] = '~';
            }

            if (i >= 0 && i < 10 && y + size < 10 && y + size >= 0 && gameBoard[i, y + size] == '\0')
            {
                gameBoard[i, y + size] = '~';
            }
        }
    }
}
