using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements
{

    public class ElementUnitTests
    {
        [Theory]
        [InlineData(ScenariosType.NotClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(ScenariosType.NotClassifiedAbsentElementWithCompletePerimeterAnalysis, 968.6)]
        [InlineData(ScenariosType.ClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(ScenariosType.ClassifiedAbsentElementWithCompletePerimeterAnalysis, 730.8)]
        [InlineData(ScenariosType.IncompleteElementWithMissingPerimeterAnalysis, 750)]
        [InlineData(ScenariosType.IncompleteElementWithCompletePerimeterAnalysis, 750)]
        [InlineData(ScenariosType.CompleteElementWithMissingPerimeterAnalysis, 343)]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 343)]
        public void ResidualRisk_DifferentElements(ScenariosType scenarioType, double expectedresidualriskvalue)
        {
            IElement element = (IElement) ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetResidualRisk();

            result.Should().Be(expectedresidualriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 230)]
        public void ManagedRiskBIA_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement) ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetManagedRiskBIA();

            result.Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 177)]
        public void ManagedRiskCOMPL_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetManagedRiskCOMPL();

            result.Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 220)]
        public void ResidualRiskBIA_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetResidualRiskBIA();

            result.Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 123)]
        public void ResidualRiskCOMPL_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetResidualRiskCOMPL();

            result.Should().Be(expectedmanagedriskvalue);
        }

    }
}





