using Xunit;
using FluentAssertions;
using CalcoloRischioResiduo;


namespace UnitTests
{
    public class ElementUnitTests
    {

        private Element element;

        private FunctionalPerimeterAnalysisCoverage coverage;

        public ElementUnitTests()
        {
            element = new Element();

            // Build a coverage
            coverage = new FunctionalPerimeterAnalysisCoverage();
            coverage.Add( FunctionalPerimeters.InformationTechnology, 811, 350, 0.9);
            coverage.Add( FunctionalPerimeters.AdministrationFinanceAndControl, 923, 615, 0.87);
            coverage.Add( FunctionalPerimeters.BrandStrategyAndMedia, 821, 587, 0.35);

        }

        [Fact]
        public void NewElement_ByDefaultIsAbsent()
        {
            bool isAbsent = element.isClassified();

            isAbsent.Should().BeTrue();
        }


        [Fact]
        public void ElementFunctionalPerimeter_SetupInformationTechnology_ExpectedInformationTechnology()
        {
            element.functionalperimeter = FunctionalPerimeters.InformationTechnology;

            FunctionalPerimeters result = element.functionalperimeter;

            result.Should().Be(FunctionalPerimeters.InformationTechnology);
        }

        //[Theory]
        //[InlineData(FunctionalPerimeters.HumanResourcesAndOrganizationalDevelopment, 1250)] // perimeter not covered
        //[InlineData(FunctionalPerimeters.InformationTechnology, 1250)]  // is covered
        //public void ResidualRisk_NotClassifiedElementAsPartOfFunctionalPerimeterScenarios_ReturnExpectedVCIValue(FunctionalPerimeters perimeter, double vcivalue)
        //{

        //    // Element is absent by default
        //    element.functionalperimeter = perimeter;



        //    double estimatedRR = element.CalculateResidualRisk();

        //    estimatedRR.Should().Be(vcivalue);
        //}




    }
}
