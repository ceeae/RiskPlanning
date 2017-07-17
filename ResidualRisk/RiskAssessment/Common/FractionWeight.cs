using ResidualRisk.RiskAssessment.Common;
using ResidualRisk.RiskAssessment.Exceptions;

namespace ResidualRisk.RiskAssessment.Common
{
    public class FractionWeight
    {
        public static int MIN = 1;
        public static int MAX = 5;

        public double Value { get; }

        public FractionWeight(double value)
        {
            if (value < MIN || value > MAX)
            {
                throw new InvalidWeightValueException();
            }

            Value = value;
        }
    }
}

namespace ResidualRisk.RiskAssessment.Requirements
{
    public class DefaulFractiontWeight : Weight
    {
        public DefaulFractiontWeight() : base(0)
        {

        }
    }
}
