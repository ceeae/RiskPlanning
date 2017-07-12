using System.Collections.Generic;

namespace CalcoloRischioResiduo
{

    public class FunctionalPerimeterAnalysisCoverage : List<FunctionalPerimeterAnalysis>
    {

        public void Add(FunctionalPerimeters perimeter, double avgVCIC3, double avgVCIAll, double withVCI)
        {
            FunctionalPerimeterAnalysis analysis = new FunctionalPerimeterAnalysis(perimeter, avgVCIC3, avgVCIAll, withVCI);
            this.Add(analysis);
        }

        public bool IsCovered(FunctionalPerimeters perimeter)
        {
            FunctionalPerimeterAnalysis analysis = FindAnalysis(perimeter);
            return analysis?.isAnalyzed() ?? false;
        }

        public FunctionalPerimeterAnalysis FindAnalysis(FunctionalPerimeters perimeter)
        {
            return this.Find(e => e.isPerimeter(perimeter));
        }


    }
}
