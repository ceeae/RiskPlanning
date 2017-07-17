using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Analysis;
using ResidualRisk.RiskAssessment.Requirements;

namespace UnitTests.Elements.CompleteElements
{
    public class CaseLowManagedRisk : AbstractCase
    {
        protected override PerimeterType SelectPerimeter()
        {
            return PerimeterType.AdministrationFinanceAndControl;                   // Selected Complete Perimeter 
        }

        protected override PerimetersAnalysis CreatePerimetersAnalysis()
        {
            // Build perimeters analysis object
            PerimetersAnalysis perimeters = new PerimetersAnalysis
            {
                { PerimeterType.InformationTechnology,              800, 700, 0.72},       // Missing Perimeter Analysis
                { PerimeterType.AdministrationFinanceAndControl,    835, 630, 0.84},       // Complete Perimeter Analysis
            };
            return perimeters;
        }

        protected override RPvci CreateRPVci()
        {
            return new RPvci(630, 110);
        }

        protected override RPpds CreateRPpds()
        {
            RequirementsSet set = new RequirementsSet();

            set.AddRequirement(101, 5.0, 0.0, false, new int[3]
            {
                1, 5, 2, 

            });

            set.AddRequirement(102, 5.0, 1.0, false, new int[3]
            {
                1, 5, 1,
            });

            set.AddRequirement(103, 1.0, 0.0, true, new int[3]
            {
                5, 4, 1,
            });

            set.AddRequirement(104, 1.0, 0.0, true, new int[3]
            {
                3, 4, 1,
            });

            set.AddRequirement(105, 1.0, 0.0, true, new int[3]
            {
                2, 2, 5,
            });

            return new RPpds(set);
        }
    }
}
