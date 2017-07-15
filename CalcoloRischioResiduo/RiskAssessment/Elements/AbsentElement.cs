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
        public AbsentElement(Types elperimeter, PerimetersAnalysis elperimeters) 
            : base(ElementTypes.Classified, elperimeter, elperimeters)
        {

        }
    }
}
