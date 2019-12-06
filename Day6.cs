using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2019
{
    public class Day6
    {
        public void Run()
        {
            var input = GetInput();
            var result1 = PartOne(input);
            var result2 = PartTwo(input);

            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        private int PartOne(string[] input)
        {
            var planets = new Dictionary<string, string>();

            List<string> GetOrbits(string planet)
            {
                var orbits = new List<string>();                
                for (var thisPlanet = planet; thisPlanet != "COM"; thisPlanet = planets[thisPlanet])
                {
                    orbits.Add(thisPlanet);
                }
                return orbits;
            }

            foreach (var orbit in input)
            {
                var orbitSplit = orbit.Split(")");
                var planet = orbitSplit[1];
                var orbiting = orbitSplit[0];
                planets.Add(planet, orbiting);
            }

            //var orbitCount = 0;
            //foreach (var p in planets)
            //{
            //    var theseOrbits = GetOrbits(p.Key);
            //    orbitCount += theseOrbits.Count();
            //}
            var orbitCount = planets.Keys.Sum(p => GetOrbits(p).Count);
            return orbitCount;
        }

        private int PartTwo(string[] input)
        {
            var planets = new Dictionary<string, string>();

            List<string> GetOrbits(string planet)
            {
                var orbits = new List<string>();
                for (var thisPlanet = planet; thisPlanet != "COM"; thisPlanet = planets[thisPlanet])
                {
                    orbits.Add(thisPlanet);
                }
                return orbits;
            }

            foreach (var orbit in input)
            {
                var orbitSplit = orbit.Split(")");
                var planet = orbitSplit[1];
                var orbiting = orbitSplit[0];
                planets.Add(planet, orbiting);
            }

            var myOrbit = planets["YOU"];
            var santaOrbit = planets["SAN"];

            var myOrbits = GetOrbits(myOrbit);
            var santaOrbits = GetOrbits(santaOrbit);
            var intersectPoints = myOrbits.Intersect(santaOrbits);
            var firstIntersectPoint = intersectPoints.First();
            var distanceMeToPoint = myOrbits.IndexOf(firstIntersectPoint);
            var distancePointToSanta = santaOrbits.IndexOf(firstIntersectPoint);

            var totalDistance = distanceMeToPoint + distancePointToSanta;

            return totalDistance;
        }


        private string[] GetInput()
        {
            return File.ReadAllLines(@"input/day6.txt");
        }
    }
}
