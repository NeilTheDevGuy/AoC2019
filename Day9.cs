using AoC2019.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static AoC2019.Utils.IntCode;

namespace AoC2019
{
    public class Day9
    {
        public void Run()
        {
            var result1 = PartOne();
            var result2 = PartTwo();
            Console.WriteLine($"PartOne - {result1}");
            Console.WriteLine($"PartTwo - {result2}");
        }

        private long PartOne()
        {
            var boost = new IntCode(GetInput());
            boost.Inputs.Enqueue(1);
            while (boost.ProgramState != State.Halt)
            {
                boost.Run();
            }
            return boost.Outputs.Last();
        }

        private long PartTwo()
        {
            var boost = new IntCode(GetInput());
            boost.Inputs.Enqueue(2);
            while (boost.ProgramState != State.Halt)
            {
                boost.Run();
            }
            return boost.Outputs.Last();
        }

        private long[] GetInput()
        {
            return File.ReadAllText(@"input/day9.txt")            
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }
    }
}
