using System;
using ComplexAlgebra;

namespace Calculus
{
    /// <summary>
    /// A calculator for <see cref="Complex"/> numbers, supporting 2 operations ('+', '-').
    /// The calculator visualizes a single value at a time.
    /// Users may change the currently shown value as many times as they like.
    /// Whenever an operation button is chosen, the calculator memorises the currently shown value and resets it.
    /// Before resetting, it performs any pending operation.
    /// Whenever the final result is requested, all pending operations are performed and the final result is shown.
    /// The calculator also supports resetting.
    /// </summary>
    ///
    /// HINT: model operations as constants
    /// HINT: model the following _public_ properties methods
    /// HINT: - a property/method for the currently shown value
    /// HINT: - a property/method to let the user request the final result
    /// HINT: - a property/method to let the user reset the calculator
    /// HINT: - a property/method to let the user request an operation
    /// HINT: - the usual ToString() method, which is helpful for debugging
    /// HINT: - you may exploit as many _private_ fields/methods/properties as you like
    ///
    /// TODO: implement the calculator class in such a way that the Program below works as expected
    class Calculator
    {
        public const char OperationPlus = '+';
        public const char OperationMinus = '-';

        private int _nValues;
        private Complex _shownValue;
        public Complex[] _operands;
        private char? _operation;

        public Calculator() {
            _nValues = 0;
            _shownValue = null;
            _operands = new Complex[2];
            _operation = null;
        }

        public char? Operation
        {
            get => _operation;
            set
            {   
                ComputeResult();
                _shownValue = null;
                _operation = value;
            }
        }

        public Complex Value 
        { 
            get => _shownValue;
            set
            {
                if (_nValues >= 2) {
                    ComputeResult();
                    _nValues = 1;
                }
                _operands[_nValues] = value;
                _shownValue = value;
                _nValues++;
            } 
        }

        public void ComputeResult() 
        {   
            if (Operation != null && _operands[0] != null && _operands[1] != null) {
                if (Operation.Equals('+')) _operands[0] = _operands[0].Plus(_operands[1]);
                else _operands[0] = _operands[0].Minus(_operands[1]);
                _shownValue = _operands[0];
                _operation = ' ';
                _operands[1] = null;
            }
        }

        public void Reset()
        {
            _operands[0] = _operands[1] = null;
            _shownValue = null;
            _nValues = 0;
            _operation = null;
        }

        public override string ToString()
        {   
            return Value == null ? "null" : $"{Value}";
        }
    }
}