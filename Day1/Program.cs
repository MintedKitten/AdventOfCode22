// See https://aka.ms/new-console-template for more information

StreamReader reader = new("D:\\Files\\AoC\\input_d1.txt");
List<Elf> elves = new();

string? line = "";
Elf grah = new();
while ((line = reader.ReadLine()) != null)
{
    if (int.TryParse(line, out int Cal))
    {
        grah.AddFood(Cal);
    }
    else
    {
        elves.Add(grah);
        grah = new();
    }
}

int Max = elves.Max<Elf>((elf) => { return elf.Calories; });

Console.WriteLine($"Amount: {elves.Count}");
Console.WriteLine($"The Most Calories is: {Max}");
elves.Sort((Elf a, Elf b) => { return a.Calories - b.Calories; });
elves.Reverse();
Console.WriteLine($"1#: {elves[0].Calories}");
Console.WriteLine($"2#: {elves[1].Calories}");
Console.WriteLine($"3#: {elves[2].Calories}");
int Top3 = elves[0].Calories + elves[1].Calories + elves[2].Calories;
Console.WriteLine($"Total: {Top3}");

class Elf
{
    public int Calories
    {
        get
        {
            int total = 0;
            for (int i = 0; i < cals.Count; i++)
            {
                total += cals[i];
            }
            return total;
        }
    }
    private readonly List<int> cals;

    public Elf()
    {
        cals = new List<int>();
    }

    public void AddFood(int food)
    {
        cals.Add(food);
    }

}