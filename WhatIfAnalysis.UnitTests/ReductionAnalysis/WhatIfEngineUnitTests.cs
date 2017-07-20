using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using WhatIfAnalysis.Elements;
using WhatIfAnalysis.ReductionAnalysis;


namespace WhatIfAnalysis.UnitTests.ReductionAnalysis
{
    public class WhatIfEngineUnitTests
    {
        private readonly WhatIfEngine _whatIfEngine;

        public WhatIfEngineUnitTests()
        {
            var elements = new ElementsSet
            {
                new Element(101, 630, 440),
                new Element(102, 512, 234),
                new Element(103, 750, 156),
                new Element(104, 1086, 845),
                new Element(105, 820, 532),
            };

            _whatIfEngine = new WhatIfEngine(elements);

            var _actions = new List<ReductionAction>
            {
                new ReductionAction(1011, 101, "A101-1", 30, 40, 7),
                new ReductionAction(1012, 101, "A101-2", 12, 20, 27),
                new ReductionAction(1013, 101, "A101-3", 43, 10, 37),
                new ReductionAction(1014, 101, "A101-4", 22, 20, 27),

                new ReductionAction(1021, 102, "B102-1", 78, 50, 5),
                new ReductionAction(1022, 102, "B102-2", 43, 50, 5),
                new ReductionAction(1023, 102, "B102-3", 22, 5, 50),
                new ReductionAction(1024, 102, "B102-4", 12, 5, 50),
                new ReductionAction(1025, 102, "B102-5", 66, 25, 20),

                new ReductionAction(1031, 102, "C103-1", 145, 100, 98),
                new ReductionAction(1032, 102, "C103-2", 23, 150, 48),
                new ReductionAction(1033, 102, "C103-3", 25, 50, 148),

                new ReductionAction(1041, 104, "D104-1", 78, 41, 200),

                new ReductionAction(1051, 105, "E105-1", 115, 20, 21),
                new ReductionAction(1052, 105, "E105-2", 132, 0, 41),
                new ReductionAction(1053, 105, "E105-3", 7, 41, 0),
                new ReductionAction(1054, 105, "E105-4", 89, 10, 31),
                new ReductionAction(1055, 105, "E105-5", 25, 31, 10),
                new ReductionAction(1056, 105, "E105-6", 34, 20, 21),
                new ReductionAction(1057, 105, "E105-7", 112, 20, 21),
            };

            _whatIfEngine.SetActions(_actions);

            _whatIfEngine.ExecuteAnalysis();
        }

        [Fact]
        public void ApplyWhatIfAnalysis_GivenElementsAndStandardCosts_ExpectedReductionActivities()
        {

            List<ReductionAction> actions = _whatIfEngine.Actions;

            // Expected Actions
            actions.Count.Should().Be(20);

            R2(actions[0].Importance).Should().Be(8.61);
            R2(actions[1].Importance).Should().Be(7.92);
            R2(actions[19].Importance).Should().Be(0.31);

        }

        [Fact]
        public void ApplyWhatIfAnalysis_GivenElementsAndStandardCosts_ExcpectedResidualRiskAndCostsFigures()
        {

            // Expected Figures
            R0(_whatIfEngine.TotalResidualRisk).Should().Be(16);

            R0(_whatIfEngine.TotalIngCost).Should().Be(0);

            R0(_whatIfEngine.TotalCost).Should().Be(1113);

        }

        [Fact]
        public void GetTargetRisk_GivenElementsAndStandardCosts_ExcpectedTargetFigure()
        {

            KeyValuePair<long, Projection> target = _whatIfEngine.GetClosestResidualRiskProjection(230);

            // Target Residual Risk Found
            target.Key.Should().Be(1013);

            R0(target.Value.ResidualRisk).Should().Be(280);           // Actual Residual Risk
            R0(target.Value.TotalCost).Should().Be(521);           // Actual Costs
            //R0(target.Value.TotalIngCost).Should().Be(0);           // Actual IngCosts

        }

        [Fact]
        public void GetTargetBudget_GivenElementsAndStandardCosts_ExcpectedTargetFigure()
        {

            KeyValuePair<long, Projection> target = _whatIfEngine.GetClosestBudgetProjection(420);

            // Target Budget Found
            target.Key.Should().Be(1031);

            R0(target.Value.ResidualRisk).Should().Be(423);       // Actual Residual Risk
            R0(target.Value.TotalCost).Should().Be(401);       // Actual Costs
            //R0(target.Value.TotalIngCost).Should().Be(0);         // Actual IngCosts

        }

        private double R0(double value) { return Math.Round(value, 0); }

        private double R2(double value) { return Math.Round(value, 2); }
    }
}