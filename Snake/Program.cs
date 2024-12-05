using Snake;
using System.Drawing;
Coord gridDimensions = new Coord(40, 20);
Coord snakePos = new Coord(10, 1);

Random random = new Random();
Coord applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
int frameDelayMili = 100;
Direction movementDirection = Direction.Down;
int score = 0;
List<Coord> snakePosHistory = new List<Coord>();
int tailLength = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine($"Score: {score}");
    snakePos.ApplyMovementDirection(movementDirection);


    for (int y = 0; y < gridDimensions.Y; y++)
    {
        for (int x = 0; x < gridDimensions.X; x++)
        {
            Coord currentCoord = new Coord(x, y);
            if (snakePos.Equals(currentCoord) || snakePosHistory.Contains(currentCoord))
                Console.Write("■");
            else if (applePos.Equals(currentCoord))
                Console.Write("A");

            else if (x == 0 || x == gridDimensions.X - 1 || y == 0 || y == gridDimensions.Y - 1)
                Console.Write("#");


            else
                Console.Write(" ");
        }
        Console.WriteLine();
    }

    if (snakePos.Equals(applePos))
    {
        score++;
        Console.Beep(1000, 300);
        tailLength++;
        applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
    }

    else if (snakePos.X == 0 || snakePos.Y == 0 || snakePos.X == gridDimensions.X - 1 ||
             snakePos.Y == gridDimensions.Y - 1 || snakePosHistory.Contains(snakePos))
    {

        score = 0;
        tailLength = 1;
        Console.Beep(1000, 900);
        snakePos = new Coord(10, 1);
        snakePosHistory.Clear();
        movementDirection = Direction.Down;
        continue;
    }

    snakePosHistory.Add(new Coord(snakePos.X, snakePos.Y));
    if (snakePosHistory.Count > tailLength)
        snakePosHistory.RemoveAt(0);



    DateTime time = DateTime.Now;

    while ((DateTime.Now - time).Milliseconds < frameDelayMili)
    {

        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirection = Direction.Left;
                    break;

                case ConsoleKey.RightArrow:
                    movementDirection = Direction.Right;
                    break;

                case ConsoleKey.UpArrow:
                    movementDirection = Direction.Up;
                    break;

                case ConsoleKey.DownArrow:
                    movementDirection = Direction.Down;
                    break;
            }
        }

    }
}