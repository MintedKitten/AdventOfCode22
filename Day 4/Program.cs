// See https://aka.ms/new-console-template for more information

StreamReader reader = new("../../../../Input/input_d4.txt");

string? line = "";
//int nonlapse = 0, all = 0, lapse = 0;
int nonlapse = 0, somelapse = 0;
while ((line = reader.ReadLine()) != null)
{
    //all++;
    line = line.Trim();
    string[] assignments = line.Split(',');
    string[][] arr = assignments.Select((arrs) => { return arrs.Split("-"); }).ToArray();

    int[] first = { int.Parse(arr[0][0]), int.Parse(arr[0][1]) };
    int[] second = { int.Parse(arr[1][0]), int.Parse(arr[1][1]) };

    //int checkFront = first[0] - second[0];
    //int checkBack = first[1] - second[1];

    //if (checkFront * checkBack > 0)
    //{
    //    nonlapse++;
    //    Console.WriteLine("Not entirely overlap");
    //}
    //else
    //{
    //    lapse++;
    //    if (checkFront < 0 || checkBack < 0)
    //    {
    //        Console.WriteLine("The second fully contains the first");
    //    }
    //    else
    //    {
    //        Console.WriteLine("The first fully contains the second");
    //    }
    //}

    bool isFirstHigher = first[1] >= second[0];
    bool isSecondHigher = second[1] >= first[0];

    if (isFirstHigher ^ isSecondHigher)
    {
        nonlapse++;
    }
    else
    {
        somelapse++;
    }
}
Console.WriteLine($"Nonlapse: {nonlapse}, Somelapse: {somelapse}");
//Console.WriteLine($"Nonlapse: {nonlapse}, all: {all}, lapse: {lapse}");
