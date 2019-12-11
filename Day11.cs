using AoC2019.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using static AoC2019.Utils.IntCode;

namespace AoC2019
{
    public class Day11
    {
        public void Run()
        {
            RunDay(GetInput(), 0, true, false);
            RunDay(GetInput(), 1, false, true);
        }

        private void RunDay(long[] program, int initialInput, bool writeCount, bool draw)
        {
            var hull = new Dictionary<Point, long>();
            var intCode = new IntCode(program);
            var robotDirection = Direction.Up;
            int x = 0, y = 0;
            long inputInstruction = initialInput;            
            while (intCode.ProgramState != State.Halt)
            {
                //Get the colour
                intCode.Inputs.Enqueue(inputInstruction);
                intCode.Run();
                var colour = intCode.Outputs.Last();
                var point = new Point(x, y);
                if (!hull.ContainsKey(point))
                {
                    hull.Add(point, colour);
                }
                else
                {
                    hull[point] = colour;
                }
                
                //Get the next direction
                intCode.Run();
                var nextDir = intCode.Outputs.Last();
                robotDirection = ChangeDirection(robotDirection, nextDir == 1);
                (x, y) = GetNextCoordinates(x, y, robotDirection);

                //Set the input
                var nextPoint = new Point(x, y);
                if (hull.ContainsKey(nextPoint))
                {
                    inputInstruction = hull[nextPoint];
                }
                else
                {
                    inputInstruction = 0; //If we haven't painted it, it's black.
                }                
            }

            if (writeCount)
            {
                Console.WriteLine($"PartOne - {hull.Count}");
            }

            if (draw)
            {
                DrawOutput(hull);
            }            
        }
             
        private void DrawOutput(Dictionary<Point, long> hull)
        {            
            var minX = hull.Keys.Min(k => k.X);
            var maxX = hull.Keys.Max(k => k.X);
            var minY = hull.Keys.Min(k => k.Y);
            var maxY = hull.Keys.Max(k => k.Y);

            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var thisPoint = new Point(x, y);
                    var toWrite = " ";
                    if (hull.ContainsKey(thisPoint) && hull[thisPoint] != 0) {
                        toWrite = "#";                        
                    }
                    Console.Write(toWrite);
                }
                Console.Write(Environment.NewLine);
            }
        }

        private (int, int) GetNextCoordinates(int x, int y, Direction direction)
        {
            if (direction == Direction.Up) return (x, ++y);
            if (direction == Direction.Down) return (x, --y);
            if (direction == Direction.Left) return (--x, y);
            if (direction == Direction.Right) return (++x, y);
            throw new Exception("Invalid direction");

        }
        private Direction ChangeDirection(Direction direction, bool turnRight)
        {
            if (direction == Direction.Up && turnRight) return Direction.Right;
            if (direction == Direction.Up && !turnRight) return Direction.Left;
            if (direction == Direction.Down && turnRight) return Direction.Left;
            if (direction == Direction.Down && !turnRight) return Direction.Right;
            if (direction == Direction.Left && turnRight) return Direction.Up;
            if (direction == Direction.Left && !turnRight) return Direction.Down;
            if (direction == Direction.Right && turnRight) return Direction.Down;
            if (direction == Direction.Right && !turnRight) return Direction.Up;
            throw new Exception("Unknown direction");
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private long[] GetInput()
        {
            return File.ReadAllText(@"input/day11.txt")
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }
    }
}
