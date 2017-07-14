using CalcoloRischioResiduo;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment;
using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements
{
    public class ScenarioElementsBuilder
    {

        public static IElement CreateFromScenario(Scenarios scenario)
        {
            IElement element = null;
            PerimetersAnalysis perimeters = CreatePerimetersAnalysis();
            SlimVCI vci = new SlimVCI(450, 300); // vci=750
            SlimPDS pds = new SlimPDS(536);

            switch (scenario)
            {

                case Scenarios.NotClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element = new NotClassifiedAbstractElement(Types.InformationTechnology, perimeters);
                    break;

                case Scenarios.NotClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element = new NotClassifiedAbstractElement(Types.AdministrationFinanceAndControl, perimeters);
                    break;

                case Scenarios.ClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element = new AbsentElement(Types.InformationTechnology, perimeters);
                    break;

                case Scenarios.ClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element = new AbsentElement(Types.AdministrationFinanceAndControl, perimeters);

                    break;

                case Scenarios.IncompleteElementWithMissingPerimeterAnalysis:
                    element = new IncompleteAbstractElement(Types.InformationTechnology, vci, perimeters);
                    break;

                case Scenarios.IncompleteElementWithCompletePerimeterAnalysis:
                    element = new IncompleteAbstractElement(Types.AdministrationFinanceAndControl, vci, perimeters);
                    break;

                case Scenarios.CompleteElementWithMissingPerimeterAnalysis:
                    element = new CompleteAbstractElement(Types.InformationTechnology, vci, pds, perimeters);
                    break;

                case Scenarios.CompleteElementWithCompletePerimeterAnalysis:
                    element = new CompleteAbstractElement(Types.AdministrationFinanceAndControl, vci, pds, perimeters);
                    break;
            }
            return element;
        }

        public static PerimetersAnalysis CreatePerimetersAnalysis()
        {
            // Build perimeters analysis object
            PerimetersAnalysis perimeters = new PerimetersAnalysis
            {
                { Types.InformationTechnology, 800, 700, 0.72},                  // Missing Perimeter Analysis
                { Types.AdministrationFinanceAndControl, 835, 630, 0.84},       // Complete Perimeter Analysis
            };
            return perimeters;
        }

    }
}
