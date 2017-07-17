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
        private ElementsSet set;

        public ElementsSetUnitTests()
        {
            set = new ElementsSet();

            set.Add(new Element(1, 0, 0 ));
            set.Add(new Element(2, 0, 0 ));
            set.Add(new Element(3, 700, 0 ));
            set.Add(new Element(4, 220, 0 ));
            set.Add(new Element(5, 280, 0 ));
            set.Add(new Element(6, 1100, 800 ));
            set.Add(new Element(7, 890, 350));
            set.Add(new Element(8, 430, 10));
            set.Add(new Element(9, 320, 112));
            set.Add(new Element(10, 180, 65));
        }

        [Fact]
        public void MakePerimeterAnalysis_GivenSet_ExceptedValues()
        {

            set.MakePerimeterAnalysis();

            // Incomplete + Complete elements
            set.N_IC.Should().Be(8);
            set.PR_IC.Should().Be(4120);

            // Complete
            set.PR_C.Should().Be(2920);

            // C3
            set.N_C3.Should().Be(4);
            set.PR_C3.Should().Be(3120);

            // Functional Perimeter Analysis
            set.AvgPR.Should().Be(515);
            set.AvgPR_C3.Should().Be(780);

            // Totals
            set.PR_Tot.Should().Be(5150);
            set.MR_Tot.Should().Be(1337);

            // Percentages factor
            R2(set.WithVCI).Should().Be(0.80);
            R2(set.MRReductionFactor).Should().Be(0.46);

        }


        public static double R2(double value)
        {
            return Math.Round(value, 2);
        }

    }

}
