using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8
{
    internal class Part_Two
    {
        static readonly List<List<TreeHouse>> trees = new()
            {
                new()
            };
        public static void Main()
        {
            using StreamReader reader = new("../../../../Input/input_d8.txt");


            for (int i = 0; reader.Peek() != -1; i++)
            {
                string nc = ((char)reader.Read()).ToString();
                if (int.TryParse(nc, out int height))
                {
                    trees[^1].Add(new(height));
                }
                else
                {
                    trees.Add(new());
                }
            }
            // Assign Score
            Scouting();
            int MaxScenicScore = 0;
            TreeHouse maxtreeh = new(0);
            foreach (List<TreeHouse> _trees in trees)
            {
                foreach (TreeHouse tree in _trees)
                {
                    if (tree.ScenicScore() > MaxScenicScore)
                    {
                        MaxScenicScore = tree.ScenicScore();
                        maxtreeh = tree;
                    }
                }
            }
            Console.WriteLine($"Max Scenic Score: {MaxScenicScore}, Tree House: {maxtreeh.UpView},{maxtreeh.DownView},{maxtreeh.LeftView},{maxtreeh.RightView}");
        }

        public static void Scouting()
        {
            for (int i = 0; i < trees.Count; i++)
            {
                for (int j = 0; j < trees[i].Count; j++)
                {
                    int up = trees[i][j].Height, down = up, left = down, right = left;
                    for (int k = 1; (new int[] { up, down, left, right }).Max() > 0; k++)
                    {
                        // Up
                        if (up > -1)
                        {
                            try
                            {
                                if (trees[i - k][j].Height >= up)
                                {
                                    trees[i][j].UpView = k;
                                    up = -1;
                                }
                            }
                            catch
                            {
                                trees[i][j].UpView = k - 1;
                                up = -1;
                            }
                        }
                        // Down
                        if (down > -1)
                        {
                            try
                            {
                                if (trees[i + k][j].Height >= down)
                                {
                                    trees[i][j].DownView = k;
                                    down = -1;
                                }
                            }
                            catch
                            {
                                trees[i][j].DownView = k - 1;
                                down = -1;
                            }
                        }
                        // Left
                        if (left > -1)
                        {
                            try
                            {
                                if (trees[i][j - k].Height >= left)
                                {
                                    trees[i][j].LeftView = k;
                                    left = -1;
                                }
                            }
                            catch
                            {
                                trees[i][j].LeftView = k - 1;
                                left = -1;
                            }
                        }
                        // Right
                        if (right > -1)
                        {
                            try
                            {
                                if (trees[i][j + k].Height >= right)
                                {
                                    trees[i][j].RightView = k;
                                    right = -1;
                                }
                            }
                            catch
                            {
                                trees[i][j].RightView = k - 1;
                                right = -1;
                            }
                        }
                    }
                }
            }
        }
    }
    class TreeHouse : Tree
    {
        public int LeftView { get; set; } = 0;
        public int RightView { get; set; } = 0;
        public int UpView { get; set; } = 0;
        public int DownView { get; set; } = 0;

        public TreeHouse(int height) : base(height)
        {
        }

        public int ScenicScore()
        {
            return LeftView * RightView * UpView * DownView;
        }
    }
}
