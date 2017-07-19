using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using Xunit;
using FluentAssertions;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.UnitTests.Elements
{
    public class ElementsSetUnitTests
    {
        private readonly ElementsSet _elements;

        public ElementsSetUnitTests()
        {
            _elements = new ElementsSet
            {
                // Id, PotentialRisk (vci), ManagedRisk (pds)
                new Element(1, 0, 0),
                new Element(2, 0, 0),
                new Element(3, 700, 0),
                new Element(4, 220, 0),
                new Element(5, 280, 0),
                new Element(6, 1100, 800),
                new Element(7, 890, 350),
                new Element(8, 430, 10),
                new Element(9, 320, 112),
                new Element(10, 180, 65)
            };

        }

        [Fact]
        public void Analyze_GivenSet_ExceptedCalculatedValues()
        {

            _elements.CalculatePerimeterRiskFactorsAndUpdateAbsentElements();

            // Incomplete + Complete elements
            _elements.CountOfIncompleteAndComplete.Should().Be(8);
            _elements.PotentialRiskOfIncompleteAndComplete.Should().Be(4120);

            // Complete
            _elements.PotentialRiskOfComplete.Should().Be(2920);

            // C3
            _elements.CountOfC3Class.Should().Be(4);
            _elements.PotentialRiskOfC3Class.Should().Be(3120);

            // Functional Perimeter Analysis
            _elements.AveragePotentialRisk.Should().Be(515);
            _elements.AveragePotentialRiskOfC3Class.Should().Be(780);

            // Totals
            _elements.TotalPotentialRisk.Should().Be(5150);
            _elements.TotalManagedRisk.Should().Be(1337);

            // Percentages factor
            R2(_elements.FractionOfIncompleteAndComplete).Should().Be(0.80);
            R2(_elements.ManagedRiskReductionFactor).Should().Be(0.46);

        }


        private static double R2(double value)
        {
            return Math.Round(value, 2);
        }

    }

}
