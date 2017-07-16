using CalcoloRischioResiduo;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment;
using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using CalcoloRischioResiduo.RiskAssessment.Requirements;
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
            RPvci vci = new RPvci(450, 300); // vci=750
            RPpds pds = CreateRPpds();

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

        public static RPpds CreateRPpds()
        {
            RequirementsSet set = new RequirementsSet();

            set.AddRequirement(101, 4.8, 0.2, true, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,

            });

            set.AddRequirement(102, 3.2, 0.0, false, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,
            });

            set.AddRequirement(103, 1.0, 0.0, false, new int[3]
            {
                5, 4, 1
            });

            set.AddRequirement(104, 1.0, 0.0, false, new int[3]
            {
                3, 4, 1
            });

            set.AddRequirement(105, 1.0, 0.0, true, new int[3]
            {
                3, 3, 1
            });

            return new RPpds(set);
        }

    }
}
