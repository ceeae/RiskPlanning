
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Elements;

namespace UnitTests.Elements.CompleteElements
{
    public abstract class AbstractCase : IBuilder
    {

        public CompleteElement CreateCase()
        {
            PerimeterType perimeter = SelectPerimeter();
            PerimetersAnalysis perimeters = CreatePerimetersAnalysis();
            RPvci vci = CreateRPVci();                                               // vci=750
            RPpds pds = CreateRPpds();

            return new CompleteElement(perimeter, perimeters, vci, pds);
        }
       
        protected abstract PerimeterType SelectPerimeter();

        protected abstract PerimetersAnalysis CreatePerimetersAnalysis();

        protected abstract RPvci CreateRPVci();

        protected abstract RPpds CreateRPpds();

    }
}
