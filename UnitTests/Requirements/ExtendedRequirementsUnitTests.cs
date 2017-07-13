using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace UnitTests.Requirements
{
    public class ExtendedRequirementsUnitTests
    {
        private ExtentedRequirements ereqs = new ExtentedRequirements();

        public ExtendedRequirementsUnitTests()
        {

            ExtendedRequirement req1 = new ExtendedRequirement(1, 2.0, 0);
            COMPLIANCEValues val1 = new COMPLIANCEValues(new int[]
            {
                3, 2, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1
            });
            req1.SetRIDWeights(5, 4, 1);
            req1.SetComplianceWeights(val1);

            ExtendedRequirement req2 = new ExtendedRequirement(2, 3.2, 0.1);
            COMPLIANCEValues val2 = new COMPLIANCEValues(new int[]
            {
                4, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1
            });
            req2.SetRIDWeights(4, 4, 1);
            req2.SetComplianceWeights(val2);

            ExtendedRequirement req3 = new ExtendedRequirement(3, 1.6, 0);
            COMPLIANCEValues val3 = new COMPLIANCEValues(new int[]
            {
                4, 1, 2, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1
            });
            req3.SetRIDWeights(3, 2, 1);
            req3.SetComplianceWeights(val3);

            ExtendedRequirement req4 = new ExtendedRequirement(4, 4.3, 0);
            COMPLIANCEValues val4 = new COMPLIANCEValues(new int[]
            {
                4, 1, 2, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1
            });
            req4.SetRIDWeights(3, 1, 2);
            req4.SetComplianceWeights(val4);

            ExtendedRequirement req5 = new ExtendedRequirement(5, 1.0, 0);
            COMPLIANCEValues val5 = new COMPLIANCEValues(new int[]
            {
                4, 1, 2, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1
            });
            req5.SetRIDWeights(2, 2, 2);
            req5.SetComplianceWeights(val5);

            ereqs.Add(req1);
            ereqs.Add(req2);
            ereqs.Add(req3);
            ereqs.Add(req4);
            ereqs.Add(req5);
        }



        //[Fact]
        //public void CreateNew_WithTwoExtendedRequirementsWithSameId_ThrowsException()
        //{
        //    ExtentedRequirements exreq = new ExtentedRequirements();

        //    exreq.Add(new ExtendedRequirement(1, 0, 0));

        //    Assert.Throws<DuplicatedKeyException>(
        //        () => exreq.Add(new ExtendedRequirement(1, 1, 1)
        //        ));
        //}


        [Fact]
        public void CalculatePotentialRiskBIAFactor_Scenario_ExpectedResult()
        {
            double result = ereqs.CalculatePotentialRiskBIAFactor();

            result.Should().Be(7.4);
        }

        [Fact]
        public void CalculatePotentialRiskBIAIDFactor_Scenario_ExpectedResult()
        {
            double result = ereqs.CalculatePotentialRiskBIAIDFactor();

            result.Should().Be(4.9);
        }

        [Fact]
        public void CalculatePotentialRiskCOMPLIANCEFactor_Scenario_ExpectedResult()
        {
            double result = ereqs.CalculatePotentialRiskCOMPLIANCEFactor();

            result.Should().Be(85.3);
        }

        [Fact]
        public void CalculateBIATotals_Scenario_ExpectedResult()
        {
            BIAValues result = ereqs.GetBIATotal();

            Assert.True(result.Equals(
                    new BIAValues(17, 13, 7)
                ));
        }


        [Fact]
        public void DistributionPotentialRiskBIAByRequrement_GivenScenario_ExpectedDistribution()
        {
            Dictionary<long, double> result = ereqs.GetRequirementsRiskPotentialBIA();

            Dictionary<long, double> exptected = new Dictionary<long, double>();
            exptected.Add(1, 1.5);
            exptected.Add(2, 2.3);
            exptected.Add(3, 0.8);
            exptected.Add(4, 2.3);
            exptected.Add(5, 0.6);

            result.Should().Equal(exptected);
        }

        [Fact]
        public void DistributionPotentialRiskBIAIDByRequrement_GivenScenario_ExpectedDistribution()
        {
            Dictionary<long, double> result = ereqs.GetRequirementsRiskPotentialBIAID();

            Dictionary<long, double> exptected = new Dictionary<long, double>();
            exptected.Add(1, 0.9);
            exptected.Add(2, 1.5);
            exptected.Add(3, 0.5);
            exptected.Add(4, 1.6);
            exptected.Add(5, 0.4);

            result.Should().Equal(exptected);
        }

        [Fact]
        public void DistributionPotentialRiskCOMPLIANCEByRequrement_GivenScenario_ExpectedDistribution()
        {
            Dictionary<long, double> result = ereqs.GetRequirementsRiskPotentialCOMPLIANCE();

            Dictionary<long, double> exptected = new Dictionary<long, double>();
            exptected.Add(1, 14.0);
            exptected.Add(2, 22.8);
            exptected.Add(3, 11.2);
            exptected.Add(4, 30.2);
            exptected.Add(5, 7.0);

            result.Should().Equal(exptected);
        }

        [Fact]
        public void CalculateComplianceTotals_Scenario_ExpectedResult()
        {

            COMPLIANCEValues result = ereqs.GetCOMPLIANCETotal();
            
            Assert.True(result.Equals(
                new COMPLIANCEValues(new int[] {
                    19, 6, 8, 5, 5,
                    5, 5, 5, 5, 5,
                    5, 5, 5, 5, 5,
                    5, 5, 5, 5, 5,
                    5, 5, 5, 5, 5,
                    5, 5, 5, 5, 5,
                    5, 5, 5, 5, 5
                })
                ));

        }
    }
}
