using ResidualRisk.RiskAssessment.Analysis;
using ResidualRisk.RiskAssessment.Elements;

namespace ResidualRisk.FunctionalPerimeters
{
    public class Perimeter
    {

        public const double THRESHOLD = 0.75;       // by req a perimetertype is "covered by analysis" if 75% elements owns a VCI

        private readonly PerimeterType _perimetertype;

        private readonly double _averageVCIC3;

        private readonly double _averageVCI;

        private readonly double _fractionWithVCI;

        public Perimeter(PerimeterType perimetertype, double averageVCIC3, double averageVCI, double fractionWithVCI)
        {
            _perimetertype = perimetertype;

            _averageVCIC3 = averageVCIC3;

            _averageVCI = averageVCI;

            _fractionWithVCI = fractionWithVCI;
        }

        public bool IsAnalyzed()
        {
            return _fractionWithVCI >= THRESHOLD;
        }

        public bool IsTypeOf(PerimeterType perimetertype)
        {
            return _perimetertype == perimetertype;
        }

        public double GetResidualRiskEstimate(ElementTypes classification)
        {
            double correctionFactor = 1 - _fractionWithVCI;

            double result = 0;

            if (classification == ElementTypes.NotClassified)
            {
                result = IsAnalyzed() ? _averageVCIC3*(1 + correctionFactor) : RiskPlanningVCI.VCI_MAX;
            }
            else
            {
                result = IsAnalyzed() ? _averageVCI * (1 + correctionFactor) : RiskPlanningVCI.VCI_MAX;
            }

            return result;
        }

        public AnalysisStatus GetStatus()
        {
            return IsAnalyzed() ? AnalysisStatus.Complete : AnalysisStatus.BelowThreshold;
        }
    }
}