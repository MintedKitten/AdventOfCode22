// See https://aka.ms/new-console-template for more information

using StreamReader reader = new("../../../../Input/input_d5.txt");

Stack<string>[] stacks = (Stack<string>[])Array.CreateInstance(typeof(Stack<string>), 9);

for (int i = 0; i < stacks.Length; i++)
{
    stacks[i] = new Stack<string>();
}

string? line = "";
int startIndex = 1, tab = 4;
while ((line = reader.ReadLine()?.Trim()) != null)
{
    if (int.TryParse(line[startIndex] + "", out _))
    {
        break;
    };
    for (int i = 0; i < stacks.Length; i++)
    {
        int index = startIndex + (tab * i);
        if (line[index] + "" != " ")
        {
            stacks[i].Push(line[index] + "");
        }
    }
}
for (int i = 0; i < stacks.Length; i++)
{
    string[] rStack = stacks[i].ToArray();
    stacks[i] = new();
    Array.ForEach(rStack, (item) =>
    {
        stacks[i].Push(item);
    });
}
line = reader.ReadLine();
while ((line = reader.ReadLine()?.Trim()) != null)
{
    string[] instuction = line.Split(' ');
    int loop = int.Parse(instuction[1]), from = int.Parse(instuction[3]) - 1, to = int.Parse(instuction[5]) - 1;
    List<string> moving = new();
    for (int i = 0; i < loop; i++)
    {
        moving.Add(stacks[from].Pop());
    }
    moving.Reverse(); // 2nd Star
    for (int i = 0; i < moving.Count; i++)
    {
        stacks[to].Push(moving[i]);
    }
}
string endCrates = "";
for (int i = 0; i < stacks.Length; i++)
{
    endCrates += stacks[i].Pop();
}
Console.WriteLine($"Ending Crates: {endCrates}");