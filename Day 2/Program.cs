// See https://aka.ms/new-console-template for more information

StreamReader reader = new("../Input/input_d2.txt");

//Dictionary<string, string> rs = new() { { "A", "Rock" }, { "B", "Paper" }, { "C", "Scissors" }, { "X", "Rock" }, { "Y", "Paper" }, { "Z", "Scissors" } };
//ConcurrentDictionary<string, string> actions = new() { ["A"] = "Rock", ["B"] = "Paper", ["C"] = "Scissors", ["X"] = "Rock", ["Y"] = "Paper", ["Z"] = "Scissors" };

//ConcurrentDictionary<string, string> winacts = new() { ["A"] = "B", ["B"] = "C", ["C"] = "A" };
//ConcurrentDictionary<string, string> loseacts = new() { ["A"] = "C", ["B"] = "A", ["C"] = "B" };

//string[] winconds = { "A Y", "B Z", "C X" };
int win = 0, draw = 0, lose = 0, rock = 0, paper = 0, scissors = 0;

string? line = "";
while ((line = reader.ReadLine()?.Trim()) != null)
{
    switch ($"{line[2]}")
    {
        case "X":
            lose++;
            switch ($"{line[0]}")
            {
                case "A":
                    scissors++;
                    break;
                case "B":
                    rock++;
                    break;
                case "C":
                    paper++;
                    break;
            }
            break;
        case "Y":
            draw++;
            switch ($"{line[0]}")
            {
                case "A":
                    rock++;
                    break;
                case "B":
                    paper++;
                    break;
                case "C":
                    scissors++;
                    break;
            }
            break;
        case "Z":
            win++;
            switch ($"{line[0]}")
            {
                case "A":
                    paper++;
                    break;
                case "B":
                    scissors++;
                    break;
                case "C":
                    rock++;
                    break;
            }
            break;
    }
}

int score = win * 6 + draw * 3 + lose * 0 + rock * 1 + paper * 2 + scissors * 3;
Console.WriteLine($"Score: {score}");
