using CalcoloRischioResiduo.FunctionalPerimeters;
using Xunit;
using FluentAssertions;

namespace UnitTests.FunctionalPerimeters
{
    public class PerimetersAnalysisUnitTests
    {

        // UnitOfWork_Scenario_ExpectedResult
        // e.g. class LogAnalyzerTests 
        //      [Fact] IsValidFilename_BadExtension_ReturnFalse

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
        [InlineData(Types.RegulatoryAffairsAndEquivalence, AnalysisStatus.Missing)]                 // not found in perimeters
        [InlineData(Types.InformationTechnology, AnalysisStatus.BelowThreshold)]                    // below 75% with VCI
        [InlineData(Types.AdministrationFinanceAndControl, AnalysisStatus.Complete)]
        [InlineData(Types.Technology, AnalysisStatus.Missing)]
        public void CoverageStatus_DifferentFunctionalPerimeters_ReturnStatusAccordingly(Types perimeter, AnalysisStatus status)
        {

            AnalysisStatus result = perimeters.GetStatus(perimeter);

            result.Should().Be(status);
        }

        [Theory]
        [InlineData(Types.InformationTechnology, false, 1250)]                  // not-classified element with perimeter covered
        [InlineData(Types.AdministrationFinanceAndControl, false, 1069.5)]      // not-classified element with perimeter not covered
        [InlineData(Types.InformationTechnology, true, 1250)]                   // classified element (absent) with perimeter covered
        [InlineData(Types.AdministrationFinanceAndControl, true, 718.75)]       // classified element (absent) with perimeter not covered
        public void EstimateVCI_DifferentFunctionalPerimeters_ReturnValueAccordingly(
            Types perimeter, bool isClassified, double expectedvci
            )
        {
            Perimeter analysis = perimeters.FindByType(perimeter);

            double estimatedVCI = analysis.GetResidualRiskEstimate(isClassified);

            estimatedVCI.Should().Be(expectedvci);
        }


    }
}
