// See https://aka.ms/new-console-template for more information

using System.Numerics;

using StreamReader reader = new("../../../../Input/input_d11.txt");

List<Monkey> monkeys = new();
string? line = "";
while ((line = reader.ReadLine()?.Trim()) != null)
{
    string name = line;
    string sitems = reader.ReadLine()?.Trim() ?? "";
    string soprt = reader.ReadLine()?.Trim() ?? "";
    string stest = reader.ReadLine()?.Trim() ?? "";
    string siftrue = reader.ReadLine()?.Trim() ?? "";
    string siffalse = reader.ReadLine()?.Trim() ?? "";
    string smt = reader.ReadLine()?.Trim() ?? "";

    int[] items = sitems.Replace("Starting items: ", "").Split(",").Select((item) => { return int.Parse(item); }).ToArray();
    string oprts = soprt.Replace("Operation: new = ", "");
    int test = int.Parse(stest.Replace("Test: divisible by ", ""));
    int iftrue = int.Parse(siftrue.Replace("If true: throw to monkey ", ""));
    int iffalse = int.Parse(siffalse.Replace("If false: throw to monkey ", ""));

    monkeys.Add(new(items, test, iftrue, iffalse, oprts));
}
Console.WriteLine($"Monkey.Count: {monkeys.Count}");
int mod_all = 1;
foreach (Monkey monkey in monkeys)
{
    mod_all *= monkey.DivTest;
}
int rounds = 10000;
BigInteger[] inspects = new BigInteger[monkeys.Count];
for (int i = 0; i < rounds; i++)
{
    Console.WriteLine($"Round: {i}");
    for (int j = 0; j < monkeys.Count; j++)
    {
        int itemamount = monkeys[j].Items.Count;
        for (int k = 0; k < itemamount; k++)
        {
            inspects[j]++;
            BigInteger Item = monkeys[j].GetNew(monkeys[j].Items[0]);
            monkeys[j].Items.RemoveAt(0);
            if (Item % monkeys[j].DivTest == 0)
            {
                monkeys[monkeys[j].IfTrue].Items.Add(Item % mod_all);
            }
            else
            {
                monkeys[monkeys[j].IfFalse].Items.Add(Item % mod_all);
            }
        }
    }
}
Array.Sort(inspects);
Array.Reverse(inspects);
BigInteger monkeybiz = inspects[0] * inspects[1];
Console.WriteLine($"Monkey Biz: {monkeybiz}");
foreach (var (inspcts, index) in inspects.WithIndex())
{
    Console.WriteLine($"Monkey {index}: {inspcts}");
}
Console.ReadLine();


class Monkey
{
    public List<BigInteger> Items { get; set; } = new();
    public int DivTest { get; set; }
    public int IfTrue { get; set; }
    public int IfFalse { get; set; }
    public int Right { get; set; }
    public string Oprt { get; set; }

    public Monkey(IEnumerable<int> items, int test, int ifTrue, int ifFalse, string oprt)
    {
        foreach (int item in items)
        {
            Items.Add(item);
        }
        DivTest = test;
        IfTrue = ifTrue;
        IfFalse = ifFalse;
        Right = 0;
        Oprt = oprt;
    }

    public BigInteger GetNew(BigInteger Old)
    {
        BigInteger New = 0, left = 0, right = 0;
        string[] commands = Oprt.Split(' ');
        if (int.TryParse(commands[0], out int Left))
        {
            left = Left;
        }
        else if (commands[0] == "old")
        {
            left = Old;
        }
        if (int.TryParse(commands[2], out int Right))
        {
            right = Right;
        }
        else if (commands[0] == "old")
        {
            right = Old;
        }
        switch (commands[1])
        {
            case "+":
                New = left + right;
                break;
            case "*":
                New = left * right;
                break;
        }
        return New;
    }
}

public static class EnumExtension
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
       => self.Select((item, index) => (item, index));
}