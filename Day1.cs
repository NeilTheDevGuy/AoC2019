using System;
using System.IO;

namespace AoC2019
{
    public class Day1
    {
        public void Run()
        {
            var result = PartTwo();

            Console.WriteLine(result);
        }

        private int PartOne()
        {
            var input = File.ReadAllLines("day1.txt");
            var totalFuelRequired = 0;
            foreach (var line in input)
            { 
                totalFuelRequired += GetFuelRequirement(Decimal.Parse(line));
            }
            return totalFuelRequired;
        }

        private int PartTwo()
        {
            var input = File.ReadAllLines("day1.txt");
            var totalFuelRequired = 0;
            foreach (var line in input)
            {
                totalFuelRequired += GetTotalFuelRequirement(Decimal.Parse(line));
            }
            return totalFuelRequired;
        }

        private int GetFuelRequirement(decimal mass)
        {
            return (int)(Math.Floor(mass / 3) - 2);
        }

        private int GetTotalFuelRequirement(decimal initialMass)
        {
            var totalFuelRequirement = 0;
            var fuelMass = initialMass;
            while (true)
            {
                var fuelRequired = GetFuelRequirement(fuelMass);
                if (fuelRequired > 0)
                {
                    totalFuelRequirement += fuelRequired;
                    fuelMass = fuelRequired;
                }
                else
                {
                    break;
                }
            }
            return totalFuelRequirement;
        }
    }
}
