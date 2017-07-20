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

        public WeightFraction PAS { get; }

        public CorrectionFactor Alpha { get; }

        public bool Adequate { get; }

        public Weights Weights { get; }    

        public double PotentialRiskBIA { get; private set; } = 0;

        public double PotentialRiskBIAID { get; private set; } = 0;

        public double PotentialRiskCOMPL { get; private set; } = 0;

        #endregion 

        public Requirement(long id, WeightFraction pas, CorrectionFactor alpha, bool adequate)
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

        public Requirement(long id, WeightFraction pas, CorrectionFactor alpha, bool adequate, int[] weights)
            : this(id, pas, alpha, adequate)
        {
            InitializeWeightsWithIntArray(weights);
        }

        //public Requirement(long id, WeightFraction pas, CorrectionFactor alpha, bool adequate, int[] weights) 
        //    : this(id, pas, alpha, adequate)
        //{
        //    InitializeWeightsWithIntArray(weights);
        //}

        private void InitializeWeightsWithIntArray(int startIndex, int[] weights)
        {
            int i;
            for (i = startIndex;
                i <= startIndex + weights.Length - 1;
                i++)
            {
                Weights[i] = new Weight(weights[i - startIndex]);
            }
        }

        private void InitializeWeightsWithIntArray(int[] weights)
        {
            InitializeWeightsWithIntArray(0, weights);
        }

        public void CalculatePotentialRisk(List<int> totals)
        {
            double correctedPAS = PAS.Value + Alpha.Value;

            List<double> fractions = Weights / totals;

            PotentialRiskBIA =    fractions.Take(3).Sum()         * correctedPAS;

            PotentialRiskBIAID =  fractions.Skip(1).Take(2).Sum() * correctedPAS;

            PotentialRiskCOMPL =  fractions.Skip(3).Sum()         * correctedPAS;
        }
    }
}
