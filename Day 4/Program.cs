// See https://aka.ms/new-console-template for more information

StreamReader reader = new("../../../../Input/input_d4.txt");

string? line = "";
while ((line = reader.ReadLine()) != null)
{
    line = line.Trim();
    string[] assignments = line.Split(',');
    string[][] arr = assignments.Select((arrs) => { return arrs.Split("-"); }).ToArray();

    foreach (string[] _arr in arr)
    {
        Console.WriteLine($"{_arr[0]} {_arr[1]}");
    }
    Console.WriteLine($"====");
}
