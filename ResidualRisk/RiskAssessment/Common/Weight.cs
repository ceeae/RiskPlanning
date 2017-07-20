using ResidualRisk.RiskAssessment.Exceptions;

namespace ResidualRisk.RiskAssessment.Common
{
    public class Weight
    {
        public static int MIN = 1;
        public static int MAX = 5;

        public int Value { get; }

        public Weight(int value)
        {
            if (value < MIN || value > MAX)
            {
                throw new InvalidWeightValueException();
            }

            Value = value;
        }
    }

    public class DefaultWeight : Weight
    {
        public DefaultWeight() : base(1)
        {
            
        }
    }
}