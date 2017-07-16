
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
        [InlineData(Scenarios.CompleteElementWithMissingPerimeterAnalysis, 343)]
        [InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 343)]
        public void ResidualRisk_DifferentElements(Scenarios scenario, double expectedresidualriskvalue)
        {
            IElement element = (IElement) ScenarioElementsBuilder.CreateFromScenario(scenario);

            double result = element.GetResidualRisk();

            result.Should().Be(expectedresidualriskvalue);
        }

        [Theory]
        [InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 230)]
        public void ManagedRiskBIA_CompleteElementGivenScenario(Scenarios scenario, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement) ScenarioElementsBuilder.CreateFromScenario(scenario);

            double result = element.GetManagedRiskBIA();

            result.Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 177)]
        public void ManagedRiskCOMPL_CompleteElementGivenScenario(Scenarios scenario, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenario);

            double result = element.GetManagedRiskCOMPL();

            result.Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 220)]
        public void ResidualRiskBIA_CompleteElementGivenScenario(Scenarios scenario, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenario);

            double result = element.GetResidualRiskBIA();

            result.Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(Scenarios.CompleteElementWithCompletePerimeterAnalysis, 123)]
        public void ResidualRiskCOMPL_CompleteElementGivenScenario(Scenarios scenario, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenario);

            double result = element.GetResidualRiskCOMPL();

            result.Should().Be(expectedmanagedriskvalue);
        }




    }
}





