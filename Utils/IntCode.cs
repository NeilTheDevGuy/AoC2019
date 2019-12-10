using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019.Utils
{
    public class IntCode
    {
        private long[] _program;
        private Dictionary<long, long> _highMem = new Dictionary<long, long>();
        private long _ptr = 0;
        private long _relativeOffset = 0;
        
        public Queue<long> Inputs = new Queue<long>();
        public List<long> Outputs = new List<long>();
        public State ProgramState;

        public IntCode(long[] program)
        {
            _program = program;
        }

        public void Reset(long[] program)
        {
            _program = program;
            _ptr = 0;
            _relativeOffset = 0;
            Inputs = new Queue<long>();
            Outputs = new List<long>();
            _highMem = new Dictionary<long, long>();
            ProgramState = State.Running;
        }

        public void Run()
        {                       
            while (_ptr <= _program.Length)
            {
                var opCode = GetOpCode(Find(_ptr).ToString());
                long param1, param2;
                switch (opCode.Type)
                {
                    case 1:
                        param1 = GetVal(opCode.ModeOne, Find(_ptr + 1));
                        param2 = GetVal(opCode.ModeTwo, Find(_ptr + 2));
                        SetVal(Find(_ptr + 3), param1 + param2, opCode.ModeThree);
                        _ptr += 4;
                        break;
                    case 2:
                        param1 = GetVal(opCode.ModeOne, Find(_ptr + 1));
                        param2 = GetVal(opCode.ModeTwo, Find(_ptr + 2));
                        SetVal(Find(_ptr + 3), param1 * param2, opCode.ModeThree);
                        _ptr += 4;
                        break;
                    case 3:
                        if (Inputs.Any())
                        {
                            SetVal(Find(_ptr + 1), Inputs.Dequeue(), opCode.ModeOne);
                            ProgramState = State.Running;
                            _ptr += 2;
                        }
                        else
                        {
                            ProgramState = State.Wait;
                            return;
                        }
                        break;
                    case 4:
                        Outputs.Add(GetVal(opCode.ModeOne, Find(_ptr + 1)));
                        _ptr += 2;
                        return;
                    case 5:
                        if (GetVal(opCode.ModeOne, Find(_ptr + 1)) == 0)
                        {
                            _ptr += 3;
                        }
                        else
                        {
                            _ptr = GetVal(opCode.ModeTwo, Find(_ptr + 2));
                        }
                        break;
                    case 6:
                        if (GetVal(opCode.ModeOne, Find(_ptr + 1)) != 0)
                        {
                            _ptr += 3;
                        }
                        else
                        {
                            _ptr = GetVal(opCode.ModeTwo, Find(_ptr + 2));
                        }
                        break;
                    case 7:
                        if (GetVal(opCode.ModeOne, Find(_ptr + 1)) < GetVal(opCode.ModeTwo, Find(_ptr + 2)))
                        {
                            SetVal(Find(_ptr + 3), 1, opCode.ModeThree);
                        }
                        else
                        {
                            SetVal(Find(_ptr + 3), 0, opCode.ModeThree);
                        }
                        _ptr += 4;
                        break;
                    case 8:
                        if (GetVal(opCode.ModeOne, Find(_ptr + 1)) == GetVal(opCode.ModeTwo, Find(_ptr + 2)))
                        {
                            SetVal(Find(_ptr + 3), 1, opCode.ModeThree);
                        }
                        else
                        {
                            SetVal(Find(_ptr + 3), 0, opCode.ModeThree);
                        }
                        _ptr += 4;
                        break;
                    case 9:
                        _relativeOffset += GetVal(opCode.ModeOne, Find(_ptr + 1));
                        _ptr += 2;
                        break;
                    case 99:
                        ProgramState = State.Halt;
                        return;
                    default:
                        throw new Exception($"Unexpected opcode at position {_ptr} : {opCode.Type}");
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
                return new OpCode(int.Parse(instruction.Substring(1).ToString()), int.Parse(instruction[0].ToString()), 0, 0);
            }
            if (length == 4)
            {
                return new OpCode(int.Parse(instruction.Substring(2).ToString()), int.Parse(instruction[1].ToString()), int.Parse(instruction[0].ToString()), 0);
            }
            if (length == 5)
            {
                return new OpCode(int.Parse(instruction.Substring(3).ToString()), int.Parse(instruction[2].ToString()), int.Parse(instruction[1].ToString()), int.Parse(instruction[0].ToString()));
            }
            throw new Exception("Instruction is invalid length");
        }

        private void SetVal(long position, long value, int mode)
        {
            if (mode == 2)
            {
                position += _relativeOffset;
            }
            if (position > _program.Length)
            {
                if (_highMem.ContainsKey(position))
                {
                    _highMem[position] = value;
                }
                else
                {
                    _highMem.Add(position, value);
                }
            }
            else
            {
                _program[position] = value;
            }
        }

        private long GetVal(int mode, long param)
        {
            if (mode == 0) return Find(param);
            if (mode == 1) return param;
            if (mode == 2) return Find(param + _relativeOffset);

            throw new Exception("$Invalid Mode: {mode}");
        }

        private long Find(long location)
        {
            if (location > _program.Length)
            {
                if (_highMem.ContainsKey(location))
                {
                    return _highMem[location];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return _program[location];
            }
        }

        public enum State
        {
            Running,
            Wait,
            Halt
        }

        public class OpCode
        {
            public int Type { get; set; }
            public int ModeOne { get; set; }
            public int ModeTwo { get; set; }
            public int ModeThree { get; set; }

            public OpCode(int type, int modeOne, int modeTwo, int modeThree)
            {
                Type = type;
                ModeOne = modeOne;
                ModeTwo = modeTwo;
                ModeThree = modeThree;
            }
        }
    }
}
