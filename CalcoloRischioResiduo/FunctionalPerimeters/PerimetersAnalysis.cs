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

        public int GetAnalysisStatus(Types type)
        {
            Perimeter perimeter = FindByType(type);
            if (perimeter == null)
            {
                return AnalysisStatus.Missing;
            }
            return perimeter.IsAnalyzed() ? AnalysisStatus.Complete : AnalysisStatus.BelowThreshold;
        }

        public Perimeter FindByType(Types type)
        {
            return this.Find(e => e.IsTypeOf(type));
        }

    }
}
