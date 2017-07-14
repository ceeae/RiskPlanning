using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class AbsentElement : AbstractElement
    {
        public AbsentElement(Types perimeter, PerimetersAnalysis perimeters) : base(ElementTypes.Classified)
        {
            this.Perimeter = perimeter;
            this.AssociateWith(perimeters);
        }

        public override double GetResidualRiskEstimate()
        {
            if (BelongsToAnalyzedPerimeter())
            {
                return GetAssociatedPerimeter().GetResidualRiskEstimate(classification);
            }

            return SlimVCI.VCIMAX;
        }
    }
}
