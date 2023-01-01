// See https://aka.ms/new-console-template for more information

using System.Text;

using StreamReader reader = new("../../../../Input/input_d7.txt");

string? line = "";
Dir root = new();
Dir currentDir = root;

while ((line = reader.ReadLine()?.Trim()) != null)
{
    string[] commands = line.Split(' ');
    if (commands[0] == "$")
    { // Line is a command
        if (commands[1] == "cd")
        {
            // Go to root, go to parent, go into folder
            if (commands[2] == "/")
            {
                currentDir = root;
            }
            else if (commands[2] == "..")
            {
                currentDir = currentDir.Parent ?? new Func<Dir>(() => { Console.WriteLine("Parent is null, returning to root"); return root; })();
            }
            else
            {
                currentDir = currentDir.DirDict[commands[2]];
            }
        }
    }
    else if (commands[0] == "dir")
    { // Line is Directory
        currentDir.AddFolder(commands[1]);
    }
    else if (int.TryParse(commands[0], out var filesize))
    { // Line is File
        currentDir.AddFile(commands[1], filesize);
    }
}
currentDir = root;
Console.WriteLine(currentDir);
int total = 0;
int threadhold = 100000;
ThreadholdLess(currentDir);
Console.WriteLine($"Total size: {total}");
int wholeSysSpace = 70000000;
int minSpace = 30000000;
int needSpace = minSpace - (wholeSysSpace - root.Size);
Console.WriteLine($"Need space: {needSpace}");
Dir smallestDir = new();
ThreadholdMore(currentDir);
Console.WriteLine($"Delete folder: {smallestDir.Name}, size: {smallestDir.Size}");

void ThreadholdLess(Dir folder)
{
    if (folder.Size <= threadhold)
    {
        total += folder.Size;
    }
    foreach (string folderName in folder.DirDict.Keys.ToArray())
    {
        ThreadholdLess(folder.DirDict[folderName]);
    }
}
void ThreadholdMore(Dir folder)
{
    if (smallestDir.Size == 0)
    {
        smallestDir = folder;
    }
    if (folder.Size >= needSpace && folder.Size < smallestDir.Size)
    {
        smallestDir = folder;
    }
    foreach (string folderName in folder.DirDict.Keys.ToArray())
    {
        ThreadholdMore(folder.DirDict[folderName]);
    }
}

class Dir
{
    public string Name { get; }
    public int Size { get; set; }
    public Dir? Parent { get; }
    public Dictionary<string, Dir> DirDict { get; }
    public Dictionary<string, int> FilesDict { get; }
    private readonly string indent = "|  ";

    public Dir() : this("/", null) { }

    public Dir(string name, Dir? parent) : this(name, 0, new(), new(), parent) { }

    public Dir(string name, int size, Dictionary<string, Dir> dirDict, Dictionary<string, int> filesDict, Dir? parent)
    {
        Name = name;
        Size = size;
        DirDict = dirDict;
        FilesDict = filesDict;
        Parent = parent;
    }

    public void AddSize(int addsize)
    {
        Size += addsize;
        Parent?.AddSize(addsize);
    }

    public void RemoveSize(int removesize)
    {
        Size -= removesize;
        Parent?.RemoveSize(removesize);
    }

    public void AddFile(string filename, int filesize)
    {
        AddSize(filesize);
        FilesDict.Add(filename, filesize);
    }

    public void AddFolder(string foldername)
    {
        DirDict.Add(foldername, new Dir(foldername, this));
    }

    public void RemoveFiles(string filename)
    {
        int removesize = FilesDict[filename];
        RemoveSize(removesize);
        FilesDict.Remove(filename);
    }

    public void RemoveFolder(string foldername)
    {
        int removesize = DirDict[foldername].Size;
        RemoveSize(removesize);
        DirDict.Remove(foldername);
    }

    public Dir Clone()
    {
        Dictionary<string, int> cloneFilesDict = new(FilesDict);
        Dictionary<string, Dir> cloneDirDict = new();
        foreach (string Key in DirDict.Keys.ToArray())
        {
            cloneDirDict.Add(Key, DirDict[Key].Clone());
        }
        Dir? cloneParent = Parent?.Clone();
        return new Dir(Name, Size, cloneDirDict, cloneFilesDict, cloneParent);
    }

    public override string ToString()
    {
        string path = $"- {Name} (dir, {Size})";
        int indentLevel = 1;

        return path + ToString(indentLevel);
    }
    private string ToString(int indentLevel)
    {
        string filesandfolder = "";
        if (FilesDict.Count > 0)
        {
            string[] folderNames = FilesDict.Keys.ToArray();
            for (int i = 0; i < folderNames.Length; i++)
            {
                filesandfolder += $"\n{indent.Repeat(indentLevel)}- {folderNames[i]} (file, {FilesDict[folderNames[i]]})";
            }
        }
        if (DirDict.Count > 0)
        {
            string[] keys = DirDict.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                filesandfolder += $"\n{indent.Repeat(indentLevel)}- {keys[i]} (dir, {DirDict[keys[i]].Size})";
                filesandfolder += DirDict[keys[i]].ToString(indentLevel + 1);
            }
        }
        return filesandfolder;
    }
}

// Bob Sammers https://stackoverflow.com/questions/3754582/is-there-an-easy-way-to-return-a-string-repeated-x-number-of-times
public static class StringExtensions
{
    public static string Repeat(this string s, int n)
        => new StringBuilder(s.Length * n).Insert(0, s, n).ToString();
}