using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class IncompleteElement: AbstractElement { 

        public IncompleteElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters, RPvci elvci) // Classified by default
            : base(elperimeter, elperimeters, elvci)
        {

        }

        public override double GetResidualRisk()
        {
            return vci.GetPotentialRisk();                          // Residual Risk = Potential Risk (Managed Risk = 0)
        }
    }
}
