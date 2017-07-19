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
    public class WhatIfEngineUnitTests
    {
        private readonly WhatIfEngine _whatIfEngine;
        private readonly List<CoverageActivity> _activities;

        public WhatIfEngineUnitTests()
        {

            ElementsSet elements = new ElementsSet
            {
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

            _whatIfEngine = new WhatIfEngine(elements);

            // PDS cost, IngPDS cost, NotClassified volume
            _whatIfEngine.ExecuteAnalysis(12, 2, 3);

            _activities = _whatIfEngine.Activities;

        }

        [Fact]
        public void ApplyWhatIfAnalysis_GivenElementsAndStandardCosts_ExpectedCoverageActivities()
        {

            // Expected Activities
            _activities.Count.Should().Be(5);

            _activities.Count(activity => activity.IsPDS()).Should().Be(5);

            R0(_activities[0].Ranking).Should().Be(23);

            R0(_activities[1].Ranking).Should().Be(17);

        }

        [Fact]
        public void ApplyWhatIfAnalysis_GivenElementsAndStandardCosts_ExcpectedResidualRiskAndCostsFigures()
        {

            // Expected Figures
            R0(_whatIfEngine.TotalResidualRisk).Should().Be(5132);

            R0(_whatIfEngine.TotalIngCost).Should().Be(10);

            R0(_whatIfEngine.TotalCost).Should().Be(60);
        }

        [Fact]
        public void GetTargetRisk_GivenElementsAndStandardCosts_ExcpectedTargetFigure()
        {

            KeyValuePair<long, double[]> target = _whatIfEngine.GetClosestResidualRiskProjection(5300); 

            // Target Residual Risk Found
            target.Key.Should().Be(2);

            R0(target.Value[0]).Should().Be(5361);  // Actual Residual Risk
            R0(target.Value[1]).Should().Be(36);    // Actual Costs
            R0(target.Value[2]).Should().Be(6);     // Actual IngCosts

        }

        [Fact]
        public void GetTargetBudget_GivenElementsAndStandardCosts_ExcpectedTargetFigure()
        {

            KeyValuePair<long, double[]> target = _whatIfEngine.GetClosestBudgetProjection(50); 

            // Target Budget Found
            target.Key.Should().Be(5);
            R0(target.Value[0]).Should().Be(5233);  // Actual Residual Risk
            R0(target.Value[1]).Should().Be(48);    // Actual Costs
            R0(target.Value[2]).Should().Be(8);     // Actual IngCosts

        }

        private double R0(double value)
        {
            return Math.Round(value, 0);
        }
    }
}
