// See https://aka.ms/new-console-template for more information

using System.Collections;

using StreamReader reader = new("../../../../Input/input_d13t.txt");

List<IList> first = new();
List<IList> second = new();
string? line = "";
while ((line = reader.ReadLine()) != null)
{
    string line1 = line.Trim() + ",";
    string line2 = reader.ReadLine()?.Trim() + "," ?? "";
    string _ = reader.ReadLine()?.Trim() ?? "";

    string blockS = "";
    for (int i = 0; i < line1.Length; i++)
    {
        string tmp = line1[i].ToString();
        if (tmp == ",")
        {
            for (int j = 0; j < blockS.Length; j++)
            {
                string sectionS = blockS[j].ToString();
                if (sectionS == "[")
                {
                    Console.Write("SAr ");
                }
                else if (int.TryParse(sectionS, out int rc))
                {
                    Console.Write($"N:{rc} ");
                }
                else if (sectionS == "]")
                {
                    Console.Write("EAr ");
                }
            }
            blockS = "";
        }
        else
        {
            blockS += tmp;
        }
    }
    Console.WriteLine();
    blockS = "";
    for (int i = 0; i < line2.Length; i++)
    {
        string tmp = line2[i].ToString();
        if (tmp == ",")
        {
            for (int j = 0; j < blockS.Length; j++)
            {
                string sectionS = blockS[j].ToString();
                if (sectionS == "[")
                {
                    Console.Write("SAr ");
                }
                else if (int.TryParse(sectionS, out int rc))
                {
                    Console.Write($"N:{rc} ");
                }
                else if (sectionS == "]")
                {
                    Console.Write("EAr ");
                }
            }
            blockS = "";
        }
        else
        {
            blockS += tmp;
        }
    }
    Console.WriteLine();
    Console.WriteLine();
}