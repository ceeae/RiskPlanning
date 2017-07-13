using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class IncompleteElement: Element { 

        public IncompleteElement(Types perimeter, SlimVCI vci, PerimetersAnalysis perimeters) : base(vci)
        {
            this.Perimeter = perimeter;
            this.AssociateWith(perimeters);
        }

        public override double EstimateResidualRisk()
        {
            return _vci.GetPotentialRiskValue(); // Residual Risk = Potential Risk (Managed Risk = 0)
        }
    }
}
