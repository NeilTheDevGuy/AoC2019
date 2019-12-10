using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AoC2019.Utils.IntCode;

namespace AoC2019
{
    public class Day5
    {
        public void Run()
        {
            var result1 = RunDay(GetInput(), 1);
            var result2 = RunDay(GetInput(), 5);

            Console.WriteLine($"Part1 - {result1}");
            Console.WriteLine($"Part2 - {result2}");
        }

        private int RunDay(int[] input, int inputParameter)
        {
            int i = 0;
            var outputs = new List<int>();
            while (i <= input.Length)
            {
                var opCode = GetOpCode(input[i].ToString());
                int param1, param2;
                switch (opCode.Type)
                {
                    case 1:
                        param1 = GetVal(opCode.ModeOne, input[i + 1], input);
                        param2 = GetVal(opCode.ModeTwo, input[i + 2], input);
                        input[input[i + 3]] = param1 + param2;
                        i += 4;
                        break;
                    case 2:
                        param1 = GetVal(opCode.ModeOne, input[i + 1], input);
                        param2 = GetVal(opCode.ModeTwo, input[i + 2], input);
                        input[input[i + 3]] = param1 * param2;
                        i += 4;
                        break;
                    case 3:
                        input[input[i + 1]] = inputParameter;
                        i += 2;
                        break;
                    case 4:
                        outputs.Add(GetVal(opCode.ModeOne, input[i + 1], input));
                        i += 2;
                        break;
                    case 5:
                        if (GetVal(opCode.ModeOne, input[i + 1], input) == 0)
                        {
                            i += 3;
                        }
                        else
                        {
                            i = GetVal(opCode.ModeTwo, input[i + 2], input);
                        }
                        break;
                    case 6:
                        if (GetVal(opCode.ModeOne, input[i + 1], input) != 0)
                        {
                            i += 3;
                        }
                        else
                        {
                            i = GetVal(opCode.ModeTwo, input[i + 2], input);
                        }
                        break;
                    case 7:
                        if (GetVal(opCode.ModeOne, input[i + 1], input) < GetVal(opCode.ModeTwo, input[i + 2], input))
                        {
                            input[input[i + 3]] = 1;
                        }
                        else
                        {
                            input[input[i + 3]] = 0;
                        }
                        i += 4;
                        break;
                    case 8:
                        if (GetVal(opCode.ModeOne, input[i + 1], input) == GetVal(opCode.ModeTwo, input[i + 2], input))
                        {
                            input[input[i + 3]] = 1;
                        }
                        else
                        {
                            input[input[i + 3]] = 0;
                        }
                        i += 4;
                        break;
                    case 99:
                        return outputs.Last();
                    default:
                        throw new Exception($"Unexpected opcode at position {i} : {opCode.Type}");
                }
            }
            throw new Exception("No Halt opcode found");
        }

        private OpCode GetOpCode(string instruction)
        {
            var length = instruction.Length;
            if (length <= 2)
            {
                return new OpCode(int.Parse(instruction), 0, 0, 0);
            }
            if (length == 3)
            {
                return new OpCode(int.Parse(instruction.Substring(1).ToString()), 1, 0, 0);
            }
            if (length == 4)
            {
                return new OpCode(int.Parse(instruction.Substring(2).ToString()), int.Parse(instruction[1].ToString()), int.Parse(instruction[0].ToString()), 0);
            }
            throw new Exception("Instruction is invalid length");
        }
        
        private int GetVal(int mode, int param, int[] input)
        {
            return mode == 1
                ? param
                : input[param];
        }

        private int[] GetInput()
        {
            return File.ReadAllText(@"input/day5.txt")
                .Split(",")
                .Select(int.Parse)
                .ToArray();
        }
    }
}
