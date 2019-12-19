using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019
{
    public class Day12
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
            var moons = GetMoons();
            for (int i = 0; i <1000; i++)
            {
                //Apply Gravity - increase velocities. Hideous code.
                if (moons[0].Position.X > moons[1].Position.X) { moons[1].Velocity.X++; moons[0].Velocity.X--; } else if (moons[0].Position.X < moons[1].Position.X) { moons[0].Velocity.X++; moons[1].Velocity.X--; }
                if (moons[0].Position.X > moons[2].Position.X) { moons[2].Velocity.X++; moons[0].Velocity.X--; } else if (moons[0].Position.X < moons[2].Position.X) { moons[0].Velocity.X++; moons[2].Velocity.X--; }
                if (moons[0].Position.X > moons[3].Position.X) { moons[3].Velocity.X++; moons[0].Velocity.X--; } else if (moons[0].Position.X < moons[3].Position.X) { moons[0].Velocity.X++; moons[3].Velocity.X--; }
                if (moons[1].Position.X > moons[2].Position.X) { moons[2].Velocity.X++; moons[1].Velocity.X--; } else if (moons[1].Position.X < moons[2].Position.X) { moons[1].Velocity.X++; moons[2].Velocity.X--; }
                if (moons[1].Position.X > moons[3].Position.X) { moons[3].Velocity.X++; moons[1].Velocity.X--; } else if (moons[1].Position.X < moons[3].Position.X) { moons[1].Velocity.X++; moons[3].Velocity.X--; }
                if (moons[2].Position.X > moons[3].Position.X) { moons[3].Velocity.X++; moons[2].Velocity.X--; } else if (moons[2].Position.X < moons[3].Position.X) { moons[2].Velocity.X++; moons[3].Velocity.X--; }

                if (moons[0].Position.Y > moons[1].Position.Y) { moons[1].Velocity.Y++; moons[0].Velocity.Y--; } else if (moons[0].Position.Y < moons[1].Position.Y) { moons[0].Velocity.Y++; moons[1].Velocity.Y--; }
                if (moons[0].Position.Y > moons[2].Position.Y) { moons[2].Velocity.Y++; moons[0].Velocity.Y--; } else if (moons[0].Position.Y < moons[2].Position.Y) { moons[0].Velocity.Y++; moons[2].Velocity.Y--; }
                if (moons[0].Position.Y > moons[3].Position.Y) { moons[3].Velocity.Y++; moons[0].Velocity.Y--; } else if (moons[0].Position.Y < moons[3].Position.Y) { moons[0].Velocity.Y++; moons[3].Velocity.Y--; }
                if (moons[1].Position.Y > moons[2].Position.Y) { moons[2].Velocity.Y++; moons[1].Velocity.Y--; } else if (moons[1].Position.Y < moons[2].Position.Y) { moons[1].Velocity.Y++; moons[2].Velocity.Y--; }
                if (moons[1].Position.Y > moons[3].Position.Y) { moons[3].Velocity.Y++; moons[1].Velocity.Y--; } else if (moons[1].Position.Y < moons[3].Position.Y) { moons[1].Velocity.Y++; moons[3].Velocity.Y--; }
                if (moons[2].Position.Y > moons[3].Position.Y) { moons[3].Velocity.Y++; moons[2].Velocity.Y--; } else if (moons[2].Position.Y < moons[3].Position.Y) { moons[2].Velocity.Y++; moons[3].Velocity.Y--; }

                if (moons[0].Position.Z > moons[1].Position.Z) { moons[1].Velocity.Z++; moons[0].Velocity.Z--; } else if (moons[0].Position.Z < moons[1].Position.Z) { moons[0].Velocity.Z++; moons[1].Velocity.Z--; }
                if (moons[0].Position.Z > moons[2].Position.Z) { moons[2].Velocity.Z++; moons[0].Velocity.Z--; } else if (moons[0].Position.Z < moons[2].Position.Z) { moons[0].Velocity.Z++; moons[2].Velocity.Z--; }
                if (moons[0].Position.Z > moons[3].Position.Z) { moons[3].Velocity.Z++; moons[0].Velocity.Z--; } else if (moons[0].Position.Z < moons[3].Position.Z) { moons[0].Velocity.Z++; moons[3].Velocity.Z--; }
                if (moons[1].Position.Z > moons[2].Position.Z) { moons[2].Velocity.Z++; moons[1].Velocity.Z--; } else if (moons[1].Position.Z < moons[2].Position.Z) { moons[1].Velocity.Z++; moons[2].Velocity.Z--; }
                if (moons[1].Position.Z > moons[3].Position.Z) { moons[3].Velocity.Z++; moons[1].Velocity.Z--; } else if (moons[1].Position.Z < moons[3].Position.Z) { moons[1].Velocity.Z++; moons[3].Velocity.Z--; }
                if (moons[2].Position.Z > moons[3].Position.Z) { moons[3].Velocity.Z++; moons[2].Velocity.Z--; } else if (moons[2].Position.Z < moons[3].Position.Z) { moons[2].Velocity.Z++; moons[3].Velocity.Z--; }

                //Apply Velocity - adjust positions
                moons.ForEach(p => p.Position.X += p.Velocity.X);
                moons.ForEach(p => p.Position.Y += p.Velocity.Y);
                moons.ForEach(p => p.Position.Z += p.Velocity.Z);
            }

            var totalEnergy = 0;
            foreach (var moon in moons)
            {
                var moonPot = Math.Abs(moon.Position.X) + Math.Abs(moon.Position.Y) + Math.Abs(moon.Position.Z);
                var moonKin = Math.Abs(moon.Velocity.X) + Math.Abs(moon.Velocity.Y) + Math.Abs(moon.Velocity.Z);
                totalEnergy += (moonPot * moonKin);
            }
                        
            return totalEnergy;
        }

        private long PartTwo()
        {
            var moons = GetMoons();
            //var initialMoons = GetMoons();
            //var moonSet = new HashSet<(int, int, int, int, int, int)>();
            //THIS WON'T WORK!
            //IT WILL MATCH WHEN ALL OF THE STATES HAVE HAPPENED AT SOME POINT, NOT ALL AT THE SAME TIME.
            //NEED SOMETHING TO STORE THE STATE OF ALL POSITIONS IN ONE GO - MAYBE A SUPERMOON STRUCT THAT HAS XYZ FOR ALL 4 MOONS
            //moonSet.Add((moons[0].Position.X, moons[0].Position.Y, moons[0].Position.Z, moons[0].Velocity.X, moons[0].Velocity.Y, moons[0].Velocity.Z));
            //moonSet.Add((moons[1].Position.X, moons[1].Position.Y, moons[1].Position.Z, moons[1].Velocity.X, moons[1].Velocity.Y, moons[1].Velocity.Z));
            //moonSet.Add((moons[2].Position.X, moons[2].Position.Y, moons[2].Position.Z, moons[0].Velocity.X, moons[2].Velocity.Y, moons[2].Velocity.Z));
            //moonSet.Add((moons[3].Position.X, moons[3].Position.Y, moons[3].Position.Z, moons[3].Velocity.X, moons[3].Velocity.Y, moons[3].Velocity.Z));
            long i = 0;

            while (true)
            {
                //Apply Gravity - increase velocities. Hideous code.
                if (moons[0].Position.X > moons[1].Position.X) { moons[1].Velocity.X++; moons[0].Velocity.X--; } else if (moons[0].Position.X < moons[1].Position.X) { moons[0].Velocity.X++; moons[1].Velocity.X--; }
                if (moons[0].Position.X > moons[2].Position.X) { moons[2].Velocity.X++; moons[0].Velocity.X--; } else if (moons[0].Position.X < moons[2].Position.X) { moons[0].Velocity.X++; moons[2].Velocity.X--; }
                if (moons[0].Position.X > moons[3].Position.X) { moons[3].Velocity.X++; moons[0].Velocity.X--; } else if (moons[0].Position.X < moons[3].Position.X) { moons[0].Velocity.X++; moons[3].Velocity.X--; }
                if (moons[1].Position.X > moons[2].Position.X) { moons[2].Velocity.X++; moons[1].Velocity.X--; } else if (moons[1].Position.X < moons[2].Position.X) { moons[1].Velocity.X++; moons[2].Velocity.X--; }
                if (moons[1].Position.X > moons[3].Position.X) { moons[3].Velocity.X++; moons[1].Velocity.X--; } else if (moons[1].Position.X < moons[3].Position.X) { moons[1].Velocity.X++; moons[3].Velocity.X--; }
                if (moons[2].Position.X > moons[3].Position.X) { moons[3].Velocity.X++; moons[2].Velocity.X--; } else if (moons[2].Position.X < moons[3].Position.X) { moons[2].Velocity.X++; moons[3].Velocity.X--; }

                if (moons[0].Position.Y > moons[1].Position.Y) { moons[1].Velocity.Y++; moons[0].Velocity.Y--; } else if (moons[0].Position.Y < moons[1].Position.Y) { moons[0].Velocity.Y++; moons[1].Velocity.Y--; }
                if (moons[0].Position.Y > moons[2].Position.Y) { moons[2].Velocity.Y++; moons[0].Velocity.Y--; } else if (moons[0].Position.Y < moons[2].Position.Y) { moons[0].Velocity.Y++; moons[2].Velocity.Y--; }
                if (moons[0].Position.Y > moons[3].Position.Y) { moons[3].Velocity.Y++; moons[0].Velocity.Y--; } else if (moons[0].Position.Y < moons[3].Position.Y) { moons[0].Velocity.Y++; moons[3].Velocity.Y--; }
                if (moons[1].Position.Y > moons[2].Position.Y) { moons[2].Velocity.Y++; moons[1].Velocity.Y--; } else if (moons[1].Position.Y < moons[2].Position.Y) { moons[1].Velocity.Y++; moons[2].Velocity.Y--; }
                if (moons[1].Position.Y > moons[3].Position.Y) { moons[3].Velocity.Y++; moons[1].Velocity.Y--; } else if (moons[1].Position.Y < moons[3].Position.Y) { moons[1].Velocity.Y++; moons[3].Velocity.Y--; }
                if (moons[2].Position.Y > moons[3].Position.Y) { moons[3].Velocity.Y++; moons[2].Velocity.Y--; } else if (moons[2].Position.Y < moons[3].Position.Y) { moons[2].Velocity.Y++; moons[3].Velocity.Y--; }

                if (moons[0].Position.Z > moons[1].Position.Z) { moons[1].Velocity.Z++; moons[0].Velocity.Z--; } else if (moons[0].Position.Z < moons[1].Position.Z) { moons[0].Velocity.Z++; moons[1].Velocity.Z--; }
                if (moons[0].Position.Z > moons[2].Position.Z) { moons[2].Velocity.Z++; moons[0].Velocity.Z--; } else if (moons[0].Position.Z < moons[2].Position.Z) { moons[0].Velocity.Z++; moons[2].Velocity.Z--; }
                if (moons[0].Position.Z > moons[3].Position.Z) { moons[3].Velocity.Z++; moons[0].Velocity.Z--; } else if (moons[0].Position.Z < moons[3].Position.Z) { moons[0].Velocity.Z++; moons[3].Velocity.Z--; }
                if (moons[1].Position.Z > moons[2].Position.Z) { moons[2].Velocity.Z++; moons[1].Velocity.Z--; } else if (moons[1].Position.Z < moons[2].Position.Z) { moons[1].Velocity.Z++; moons[2].Velocity.Z--; }
                if (moons[1].Position.Z > moons[3].Position.Z) { moons[3].Velocity.Z++; moons[1].Velocity.Z--; } else if (moons[1].Position.Z < moons[3].Position.Z) { moons[1].Velocity.Z++; moons[3].Velocity.Z--; }
                if (moons[2].Position.Z > moons[3].Position.Z) { moons[3].Velocity.Z++; moons[2].Velocity.Z--; } else if (moons[2].Position.Z < moons[3].Position.Z) { moons[2].Velocity.Z++; moons[3].Velocity.Z--; }

                //Apply Velocity - adjust positions
                moons.ForEach(p => p.Position.X += p.Velocity.X);
                moons.ForEach(p => p.Position.Y += p.Velocity.Y);
                moons.ForEach(p => p.Position.Z += p.Velocity.Z);

                //moons.Add(new Moon { Position = new Dimensions { X = -5, Y = 6, Z = -11 } });
                //moons.Add(new Moon { Position = new Dimensions { X = -8, Y = -4, Z = -2 } });
                //moons.Add(new Moon { Position = new Dimensions { X = 1, Y = 16, Z = 4 } });
                //moons.Add(new Moon { Position = new Dimensions { X = 11, Y = 11, Z = -4 } });

                if (moons[0].Position.X == -5 && moons[0].Position.Y == 6 && moons[0].Position.Z == -11 && moons[0].Velocity.X == 0 && moons[0].Velocity.Y == 00 && moons[0].Velocity.Z == 0
                    && moons[1].Position.X == -8 && moons[1].Position.Y == -4 && moons[1].Position.Z == -2 && moons[1].Velocity.X == 0 && moons[1].Velocity.Y == 00 && moons[1].Velocity.Z == 0
                    && moons[2].Position.X == 1 && moons[2].Position.Y == 16 && moons[2].Position.Z == 4 && moons[2].Velocity.X == 0 && moons[2].Velocity.Y == 00 && moons[2].Velocity.Z == 0
                    && moons[3].Position.X == 11 && moons[3].Position.Y == 11 && moons[3].Position.Z == -4 && moons[3].Velocity.X == 0 && moons[3].Velocity.Y == 00 && moons[3].Velocity.Z == 0)
                {
                    return i;
                }

                if (i % 1000000 == 0) Console.WriteLine(i);
                //moonSet.Add(moons[0]);
                //moonSet.Add(moons[1]);
                //moonSet.Add(moons[2]);
                //moonSet.Add(moons[3]);

                //if (moonSet.Contains(moons[0]) && moonSet.Contains(moons[1]) && moonSet.Contains(moons[2]) && moonSet.Contains(moons[3])) {
                //    return i;
                //}

                i++;
            }
        }
                

        private List<Moon> GetMoons()
        {
            //< x = -5, y = 6, z = -11 >
            //< x = -8, y = -4, z = -2 >
            //< x = 1, y = 16, z = 4 >
            //< x = 11, y = 11, z = -4 >
            var moons = new List<Moon>();
            moons.Add(new Moon { Position = new Dimensions { X = -5, Y = 6, Z = -11 } });
            moons.Add(new Moon { Position = new Dimensions { X = -8, Y = -4, Z = -2 } });
            moons.Add(new Moon { Position = new Dimensions { X = 1, Y = 16, Z = 4 } });
            moons.Add(new Moon { Position = new Dimensions { X = 11, Y = 11, Z = -4 } });
            return moons;
        }

        private List<Moon> GetTestMoons()
        {            
            var moons = new List<Moon>();            

            //< x = -1, y = 0, z = 2 >
            //< x = 2, y = -10, z = -7 >
            //< x = 4, y = -8, z = 8 >
            //< x = 3, y = 5, z = -1 >
            moons.Add(new Moon { Position = new Dimensions { X = -1, Y = 0, Z = 2 } });
            moons.Add(new Moon { Position = new Dimensions { X = 2, Y = -10, Z = -7 } });
            moons.Add(new Moon { Position = new Dimensions { X = 4, Y = -8, Z = 8 } });
            moons.Add(new Moon { Position = new Dimensions { X = 3, Y = 5, Z = -1 } });
            return moons;
        }

        private class Moon 
        {
            public Dimensions Position = new Dimensions();
            public Dimensions Velocity = new Dimensions();
        }

        public class Dimensions
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

    }
}
