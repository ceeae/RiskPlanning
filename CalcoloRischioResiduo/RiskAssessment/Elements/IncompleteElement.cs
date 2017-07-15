using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class IncompleteElement: AbstractElement { 

        public IncompleteElement(Types elperimeter, PerimetersAnalysis elperimeters, RPvci elvci) // Classified by default
            : base(elperimeter, elperimeters, elvci)
        {

        }

        public override double GetResidualRiskEstimate()
        {
            return vci.GetPotentialRisk();                          // Residual Risk = Potential Risk (Managed Risk = 0)
        }
    }
}
