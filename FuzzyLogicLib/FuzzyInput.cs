using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyLogicLib
{
    internal class FuzzyInput
    {
        private float _from;
        private float _to;

        public float Value { get; set; }

        public FuzzyInput(int from, int to)
        {
            this._from = from;
            this._to = to;
        }
    }
}