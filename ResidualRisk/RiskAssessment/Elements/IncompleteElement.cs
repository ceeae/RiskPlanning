using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Analysis;

namespace ResidualRisk.RiskAssessment.Elements
{
    public class IncompleteElement: AbstractElement { 

        public IncompleteElement(PerimeterType perimeterType, PerimetersAnalysis perimeters, RiskPlanningVCI elvci) // Classified by default
            : base(perimeterType, perimeters, elvci)
        {

        }

        public override double GetResidualRisk()
        {
            return vci.GetPotentialRisk();                          // Residual Risk = Potential Risk (Managed Risk = 0)
        }
    }
}
