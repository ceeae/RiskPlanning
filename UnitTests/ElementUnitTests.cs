using Xunit;
using FluentAssertions;
using CalcoloRischioResiduo;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment;


namespace UnitTests
{
    public class ElementUnitTests
    {

        private Element element;

        public ElementUnitTests()
        {
            element = new Element();
        }

        [Fact]
        public void NewElement_ByDefaultIsNotClassified()
        {
            bool isClassified = element.IsClassified();

            isClassified.Should().BeFalse();
        }

        [Fact]
        public void NewElement_CreatedAsClassified_ExpectedClassified()
        {
            element = new Element(true);
            bool isClassified = element.IsClassified();

            isClassified.Should().BeTrue();
        }

        [Fact]
        public void SetElementPerimeter_AsInformationTechnology_ExpectedInformationTechnology()
        {
            element.Perimeter = Types.InformationTechnology;

            Types result = element.Perimeter;

            result.Should().Be(Types.InformationTechnology);
        }


        [Fact]
        public void NewElement_WithVCIOnly_ExpectedNotHasPdSAndIncomplete()
        {
            SlimVCI vci = new SlimVCI(800, 400);
            element = new Element(true, vci);

            element.HasPDS().Should().BeFalse();
            element.IsIncomplete().Should().BeTrue();
        }


    }
}
