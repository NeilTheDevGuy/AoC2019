using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2019
{
    public class Day2
    {
        public void Run()
        {
            var result1 = PartOne(GetInput());
            var result2 = PartTwo(19690720);
            
            Console.WriteLine($"Part1 - {result1}");
            Console.WriteLine($"Part2 - {result2}");
        }

        private int PartOne(int[] input)
        {
            //Change these input values as per instructions
            input[1] = 12;
            input[2] = 2;
            for (int i = 0; i < input.Length; i+=4)
            {
                var opCode = input[i];
                switch (opCode)
                {
                    case 1:
                        input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];                        
                        break;
                    case 2:
                        input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                        break;
                    case 99:                        
                        return input[0];
                    default:
                        throw new Exception($"Unexpected opcode at position {i} : {opCode}");
                }
            }
            throw new Exception("No Halt opcode found");
        }

        private int PartTwo(int desiredOutput)
        {
            for (int param1 = 0; param1 <= 99; param1++)
            {
                for (int param2 = 0; param2 <= 99; param2++)
                {
                    var input = GetInput();
                    input[1] = param1;
                    input[2] = param2;
                    for (int i = 0; i < input.Length; i += 4)
                    {
                        var opCode = input[i];
                        switch (opCode)
                        {
                            case 1:
                                input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                                break;
                            case 2:
                                input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                                break;
                            case 99:
                                if (input[0] == desiredOutput)
                                {
                                    return (100 * param1) + param2;
                                }
                                i = input.Length; //fast-forward to end of loop
                                break;
                            default:
                                throw new Exception($"Unexpected opcode at position {i} : {opCode}");
                        }
                    }
                }
            }
            throw new Exception("No Halt opcode found");
        }

        private int[] GetInput()
        {
            return "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,13,19,1,9,19,23,2,13,23,27,2,27,13,31,2,31,10,35,1,6,35,39,1,5,39,43,1,10,43,47,1,5,47,51,1,13,51,55,2,55,9,59,1,6,59,63,1,13,63,67,1,6,67,71,1,71,10,75,2,13,75,79,1,5,79,83,2,83,6,87,1,6,87,91,1,91,13,95,1,95,13,99,2,99,13,103,1,103,5,107,2,107,10,111,1,5,111,115,1,2,115,119,1,119,6,0,99,2,0,14,0"
                .Split(",")
                .Select(int.Parse)
                .ToArray();
        }
    }
}
