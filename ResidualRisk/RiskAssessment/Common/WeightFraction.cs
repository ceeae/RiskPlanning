using ResidualRisk.RiskAssessment.Common;
using ResidualRisk.RiskAssessment.Exceptions;

namespace ResidualRisk.RiskAssessment.Common
{
    public class WeightFraction
    {
        public static int MIN = 1;

        public static int MAX = 5;

        public double Value { get; }

        public WeightFraction(double value)
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
    public class DefaultWeightFraction : Weight
    {
        public DefaultWeightFraction() : base(0)
        {

        }
    }
}
