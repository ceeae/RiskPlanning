using Xunit;
using FluentAssertions;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.UnitTests.Elements
{
    public class ElementUnitTests
    {

        [Theory]
        [InlineData(700, 0,     ElementType.Incomplete, VCIClass.C3)]
        [InlineData(0, 0,       ElementType.Absent,     VCIClass.C1)]
        [InlineData(0, 400,     ElementType.Absent,     VCIClass.C1)]
        [InlineData(800, 400,   ElementType.Complete,   VCIClass.C3)]
        [InlineData(350, 100,   ElementType.Complete,   VCIClass.C2)]
        public void NewElement_GivenParamters_ExcptedElementType(
            int potentialRiskvalue, int managedRiskvalue, ElementType expectedType, VCIClass expectedVCIclass)
        {

            Element element = new Element(101, potentialRiskvalue, managedRiskvalue);

            ElementType elementType = element.GetElementType();
            VCIClass vciClass = element.VciClass;

            elementType.Should().Be(expectedType);
            vciClass.Should().Be(expectedVCIclass);
        }
    }
}
