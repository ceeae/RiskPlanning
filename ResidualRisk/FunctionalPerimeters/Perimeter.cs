using System;
using System.ComponentModel;
using ResidualRisk.RiskAssessment;
using ResidualRisk.RiskAssessment.Analysis;
using ResidualRisk.RiskAssessment.Elements;

namespace ResidualRisk.FunctionalPerimeters
{
    public class Perimeter
    {

        public const double THRESHOLD = 0.75;       // by req a perimetertype is "covered by analysis" if 75% elements owns a VCI

        private readonly PerimeterType _perimetertype;
        private readonly double _avgVCIC3;
        private readonly double _avgVCIAll;
        private readonly double _withVCI;

        public Perimeter(PerimeterType perimetertype, double avgVCIC3, double avgVCIAll, double withVCI)
        {
            _perimetertype = perimetertype;
            _avgVCIC3 = avgVCIC3;
            _avgVCIAll = avgVCIAll;
            _withVCI = withVCI;
        }

        public bool IsAnalyzed()
        {
            return _withVCI >= THRESHOLD;
        }

        public bool IsTypeOf(PerimeterType perimetertype)
        {
            return _perimetertype == perimetertype;
        }

        public double GetResidualRiskEstimate(ElementTypes classification)
        {
            double correctionFactor = 1 - _withVCI;
            double result = 0;

            if (classification == ElementTypes.NotClassified)
            {
                result = IsAnalyzed() ? _avgVCIC3*(1 + correctionFactor) : RiskPlanningVCI.VCI_MAX;
            }
            else
            {
                result = IsAnalyzed() ? _avgVCIAll * (1 + correctionFactor) : RiskPlanningVCI.VCI_MAX;
            }

            return result;
        }

        public AnalysisStatus GetStatus()
        {
            return IsAnalyzed() ? AnalysisStatus.Complete : AnalysisStatus.BelowThreshold;
        }
    }
}