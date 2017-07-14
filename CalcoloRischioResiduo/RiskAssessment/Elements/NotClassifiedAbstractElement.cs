using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class NotClassifiedAbstractElement : AbstractElement
    {
        public NotClassifiedAbstractElement(Types perimeter, PerimetersAnalysis perimeters) : base()
        {
            this.Perimeter = perimeter;
            this.AssociateWith(perimeters);
        }

        public override double EstimateResidualRisk()
        {
            if (BelongsToAnalyzedPerimeter())
            {
                return GetAssociatedPerimeter().GetResidualRiskEstimate(classification);
            }

            return SlimVCI.VCIMAX;
        }
    }
}
