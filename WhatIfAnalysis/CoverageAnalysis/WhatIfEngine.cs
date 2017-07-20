using System.Collections.Generic;
using System.Linq;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.CoverageAnalysis
{
    public class WhatIfEngine
    {
      
        #region private variables

        private readonly ElementsSet _elements;

        private bool _includeNotClassified = false;

        #endregion

        #region properties

        public List<CoverageAction> Actions;               // "What" <Projections> "If I Do" <Actions>

        public Dictionary<long, double[]> Projections;

        public double TotalResidualRisk { get; private set; } = 0;

        public double TotalCost { get; private set; } = 0;

        public double TotalIngCost { get; private set; } = 0;

        #endregion

        public WhatIfEngine(ElementsSet elements)
        {
            _elements = elements;

            _elements.CalculatePerimeterRiskFactorsAndUpdateAbsentElements();
        }

        public void IncludeNotClassified()
        {
            _includeNotClassified = true;
        }

        public void ExecuteAnalysis(int pdsCost, int pdsIngcost, int notClassifiedVolumeEstimate)
        {
            GenerateActions(pdsCost, pdsIngcost);

            OrderActionsByImportance();

            CreateRiskReductionProjections(notClassifiedVolumeEstimate);
        }

        private void GenerateActions(int pdsCost, int pdsIngcost)
        {
            Actions = new List<CoverageAction>();

            _elements.ForEach(element => GenerateAction(element, pdsCost, pdsIngcost));

            if (_includeNotClassified)
            {
                GenerateActionsForNotClassifiedElements(0, 0); // Not implemented!
            }
        }

        private void GenerateActionsForNotClassifiedElements(int totalcost, int ingtotalcost)
        {
            // Feature not implemented! ... generate VCI + PDS Actions for NotClassified elements
        }

        private void GenerateAction(Element element, int pdscost, int ingpdscost)
        {

            switch (element.GetElementType())
            {

                case ElementType.Incomplete:
                case ElementType.Absent:
                {
                    var managedRiskReduction = (double) element.PotentialRisk * _elements.ManagedRiskReductionFactor;

                    Actions.Add(
                            new CoverageAction(element, ActionType.PDS, pdscost, ingpdscost, managedRiskReduction)
                        );
                }
                break;

            }

        }

        private void OrderActionsByImportance()
        {
            Actions = Actions.OrderByDescending(action => action.Importance).ToList();
        }

        private void CreateRiskReductionProjections(int notClassified)
        {

            double totalPotentialRisk = (double) notClassified * _elements.AveragePotentialRiskOfC3Class + _elements.TotalPotentialRisk;

            TotalResidualRisk = totalPotentialRisk - _elements.TotalManagedRisk;

            TotalCost = 0;

            Projections = new Dictionary<long, double[]>();
            
            Actions.ForEach(CreateRiskProjection );
        }

        private void CreateRiskProjection(CoverageAction action)
        {
            
            DecreaseTotalRiskAndCostByAction(action);

            Projections.Add(

                action.GetActionId(), 
                
                new double[] { TotalResidualRisk, TotalCost, TotalIngCost}
                
                );
        }

        private void DecreaseTotalRiskAndCostByAction(CoverageAction action)
        {
            TotalResidualRisk = TotalResidualRisk - (double) action.ManagedRiskReduction;

            TotalCost += (double) action.Cost;

            TotalIngCost += (double) action.IngCost;
        }

        public KeyValuePair<long, double[]> GetClosestResidualRiskProjection(int targetRisk)
        {
            long nearestId = -1;

            double nearestTargetRisk = _elements.TotalPotentialRisk;

            foreach (var projection in Projections)
            {
                double deltaRisk = projection.Value[0] - targetRisk;

                if (deltaRisk >= 0 && deltaRisk < nearestTargetRisk)
                {
                    nearestId = (long)projection.Key;
                    nearestTargetRisk = deltaRisk;
                }
            }

            if (nearestId == -1) return new KeyValuePair<long, double[]>();

            return new KeyValuePair<long, double[]>(nearestId, Projections[nearestId]);
        }

        public KeyValuePair<long, double[]> GetClosestBudgetProjection(int targetBudget)
        {
            long nearestId = -1;
            double nearestTargetBudget = targetBudget;

            foreach (var projection in Projections)
            {
                double deltaBudget = targetBudget - projection.Value[1];

                if (deltaBudget >= 0 && deltaBudget < nearestTargetBudget)
                {
                    nearestId = (long) projection.Key;
                    nearestTargetBudget = deltaBudget;
                }
            }

            if (nearestId == -1) return new KeyValuePair<long, double[]>();

            return new KeyValuePair<long, double[]>(nearestId, Projections[nearestId]);
        }

    }
}
