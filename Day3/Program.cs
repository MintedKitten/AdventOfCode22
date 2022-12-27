// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;

StreamReader reader = new("../../../../Input/input_d3.txt");

string? line = "";
List<string> lines = new();
int linec = 0;
int prio = 0;
while ((line = reader.ReadLine()) != null)
{
    linec++;
    Console.WriteLine(lines.Count);
    line = line.Trim();
    if (lines.Count == 3)
    {
        foreach (char letter in lines[0])
        {
            // Do things
            if (lines[1].Contains(letter) && lines[2].Contains(letter))
            {
                
                if (letter > 'a')
                {
                    prio += letter - 'a' + 1;
                    Console.WriteLine($"{letter} : {letter - 'a' + 1}");
                }
                else if (letter > 'A')
                {
                    prio += letter - 'A' + 27;
                    Console.WriteLine($"{letter} : {letter - 'A' + 27}");
                }
                break;
            }
        }
        lines = new();
    }
    else
    {
        lines.Add(line);
    }

    //string[] comps = { line[..(line.Length / 2)], line[(line.Length / 2)..] };
    //foreach (char letter in comps[0])
    //{
    //    if (comps[1].Contains(letter))
    //    {
    //        if (letter > 'a')
    //        {
    //            prio += letter - 'a' + 1;
    //            Console.WriteLine($"{letter} : {letter - 'a' + 1}");
    //        }
    //        else if (letter > 'A')
    //        {
    //            prio += letter - 'A' + 27;
    //            Console.WriteLine($"{letter} : {letter - 'A' + 27}");
    //        }
    //        break;
    //    }
    //}
}
Console.WriteLine($"Total Priority: {prio} {linec}");