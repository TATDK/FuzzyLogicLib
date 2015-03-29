using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FuzzyLogicLib
{
    internal class FuzzyDataHandler
    {
        private List<FuzzyInput> _inputs = new List<FuzzyInput>();
        private List<FuzzyRuleSet> _ruleSets = new List<FuzzyRuleSet>();

        internal void Add(FuzzyInput input, FuzzyRuleSet ruleSet)
        {
            this._inputs.Add(input);
            this._ruleSets.Add(ruleSet);
        }

        internal PointF Evaluate()
        {
            PointF centroid = new PointF(0, 0);

            for (int i = 0; i < _inputs.Count; i++)
            {
                var output = _ruleSets[i].Evaluate(_inputs[i].Value);
                centroid.X += output.X;
                centroid.Y += output.Y;
            }

            centroid.X /= _inputs.Count;
            centroid.Y /= _inputs.Count;

            return centroid;
        }
    }
}