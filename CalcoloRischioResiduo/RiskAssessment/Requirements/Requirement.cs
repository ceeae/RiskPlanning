

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class Requirement
    {
        public const int WEIGHTS_NUM = 3 + 35;  // indexed by 0..

        public long Id { get; }

        public FractionWeight PAS { get; }
        public CorrectionFactor Alpha { get; }

        public List<Weight> Weights { get;  }     // RID + 35 compliance weights

        #region calculated factors
        // Calculated factors

        private double _prbia = 0;
        private double _prbiaid = 0;
        private double _prcompl = 0;

        public double PRbia => Math.Round(_prbia, 1);

        public double PRbiaID => Math.Round(_prbiaid, 1);

        public double PRcompl => Math.Round(_prcompl, 1);
        #endregion calculated factors

        public Requirement(long id, FractionWeight pas, CorrectionFactor alpha)
        {
            if (id <= 0)
            {
                throw new InvalidKeyException();
            }

            Id = id;
            PAS = pas;
            Alpha = alpha;

            Weights = Enumerable.Repeat((Weight) new DefaultWeight(), WEIGHTS_NUM).ToList();
        }

        public void InitializeWeightsWithIntArray(int index, int[] values)
        {
            int i;
            for (i = index;
                i <= index + values.Length - 1;
                i++)
            {
                Weights[i] = new Weight(values[i - index]);
            }
        }

        public void CalculaterPotentialRiskFactors(List<int> totals)
        {
            double correctedPAS = PAS.Value + Alpha.Value;

            List<double> f = DivideWeightsBy(totals); // f = Weights / Totals

            _prbia = f.Take(3).Sum() * correctedPAS;
            _prbiaid = f.Skip(1).Take(2).Sum() * correctedPAS;
            _prcompl = f.Skip(3).Sum() * correctedPAS;
        }


        private List<double> DivideWeightsBy(List<int> totals)
        {
            int i;
            List<double> fraction = Enumerable.Repeat((double) 0, WEIGHTS_NUM).ToList();

            for (i = 0; i < WEIGHTS_NUM - 1; i++)
            {
                fraction[i] = (double) Weights[i].Value/totals[i];
            }
            return fraction;
        }

    }
}
