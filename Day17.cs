using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using AoC2019.Utils;

namespace AoC2019
{
    public class Day17
    {
        public void Run()
        {
            var result1 = PartOne();

            Console.WriteLine($"PartOne - {result1}");
        }

        private int PartOne()
        {
            var program = GetInput();
            var intCode = new IntCode(program);
            var grid = new Dictionary<(int, int), char>();
            int x = 0, y = 0;
            while (intCode.ProgramState != IntCode.State.Halt)
            {
                intCode.Run();
                var output = intCode.Outputs.Last();
                switch (output)
                {
                    case 10:
                        Console.Write(y);
                        Console.Write(Environment.NewLine);
                        y++;
                        x = 0;
                        break;
                    default:
                        grid.Add((x,y), (char)output);
                        x++;
                        Console.Write((char)output);
                        break;
                }
            }
            Console.Write(Environment.NewLine);
            var intersectionsSum = 0;
            foreach (var item in grid)
            {
                var (thisX, thisY) = item.Key;
                if ((grid.ContainsKey((thisX - 1, thisY)) && grid[(thisX - 1, thisY)] == '#')
                    && (grid.ContainsKey((thisX + 1, thisY)) && grid[(thisX + 1, thisY)] == '#')
                    && (grid.ContainsKey((thisX, thisY - 1)) && grid[(thisX, thisY - 1)] == '#')
                    && (grid.ContainsKey((thisX, thisY + 1)) && grid[(thisX, thisY + 1)] == '#')
                    && (grid[(thisX, thisY)] == '#'))
                {
                    intersectionsSum += (thisX * thisY);
                    Console.WriteLine($"Intersection Found - {thisX}, {thisY} = {thisX * thisY}");
                }
            }

            return intersectionsSum;
        }

        private long[] GetInput()
        {
            return File.ReadAllText(@"input/day17.txt")
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }
    }
}
