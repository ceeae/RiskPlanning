
using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Analysis;
using ResidualRisk.RiskAssessment.Elements;

namespace UnitTests.Elements.CompleteElements
{
    public abstract class AbstractCase : IBuilder
    {

        public CompleteElement CreateCase()
        {
            PerimeterType perimeter = SelectPerimeter();
            PerimetersAnalysis perimeters = CreatePerimetersAnalysis();
            RiskPlanningVCI vci = CreateRPVci();                                               // vci=750
            RiskPlanningPDS pds = CreateRPpds();

            return new CompleteElement(perimeter, perimeters, vci, pds);
        }
       
        protected abstract PerimeterType SelectPerimeter();

        protected abstract PerimetersAnalysis CreatePerimetersAnalysis();

        protected abstract RiskPlanningVCI CreateRPVci();

        protected abstract RiskPlanningPDS CreateRPpds();

    }
}
