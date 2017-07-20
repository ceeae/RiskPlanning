using System;
using System.Collections.Generic;
using System.Linq;
using ResidualRisk.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Requirements
{
    public class RequirementsSetUnitTests
    {
        private readonly RequirementsSet _requirements;

        public RequirementsSetUnitTests()
        {
            _requirements = new RequirementsSet();

            _requirements.AddRequirement(101, 4.8, 0.2, true, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,

            });

            _requirements.AddRequirement(102, 3.2, 0.0, false, new int[38]
            {
                3, 1, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                5, 5, 5,
            });

            _requirements.AddRequirement(103, 1.0, 0.0, false, new int[3]
            {
                5, 4, 1
            });

            _requirements.AddRequirement(104, 1.0, 0.0, false, new int[3]
            {
                3, 4, 1
            });

            _requirements.AddRequirement(105, 1.0, 0.0, true, new int[3]
            {
                3, 3, 1
            });

        }

        [Fact]
        public void CreateNewRequirementsSet_ValidRequirements_ExpectedTotals()
        {

            _requirements.CalculateRisk();

            List<int> totals = _requirements.Totals;

            totals.Should().Equal(new int[38]
            {
                17, 13, 7, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
            });

        }

        [Fact]
        public void GetPotentialRiskDistributionFactors_GivenValidScenario_ExpectedDistribution()
        {

            _requirements.VEF = 2000.0;

            _requirements.CalculateRisk();

            Dictionary<long, double[]> distribution = _requirements.Distribution; // Id, RPbia, RPbiaID, RPcompl, VEFreq

            ExtractVEF(distribution).Should().Equal(new Dictionary<long, double>
            {
                { 101, 853.60 },
                { 102, 546.30 },
                { 103, 212.11 },
                { 104, 212.11 },
                { 105, 175.89 },
            });

            R2(_requirements.TotalPotentialRiskBIA).Should().Be(6.34);
            R2(_requirements.TotalPotentialRiskBIAID).Should().Be(4.25);
            R2(_requirements.TotalPotentialRiskCOMPL).Should().Be(118.46);
        }

        private static Dictionary<long, double> ExtractVEF(Dictionary<long, double[]> distrib)
        {
            return distrib.ToDictionary(factors => factors.Key, factors => R2(factors.Value[3]));
        }


        [Fact]
        public void GetManagedRiskBIAAndCOMPLFactors_GiveScenario_ExpectedValues()
        {
            _requirements.CalculateRisk();

            double MRbia = _requirements.ManagedRiskBIA;
            double MRcompl = _requirements.ManagedRiskCOMPL;

            R2(MRbia).Should().Be(3.25);
            R2(MRcompl).Should().Be(70.0);
        }

        [Fact]
        public void GetResidualRiskBIAAndCOMPLFactors_GiveScenario_ExpectedValues()
        {
            _requirements.CalculateRisk();

            double RRbia = _requirements.ResidualRiskBIA;
            double RRcompl = _requirements.ResidualRiskCOMPL;

            R2(RRbia).Should().Be(3.10);
            R2(RRcompl).Should().Be(48.46);
        }


        private static double R2(double result)
        {
            return Math.Round(result, 2);
        }

    }
}