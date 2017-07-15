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

    public class CompleteElement : AbstractElement
    {

        public CompleteElement(Types elperimeter, PerimetersAnalysis elperimeters, SlimVCI vci, SlimPDS pds) // Classified by default
            : base(elperimeter, elperimeters, vci, pds)
        {

        }

        public override double GetResidualRiskEstimate()
        {
            return pds.GetResidualRisk();
        }
    }
}
