using System;
using System.Collections.Generic;
using System.Linq;
using ResidualRisk.RiskAssessment.Common;
using ResidualRisk.RiskAssessment.Exceptions;

namespace ResidualRisk.RiskAssessment.Requirements
{
    public class Requirement
    {
        public const int WEIGHTS_NUM = 3 + 35;  // RID + 35 compliance Weights indexed by 0..

        #region Properties Risk Factors (calculated - BIA, BIAID, COMPL - format 00.00)

        public long Id { get; }

        public FractionWeight PAS { get; }

        public CorrectionFactor Alpha { get; }

        public bool Adequate { get; }

        public Weights Weights { get; }    

        public double PotentialRiskBIA { get; private set; } = 0;

        public double PotentialRiskBIAID { get; private set; } = 0;

        public double PotentialRiskCOMPL { get; private set; } = 0;

        #endregion 

        public Requirement(long id, FractionWeight pas, CorrectionFactor alpha, bool adequate)
        {
            if (id <= 0)
            {
                throw new InvalidKeyException();
            }

            Id = id;
            PAS = pas;
            Alpha = alpha;
            Adequate = adequate;

            Weights = new Weights(Enumerable.Repeat((Weight)new DefaultWeight(), WEIGHTS_NUM).ToList()); 
        }

        public Requirement(long id, FractionWeight pas, CorrectionFactor alpha, bool adequate, int[] weights)
            : this(id, pas, alpha, adequate)
        {
            InitializeWeightsWithIntArray(weights);
        }

        //public Requirement(long id, FractionWeight pas, CorrectionFactor alpha, bool adequate, int[] weights) 
        //    : this(id, pas, alpha, adequate)
        //{
        //    InitializeWeightsWithIntArray(weights);
        //}

        public void InitializeWeightsWithIntArray(int startIndex, int[] weights)
        {
            int i;
            for (i = startIndex;
                i <= startIndex + weights.Length - 1;
                i++)
            {
                Weights[i] = new Weight(weights[i - startIndex]);
            }
        }

        public void InitializeWeightsWithIntArray(int[] weights)
        {
            InitializeWeightsWithIntArray(0, weights);
        }

        public void CalculatePotentialRiskFactors(List<int> totals)
        {
            double correctedPAS = PAS.Value + Alpha.Value;

            List<double> f = DivideWeightsBy(totals);                       // f = Weights / CalculateWeightsTotals

            PotentialRiskBIA =    f.Take(3).Sum()         * correctedPAS;
            PotentialRiskBIAID =  f.Skip(1).Take(2).Sum() * correctedPAS;
            PotentialRiskCOMPL =  f.Skip(3).Sum()         * correctedPAS;
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
