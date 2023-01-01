// See https://aka.ms/new-console-template for more information

using Day_9;

Part_Two.Main();
Environment.Exit(0);

using StreamReader reader = new("../../../../Input/input_d9.txt");

string? line = "";
Rope rp = new();
while ((line = reader.ReadLine()?.Trim()) != null)
{
    string[] movement = line.Split(' ');
    switch (movement[0])
    {
        case "L":
            for (int i = 0; i < int.Parse(movement[1]); i++)
            {
                rp.MoveHeadLeft();
            }
            break;
        case "R":
            for (int i = 0; i < int.Parse(movement[1]); i++)
            {
                rp.MoveHeadRight();
            }
            break;
        case "U":
            for (int i = 0; i < int.Parse(movement[1]); i++)
            {
                rp.MoveHeadUp();
            }
            break;
        case "D":
            for (int i = 0; i < int.Parse(movement[1]); i++)
            {
                rp.MoveHeadDown();
            }
            break;
    }
}
Console.WriteLine($"End Unique Tail History: {rp.TailHistory.Count}");

class Rope
{
    public int[] Head { get; } = { 0, 0 };
    public int[] Tail { get; } = { 0, 0 };
    public HashSet<string> TailHistory { get; } = new() { string.Join("-", new int[] { 0, 0 }) };

    public Rope()
    {
    }
    public void MoveHeadUp()
    {
        Head[1]++;
        if (TailNeedsToMove())
        {
            Tail[0] = Head[0];
            Tail[1] = Head[1] - 1;
            string temp = string.Join("-", new int[] { Tail[0], Tail[1] });
            TailHistory.Add(temp);
        }
    }
    public void MoveHeadDown()
    {
        Head[1]--;
        if (TailNeedsToMove())
        {
            Tail[0] = Head[0];
            Tail[1] = Head[1] + 1;
            string temp = string.Join("-", new int[] { Tail[0], Tail[1] });
            TailHistory.Add(temp);
        }
    }
    public void MoveHeadLeft()
    {
        Head[0]--;
        if (TailNeedsToMove())
        {
            Tail[0] = Head[0] + 1;
            Tail[1] = Head[1];
            string temp = string.Join("-", new int[] { Tail[0], Tail[1] });
            TailHistory.Add(temp);
        }
    }
    public void MoveHeadRight()
    {
        Head[0]++;
        if (TailNeedsToMove())
        {
            Tail[0] = Head[0] - 1;
            Tail[1] = Head[1];
            string temp = string.Join("-", new int[] { Tail[0], Tail[1] });
            TailHistory.Add(temp);
        }
    }
    public bool TailNeedsToMove()
    {
        if (Math.Abs(Head[0] - Tail[0]) == 2)
        {
            return true;
        }
        if (Math.Abs(Head[1] - Tail[1]) == 2)
        {
            return true;
        }
        return false;
    }
}