using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class IncompleteAbstractElement: AbstractElement { 

        public IncompleteAbstractElement(Types perimeter, SlimVCI vci, PerimetersAnalysis perimeters) : base(vci)
        {
            this.Perimeter = perimeter;
            this.AssociateWith(perimeters);
        }

        public override double GetResidualRiskEstimate()
        {
            return _vci.GetPotentialRisk(); // Residual Risk = Potential Risk (Managed Risk = 0)
        }
    }
}
