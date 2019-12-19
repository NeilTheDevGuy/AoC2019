using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AoC2019.Utils;

namespace AoC2019
{
    public class Day19
    {
        public void Run()
        {
            var result1 = PartOne();
            var result2 = PartTwo();
            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        private long PartOne()
        {
            var input = GetInput();
            var beamCount = 0;
            for (var x = 0; x < 50; x++)
            {
                for (var y = 0; y < 50; y++)
                {
                    if (IsInBeam(x, y, input))
                        beamCount++;
                }
            }
            return beamCount;
        }

        private long PartTwo()
        {
            var input = GetInput();
            int x = 0, y = 100; //Start y at 100 as that is the min for a 100x100 square. Could probably bump it up a bit.
            while (true)
            {
                while (!IsInBeam(x, y, input)) //Look for an x that's in the beam on this y coordinate.
                    x++;

                if (IsInBeam(x + 99, y - 99, input)) //Check the diagonally opposite coordinate.
                    break;

                y++;
            }
            return 10000 * x + (y - 99);
        }

        private bool IsInBeam(int x, int y, long[] input)
        {
            var program = new IntCode(input);
            program.Inputs.Enqueue(x);
            program.Inputs.Enqueue(y);
            program.Run();
            var output = program.Outputs.Last();
            return output == 1;
        }

        private long[] GetInput()
        {
            return File.ReadAllText(@"input/day19.txt")
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }
    }
}
