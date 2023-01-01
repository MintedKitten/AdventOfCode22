using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9
{
    internal class Part_Two
    {
        public static void Main()
        {
            using StreamReader reader = new("../../../../Input/input_d9.txt");

            string? line = "";
            RopeKnots ropes = new(10);
            while ((line = reader.ReadLine()?.Trim()) != null)
            {
                line = line.Trim();
                string[] movement = line.Split(' ');
                switch (movement[0])
                {
                    case "L":
                        for (int i = 0; i < int.Parse(movement[1]); i++)
                        {
                            ropes.MoveHeadLeft();
                        }
                        break;
                    case "R":
                        for (int i = 0; i < int.Parse(movement[1]); i++)
                        {
                            ropes.MoveHeadRight();
                        }
                        break;
                    case "U":
                        for (int i = 0; i < int.Parse(movement[1]); i++)
                        {
                            ropes.MoveHeadUp();
                        }
                        break;
                    case "D":
                        for (int i = 0; i < int.Parse(movement[1]); i++)
                        {
                            ropes.MoveHeadDown();
                        }
                        break;
                }
            }
            Console.WriteLine($"A Unique Tail History: {ropes.TailHistory.Count}");
        }

        class RopeKnots
        {
            public int[][] Knots { get; }
            public HashSet<string> TailHistory { get; } = new() { string.Join("-", new int[] { 0, 0 }) };

            public RopeKnots(int amount)
            {
                Knots = new int[amount][];
                for (int i = 0; i < Knots.Length; i++)
                {
                    Knots[i] = new int[2] { 0, 0 };
                }
            }
            public string KnotsPos()
            {
                return string.Join(" ", Knots.Select((knot, index) => { return $"[{knot[0]},{knot[1]}]"; }));
            }
            public void UpdateKnots()
            {
                for (int i = 0; i < Knots.Length; i++)
                {
                    if (i + 1 == Knots.Length)
                    {
                        string temp = string.Join("-", new int[] { Knots[i][0], Knots[i][1] });
                        TailHistory.Add(temp);
                        break;
                    }
                    if (TailNeedsToMove(Knots[i], Knots[i + 1], out int dx, out int dy))
                    {
                        Knots[i + 1][0] = Knots[i + 1][0] + dx;
                        Knots[i + 1][1] = Knots[i + 1][1] + dy;
                    }
                }
            }
            public void MoveHeadUp()
            {
                Knots[0][1]++;
                UpdateKnots();
            }
            public void MoveHeadDown()
            {
                Knots[0][1]--;
                UpdateKnots();
            }
            public void MoveHeadLeft()
            {
                Knots[0][0]--;
                UpdateKnots();
            }
            public void MoveHeadRight()
            {
                Knots[0][0]++;
                UpdateKnots();
            }
            public static bool TailNeedsToMove(int[] front, int[] back, out int dx, out int dy)
            {
                if (front.Length != 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(front), "Array length should be 2");
                }
                if (back.Length != 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(back), "Array length should be 2");
                }
                dx = Math.Sign(front[0] - back[0]);
                dy = Math.Sign(front[1] - back[1]);
                if (Math.Abs(front[0] - back[0]) == 2)
                {
                    return true;
                }
                if (Math.Abs(front[1] - back[1]) == 2)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
