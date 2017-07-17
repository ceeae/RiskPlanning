using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;
using FluentAssertions;
using WhatIfAnalysis.CoverageAnalysis;
using WhatIfAnalysis.Elements;


namespace WhatIfAnalysis.UnitTests.CoverageAnalysis
{
    public class EngineUnitTests
    {
        private Engine engine;

        public EngineUnitTests()
        {

            ElementsSet elements = new ElementsSet();

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

            engine = new Engine(elements);

            // PDS and IngPDS costs
            engine.ApplyWhatIfAnalysis(12, 2, 3, 50, 5300);

        }

        [Fact]
        public void ApplyWhatIfAnalysis_GivenElementsAndStandardCosts_ExpectedCoverageActivities()
        {

            // Expected Activities
            engine.activities.Count.Should().Be(5);
            engine.activities.Count(activity => activity._activityType == ActivityType.PDS).Should().Be(5);
            R0(engine.activities[0]._order).Should().Be(23);
            R0(engine.activities[1]._order).Should().Be(17);

        }

        [Fact]
        public void ApplyWhatIfAnalysis_GivenElementsAndStandardCosts_ExcpectedResidualRiskAndCostsFigures()
        {

            // Expected Figures
            R0(engine.ResidualRisk).Should().Be(5132);
            R0(engine.TotalIngCosts).Should().Be(10);
            R0(engine.TotalCosts).Should().Be(60);
        }

        [Fact]
        public void GetTargetRisk_GivenElementsAndStandardCosts_ExcpectedTargetFigure()
        {

            KeyValuePair<long, double[]> target = engine.GetTargetRisk(); // 5300

            // Target Residual Risk Found
            target.Key.Should().Be(2);
            R0(target.Value[0]).Should().Be(5361);  // Actual Residual Risk
            R0(target.Value[1]).Should().Be(36);    // Actual Costs
            R0(target.Value[2]).Should().Be(6);     // Actual IngCosts

        }

        [Fact]
        public void GetTargetBudget_GivenElementsAndStandardCosts_ExcpectedTargetFigure()
        {

            KeyValuePair<long, double[]> target = engine.GetTargetBudget(); // 50

            // Target Budget Found
            target.Key.Should().Be(5);
            R0(target.Value[0]).Should().Be(5233);  // Actual Residual Risk
            R0(target.Value[1]).Should().Be(48);    // Actual Costs
            R0(target.Value[2]).Should().Be(8);     // Actual IngCosts

        }
        
        public double R0(double value)
        {
            return Math.Round(value, 0);
        }
    }
}
