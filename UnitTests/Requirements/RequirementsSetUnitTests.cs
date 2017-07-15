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

            set.AddRequirement(101, 4.8, 2.0, new int[38]
                {
                    3, 1, 2, 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,

                });

            set.AddRequirement(102, 3.2, 0.0, new int[38]
                {
                    3, 1, 2, 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,
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

            totals.Should().Equal( new int[38]
            {
                17, 13, 7, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
            });

        }

        [Fact]
        public void GetPotentialRiskDistribution_GivenValidScenario_ExpectedDistribution()
        {
            Dictionary<long, double> PRdistribBIA = set.GetPotentialRiskDistributionBIA();

            PRdistribBIA.Should().Equal( new Dictionary<long, double>
            {
                { 101, 0.5 },
                { 102, 0.5 },
                { 103, 0.5 },
                { 104, 0.5 },
                { 105, 0.5 },

            });

        }

        // Add other distributions: COMPL and VEF BIAID

        // Managed Risk (single value)

        // Residual Risk (BIA * COMPL values)


    }
}
