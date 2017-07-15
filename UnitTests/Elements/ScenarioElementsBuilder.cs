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
                    element = new NotClassifiedElement(Types.InformationTechnology, perimeters);
                    break;

                case Scenarios.NotClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element = new NotClassifiedElement(Types.AdministrationFinanceAndControl, perimeters);
                    break;

                case Scenarios.ClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element = new AbsentElement(Types.InformationTechnology, perimeters);
                    break;

                case Scenarios.ClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element = new AbsentElement(Types.AdministrationFinanceAndControl, perimeters);

                    break;

                case Scenarios.IncompleteElementWithMissingPerimeterAnalysis:
                    element = new IncompleteElement(Types.InformationTechnology, perimeters, vci);
                    break;

                case Scenarios.IncompleteElementWithCompletePerimeterAnalysis:
                    element = new IncompleteElement(Types.AdministrationFinanceAndControl, perimeters, vci);
                    break;

                case Scenarios.CompleteElementWithMissingPerimeterAnalysis:
                    element = new CompleteElement(Types.InformationTechnology, perimeters, vci, pds);
                    break;

                case Scenarios.CompleteElementWithCompletePerimeterAnalysis:
                    element = new CompleteElement(Types.AdministrationFinanceAndControl, perimeters, vci, pds);
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
