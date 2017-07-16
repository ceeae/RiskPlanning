using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.FunctionalPerimeters
{
    public class PerimetersAnalysisUnitTests
    {

        private PerimetersAnalysis perimeters;

        public PerimetersAnalysisUnitTests()
        {
            // Build a perimeters analysis object (missing types are considered not analyzed perimeters)
            perimeters = new PerimetersAnalysis
            {
                { PerimeterType.InformationTechnology, 1000, 750, 0.6},
                { PerimeterType.AdministrationFinanceAndControl, 930, 625, 0.85},
            };
        }

        [Theory]
        [InlineData(PerimeterType.RegulatoryAffairsAndEquivalence,      AnalysisStatus.Missing)]                 // not found in perimeters
        [InlineData(PerimeterType.InformationTechnology,                AnalysisStatus.BelowThreshold)]          // below 75% with VCI
        [InlineData(PerimeterType.AdministrationFinanceAndControl,      AnalysisStatus.Complete)]
        [InlineData(PerimeterType.Technology,                           AnalysisStatus.Missing)]
        public void GetStatus_DifferentFunctionalPerimeters_ExpectedAnalysisStatus(PerimeterType perimeter, AnalysisStatus status)
        {

            AnalysisStatus result = perimeters.GetStatus(perimeter);

            result.Should().Be(status);
        }

        [Theory]
        [InlineData(PerimeterType.InformationTechnology,            ElementTypes.NotClassified, 1250)]                  // not-classified element with perimeter covered
        [InlineData(PerimeterType.AdministrationFinanceAndControl,  ElementTypes.NotClassified, 1069.5)]                // not-classified element with perimeter not covered
        [InlineData(PerimeterType.InformationTechnology,            ElementTypes.Classified,    1250)]                  // classified element (absent) with perimeter covered
        [InlineData(PerimeterType.AdministrationFinanceAndControl,  ElementTypes.Classified,    718.75)]                // classified element (absent) with perimeter not covered
        public void GetResidualRiskEstimate_DifferentFunctionalPerimeters_ReturnRREstimate(
                                                PerimeterType perimeter, ElementTypes classification, double expectedvci
                                                )
        {
            Perimeter analysis = perimeters.FindByType(perimeter);

            double estimatedVCI = analysis.GetResidualRiskEstimate(classification);

            estimatedVCI.Should().Be(expectedvci);
        }


    }
}
