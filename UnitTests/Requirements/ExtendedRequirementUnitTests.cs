using CalcoloRischioResiduo.RiskAssessment.Requirements;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using Xunit;
using FluentAssertions;

namespace UnitTests.Requirements
{
    public class ExtendedRequirementUnitTests
    {

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0.9, 0)]
        public void NewExtendedRequirement_WithInvalidProbabilityParameters_ThrowsException(double PAS, double Alpha)
        {
            Assert.Throws<InvalidProbabilityValueException>(() =>
            {
                ExtendedRequirement req = new ExtendedRequirement(1, PAS, Alpha);
            });
        }

        [Theory]
        [InlineData(1, -0.1)]
        [InlineData(1, 1.1)]
        public void NewExtendedRequirement_WithInvalidCorrectionParameter_ThrowsException(double PAS, double Alpha)
        {
            Assert.Throws<InvalidCorrectionValueException>(() =>
            {
                ExtendedRequirement req = new ExtendedRequirement(1, PAS, Alpha);
            });

        }

        [Theory]
        [InlineData(101, 2.0, 0)]
        [InlineData(102, 3.4, 0.2)]
        public void NewExtendedRequirement_WithSlimRequirement_SlimDataAreCorrect(long id, double PAS, double Alpha)
        {
            ExtendedRequirement req = new ExtendedRequirement(id, PAS, Alpha);

            req.LibraryId().Should().Be(id);
            req.GetPAS().Should().Be(PAS);
            req.GetAlpha().Should().Be(Alpha);

            req.GetComplianceWeight(1).Should().Be(1);      // by default all weights are 1
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(36, 1)]
        public void SetComplianceWeight_WithOutOfRangeIndex_ThrowsException(int index, int weight)
        {
            ExtendedRequirement req = new ExtendedRequirement(100, 3.4, 0);

            Assert.Throws<OutOfRangeIndexException>(() => req.SetComplianceWeight(index, weight));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 6)]
        [InlineData(3, -1)]
        public void SetComplianceWeight_WithOutOfRangeWeight_ThrowsException(int index, int weight)
        {
            ExtendedRequirement req = new ExtendedRequirement(100, 4.0, 0);

            Assert.Throws<WrongWeightValueException>(() => req.SetComplianceWeight(index, weight));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 4)]
        public void SetComplianceWeight_InRangeValues_SettingIsFine(int index, int weight)
        {

            ExtendedRequirement req = new ExtendedRequirement(100, 1.5, 0);
            req.SetComplianceWeight(index, weight);

            req.GetComplianceWeight(index).Should().Be(weight);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(36)]
        [InlineData(1000)]
        public void GetComplianceWeight_WithOutOfRangeIndex_ThrowsException(int index)
        {
            ExtendedRequirement req = new ExtendedRequirement(100, 1, 0);

            Assert.Throws<OutOfRangeIndexException>(() => req.GetComplianceWeight(index));
        }

    }
}
