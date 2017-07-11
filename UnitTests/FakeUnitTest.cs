using Xunit;
using FluentAssertions;

namespace UnitTests
{
    public class FakeTest
    {

        [Fact]
        public void AlwaysReturnTrue()
        {
            bool result = true;

            result.Should().BeTrue();

        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void ForgetScenarioAlwaysReturnTrue(int param)
        {
            bool result = true; // just a comment

            result.Should().BeTrue();

        }


    }
}
