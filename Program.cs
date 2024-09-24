using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int screenWidth = 40;
    static int screenHeight = 20;
    static void Main()
    {
        try
        {
            Console.SetWindowSize(screenWidth, screenHeight);
            Console.CursorVisible = false;

            Snake snake = new Snake(screenWidth / 2, screenHeight / 2);
            Food food = new Food(screenWidth, screenHeight);
            food.Spawn(snake.Body);

            bool gameOver = false;
            Direction currentDirection = Direction.Right;

            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    currentDirection = GetDirection(key, currentDirection);
                }

                snake.Move(currentDirection);

                if (snake.HasCollided(screenWidth, screenHeight))
                {
                    gameOver = true;
                }

                if (snake.Head.Equals(food.Position))
                {
                    snake.Grow();
                    food.Spawn(snake.Body);
                }

                Draw(snake, food);

                Thread.Sleep(300);
            }
        }
        catch (Exception)
        {
            Console.Clear();
            Console.SetCursorPosition(screenWidth / 2 - 5, screenHeight / 2);
            Console.WriteLine("Game Over!");
            return;
        }

        Console.Clear();
        Console.SetCursorPosition(screenWidth / 2 - 5, screenHeight / 2);
        Console.WriteLine("Game Over!");
    }

    static Direction GetDirection(ConsoleKey key, Direction currentDirection)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow: return currentDirection == Direction.Down ? currentDirection : Direction.Up;
            case ConsoleKey.DownArrow: return currentDirection == Direction.Up ? currentDirection : Direction.Down;
            case ConsoleKey.LeftArrow: return currentDirection == Direction.Right ? currentDirection : Direction.Left;
            case ConsoleKey.RightArrow: return currentDirection == Direction.Left ? currentDirection : Direction.Right;
            default: return currentDirection;
        }
    }
    static void Draw(Snake snake, Food food)
    {
        Console.Clear();
        DrawBoardLimit(screenHeight, screenWidth);
        foreach (var part in snake.Body)
        {
            Console.SetCursorPosition(part.X, part.Y);
            Console.Write("O");
        }

        Console.SetCursorPosition(food.Position.X, food.Position.Y);
        Console.Write("X");
    }
    static void DrawBoardLimit(int height, int width)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                {
                    Console.Write("#");
                }
                else Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}