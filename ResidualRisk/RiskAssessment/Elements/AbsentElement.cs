using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Analysis;

namespace ResidualRisk.RiskAssessment.Elements
{
    public class AbsentElement : AbstractElement
    {
        public AbsentElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters) 
            : base(ElementTypes.Classified, elperimeter, elperimeters)
        {

        }
    }
}
