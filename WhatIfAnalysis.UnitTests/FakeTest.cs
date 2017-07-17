using Xunit;
using FluentAssertions;

namespace WhatIfAnalysis.UnitTests
{
    public class FakeTest
    {

        // UnitOfWork_Scenario_ExpectedResult
        // e.g. class LogAnalyzerTests 
        //      [Fact] IsValidFilename_BadExtension_ReturnFalse

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