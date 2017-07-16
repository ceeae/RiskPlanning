

using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace UnitTests.Elements.CompleteElements
{
    public class ScenarioOne : IScenarioBuilder
    {


        public CompleteElement CreateScenario()
        {
            Types perimeter = SelectPerimeter();                                    // take just a pseudo-default perimeter
            PerimetersAnalysis perimeters = CreatePerimetersAnalysis();
            RPvci vci = CreateRPVci();                                               // vci=750
            RPpds pds = CreateRPpds();

            return new CompleteElement(perimeter, perimeters, vci, pds);
        }

        protected Types SelectPerimeter()
        {
            return Types.InformationTechnology;
        }

        protected PerimetersAnalysis CreatePerimetersAnalysis()
        {
            // Build perimeters analysis object
            PerimetersAnalysis perimeters = new PerimetersAnalysis
            {
                { Types.InformationTechnology, 800, 700, 0.72},                  // Missing Perimeter Analysis
                { Types.AdministrationFinanceAndControl, 835, 630, 0.84},       // Complete Perimeter Analysis
            };
            return perimeters;
        }

        protected RPvci CreateRPVci()
        {
            return null;
        }


        protected RPpds CreateRPpds()
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
