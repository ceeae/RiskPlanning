using System;
using System.ComponentModel;
using CalcoloRischioResiduo.RiskAssessment;

namespace CalcoloRischioResiduo.FunctionalPerimeters
{
    public class Perimeter
    {

        public const double THRESHOLD = 0.75; // by req a perimetertype is "covered by analysis" if 75% elements owns a VCI

        private Types _perimetertype;
        private double _avgVCIC3;
        private double _avgVCIAll;
        private double _withVCI;

        public Perimeter(Types perimetertype, double avgVCIC3, double avgVCIAll, double withVCI)
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

        public bool IsTypeOf(Types perimetertype)
        {
            return _perimetertype == perimetertype;
        }

        public double EstimatedResidualRisk(bool isClassifiedElement)
        {
            double correctionFactor = 1 - _withVCI;
            double result = 0;

            if (!isClassifiedElement)
            {
                result = IsAnalyzed() ? _avgVCIC3*(1 + correctionFactor) : SlimVCI.VCIMAX;
            }
            else
            {
                result = IsAnalyzed() ? _avgVCIAll * (1 + correctionFactor) : SlimVCI.VCIMAX;
            }
            return Math.Round(result, 2);
        }

    }
}