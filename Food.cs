class Food
{
    private Random random = new Random();
    public Point Position { get; private set; }

    private int screenWidth;
    private int screenHeight;

    public Food(int screenWidth, int screenHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
    }

    public void Spawn(List<Point> snakeBody)
    {
        do
        {
            Position = new Point(random.Next(1, screenWidth - 1), random.Next(1, screenHeight - 2));
        } while (snakeBody.Contains(Position));
    }
}