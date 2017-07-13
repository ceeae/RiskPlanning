using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{

    public class CompleteElement : Element
    {

        public CompleteElement(Types perimeter, SlimVCI vci, SlimPDS pds, PerimetersAnalysis perimeters) : base(vci, pds)
        {
            this.Perimeter = perimeter;
            this.AssociateWith(perimeters);
        }

        public override double EstimateResidualRisk()
        {
            return _pds.GetResidualRiskValue();
        }
    }
}
