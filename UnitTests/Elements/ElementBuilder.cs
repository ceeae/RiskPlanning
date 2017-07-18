using ResidualRisk;
using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment;
using ResidualRisk.RiskAssessment.Analysis;
using ResidualRisk.RiskAssessment.Elements;
using ResidualRisk.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements
{
    public class ElementBuilder
    {

        public static IElement CreateCase(TestCase scenario)
        {
            IElement element = null;
            PerimetersAnalysis perimeters = CreatePerimetersAnalysis();
            RiskPlanningVCI vci = new RiskPlanningVCI(450, 300); // vci=750
            RiskPlanningPDS pds = CreateRPpds();

            switch (scenario)
            {

                case TestCase.NotClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element = new NotClassifiedElement(PerimeterType.InformationTechnology, perimeters);
                    break;

                case TestCase.NotClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element = new NotClassifiedElement(PerimeterType.AdministrationFinanceAndControl, perimeters);
                    break;

                case TestCase.ClassifiedAbsentElementWithMissingPerimeterAnalysis:
                    element = new AbsentElement(PerimeterType.InformationTechnology, perimeters);
                    break;

                case TestCase.ClassifiedAbsentElementWithCompletePerimeterAnalysis:
                    element = new AbsentElement(PerimeterType.AdministrationFinanceAndControl, perimeters);

                    break;

                case TestCase.IncompleteElementWithMissingPerimeterAnalysis:
                    element = new IncompleteElement(PerimeterType.InformationTechnology, perimeters, vci);
                    break;

                case TestCase.IncompleteElementWithCompletePerimeterAnalysis:
                    element = new IncompleteElement(PerimeterType.AdministrationFinanceAndControl, perimeters, vci);
                    break;

                case TestCase.CompleteElementWithMissingPerimeterAnalysis:
                    element = new CompleteElement(PerimeterType.InformationTechnology, perimeters, vci, pds);
                    break;

                case TestCase.CompleteElementWithCompletePerimeterAnalysis:
                    element = new CompleteElement(PerimeterType.AdministrationFinanceAndControl, perimeters, vci, pds);
                    break;
            }
            return element;
        }

        public static PerimetersAnalysis CreatePerimetersAnalysis()
        {
            // Build perimeters analysis object
            PerimetersAnalysis perimeters = new PerimetersAnalysis
            {
                { PerimeterType.InformationTechnology, 800, 700, 0.72},                  // Missing Perimeter Analysis
                { PerimeterType.AdministrationFinanceAndControl, 835, 630, 0.84},       // Complete Perimeter Analysis
            };
            return perimeters;
        }

        public static RiskPlanningPDS CreateRPpds()
        {
            RequirementsSet requirements = new RequirementsSet();

            requirements.AddRequirement(101, 4.8, 0.2, true, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,

            });

            requirements.AddRequirement(102, 3.2, 0.0, false, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,
            });

            requirements.AddRequirement(103, 1.0, 0.0, false, new int[3]
            {
                5, 4, 1
            });

            requirements.AddRequirement(104, 1.0, 0.0, false, new int[3]
            {
                3, 4, 1
            });

            requirements.AddRequirement(105, 1.0, 0.0, true, new int[3]
            {
                3, 3, 1
            });

            return new RiskPlanningPDS(requirements);
        }

    }
}
