using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements.CompleteElements
{

    public class CompleteElementsUnitTests
    {

        [Theory]
        [InlineData(TestCase.MissingPerimeterAnalysis, 342)]
        [InlineData(TestCase.CoveredPerimeterAnalysis, 342)]
        [InlineData(TestCase.LowManagedRisk, 541)]
        public void GetResidualRisk_GivenCase_ExpectedResult(TestCase testCase, int residualrisk)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);

            double result = element.GetResidualRisk();

            R0(result).Should().Be(residualrisk);
        }

        [Theory]
        [InlineData(TestCase.LowManagedRisk, 455, 86)]
        public void GetResidualRiskBIAAndCOMPL_GivenCase_ExpectedResult(TestCase testCase, int rrbia, int rrcompl)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);

            double rrbiaresult = element.GetResidualRiskBIA();
            double rrcomplresult = element.GetResidualRiskCOMPL();

            R0(rrbiaresult).Should().Be(rrbia);
            R0(rrcomplresult).Should().Be(rrcompl);

        }

        [Theory]
        [InlineData(TestCase.LowManagedRisk, 175, 24)]
        public void GetManagedRiskBIAAndCOMPL_GivenCase_ExpectedResult(TestCase testCase, int rrbia, int rrcompl)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);

            double rrbiaresult = element.GetManagedRiskBIA();
            double rrcomplresult = element.GetManagedRiskCOMPL();

            R0(rrbiaresult).Should().Be(rrbia);
            R0(rrcomplresult).Should().Be(rrcompl);

        }

        [Theory]
        [InlineData(TestCase.LowManagedRisk, 50000.00)]
        public void GetVEFDistrib_GivenCase_ExpectedResult(TestCase testCase, double vef)
        {
            CompleteElement element = CompleteElementBuilder.CreateCase(testCase);
            
            element.SetVEF(vef);
            
            Dictionary<long, double[]> factorsDistrib = element.GetPotentialRiskDistributionFactors();

            VEFDistrib(factorsDistrib).Should().Equals(new Dictionary<long, double>
            {
                { 101, 20270.27 },
                { 102, 18918.92 },
                { 103, 2702.70 },
                { 104, 2702.70 },
                { 105, 5405.41 },
            });
        }

        private static double R0(double result)
        {
            return Math.Round(result, 0);
        }
        private static double R2(double result)
        {
            return Math.Round(result, 0);
        }

        private Dictionary<long, double> VEFDistrib(Dictionary<long, double[]> distrib)
        {
            return distrib.ToDictionary(factors => factors.Key, factors => R2(factors.Value[3]));
        }

    }
}
