using System;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements
{

    public class ElementUnitTests
    {
        public static double R0(double result)
        {
            return Math.Round(result, 0);
        }

        [Theory]
        [InlineData(ScenariosType.NotClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(ScenariosType.NotClassifiedAbsentElementWithCompletePerimeterAnalysis, 969)]
        [InlineData(ScenariosType.ClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(ScenariosType.ClassifiedAbsentElementWithCompletePerimeterAnalysis, 731)]
        [InlineData(ScenariosType.IncompleteElementWithMissingPerimeterAnalysis, 750)]
        [InlineData(ScenariosType.IncompleteElementWithCompletePerimeterAnalysis, 750)]
        [InlineData(ScenariosType.CompleteElementWithMissingPerimeterAnalysis, 342)]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 342)]
        public void ResidualRisk_DifferentElements(ScenariosType scenarioType, double expectedresidualriskvalue)
        {
            IElement element = (IElement) ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetResidualRisk();

            R0(result).Should().Be(expectedresidualriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 230)]
        public void ManagedRiskBIA_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement) ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetManagedRiskBIA();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 177)]
        public void ManagedRiskCOMPL_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetManagedRiskCOMPL();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 220)]
        public void ResidualRiskBIA_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetResidualRiskBIA();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(ScenariosType.CompleteElementWithCompletePerimeterAnalysis, 123)]
        public void ResidualRiskCOMPL_CompleteElementGivenScenario(ScenariosType scenarioType, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ScenarioElementsBuilder.CreateFromScenario(scenarioType);

            double result = element.GetResidualRiskCOMPL();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

    }
}





