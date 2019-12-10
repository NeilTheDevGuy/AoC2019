using AoC2019.Utils;
using System;
using System.IO;
using System.Linq;
using static AoC2019.Utils.IntCode;

namespace AoC2019
{
    public class Day7
    {
        public void Run()
        {
            var result1 = PartOne();
            var result2 = PartTwo();
            Console.WriteLine($"PartOne - {result1}");
            Console.WriteLine($"PartTwo - {result2}");
        }

        private long PartOne()
        {
            var input = GetInput();
            long highestOutput = 0;

            for (int phase1 = 0; phase1 <=4; phase1++)
            {
                for (int phase2 = 0; phase2 <= 4; phase2++)
                {
                    for (int phase3 = 0; phase3 <= 4; phase3++)
                    {
                        for (int phase4 = 0; phase4 <= 4; phase4++)
                        {
                            for (int phase5 = 0; phase5 <= 4; phase5++)
                            {
                                if (phase1 != phase2 && phase1 != phase3 && phase1 != phase4 && phase1 != phase5
                                    && phase2 != phase3 && phase2 != phase4 && phase2 != phase5
                                    && phase3 != phase4 && phase3 != phase5
                                    && phase4 != phase5)
                                {   
                                    var amp = new IntCode(input);
                                    var output1 = RunAmp(amp, input, phase1, 0);
                                    var output2 = RunAmp(amp, input, phase2, output1);
                                    var output3 = RunAmp(amp, input, phase3, output2);
                                    var output4 = RunAmp(amp, input, phase4, output3);
                                    var output5 = RunAmp(amp, input, phase5, output4);                                    
                                    if (output5 > highestOutput) highestOutput = output5;
                                }
                            }
                        }
                    }
                }
            }
            return highestOutput;                
        }

        private long PartTwo()
        {
            var input = GetInput();
            long highestOutput = 0;

            for (int phase1 = 5; phase1 <= 9; phase1++)
            {
                for (int phase2 = 5; phase2 <= 9; phase2++)
                {
                    for (int phase3 = 5; phase3 <= 9; phase3++)
                    {
                        for (int phase4 = 5; phase4 <= 9; phase4++)
                        {
                            for (int phase5 = 5; phase5 <= 9; phase5++)
                            {
                                if (phase1 != phase2 && phase1 != phase3 && phase1 != phase4 && phase1 != phase5
                                    && phase2 != phase3 && phase2 != phase4 && phase2 != phase5
                                    && phase3 != phase4 && phase3 != phase5
                                    && phase4 != phase5)
                                {
                                    var amp1 = new IntCode(input);
                                    amp1.Inputs.Enqueue(phase1);
                                    var amp2 = new IntCode(input);
                                    amp2.Inputs.Enqueue(phase2);
                                    var amp3 = new IntCode(input);
                                    amp3.Inputs.Enqueue(phase3);
                                    var amp4 = new IntCode(input);
                                    amp4.Inputs.Enqueue(phase4);
                                    var amp5 = new IntCode(input);
                                    amp5.Inputs.Enqueue(phase5);
                                    long feedbackInput = 0;

                                    while (amp1.ProgramState != State.Halt &&
                                            amp2.ProgramState != State.Halt &&
                                            amp3.ProgramState != State.Halt &&
                                            amp4.ProgramState != State.Halt &&
                                            amp5.ProgramState != State.Halt
                                            )
                                    {
                                        var output1 = RunAmp(amp1, input, feedbackInput);
                                        var output2 = RunAmp(amp2, input, output1);
                                        var output3 = RunAmp(amp3, input, output2);
                                        var output4 = RunAmp(amp4, input, output3);
                                        var output5 = RunAmp(amp5, input, output4);
                                        feedbackInput = output5;
                                        if (output5 > highestOutput) highestOutput = output5;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return highestOutput;
        }

        private long RunAmp(IntCode amp, long[] program, long phase, long inputParameter)
        {
            amp.Reset(program);
            amp.Inputs.Enqueue(phase);
            amp.Inputs.Enqueue(inputParameter);
            amp.Run();
            long output = amp.Outputs.Last();
            return output;
        }

        private long RunAmp(IntCode amp, long[] program, long inputParameter)
        {   
            amp.Inputs.Enqueue(inputParameter);
            amp.Run();
            long output = amp.Outputs.Last();
            return output;
        }

        private long[] GetInput()
        {
            return File.ReadAllText(@"input/day7.txt")
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }
    }
}
