using CalcoloRischioResiduo;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment;
using Xunit;
using FluentAssertions;

namespace UnitTests.ResidualRiskEstimates
{
    public class ElementsBuilder
    {

        // UnitOfWork_Scenario_ExpectedResult
        // e.g. class LogAnalyzerTests 
        //      [Fact] IsValidFilename_BadExtension_ReturnFalse

        public static Element CreateFromScenario(Scenarios scenario)
        {
            Element element = new Element();
            PerimetersAnalysis perimeters = CreatePerimetersAnalysis();
            element.AssociateWith(perimeters);

            SlimVCI vci = new SlimVCI(750, 300);
            SlimPDS pds = new SlimPDS(536);

            switch (scenario)
            {
                case Scenarios.NotClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element.Perimeter = Types.BrandStrategyAndMedia;
                    break;

                case Scenarios.NotClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element.Perimeter = Types.AdministrationFinanceAndControl;
                    break;

                case Scenarios.ClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element.Classify();
                    element.Perimeter = Types.InformationTechnology;
                    break;

                case Scenarios.ClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element.Classify();
                    element.Perimeter = Types.AdministrationFinanceAndControl;
                    break;

                case Scenarios.IncompleteElementWithMissingPerimeterAnalysis:
                    element = new Element(vci);
                    element.AssociateWith(perimeters);
                    element.Perimeter = Types.InformationTechnology;
                    break;

                case Scenarios.IncompleteElementWithCompletePerimeterAnalysis:
                    element = new Element(vci);
                    element.AssociateWith(perimeters);
                    element.Perimeter = Types.AdministrationFinanceAndControl;
                    break;

                case Scenarios.CompleteElementWithMissingPerimeterAnalysis:
                    element = new Element(vci, pds);
                    element.AssociateWith(perimeters);
                    element.Perimeter = Types.InformationTechnology;
                    break;

                case Scenarios.CompleteElementWithCompletePerimeterAnalysis:
                    element = new Element(vci, pds);
                    element.AssociateWith(perimeters);
                    element.Perimeter = Types.AdministrationFinanceAndControl;
                    break;
            }
            return element;
        }

        public static PerimetersAnalysis CreatePerimetersAnalysis()
        {
            // Build perimeters analysis object
            PerimetersAnalysis perimeters = new PerimetersAnalysis
            {
                { Types.InformationTechnology, 800, 700, 0.7},                  // Missing Perimeter Analysis
                { Types.AdministrationFinanceAndControl, 835, 630, 0.84},       // Complete Perimeter Analysis
            };
            return perimeters;
        }
    }
}
