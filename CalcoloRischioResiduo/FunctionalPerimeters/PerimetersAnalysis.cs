using System.Collections.Generic;

namespace CalcoloRischioResiduo.FunctionalPerimeters
{

    public class PerimetersAnalysis : List<Perimeter>
    {

        public void Add(Types type, double avgVCIC3, double avgVCIAll, double withVCI)
        {
            Perimeter perimeter = new Perimeter(type, avgVCIC3, avgVCIAll, withVCI);
            this.Add(perimeter);
        }

        public AnalysisStatus GetStatus(Types type)
        {
            Perimeter perimeter = FindByType(type);
            return perimeter?.GetStatus() ?? AnalysisStatus.Missing;
        }

        public Perimeter FindByType(Types type)
        {
            return this.Find(e => e.IsTypeOf(type));
        }

    }
}
