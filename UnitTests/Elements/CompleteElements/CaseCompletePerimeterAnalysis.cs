using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace UnitTests.Elements.CompleteElements
{
    public class CaseCompletePerimeterAnalysis : AbstractCase
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
            return new RPvci(450, 300);
        }

        protected override RPpds CreateRPpds()
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
