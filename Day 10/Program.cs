// See https://aka.ms/new-console-template for more information

using StreamReader reader = new("../../../../Input/input_d10.txt");

string? line = "";
Register regX = new();
while ((line = reader.ReadLine()?.Trim()) != null)
{
    string[] commands = line.Split(' ');
    switch (commands[0])
    {
        case "noop":
            regX.CMNOOP();
            break;
        case "addx":
            int dregx = int.Parse(commands[1]);
            regX.CMADDX(dregx);
            break;
    }
}
Console.WriteLine($"Score: {regX.Score}");

class Register
{
    public int RegX { get; set; } = 1;
    public int Clock { get; set; } = 1;
    public int Score { get; set; } = 0;

    private readonly int[] interestClock = { 20, 60, 100, 140, 180, 220 };
    public Register()
    {
    }
    public void CMNOOP()
    {
        for (int i = 0; i < 1; i++)
        {
            if (IfCheckInterest(out int score))
            {
                Score += score;
            }
            DrawPixel();
            Clock++;
        }
    }
    public void CMADDX(int x)
    {
        for (int i = 0; i < 2; i++)
        {
            if (IfCheckInterest(out int score))
            {
                Score += score;
            }
            DrawPixel();
            Clock++;
        }
        RegX += x;
    }
    public bool IfCheckInterest(out int Score)
    {
        Score = 0;
        if (interestClock.Contains(Clock))
        {
            Score = RegX * Clock;
            return true;
        }
        return false;
    }
    public void DrawPixel()
    {
        int Pos = (Clock - 1) % 40;
        if (Pos == 0)
        {
            Console.WriteLine();
        }
        if (Pos == RegX - 1 || Pos == RegX || Pos == RegX + 1)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
}