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
    public class Day13
    {
        public void Run()
        {
            var result1 = PartOne();
            Console.WriteLine($"PartOne - {result1}");
            var result2 = PartTwo();
            Console.WriteLine($"PartTwo - {result2}");
        }

        private long PartOne()
        {
            var tileProgram = new IntCode(GetInput());
            var tiles = new List<Tile>();
            while (tileProgram.ProgramState != State.Halt)
            {
                tileProgram.Run();
                var nextX = tileProgram.Outputs.Last();
                tileProgram.Run();
                var nextY = tileProgram.Outputs.Last();
                tileProgram.Run();
                var nextId = tileProgram.Outputs.Last();
                var tile = new Tile
                {
                    X = nextX,
                    Y = nextY,
                    TileId = nextId
                };
                tiles.Add(tile);
            }

            return tiles.Count(t => t.TileId == 2);
        }

        private long PartTwo()
        {
            var tileProgram = new IntCode(GetInput());
            var game = new Dictionary<Point, long>();
            var ball = new Point();
            var paddle = new Point();
            
            long score = 0;
            tileProgram.WriteDirectToMemory(0, 2);            
            while (tileProgram.ProgramState != State.Halt)
            {
                tileProgram.Run();
                var nextX = tileProgram.Outputs.Last();
                tileProgram.Run();
                var nextY = tileProgram.Outputs.Last();
                tileProgram.Run();
                var nextId = tileProgram.Outputs.Last();
                if (nextX == -1 && nextY == 0)
                {
                    score = nextId;
                }
                else
                {
                    var point = new Point((int)nextX, (int)nextY);
                    if (!game.ContainsKey(point))
                    {
                        game.Add(point, nextId);
                    }
                    else
                    {
                        game[point] = nextId;
                    }

                    if (nextId == 3)
                    {
                        paddle = point;
                    }
                    else if (nextId == 4)
                    {
                        ball = point;
                    }
                }

                    if (ball.X > paddle.X)
                    {
                        tileProgram.Inputs.Enqueue(1);
                    }
                    else if (ball.X < paddle.X)
                    {
                        tileProgram.Inputs.Enqueue(-1);
                    }
                    else
                    {
                        tileProgram.Inputs.Enqueue(0);
                    }
                
            }
            return score;
        }

        private long[] GetInput()
        {
            return File.ReadAllText(@"input/day13.txt")
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }
    }

    public class Tile 
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long TileId { get; set; }                
    }
}
