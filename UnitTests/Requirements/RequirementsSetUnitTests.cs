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

            set.AddRequirement(101, 4.8, 0.2, true, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,

            });

            set.AddRequirement(102, 3.2, 0.0, false, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,
            });

            set.AddRequirement(103, 1.0, 0.0, false, new int[3]
            {
                5, 4, 1
            });

            set.AddRequirement(104, 1.0, 0.0, false, new int[3]
            {
                3, 4, 1
            });

            set.AddRequirement(105, 1.0, 0.0, true, new int[3]
            {
                3, 3, 1
            });

        }

        [Fact]
        public void CreateNewRequirementsSet_ValidRequirements_ExpectedTotals()
        {

            List<int> totals = set.CalculateWeightsTotals();

            totals.Should().Equals(new int[38]
            {
                17, 13, 7, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
            });

        }

        [Fact]
        public void GetPotentialRiskDistributionFactors_GivenValidScenario_ExpectedDistribution()
        {

            set.VEF = 2000.0;

            Dictionary<long, double[]> PRdistrib = set.GetPotentialRiskDistributionFactors(); // Id, RPbia, RPbiaID, RPcompl, VEFreq

            PRdistrib.Should().Equals(new Dictionary<long, double[]>
            {
                { 101, new double[] {2.7, 1.8, 65.4, 853.60 }},
                { 102, new double[] {1.7, 1.2, 41.8, 546.30 }},
                { 103, new double[] {0.7, 0.5, 2.6, 212.11 }},
                { 104, new double[] {0.6, 0.5, 2.6, 212.11 }},
                { 105, new double[] {0.6, 0.4, 2.6, 175.89 }},

            });

            set.PRbiaTot.Should().Be(6.35);
            set.PRbiaIDTot.Should().Be(4.24);
            set.PRcomplTot.Should().Be(115.09);
        }

        [Fact]
        public void GetManagedRiskBIAAndCOMPLFactors_GiveScenario_ExpectedValues()
        {
            double MRbia = set.GetManagedRiskBIAFactor();
            double MRcompl = set.GetManagedRiskCOMPLFactor();


            MRbia.Should().Be(3.25);
            MRcompl.Should().Be(68.0);
        }

        [Fact]
        public void GetResidualRiskBIAAndCOMPLFactors_GiveScenario_ExpectedValues()
        {
            double RRbia = set.GetResidualRiskBIAFactor();
            double RRcompl = set.GetResidualRiskCOMPLFactor();


            RRbia.Should().Be(3.10);
            RRcompl.Should().Be(47.09);
        }



        // Managed Risk (single value)

        // Residual Risk (BIA * COMPL values)

    }
}