using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Xunit;
using FluentAssertions;
using WhatIfAnalysis.CoverageAnalysis;
using WhatIfAnalysis.Elements;


namespace WhatIfAnalysis.UnitTests.CoverageAnalysis
{
    public class EngineUnitTests
    {
        private Engine coverageAnalysis;
        private ElementsSet elements;

        public EngineUnitTests()
        {

            elements = new ElementsSet();

            elements.Add(new Element(1, 0, 0));
            elements.Add(new Element(2, 0, 0));
            elements.Add(new Element(3, 700, 0));
            elements.Add(new Element(4, 220, 0));
            elements.Add(new Element(5, 280, 0));
            elements.Add(new Element(6, 1100, 800));
            elements.Add(new Element(7, 890, 350));
            elements.Add(new Element(8, 430, 10));
            elements.Add(new Element(9, 320, 112));
            elements.Add(new Element(10, 180, 65));

        }

        [Fact]
        public void CreateCoverageActivities_GivenSetOfElements_ExpectedCoverageActivities()
        {

            Engine engine = new Engine(elements);

            // PDS and IngPDS costs
            engine.ApplyWhatIfAnalysis(12, 2, 3);

            // Expected Activities
            engine.activities.Count.Should().Be(5);
            engine.activities.Count(activity => activity._activityType == ActivityType.PDS).Should().Be(5);
            R0(engine.activities[0]._order).Should().Be(23);
            R0(engine.activities[1]._order).Should().Be(17);

        }

        [Fact]
        public void CreateResidualRiskAndCosts_GivenSetOfElements_CheckFinalResults()
        {
            Engine engine = new Engine(elements);

            engine.ApplyWhatIfAnalysis(12, 2, 3);

            engine.
            

            //true.Should().BeTrue();


        }



        public double R0(double value)
        {
            return Math.Round(value, 0);
        }
    }
}
