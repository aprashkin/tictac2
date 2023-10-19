using System;

class Program
{
  static char[,] board = new char[3, 3]; // Игровое поле, представленное двумерным массивом
  static char currentPlayer = 'X';

  static void Main()
  {
    bool shouldExit = false;
    do
    {
      Console.Clear();
      Console.WriteLine("Добро пожаловать в игру Крестики-нолики!");
      InitializeBoard(); // Инициализация игрового поля
      int menuChoice = DisplayMainMenu();
      switch (menuChoice)
      {
        case 1:
          PlayGame();
          break;
        case 2:
          DisplayRules();
          break;
        case 3:
          shouldExit = true;
          break;
        default:
          Console.WriteLine("Недопустимый выбор. Попробуйте еще раз.");
          break;
      }
    } while (!shouldExit);
  }

  static void InitializeBoard()
  {
    for (int row = 0; row < 3; row++)
    {
      for (int col = 0; col < 3; col++)
      {
        board[row, col] = (row * 3 + col + 1).ToString()[0];
      }
    }
  }

  static int DisplayMainMenu()
  {
    Console.WriteLine("Главное меню:");
    Console.WriteLine("1. Начать игру");
    Console.WriteLine("2. Прочитать правила");
    Console.WriteLine("3. Выйти");
    int choice;
    while (true)
    {
      Console.Write("Выберите вариант (1-3): ");
      if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
      {
        return choice;
      }
      else
      {
        Console.WriteLine("Недопустимый выбор. Попробуйте еще раз.");
      }
    }
  }

  static void DisplayRules()
  {
    Console.WriteLine("Правила игры:");
    Console.WriteLine("Игроки ходят по очереди.");
    Console.WriteLine("Для совершения хода введите номер ячейки (1-9).");
    Console.WriteLine("Цель - собрать три своих символа в ряд по горизонтали, вертикали или диагонали.");
    Console.WriteLine("Если все ячейки заполняются, и победителя не выявлено, игра объявляется ничьей.");
    Console.WriteLine("Удачи!");
    Console.WriteLine("Нажмите Enter, чтобы вернуться в главное меню.");
    Console.ReadLine();
  }

  static void PlayGame()
  {
    bool gameOver = false;
    int moves = 0;
    do
    {
      Console.Clear();
      DisplayBoard();
      int choice;
      bool validInput;
      do
      {
        Console.Write($"Игрок {currentPlayer}, введите номер ячейки (1-9): ");
        validInput = int.TryParse(Console.ReadLine(), out choice);
        if (!validInput || choice < 1 || choice > 9)
        {
          Console.WriteLine("Недопустимый ход. Попробуйте еще раз.");
          validInput = false;
        }
      } while (!validInput);

      if (MakeMove(choice))
      {
        moves++;
        if (CheckForWin())
        {
          Console.Clear();
          DisplayBoard();
          Console.WriteLine($"Игрок {currentPlayer} выиграл!");
          gameOver = true;
        }
        else if (moves == 9)
        {
          Console.Clear();
          DisplayBoard();
          Console.WriteLine("Ничья!");
          gameOver = true;
        }
        else
        {
          currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
      }
      else
      {
        Console.WriteLine("Ячейка уже занята. Попробуйте еще раз.");
      }
    } while (!gameOver);
    Console.Write("Нажмите Enter, чтобы вернуться в главное меню...");
    Console.ReadLine();
  }
  static void DisplayBoard()
  {
    Console.WriteLine("----+---+---");
    for (int row = 0; row < 3; row++)
    {
      Console.Write("  ");
      for (int col = 0; col < 3; col++)
      {
        Console.Write(board[row, col]);
        if (col < 2)
        {
          Console.Write(" | ");
        }
      }
      Console.WriteLine();
      if (row < 2)
      {
        Console.WriteLine("---+---+---");
      }
    }
  }

  static bool MakeMove(int choice)
  {
    // Проверяем, свободна ли выбранная ячейка и делаем ход, если она свободна
    int row = (choice - 1) / 3;
    int col = (choice - 1) % 3;
    if (board[row, col] != 'X' && board[row, col] != 'O')
    {
      board[row, col] = currentPlayer;
      return true;
    }
    return false;
  }

  static bool CheckForWin()
  {
    // Проверяем победные комбинации
    for (int i = 0; i < 3; i++)
    {
      if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
      {
        return true; // Горизонталь
      }
      if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
      {
        return true; // Вертикаль
      }
    }
    if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
    {
      return true; // Диагональ \
    }
    if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
    {
      return true; // Диагональ /
    }
    return false;
  }
}
//крестики нолики две миланы в домике
