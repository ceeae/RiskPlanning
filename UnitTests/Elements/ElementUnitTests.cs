using System;
using ResidualRisk.RiskAssessment.Elements;
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

        private static double R0(double result)
        {
            return Math.Round(result, 0);
        }
    }
}





