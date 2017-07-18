using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using ResidualRisk.RiskAssessment.Elements;

namespace UnitTests.Elements.CompleteElements
{

    public class CompleteElementsUnitTests
    {

        [Theory]
        [InlineData(TestCase.MissingPerimeterAnalysis, 342)]
        [InlineData(TestCase.CoveredPerimeterAnalysis, 342)]
        [InlineData(TestCase.LowManagedRisk, 541)]
        public void GetResidualRisk_GivenCase_ExpectedResult(TestCase testCase, int expectedResidualRisk)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);

            double result = element.GetResidualRisk();

            R0(result).Should().Be(expectedResidualRisk);
        }

        [Theory]
        [InlineData(TestCase.LowManagedRisk, 455, 86)]
        public void GetResidualRiskBIAAndCOMPL_GivenCase_ExpectedResult(
            TestCase testCase, int expectedResidualRiskBIA, int expectedResidualRiskCOMPL)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);

            double residualRiskBIA = element.GetResidualRiskBIA();
            double residualRiskCOMPL = element.GetResidualRiskCOMPL();

            R0(residualRiskBIA).Should().Be(expectedResidualRiskBIA);
            R0(residualRiskCOMPL).Should().Be(expectedResidualRiskCOMPL);

        }

        [Theory]
        [InlineData(TestCase.LowManagedRisk, 175, 24)]
        public void GetManagedRiskBIAAndCOMPL_GivenCase_ExpectedResult(
            TestCase testCase, int expectedManagedRiskBIA, int exptectedManagedRiskCOMPL)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);

            double managedRiskBIA = element.GetManagedRiskBIA();
            double managedRiskCOMPL = element.GetManagedRiskCOMPL();

            R0(managedRiskBIA).Should().Be(expectedManagedRiskBIA);
            R0(managedRiskCOMPL).Should().Be(exptectedManagedRiskCOMPL);

        }

        [Theory]
        [InlineData(TestCase.LowManagedRisk, 50000.00)]
        public void GetVEFDistrib_GivenCase_ExpectedResult(TestCase testCase, double vef)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);
            
            element.SetVEF(vef);  // Setting VEF automatically force risk calculation of PDS
            
            Dictionary<long, double[]> distribution = element.GetPotentialRiskDistributionFactors();

            ExtractVEF(distribution).Should().Equal(new Dictionary<long, double>
            {
                { 101, 20270.27 },
                { 102, 18918.92 },
                { 103, 2702.70 },
                { 104, 2702.70 },
                { 105, 5405.41 },
            });
        }

        private static Dictionary<long, double> ExtractVEF(Dictionary<long, double[]> distrib)
        {
            return distrib.ToDictionary(factors => factors.Key, factors => R2(factors.Value[3]));
        }

        private static double R0(double result)
        {
            return Math.Round(result, 0);
        }

        private static double R2(double result)
        {
            return Math.Round(result, 2);
        }

    }
}
