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
                { Types.InformationTechnology, 1000, 750, 0.6},
                { Types.AdministrationFinanceAndControl, 930, 625, 0.85},
            };
        }

        [Theory]
        [InlineData(Types.RegulatoryAffairsAndEquivalence,      AnalysisStatus.Missing)]                 // not found in perimeters
        [InlineData(Types.InformationTechnology,                AnalysisStatus.BelowThreshold)]          // below 75% with VCI
        [InlineData(Types.AdministrationFinanceAndControl,      AnalysisStatus.Complete)]
        [InlineData(Types.Technology,                           AnalysisStatus.Missing)]
        public void GetStatus_DifferentFunctionalPerimeters_ExpectedAnalysisStatus(Types perimeter, AnalysisStatus status)
        {

            AnalysisStatus result = perimeters.GetStatus(perimeter);

            result.Should().Be(status);
        }

        [Theory]
        [InlineData(Types.InformationTechnology,            ElementTypes.NotClassified, 1250)]                  // not-classified element with perimeter covered
        [InlineData(Types.AdministrationFinanceAndControl,  ElementTypes.NotClassified, 1069.5)]                // not-classified element with perimeter not covered
        [InlineData(Types.InformationTechnology,            ElementTypes.Classified,    1250)]                  // classified element (absent) with perimeter covered
        [InlineData(Types.AdministrationFinanceAndControl,  ElementTypes.Classified,    718.75)]                // classified element (absent) with perimeter not covered
        public void GetResidualRiskEstimate_DifferentFunctionalPerimeters_ReturnRREstimate(
                                                Types perimeter, ElementTypes classification, double expectedvci
                                                )
        {
            Perimeter analysis = perimeters.FindByType(perimeter);

            double estimatedVCI = analysis.GetResidualRiskEstimate(classification);

            estimatedVCI.Should().Be(expectedvci);
        }


    }
}
