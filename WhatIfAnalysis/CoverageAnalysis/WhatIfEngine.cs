using System;
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

        private int _targetBudget = 0;

        private int _targetResidualRisk = 0;

        #endregion

        #region properties

        public List<CoverageActivity> Activities;               // What <Projections> If Implement <Activities>

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
            GenerateCoverageActivities(pdsCost, pdsIngcost);

            OrderActivitiesByRanking();

            ProduceRiskReductionScenario(notClassifiedVolumeEstimate);
        }

        private void GenerateCoverageActivities(int pdsCost, int pdsIngcost)
        {
            Activities = new List<CoverageActivity>();

            _elements.ForEach(element => GenerateCoverageActivity(element, pdsCost, pdsIngcost));

            if (_includeNotClassified)
            {
                GenerateCoverageActivitiesForNotClassifiedElements(0, 0); // Not implemented!
            }
        }

        private void GenerateCoverageActivitiesForNotClassifiedElements(int totalcost, int ingtotalcost)
        {
            // Feature not implemented! ... generate VCI + PDS Activities for NotClassified elements
        }

        private void GenerateCoverageActivity(Element element, int pdscost, int ingpdscost)
        {

            switch (element.GetElementType())
            {

                case ElementType.Incomplete:
                case ElementType.Absent:
                {
                    var managedRiskReduction = (double) element.PotentialRisk * _elements.ManagedRiskReductionFactor;

                    Activities.Add(
                            new CoverageActivity(element, ActivityType.PDS, pdscost, ingpdscost, managedRiskReduction)
                        );
                }
                break;

            }

        }

        private void OrderActivitiesByRanking()
        {
            Activities = Activities.OrderByDescending(activity => activity.Ranking).ToList();
        }

        private void ProduceRiskReductionScenario(int notClassified)
        {

            double totalPotentialRisk = (double) notClassified * _elements.AveragePotentialRiskOfC3Class + _elements.TotalPotentialRisk;

            TotalResidualRisk = totalPotentialRisk - _elements.TotalManagedRisk;

            TotalCost = 0;

            Projections = new Dictionary<long, double[]>();
            
            Activities.ForEach(CalculateRiskProjectionsBy );
        }

        private void CalculateRiskProjectionsBy(CoverageActivity activity)
        {
            
            DecreaseTotalRiskAndCostByActivity(activity);

            // Calculate diff relative to target (easier to select result)
            //double riskDiff = TotalResidualRisk - _targetResidualRisk;
            //riskDiff = riskDiff > 0 ? riskDiff : TotalResidualRisk;

            //double bdgDiff = _targetBudget - TotalCost;
            //bdgDiff = bdgDiff > 0 ? bdgDiff : _targetBudget;

            //Projections.Add(id, new double[] {TotalResidualRisk, TotalCost, TotalIngCost, riskDiff, bdgDiff });
            Projections.Add(
                activity.GetElementId(), 
                new double[] { TotalResidualRisk, TotalCost, TotalIngCost, 0, 0 });
        }

        private void DecreaseTotalRiskAndCostByActivity(CoverageActivity activity)
        {
            TotalResidualRisk = TotalResidualRisk - (double) activity.ManagedRiskReduction;

            TotalCost += (double) activity.Cost;

            TotalIngCost += (double) activity.IngCost;
        }

        public KeyValuePair<long, double[]> GetClosestResidualRiskProjection(int targetRisk)
        {
            long nearestId = -1;

            double nearestTargetRisk = TotalResidualRisk;

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
