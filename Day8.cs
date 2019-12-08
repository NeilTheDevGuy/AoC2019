using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2019
{
    public class Day8
    {
        public void Run()
        {

            var result1 = PartOne(GetInput(), 25, 6);
            Console.WriteLine($"PartOne - {result1}");
            PartTwo(GetInput(), 25, 6);
        }

        private string GetInput()
        {

            return File.ReadAllText(@"input/day8.txt");
        }

        private int PartOne(string input, int width, int height)
        {
            var layersInInput = input.Length / (height * width);
            var layers = new string[layersInInput];
            var strPtr = 0;
            for (int i = 0; i < layersInInput; i++)
            {
                layers[i] = input.Substring(strPtr, height * width);
                strPtr += height * width;
            }

            var minZeroLayer = "";
            var minZeros = int.MaxValue;

            foreach (var layer in layers)
            {
                var layerZeroCount = layer.Count(c => c == '0');
                if (layerZeroCount < minZeros)
                {
                    minZeros = layerZeroCount;
                    minZeroLayer = layer;
                }
            }

            var ones = minZeroLayer.Count(c => c == '1');
            var twos = minZeroLayer.Count(c => c == '2');

            return ones * twos;
        }

        private void PartTwo(string input, int width, int height)
        {
            var layersInInput = input.Length / (height * width);
            var layers = new string[layersInInput];
            var strPtr = 0;
            for (int i = 0; i < layersInInput; i++)
            {
                layers[i] = input.Substring(strPtr, height * width);
                strPtr += height * width;
            }

            var finalLayer = "";
            for (int i=0; i < height * width; i++)
            {
                foreach (var layer in layers)
                {
                    if (layer[i] != '2')
                    {
                        finalLayer += layer[i];
                        break;
                    }
                }
            }

            strPtr = 0;
            for (int i = 0; i < height; i++)
            {
                var line = finalLayer.Substring(strPtr, width);
                foreach (var c in line)
                {
                    if (c == '0')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
                Console.Write(Environment.NewLine);
                strPtr += width;
            }
        }
    }
}
