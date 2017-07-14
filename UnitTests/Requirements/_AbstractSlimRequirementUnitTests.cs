using System.Collections.Generic;
using System.ComponentModel;
using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using CalcoloRischioResiduo.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Requirements
{
    public class _AbstractSlimRequirementUnitTests
    {

        [Theory]
        [InlineData(-1, 1, 1)]
        [InlineData(0, 2, 2)]
        public void NewRequirement_InvalidKey_ThrowsAnExceptio(long id, double pas, double alpha)
        {
            FractionWeight PAS = new FractionWeight(pas);
            CorrectionFactor Alpha = new CorrectionFactor(alpha);

            Assert.Throws<InvalidKeyException>(() =>
            {
                Requirement sr = new Requirement(id, PAS, Alpha);
            });
        }

        [Theory]
        [InlineData(100, 1, 3, 0)]
        [InlineData(101, 2, 4, 34)]
        public void NewRequirement_ValidParameter_ExceptedValues(long id, double pas, double alpha, int n)
        {
            FractionWeight PAS = new FractionWeight(pas);
            CorrectionFactor Alpha = new CorrectionFactor(alpha);
            Requirement sr = new Requirement(id, PAS, Alpha);

            sr.PAS.Value.Should().Be(pas);
            sr.Alpha.Value.Should().Be(alpha);
            sr.Id.Should().Be(id);
            sr.Weights[n].Value.Should().Be(1); // check complianceweight n-element as default value
        }

        [Theory]
        [InlineData(4.8, 0.2,   2.7, 1.8, 65.4)]
        [InlineData(3.2, 0,     1.7, 1.2, 41.8)]
        public void CalculatePotentilRiskFactors_Scenario_CheckPotentialRiskBIA(
            double pas, double alpha, 
            double prbia, double prbiaid, double prcompl)
        {
            FractionWeight PAS = new FractionWeight(pas);
            CorrectionFactor Alpha = new CorrectionFactor(alpha);
            Requirement sr = new Requirement(101, PAS, Alpha);

            sr.InitializeWeightsWithIntArray(0, 
                new int[38]
                {
                    3, 1, 2,
                    5,5,5,5,5,5,5,
                    5,5,5,5,5,5,5,
                    5,5,5,5,5,5,5,
                    5,5,5,5,5,5,5,
                    5,5,5,5,5,5,5,
                });

            List<int> totals = new List<int>(
                new int[38]
                {
                    17, 13, 7,
                    13,13,13,13,13,13,13,
                    13,13,13,13,13,13,13,
                    13,13,13,13,13,13,13,
                    13,13,13,13,13,13,13,
                    13,13,13,13,13,13,13,
                });

            sr.CalculaterPotentialRiskFactors(totals);

            sr.PRbia.Should().Be(prbia);
            sr.PRbiaID.Should().Be(prbiaid);
            sr.PRcompl.Should().Be(prcompl);
        }


    }
}
