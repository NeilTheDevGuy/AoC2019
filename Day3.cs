using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AoC2019
{
    public class Day3
    {
        public void Run()
        {
            //var input1 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(",");
            //var input2 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(",");
            var input = File.ReadAllLines(@"input\day3.txt");
            var input1 = input[0].Split(",");
            var input2 = input[1].Split(",");
            var (result1, result2) = PartOneAndTwo(input1, input2);            
            Console.WriteLine($"Part1 - {result1}");
            Console.WriteLine($"Part2 - {result2}");
        }

        private (int,int) PartOneAndTwo(string[] wire1, string[] wire2)
        {                       
            var wire1Points = GetPoints(wire1);
            var wire2Points = GetPoints(wire2);
            var matchingCoords = wire1Points.Keys.Intersect(wire2Points.Keys).ToList();            
            var lowestDistance = matchingCoords.Min(m => Math.Abs(m.X) + Math.Abs(m.Y));
            var shortestSteps = matchingCoords.Min(m => wire1Points[m] + wire2Points[m]);
            return (lowestDistance, shortestSteps);
        }

        private Dictionary<Point, int> GetPoints(string[] wire)
        {
            var wirePoints = new Dictionary<Point, int>();
            var steps = 0;

            void AddPoint(int x, int y, int steps)
            {                
                wirePoints.TryAdd(new Point(x, y), steps);
            }
            
            foreach (var point in wire)
            {
                var direction = point.Substring(0, 1);
                var distance = int.Parse(point.Substring(1));
                var lastPoint = wirePoints.Keys.Any() ? wirePoints.Keys.Last() : new Point(0, 0);
                for (int i = 1; i <= distance; i++)
                {
                    switch (direction)
                    {
                        case "R":
                            AddPoint(lastPoint.X + i, lastPoint.Y, ++steps);
                            break;
                        case "L":
                            AddPoint(lastPoint.X - i, lastPoint.Y, ++steps);
                            break;
                        case "U":
                            AddPoint(lastPoint.X, lastPoint.Y + i, ++steps);
                            break;
                        case "D":
                            AddPoint(lastPoint.X, lastPoint.Y - i, ++steps);
                            break;
                        default:
                            throw new Exception("Unexpected direction");
                    }
                }
            }
            return wirePoints;
        }
    }
}
