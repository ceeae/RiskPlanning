using System.ComponentModel;

namespace CalcoloRischioResiduo
{
    public class FunctionalPerimetersAnalysis
    {
        private FunctionalPerimeters _perimeter;
        private double _avgVCIC3;
        private double _avgVCIAll;
        private double _withVCI;

        public static bool isAnalyzed(FunctionalPerimeters perimeter)
        {
            return false;
        }

        public FunctionalPerimetersAnalysis(FunctionalPerimeters perimeter, double avgVCIC3, double avgVCIAll, double withVCI)
        {
            _perimeter = perimeter;
            _avgVCIC3 = avgVCIC3;
            _avgVCIAll = avgVCIAll;
            _withVCI = withVCI;
        }

    }
}