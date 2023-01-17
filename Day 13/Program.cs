﻿// See https://aka.ms/new-console-template for more information

using StreamReader reader = new("../../../../Input/input_d13.txt");

string? line = "";
int round = 1;
int total = 0;
while ((line = reader.ReadLine()) != null)
{
    string line1 = line.Trim();
    string line2 = reader.ReadLine()?.Trim() ?? "";
    string _ = reader.ReadLine()?.Trim() ?? "";

    Console.WriteLine($"Round: {round}");
    List<object> left = GenNewDistressSigList(line1[1..], out _);
    List<object> right = GenNewDistressSigList(line2[1..], out _);
    writeList(left);
    Console.WriteLine();
    writeList(right);
    Console.WriteLine();
    Order result = compareLeftToRight(left, right);
    if (result == Order.Correct)
    {
        total += round;
    }
    Console.WriteLine(result);
    round++;
}
Console.WriteLine($"Total: {total}");

static Order compareLeftToRight(object left, object right)
{
    if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
    {
        int leftint = (int)left;
        int rightint = (int)right;
        if (leftint > rightint)
        {
            return Order.Incorrect;
        }
        else if (leftint < rightint)
        {
            return Order.Correct;
        }
        else
        {
            return Order.Continue;
        }
    }
    if (left.GetType() == typeof(int) && right.GetType() == typeof(List<object>))
    {
        return compareLeftToRight(new List<object>() { left }, right);
    }
    if (left.GetType() == typeof(List<object>) && right.GetType() == typeof(int))
    {
        return compareLeftToRight(left, new List<object>() { right });
    }
    if (left.GetType() == typeof(List<object>) && right.GetType() == typeof(List<object>))
    {
        List<object> leftlist = (List<object>)left;
        List<object> rightlist = (List<object>)right;
        for (int i = 0; i < Math.Max(leftlist.Count, rightlist.Count); i++)
        {
            try
            {
                object temp = leftlist[i];
            }
            catch (ArgumentOutOfRangeException)
            {
                return Order.Correct;
            }
            try
            {
                object temp = rightlist[i];
            }
            catch (ArgumentOutOfRangeException)
            {
                return Order.Incorrect;
            }
            Order result = compareLeftToRight(leftlist[i], rightlist[i]);
            if (result != Order.Continue)
            {
                return result;
            }
        }
    }
    return Order.Continue;
}

// Debug after parseing
static void writeList(List<object> lsob)
{
    Console.Write("[");
    foreach (var item in lsob)
    {
        if (item.GetType() == typeof(int))
        {
            Console.Write($"{item}-");
        }
        else if (item.GetType() == typeof(List<object>))
        {
            writeList((List<object>)item);
        }
    }
    Console.Write("]");
}

static List<object> GenNewDistressSigList(string lines, out string remain)
{
    List<object> inner_list = new();
    while (true)
    {
        if (lines[0].ToString() == "[")
        {
            inner_list.Add(GenNewDistressSigList(lines[1..], out string rm));
            lines = rm;
            continue;
        }
        else
        {
            if (lines[0].ToString() == ",")
            {   // List inside a list
                lines = lines[1..];
                continue;
            }
            else if (lines[0].ToString() == "]")
            {   // List is empty
                remain = lines[1..];
                return inner_list;
            }
            else
            {
                int potentialNextIndex = lines.IndexOf(",");
                if (potentialNextIndex >= 0 && int.TryParse(lines[..potentialNextIndex], out int num))
                {   // Number is not last in list, add number, then go to the next one
                    inner_list.Add(num);
                    lines = lines[potentialNextIndex..];
                    continue;
                }
                else
                {   // Last number of the list
                    int endOfList = lines.IndexOf("]");
                    num = int.Parse(lines[..endOfList]);
                    inner_list.Add(num);
                    lines = lines[endOfList..];
                    continue;
                }
                // Once a number is added to the list, truncated the list to the next section
            }
        }
    }
}

enum Order
{
    Correct,
    Incorrect,
    Continue
}