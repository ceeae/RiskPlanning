using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using CalcoloRischioResiduo.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Common
{
    public class WeightUnitTests
    {

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public void NewWeight_OutOfRangeValues_ThrowsAnException(int weight)
        {

            Assert.Throws<InvalidWeightValueException>(() => {
                Weight w = new Weight(weight);
            });

        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void NewWeight_CorrectValue_ExpctedWeighValue(int weightvalue)
        {
            Weight result = new Weight(weightvalue);

            result.Value.Should().Be(weightvalue);
        }
    }
}
