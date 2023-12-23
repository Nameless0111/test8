using System;
using System.Collections.Generic;
using System.Threading;

// Перечисление для максимальных границ карты
enum MapBoundary
{
    MaxRight = 40,
    MaxBottom = 20
}

class SnakeGame
{
    private int score;
    private bool gameOver;
    private MapBoundary boundary;

    private List<int> snakeX;
    private List<int> snakeY;
    private int foodX;
    private int foodY;
    private string direction;

    public SnakeGame()
    {
        score = 0;
        gameOver = false;
        boundary = MapBoundary.MaxRight;

        snakeX = new List<int>() { 10, 9, 8 }; // initial positions
        snakeY = new List<int>() { 10, 10, 10 };
        direction = "right";

        Random random = new Random();
        foodX = random.Next(1, (int)boundary);
        foodY = random.Next(1, (int)MapBoundary.MaxBottom);//
    }

    public void Start()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize((int)boundary + 2, (int)MapBoundary.MaxBottom + 2);
        Console.SetBufferSize((int)boundary + 2, (int)MapBoundary.MaxBottom + 2);
        Console.Clear();
        DrawFood();
        DrawSnake();

        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                ChangeDirection(key);
            }

            MoveSnake();
            if (IsCollision())
                gameOver = true;

            Thread.Sleep(200);
        }

        Console.SetCursorPosition(0, (int)MapBoundary.MaxBottom + 2);
        Console.WriteLine("Game Over! Final Score: " + score);
    }

    private void DrawSnake()
    {
        for (int i = 0; i < snakeX.Count; i++)
        {
            Console.SetCursorPosition(snakeX[i], snakeY[i]);
            Console.Write(i == 0 ? "@" : "o");
        }
    }

    private void DrawFood()
    {
        Console.SetCursorPosition(foodX, foodY);
        Console.Write("X");
    }

    private void MoveSnake()
    {
        Console.SetCursorPosition(snakeX[snakeX.Count - 1], snakeY[snakeY.Count - 1]);
        Console.Write(" ");

        for (int i = snakeX.Count - 1; i > 0; i--)
        {
            snakeX[i] = snakeX[i - 1];
            snakeY[i] = snakeY[i - 1];
        }

        switch (direction)
        {
            case "up":
                snakeY[0]--;
                break;
            case "down":
                snakeY[0]++;
                break;
            case "left":
                snakeX[0]--;
                break;
            case "right":
                snakeX[0]++;
                break;
        }

        if (snakeX[0] == foodX && snakeY[0] == foodY)
        {
            // eat food
            score++;
            Random random = new Random();
            foodX = random.Next(1, (int)boundary);
            foodY = random.Next(1, (int)MapBoundary.MaxBottom);
            snakeX.Add(0);
            snakeY.Add(0);

            DrawFood();
        }

        DrawSnake();
    }

    private void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (direction != "down")
                    direction = "up";
                break;
            case ConsoleKey.DownArrow:
                if (direction != "up")
                    direction = "down";
                break;
            case ConsoleKey.LeftArrow:
                if (direction != "right")
                    direction = "left";
                break;
            case ConsoleKey.RightArrow:
                if (direction != "left")
                    direction = "right";
                break;
        }
    }

    private bool IsCollision()
    {
        if (snakeX[0] <= 0 || snakeX[0] >= (int)boundary || snakeY[0] <= 0 || snakeY[0] >= (int)MapBoundary.MaxBottom)
            return true;

        for (int i = 1; i < snakeX.Count; i++)
        {
            if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
                return true;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        SnakeGame snakeGame = new SnakeGame();
        snakeGame.Start();
    }
}