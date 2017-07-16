using System;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements
{

    public class ElementUnitTests
    {
        [Theory]
        [InlineData(TestCase.NotClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(TestCase.NotClassifiedAbsentElementWithCompletePerimeterAnalysis, 969)]
        [InlineData(TestCase.ClassifiedAbsentElementWithMissingPerimeterAnalysis, 1250)]
        [InlineData(TestCase.ClassifiedAbsentElementWithCompletePerimeterAnalysis, 731)]
        [InlineData(TestCase.IncompleteElementWithMissingPerimeterAnalysis, 750)]
        [InlineData(TestCase.IncompleteElementWithCompletePerimeterAnalysis, 750)]
        public void ResidualRisk_DifferentElements(TestCase testCase, double expectedresidualriskvalue)
        {
            IElement element = (IElement) ElementBuilder.CreateCase(testCase);

            double result = element.GetResidualRisk();

            R0(result).Should().Be(expectedresidualriskvalue);
        }

        [Theory]
        [InlineData(TestCase.CompleteElementWithCompletePerimeterAnalysis, 230)]
        public void ManagedRiskBIA_CompleteElementGivenScenario(TestCase testCase, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement) ElementBuilder.CreateCase(testCase);

            double result = element.GetManagedRiskBIA();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(TestCase.CompleteElementWithCompletePerimeterAnalysis, 177)]
        public void ManagedRiskCOMPL_CompleteElementGivenScenario(TestCase testCase, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ElementBuilder.CreateCase(testCase);

            double result = element.GetManagedRiskCOMPL();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(TestCase.CompleteElementWithCompletePerimeterAnalysis, 220)]
        public void ResidualRiskBIA_CompleteElementGivenScenario(TestCase testCase, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ElementBuilder.CreateCase(testCase);

            double result = element.GetResidualRiskBIA();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        [Theory]
        [InlineData(TestCase.CompleteElementWithCompletePerimeterAnalysis, 123)]
        public void ResidualRiskCOMPL_CompleteElementGivenScenario(TestCase testCase, double expectedmanagedriskvalue)
        {
            CompleteElement element = (CompleteElement)ElementBuilder.CreateCase(testCase);

            double result = element.GetResidualRiskCOMPL();

            R0(result).Should().Be(expectedmanagedriskvalue);
        }

        public static double R0(double result)
        {
            return Math.Round(result, 0);
        }
    }
}





