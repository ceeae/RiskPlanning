
using CalcoloRischioResiduo;
using CalcoloRischioResiduo.RiskAssessment;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements
{

    public class ElementUnitTests
    {
        [Theory]
        [InlineData(Scenarios.NotClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(Scenarios.NotClassifiedAbsentElementWithCompletePerimeterAnalysis, 968.6)]
        [InlineData(Scenarios.ClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(Scenarios.ClassifiedAbsentElementWithCompletePerimeterAnalysis, 730.8)]
        [InlineData(Scenarios.IncompleteElementWithMissingPerimeterAnalysis, 750)]
        [InlineData(Scenarios.IncompleteElementWithCompletePerimeterAnalysis, 750)]
        //[InlineData(Scenarios.CompleteElementWithMissingPerimeterAnalysis, 536)]
        //[InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 536)]
        public void ResidualRiskEstimate_DifferentScenarios(Scenarios scenario, double expectedresidualriskvalue)
        {
            IElement element = (IElement) ScenarioElementsBuilder.CreateFromScenario(scenario);

            double result = element.GetResidualRiskEstimate();

            result.Should().Be(expectedresidualriskvalue);
        }
    }
}





