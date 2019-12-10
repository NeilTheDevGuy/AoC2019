using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019.Utils
{
    public class IntCode
    {
        private long[] _program;
        private long _ptr = 0;
        
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
            Inputs = new Queue<long>();
            Outputs = new List<long>();
            ProgramState = State.Running;
        }

        public void Run()
        {                       
            while (_ptr <= _program.Length)
            {
                var opCode = GetOpCode(_program[_ptr].ToString());
                long param1, param2;
                switch (opCode.Type)
                {
                    case 1:
                        param1 = GetVal(opCode.ModeOne, _program[_ptr + 1]);
                        param2 = GetVal(opCode.ModeTwo, _program[_ptr + 2]);
                        _program[_program[_ptr + 3]] = param1 + param2;
                        _ptr += 4;
                        break;
                    case 2:
                        param1 = GetVal(opCode.ModeOne, _program[_ptr + 1]);
                        param2 = GetVal(opCode.ModeTwo, _program[_ptr + 2]);
                        _program[_program[_ptr + 3]] = param1 * param2;
                        _ptr += 4;
                        break;
                    case 3:
                        if (Inputs.Any())
                        {
                            _program[_program[_ptr + 1]] = Inputs.Dequeue();
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
                        Outputs.Add(GetVal(opCode.ModeOne, _program[_ptr + 1]));
                        _ptr += 2;
                        return;
                    case 5:
                        if (GetVal(opCode.ModeOne, _program[_ptr + 1]) == 0)
                        {
                            _ptr += 3;
                        }
                        else
                        {
                            _ptr = GetVal(opCode.ModeTwo, _program[_ptr + 2]);
                        }
                        break;
                    case 6:
                        if (GetVal(opCode.ModeOne, _program[_ptr + 1]) != 0)
                        {
                            _ptr += 3;
                        }
                        else
                        {
                            _ptr = GetVal(opCode.ModeTwo, _program[_ptr + 2]);
                        }
                        break;
                    case 7:
                        if (GetVal(opCode.ModeOne, _program[_ptr + 1]) < GetVal(opCode.ModeTwo, _program[_ptr + 2]))
                        {
                            _program[_program[_ptr + 3]] = 1;
                        }
                        else
                        {
                            _program[_program[_ptr + 3]] = 0;
                        }
                        _ptr += 4;
                        break;
                    case 8:
                        if (GetVal(opCode.ModeOne, _program[_ptr + 1]) == GetVal(opCode.ModeTwo, _program[_ptr + 2]))
                        {
                            _program[_program[_ptr + 3]] = 1;
                        }
                        else
                        {
                            _program[_program[_ptr + 3]] = 0;
                        }
                        _ptr += 4;
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
                return new OpCode(int.Parse(instruction), 0, 0);
            }
            if (length == 3)
            {
                return new OpCode(int.Parse(instruction.Substring(1).ToString()), 1, 0);
            }
            if (length == 4)
            {
                return new OpCode(int.Parse(instruction.Substring(2).ToString()), int.Parse(instruction[1].ToString()), int.Parse(instruction[0].ToString()));
            }
            throw new Exception("Instruction is invalid length");
        }

        private long GetVal(int mode, long param)
        {
            return mode == 1
                ? param
                : _program[param];
        }

        public enum State
        {
            Running,
            Wait,
            Halt
        }

    }
}
