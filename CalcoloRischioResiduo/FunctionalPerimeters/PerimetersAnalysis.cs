using System.Collections.Generic;

namespace CalcoloRischioResiduo.FunctionalPerimeters
{

    public class PerimetersAnalysis : List<Perimeter>
    {

        public void Add(PerimeterType perimeterType, double avgVCIC3, double avgVCIAll, double withVCI)
        {
            Perimeter perimeter = new Perimeter(perimeterType, avgVCIC3, avgVCIAll, withVCI);
            this.Add(perimeter);
        }

        public AnalysisStatus GetStatus(PerimeterType perimeterType)
        {
            Perimeter perimeter = FindByType(perimeterType);
            return perimeter?.GetStatus() ?? AnalysisStatus.Missing;
        }

        public Perimeter FindByType(PerimeterType perimeterType)
        {
            return this.Find(e => e.IsTypeOf(perimeterType));
        }

    }
}
