using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_14
{
    internal class Part_One
    {
        List<Cords[]> caveMap = new();
        int[] dropPoint = { 500, 0 };
        int[] offSet =  { 0, 0 };

        public static void Main(string[] args)
        {
            using StreamReader reader = new("../../../../Input/input_d14.txt");

            // Sand drop: down, left, right, stop
            string? line = "";

            while ((line = reader.ReadLine()) != null)
            {
                List<int[]> rt = ParseRockPath(line);
                for (int i = 0; i < rt.Count; i++)
                {
                    Console.Write($"a:{rt[i][0]} b:{rt[i][1]} - ");
                }
                //rockPaths.Add(rt);
                Console.WriteLine();
            }

        }

        static void checkMapExpand(int[] check)
        { // Check if map needs to expand.

            // Expands to the left, change offSet.
            return;
        }

        static List<int[]> ParseRockPath(string path)
        {
            List<int[]> inner_list = new();
            int nextPosition = path.IndexOf("->");
            do
            {
                int nextCord = path.IndexOf(",");
                int x = int.Parse(path[..nextCord]);
                int y = int.Parse(path[(nextCord + 1)..nextPosition]);
                inner_list.Add(new int[] { x, y });
                path = path[(nextPosition + 2)..];
                nextPosition = path.IndexOf("->");
            } while (nextPosition > -1);
            return inner_list;
        }
    }
    public class Cords
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CordsType Type { get; set; }

        public enum CordsType
        {
            Air,
            Rock,
            Sand
        }
        public Cords(int x, int y, CordsType type)
        {
            X = x;
            Y = y;
            Type = type;
        }
        public bool isAir()
        {
            return Type === CordsType.Air;
        }
    }
}
