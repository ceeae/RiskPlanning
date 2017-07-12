
using CalcoloRischioResiduo;
using CalcoloRischioResiduo.RiskAssessment;
using Xunit;
using FluentAssertions;

namespace UnitTests.ResidualRiskEstimates
{

    public class ResidualRiskEstimatesUnitTests
    {
        [Theory]
        [InlineData(Scenarios.NotClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(Scenarios.NotClassifiedAbsentElementWithCompletePerimeterAnalysis, 968.6)]
        [InlineData(Scenarios.ClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(Scenarios.ClassifiedAbsentElementWithCompletePerimeterAnalysis, 730.8)]
        [InlineData(Scenarios.IncompleteElementWithMissingPerimeterAnalysis, 750)]
        [InlineData(Scenarios.IncompleteElementWithCompletePerimeterAnalysis, 750)]
        [InlineData(Scenarios.CompleteElementWithMissingPerimeterAnalysis, 536)]
        [InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 536)]
        public void ResidualRiskEstimate_DifferentScenarios(Scenarios scenario, double expectedresidualriskvalue)
        {
            Element element = ElementsBuilder.CreateFromScenario(scenario);

            double result = element.EstimateResidualRisk();

            result.Should().Be(expectedresidualriskvalue);
        }
    }
}





