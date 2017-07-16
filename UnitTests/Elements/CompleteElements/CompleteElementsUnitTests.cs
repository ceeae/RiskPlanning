using CalcoloRischioResiduo.RiskAssessment.Elements;
using Xunit;
using FluentAssertions;

namespace UnitTests.Elements.CompleteElements
{

    public class CompleteElementsUnitTests
    {
        [Theory]
        [InlineData(CompleteScenarioTypes.One, 1250)]
        public void AlwaysTrue(CompleteScenarioTypes scenario, int values)
        {
            true.Should().BeTrue();
        }

    }
}
