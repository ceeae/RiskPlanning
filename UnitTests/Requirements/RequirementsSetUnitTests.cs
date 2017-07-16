using System.Collections.Generic;
using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Requirements
{
    public class RequirementsSetUnitTests
    {
        private RequirementsSet set;

        public RequirementsSetUnitTests()
        {
            set = new RequirementsSet();

            set.AddRequirement(101, 4.8, 0.2, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,

            });

            set.AddRequirement(102, 3.2, 0.0, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,
            });

            set.AddRequirement(103, 1.0, 0.0, new int[3]
            {
                5, 4, 1
            });

            set.AddRequirement(104, 1.0, 0.0, new int[3]
            {
                3, 4, 1
            });

            set.AddRequirement(105, 1.0, 0.0, new int[3]
            {
                3, 3, 1
            });

        }

        [Fact]
        public void CreateNewRequirementsSet_ValidRequirements_ExpectedTotals()
        {

            List<int> totals = set.CalculateTotals();

            totals.Should().Equals(new int[38]
            {
                17, 13, 7, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
            });

        }

        [Fact]
        public void GetPotentialRiskDistributionBIA_GivenValidScenario_ExpectedDistribution()
        {

            Dictionary<long, double[]> PRdistrib = set.GetPotentialRiskDistributionFactors();

            PRdistrib.Should().Equals(new Dictionary<long, double[]>
            {
                {101, new double[] {2.7, 1.8, 65.4}},
                {102, new double[] {1.7, 1.2, 41.8}},
                {103, new double[] {0.7, 0.5, 2.6}},
                {104, new double[] {0.6, 0.5, 2.6}},
                {105, new double[] {0.6, 0.4, 2.6}},

            });

            set.PRbiaTot.Should().Be(6.35);
            set.PRbiaIDTot.Should().Be(4.24);
            set.PRcomplTot.Should().Be(115.09);
        }

        [Fact]
        public void GetVEFDistribution_GivenValidScenario_ExpectedDistribution()
        {
            set.VEF = 2000.0;

            Dictionary<long, double> VEFdistrib = set.GetVEFDistribution();

            VEFdistrib.Should().Equals(new Dictionary<long, double>
            {
                {101, 853.60},
                {102, 546.30},
                {103, 212.11},
                {104, 212.11},
                {105, 175.89},
            });
        }
        // Managed Risk (single value)

        // Residual Risk (BIA * COMPL values)

    }
}