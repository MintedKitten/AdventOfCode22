// See https://aka.ms/new-console-template for more information

using StreamReader reader = new("../../../../Input/input_d6.txt");

int length = 14, index = 0;
int[] buffer = new int[length];
for (int i = 0; reader.Peek() != -1; i++)
{
    int nc = reader.Read(); 
    buffer[i % length] = nc;
    if (i >= length - 1)
    {
        HashSet<int> tmp = new(buffer);
        if(tmp.Count == length)
        {
            index = i + 1;
            break;
        }
    }
}
Console.WriteLine($"Start at index: {index}");