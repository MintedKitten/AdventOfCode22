// See https://aka.ms/new-console-template for more information

using Day_12;

Part_Two.Main();
Environment.Exit(0);

using StreamReader reader = new("../../../../Input/input_d12.txt");

List<List<Node>> cords = new()
{
    new()
};
string? line = "";
while ((line = reader.ReadLine()) != null)
{
    line = line.Trim();
    for (int i = 0; i < line.Length; i++)
    {
        int height = line[i];
        if (height == 83)
        {
            height = 0;
        }
        else if (height == 69)
        {
            height = 27;
        }
        else if (height > 96)
        {
            height -= 96;
        }
        cords[^1].Add(new(height, cords[^1].Count, cords.Count - 1));
    }
    cords.Add(new());
}
if (cords[^1].Count < 1)
{
    cords.RemoveAt(cords.Count - 1);
}
Node startNode = new();
Node endNode = new();
for (int i = 0; i < cords.Count; i++)
{
    for (int j = 0; j < cords[i].Count; j++)
    {
        if (cords[i][j].Height == 0)
        {
            startNode = cords[i][j];
        }
        else if (cords[i][j].Height == 27)
        {
            endNode = cords[i][j];
        }
    }
}

List<Node> openSet = new();
HashSet<Node> closeSet = new();
startNode.SetEndPoint(endNode);
openSet.Add(startNode);

while (openSet.Count > 0)
{
    Node currentNode = Node.GetLeastF(openSet);
    openSet.Remove(currentNode);
    closeSet.Add(currentNode);

    if (currentNode == endNode)
    {
        RetracePath(startNode, endNode);
        break;
    }

    List<Node> neighbors = GetNeighbors(currentNode);
    foreach (Node neighbor in neighbors)
    {
        if (closeSet.Contains(neighbor) || neighbor.G == int.MaxValue)
        {
            continue;
        }
        int newNeightG = Node.CalcNewG(currentNode, neighbor);
        if ((newNeightG < neighbor.G || !openSet.Contains(neighbor)) && newNeightG < int.MaxValue)
        {
            neighbor.SetEndPoint(endNode);
            neighbor.SetParent(currentNode);
            if (!openSet.Contains(neighbor))
            {
                openSet.Add(neighbor);
            }
        }
    }
}

List<Node> GetNeighbors(Node center)
{
    int x = center.X, y = center.Y;
    List<Node> neighbors = new();
    // Up, Down, Left, Right
    try // Up
    {
        neighbors.Add(cords[y - 1][x]);
    }
    catch { }
    try // Down
    {
        neighbors.Add(cords[y + 1][x]);
    }
    catch { }
    try // Left
    {
        neighbors.Add(cords[y][x - 1]);
    }
    catch { }
    try // Right
    {
        neighbors.Add(cords[y][x + 1]);
    }
    catch { }
    return neighbors;
}

void RetracePath(Node StartNode, Node EndNode)
{
    List<Node> path = new();
    Node currentNode = EndNode;
    while (currentNode != StartNode)
    {
        path.Add(currentNode);
        currentNode = currentNode.Parent;
    }
    path.Reverse();
    int i = 0;
    foreach (Node node in path)
    {
        Console.WriteLine($"{i}: X={node.X},Y={node.Y}");
        i++;
    }
    Console.WriteLine($"Steps: {path.Count}");
}

class Node
{

    public int X { get; }
    public int Y { get; }
    public int G { get; private set; }
    public int H { get; private set; }
    public int F
    {
        get
        {
            if (G == int.MaxValue)
            {
                return int.MaxValue;
            }
            return G + H;
        }
    }
    public int Height { get; }
    public Node Parent { get; private set; }
    private Node End { get; set; }
    public Node() : this(height: 0, x: 0, y: 0)
    { }
    public Node(int height, int x, int y)
    {
        Height = height;
        G = 0;
        H = 0;
        X = x;
        Y = y;
    }
    public static int CalcNewG(Node CenterNode, Node NeighborNode)
    {
        int dx = Math.Abs(NeighborNode.X - CenterNode.X);
        int dy = Math.Abs(NeighborNode.Y - CenterNode.Y);
        int dh = NeighborNode.Height - CenterNode.Height;
        if (dh > 1)
        {
            // Not traversable
            return int.MaxValue;
        }
        else
        {
            if (dx + dy == 2)
            {
                return CenterNode.G + 14;
            }
            else if (dx == 1 || dy == 1)
            {
                return CenterNode.G + 10;
            }
            else
            {
                return CenterNode.G;
            }
        }
    }

    public void SetParent(Node ParentNode)
    {
        Parent = ParentNode;
        G = CalcNewG(Parent, this);
    }
    public void SetEndPoint(Node EndNode)
    {
        End = EndNode;
        int dx = Math.Abs(X - End.X);
        int dy = Math.Abs(Y - End.Y);
        H = (Math.Abs(dx - dy) * 10) + (Math.Min(dx, dy) * 14);
    }
    public static Node GetLeastF(IList<Node> nodes)
    {
        Node lowestF = nodes[0];
        foreach (Node node in nodes)
        {
            if (node.F < lowestF.F || node.F == lowestF.F && node.H < lowestF.H)
            {
                lowestF = node;
            }
        }
        return lowestF;
    }
}