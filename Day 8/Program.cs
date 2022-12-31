// See https://aka.ms/new-console-template for more information

using Day_8;

using StreamReader reader = new("../../../../Input/input_d8.txt");

Part_Two.Main();
Environment.Exit(0);

int visibleTrees = 0, invisibleTrees = 0;
int rowHeight = -1;
List<int> columnHeight = new() { -1 };
List<List<Tree>> trees = new()
{
    new()
};
for (int i = 0; reader.Peek() != -1; i++)
{
    string nc = ((char)reader.Read()).ToString();
    if (int.TryParse(nc, out int height))
    {
        trees[^1].Add(new(height));
        if (trees.Count == 1)
        {
            columnHeight.Add(-1);
        }
        try
        {
            // Top - Column
            if (trees[^1][^1].Height > columnHeight[trees[^1].Count - 1])
            {
                trees[^1][^1].Visible = true;
                columnHeight[trees[^1].Count - 1] = trees[^1][^1].Height;
            }
            // Left - Row
            if (trees[^1][^1].Height > rowHeight)
            {
                trees[^1][^1].Visible = true;
                rowHeight = trees[^1][^1].Height;
            }
        }
        catch (Exception)
        {
            trees[^1][^1].Visible = true;
        }
    }
    else
    {
        rowHeight = -1;
        trees.Add(new());
    }
}
// Last line is empty
if (trees[^1].Count < 1)
{
    trees.RemoveAt(trees.Count - 1);
}
for (int i = 0; i < columnHeight.Count; i++)
{
    columnHeight[i] = -1;
}
for (int i = trees.Count; i > 0; i--)
{
    rowHeight = -1;
    for (int j = trees[^i].Count; j > 0; j--)
    {
        try
        {
            // Bottom - Column
            if (trees[i - 1][j - 1].Height > columnHeight[j - 1])
            {
                trees[i - 1][j - 1].Visible = true;
                columnHeight[j - 1] = trees[i - 1][j - 1].Height;
            }
            // Right - Row
            if (trees[i - 1][j - 1].Height > rowHeight)
            {
                trees[i - 1][j - 1].Visible = true;
                rowHeight = trees[i - 1][j - 1].Height;
            }
        }
        catch (Exception)
        {
            trees[i - 1][j - 1].Visible = true;
        }
    }
}
foreach (List<Tree> _trees in trees)
{
    foreach (Tree tree in _trees)
    {
        if (tree.Visible)
        {
            Console.Write("O");
            visibleTrees++;
        }
        else
        {
            Console.Write("X");
            invisibleTrees++;
        }
    }
    Console.WriteLine();
}
Console.WriteLine($"Visible: {visibleTrees}, Invisible: {invisibleTrees}");

class Tree
{
    public int Height { get; }

    public bool Visible { get; set; } = false;

    public Tree(int height)
    {
        Height = height;
    }
}