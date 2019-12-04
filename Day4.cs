using System;

namespace AoC2019
{
    public class Day4
    {
        public void Run()
        {
            var result1 = PartOne(138307, 654504);
            var result2 = PartTwo(138307, 654504);
            Console.WriteLine($"Part1 - {result1}");
            Console.WriteLine($"Part2 - {result2}");
        }

        private int PartOne(int min, int max)
        {
            var passwordCount = 0;
            while (min <= max)
            {
                var strNum = min.ToString();
                if (
                        (
                            strNum[0] == strNum[1] ||
                            strNum[1] == strNum[2] ||
                            strNum[2] == strNum[3] ||
                            strNum[3] == strNum[4] ||
                            strNum[4] == strNum[5]
                        )
                        &&
                        (
                            (int)strNum[0] <= (int)strNum[1] &&
                            (int)strNum[1] <= (int)strNum[2] &&
                            (int)strNum[2] <= (int)strNum[3] &&
                            (int)strNum[3] <= (int)strNum[4] &&
                            (int)strNum[4] <= (int)strNum[5]
                        )
                    )
                    passwordCount++;
                
                min++;
            }
            return passwordCount;
        }

        private int PartTwo(int min, int max)
        {
            var passwordCount = 0;
            while (min <= max)
            {
                var strNum = min.ToString();
                if (
                        (
                            (strNum[0] == strNum[1] && strNum[1] != strNum[2]) ||
                            (strNum[0] != strNum[1] && strNum[1] == strNum[2] && strNum[2] != strNum[3]) ||
                            (strNum[1] != strNum[2] && strNum[2] == strNum[3] && strNum[3] != strNum[4]) ||
                            (strNum[2] != strNum[3] && strNum[3] == strNum[4] && strNum[4] != strNum[5]) ||
                            (strNum[3] != strNum[4] && strNum[4] == strNum[5])
                        )
                        &&
                        (
                            (int)strNum[0] <= (int)strNum[1] &&
                            (int)strNum[1] <= (int)strNum[2] &&
                            (int)strNum[2] <= (int)strNum[3] &&
                            (int)strNum[3] <= (int)strNum[4] &&
                            (int)strNum[4] <= (int)strNum[5]
                        )
                    )
                    passwordCount++;

                min++;
            }
            return passwordCount;
        }
    }
}
