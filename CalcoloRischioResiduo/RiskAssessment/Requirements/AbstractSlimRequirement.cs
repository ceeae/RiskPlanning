using CalcoloRischioResiduo.RiskAssessment.Exceptions;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public abstract class AbstractSlimRequirement
    {
        private long _id = 0;
        private double _PAS = 0;
        private double _Alpha = 0;

        public AbstractSlimRequirement(long libraryId, double PAS, double Alpha)
        {
            _id = libraryId;
            CheckRangeValue(PAS);
            CheckCorrectionValue(Alpha);
            _PAS = PAS;
            _Alpha = Alpha;
        }

        public long LibraryId()
        {
            return _id;
        }
        public double GetPAS()
        {
            return _PAS;
        }

        public double GetAlpha()
        {
            return _Alpha;
        }

        private void CheckRangeValue(double value)
        {
            if (value < 1 || value > 5)
            {
                throw new  InvalidProbabilityValueException();
            }
        }
        private void CheckCorrectionValue(double value)
        {
            if (value < 0 || value > 1)
            {
                throw new InvalidCorrectionValueException();
            }
        }

    }
}
