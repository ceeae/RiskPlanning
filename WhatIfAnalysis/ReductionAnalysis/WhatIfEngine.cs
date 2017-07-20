using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.ReductionAnalysis
{
    public class WhatIfEngine
    {
        private readonly ElementsSet _elements;

        public double TotalResidualRisk { get; private set; } = 0;

        public double TotalCost { get; private set; } = 0;

        public double TotalIngCost { get; private set; } = 0;

        public List<ReductionAction> Actions;

        public Dictionary<long, Projection> Projections;

        public WhatIfEngine(ElementsSet elements)
        {
            _elements = elements;

           _elements.CalculatePerimeterRiskFactorsAndUpdateAbsentElements();
        }

        public void ExecuteAnalysis()
        {
            OrderActionsByImportance();

            CreateRiskReductionProjections();
        }

        public void SetActions(List<ReductionAction> actions)
        {
            Actions = actions;
        }

        private void OrderActionsByImportance()
        {
            Actions = Actions.OrderByDescending(action => action.Importance).ToList();
        }

        private void CreateRiskReductionProjections()
        {

            double totalPotentialRisk = (double) _elements.TotalPotentialRisk;

            TotalResidualRisk = totalPotentialRisk - _elements.TotalManagedRisk;

            TotalCost = 0;

            Projections = new Dictionary<long, Projection>();

            Actions.ForEach(CreateRiskProjection);
        }

        private void CreateRiskProjection(ReductionAction action)
        {

            DecreaseTotalRiskAndCostByAction(action);

            Projections.Add(

                action.ActionId,

                new Projection(TotalResidualRisk, TotalCost, TotalIngCost)

                );
        }

        private void DecreaseTotalRiskAndCostByAction(ReductionAction action)
        {
            TotalResidualRisk = TotalResidualRisk - (double)action.ManagedRiskReduction;

            TotalCost += (double)action.Cost;

            TotalIngCost += (double)action.IngCost;
        }

        public KeyValuePair<long, Projection> GetClosestResidualRiskProjection(int targetRisk)
        {
            long nearestId = -1;

            Projection nearestProjection = null;

            double nearestTargetRisk = _elements.TotalPotentialRisk;

            foreach (var projection in Projections)
            {
                double deltaRisk = projection.Value.ResidualRisk - targetRisk;

                if (deltaRisk >= 0 && deltaRisk < nearestTargetRisk)
                {
                    nearestId = (long)projection.Key;

                    nearestProjection = projection.Value;

                    nearestTargetRisk = deltaRisk;
                }
            }

            if (nearestId == -1) return new KeyValuePair<long, Projection>();

            return new KeyValuePair<long, Projection>(nearestId, nearestProjection);
        }

        public KeyValuePair<long, Projection> GetClosestBudgetProjection(int targetBudget)
        {
            long nearestId = -1;

            Projection nearestProjection = null;

            double nearestTargetBudget = targetBudget;

            foreach (var projection in Projections)
            {
                double deltaBudget = targetBudget - projection.Value.TotalCost;

                if (deltaBudget >= 0 && deltaBudget < nearestTargetBudget)
                {
                    nearestId = (long)projection.Key;

                    nearestProjection = projection.Value;

                    nearestTargetBudget = deltaBudget;
                }
            }

            if (nearestId == -1) return new KeyValuePair<long, Projection>();

            return new KeyValuePair<long, Projection>(nearestId, nearestProjection);
        }
    }
}
