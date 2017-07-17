using Xunit;
using FluentAssertions;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.UnitTests.Elements
{
    public class ElementUnitTests
    {

        [Theory]
        [InlineData(700, 0, ElementType.Incomplete, ElementVCIClass.C3)]
        [InlineData(0, 0, ElementType.Absent, ElementVCIClass.C1)]
        [InlineData(0, 400, ElementType.Absent, ElementVCIClass.C1)]
        [InlineData(800, 400, ElementType.Complete, ElementVCIClass.C3)]
        [InlineData(350, 100, ElementType.Complete, ElementVCIClass.C2)]
        public void NewElement_GivenParamters_ExcptedElementType(
            int prvalue, int mrvalue, ElementType expectedtype, ElementVCIClass vciclass)
        {

            Element element = new Element(101, prvalue, mrvalue);

            element.GetType().Should().Be(expectedtype);
            element.VCIClass.Should().Be(vciclass);

        }
    }
}
