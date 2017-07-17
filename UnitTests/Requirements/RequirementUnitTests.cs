using System;
using System.Collections.Generic;
using System.ComponentModel;
using ResidualRisk.RiskAssessment.Common;
using ResidualRisk.RiskAssessment.Exceptions;
using ResidualRisk.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Requirements
{
    public class RequirementUnitTests
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
                Requirement sr = new Requirement(id, PAS, Alpha, true);

            });
        }

        [Theory]
        [InlineData(100, 1, 3, 0)]
        [InlineData(101, 2, 4, 34)]
        public void NewRequirement_ValidParameter_ExceptedPropertiesAndDefaultWeightIsOne(long id, double pas, double alpha, int n)
        {
            FractionWeight PAS = new FractionWeight(pas);

            CorrectionFactor Alpha = new CorrectionFactor(alpha);

            Requirement req = new Requirement(id, PAS, Alpha, true); // Weights are 1 by default

            req.PAS.Value.Should().Be(pas);
            req.Alpha.Value.Should().Be(alpha);
            req.Id.Should().Be(id);
            req.ReqWeights[n].Value.Should().Be(1); // check complianceweight n-element as default value
        }

        [Theory]
        [InlineData(4.8, 0.2,   2.70, 1.81, 65.38)]
        [InlineData(3.2, 0.0,   1.73, 1.16, 41.85)]
        public void CalculatePotentilRiskFactors_Scenario_CheckFactorsValue(
            double pas, double alpha, 
            double prbia, double prbiaid, double prcompl)
        {

            FractionWeight PAS = new FractionWeight(pas);

            CorrectionFactor Alpha = new CorrectionFactor(alpha);

            Requirement req = new Requirement(101, PAS, Alpha, true, new int[38] 
                {
                    3, 1, 2, 5 ,5 ,5 ,5 ,5 ,5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                });

            List<int> totals = new List<int>(new int[38]
                {
                    17,13,7,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,
                });

            req.CalculatePotentialRiskFactors(totals);

            R2(req.PRbia).Should().Be(prbia);
            R2(req.PRbiaID).Should().Be(prbiaid);
            R2(req.PRcompl).Should().Be(prcompl);
        }

        public static double R2(double result)
        {
            return Math.Round(result, 2);
        }

    }
}
