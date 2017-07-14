using CalcoloRischioResiduo.RiskAssessment.Exceptions;

namespace CalcoloRischioResiduo.RiskAssessment.Common
{
    public class CorrectionFactor
    {
        public static int MIN = 0;
        public static int MAX = 5;

        public double Value { get; }

        public CorrectionFactor(double value)
        {
            if (value < MIN || value > MAX)
            {
                throw new InvalidCorrectionFactorValueException();
            }

            Value = value;
        }
    }

    public class DefaulCorrectionFactor : CorrectionFactor
    {
        public DefaulCorrectionFactor() : base(0)
        {

        }
    }
}