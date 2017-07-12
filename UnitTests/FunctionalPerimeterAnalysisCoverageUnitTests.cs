using CalcoloRischioResiduo;
using Xunit;
using FluentAssertions;

namespace UnitTests
{
    public class FunctionalPerimeterAnalysisCoverageUnitTests
    {

        // UnitOfWork_Scenario_ExpectedResult
        // e.g. class LogAnalyzerTests 
        //      [Fact] IsValidFilename_BadExtension_ReturnFalse

        private FunctionalPerimeterAnalysisCoverage coverage;

        public FunctionalPerimeterAnalysisCoverageUnitTests()
        {
            // Build a coverage
            coverage = new FunctionalPerimeterAnalysisCoverage
            {
                {FunctionalPerimeters.InformationTechnology, 1000, 750, 0.6},
                {FunctionalPerimeters.AdministrationFinanceAndControl, 930, 625, 0.85},
                {FunctionalPerimeters.BrandStrategyAndMedia, 821, 587, 0.78}
            };

        }

        [Theory]
        [InlineData(FunctionalPerimeters.RegulatoryAffairsAndEquivalence, FunctionalPerimeterCoverageStatus.No)]
        [InlineData(FunctionalPerimeters.InformationTechnology, FunctionalPerimeterCoverageStatus.No)]          // below 75% with VCI
        [InlineData(FunctionalPerimeters.AdministrationFinanceAndControl, FunctionalPerimeterCoverageStatus.Yes)]
        public void CoverageStatus_DifferentFunctionalPerimeters_ReturnStatusAccordingly(FunctionalPerimeters perimeter, bool status)
        {

            bool result = coverage.IsCovered(perimeter);

            result.Should().Be(status);
        }

        [Theory]
        [InlineData(FunctionalPerimeters.AdministrationFinanceAndControl, true, 1069.5)]      // simulate classified element
        [InlineData(FunctionalPerimeters.AdministrationFinanceAndControl, false, 718.75)]     // simulate not-classified element
        [InlineData(FunctionalPerimeters.BrandStrategyAndMedia, true, 1001.62)]
        [InlineData(FunctionalPerimeters.BrandStrategyAndMedia, false, 716.14)]
        [InlineData(FunctionalPerimeters.InformationTechnology, true, 1250)]
        public void EstimateVCI_DifferentFunctionalPerimeters_ReturnValueAccordingly(
            FunctionalPerimeters perimeter, bool isClassified, double expectedvci
            )
        {
            FunctionalPerimeterAnalysis analysis = coverage.FindAnalysis(perimeter);

            double estimatedVCI = analysis.EstimateResidualRisk(isClassified);

            estimatedVCI.Should().Be(expectedvci);
        }


    }
}
