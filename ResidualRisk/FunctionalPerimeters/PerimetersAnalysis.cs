using System.Collections.Generic;

namespace ResidualRisk.FunctionalPerimeters
{

    public class PerimetersAnalysis : List<Perimeter>
    {

        public void Add(PerimeterType perimeterType, double averageVCIC3, double averageVCI, double fractionWithVCI)
        {
            Perimeter perimeter = new Perimeter(perimeterType, averageVCIC3, averageVCI, fractionWithVCI);

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
