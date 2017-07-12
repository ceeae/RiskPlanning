using Xunit;
using FluentAssertions;
using CalcoloRischioResiduo;


namespace UnitTests
{
    public class ElementoUnitTests
    {
        private double AvgVCIC3 = 1000;
        private double AvgVCIAll = 750;
        private double withVCI = 75/100;


        private Element _element = null;

        public ElementoUnitTests()
        {
            _element = new Element();

            // Create perimeter analysis
            FunctionalPerimetersAnalysis analysis = new FunctionalPerimetersAnalysis(FunctionalPerimeters.InformationTechnology, AvgVCIC3, AvgVCIAll, withVCI);
//            FunctionalPerimetersAnalysisCoverage.Add(analysis);
        }

        [Fact]
        public void NewElement_IsClassified()
        {
            bool isClassified = _element.isClassified();

            isClassified.Should().BeTrue();
        }


        [Fact]
        public void SetElementFunctionalPerimeter_InformationTechnology_ExpectedInformationTechnology()
        {
            _element.functionalperimeter = FunctionalPerimeters.InformationTechnology;

            FunctionalPerimeters result = _element.functionalperimeter;

            result.Should().Be(FunctionalPerimeters.InformationTechnology);
        }

        [Theory]
        [InlineData(true, 1250)]
        [InlineData(false, 1250)]

        public void ResidualRisk_NotClassifiedElementAsPartOfFunctionalPerimeterScenarios_ReturnExpectedVCIValue(
            bool isAnayzedFunctionalPerimeter, double vcivalue)
        {
            _element.functionalperimeter = FunctionalPerimeters.InformationTechnology;

            double estimatedRR = _element.CalculateResidualRisk();

            estimatedRR.Should().Be(vcivalue);
        }




    }
}
