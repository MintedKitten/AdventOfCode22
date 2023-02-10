using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_14
{
    internal class Part_One
    {
        public static void Main()
        {
            using StreamReader reader = new("../../../../Input/input_d14.txt");

            string? line = "";
            List<int[]> rockPath = new();
            List<List<int[]>> rockpaths = new();
            while ((line = reader.ReadLine()) != null)
            {
                List<int[]> rt = ParseRockPath(line);
                for (int i = 0; i < rt.Count; i++)
                {
                    Console.Write($"a:{rt[i][0]} b:{rt[i][1]} - ");
                }
                Console.WriteLine();
            }
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
}
