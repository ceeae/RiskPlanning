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

        private bool _supportNCAnalysis = false;

        private double _residualRisk = 0;

        private double _totalCost = 0;

        private double _totalIngCost = 0;

        private int _targetBudget = 0;

        private int _targetResidualRisk = 0;

        #endregion

        #region properties

        public List<CoverageActivity> Activities = new List<CoverageActivity>();

        public Dictionary<long, double[]> Results;

        public double ResidualRisk => _residualRisk;

        public double TotalCost => _totalCost;

        public double TotalIngCost => _totalIngCost;

        #endregion

        public WhatIfEngine(ElementsSet elements)
        {
            _elements = elements;
        }

        public void SupportNotClassifiedAnalysis(bool support)
        {
            _supportNCAnalysis = support;
        }

        public void ExecuteAnalysis(int pdsCost, int pdsIngcost, int notClassifiedVolumeEstimate, int targetBudget, int targetResidualRisk)
        {
            _targetBudget = targetBudget;

            _targetResidualRisk = targetResidualRisk;

            _elements.Analyze();

            Activities = new List<CoverageActivity>();

            _elements.ForEach( element => GenerateCoverageActivities(element, pdsCost, pdsIngcost));

            if (_supportNCAnalysis)
            {
                GenerateCoverageActivitiesForNotClassifiedElements(0, 0); // Not implemented!
            }

            Activities = Activities.OrderByDescending(activity => activity.Ranking).ToList();

            CreateResidualRiskAndCostsFigures(notClassifiedVolumeEstimate);

        }

        public KeyValuePair<long, double[]> GetTargetRisk()
        {
            return Results.OrderBy(e => e.Value[3]).FirstOrDefault();
        }

        public KeyValuePair<long, double[]> GetTargetBudget()
        {
            return Results.OrderBy(e => e.Value[4]).FirstOrDefault();
        }

        private void CreateResidualRiskAndCostsFigures(int notClassified)
        {

            double potentialrisk = (double)notClassified * _elements.AveragePotentialRiskOfC3Class + _elements.TotalPotentialRisk;

            _residualRisk = potentialrisk - _elements.TotalManagedRisk;

            _totalCost = 0;

            Results = new Dictionary<long, double[]>();
            
            Activities.ForEach(CalculateResidualRiskAndCostsFigures );
        }

        private void CalculateResidualRiskAndCostsFigures(CoverageActivity activity)
        {
            long id = activity.GetElementId();

            _residualRisk = _residualRisk - (double) activity.ManagedRiskReduction;

            _totalCost += (double) activity.Cost;

            _totalIngCost += (double) activity.IngCost;

            // Calculate diff relative to target (easier to select result)
            double riskDiff = _residualRisk - _targetResidualRisk;
            riskDiff = riskDiff > 0 ? riskDiff : _residualRisk;

            double bdgDiff = _targetBudget - _totalCost;
            bdgDiff = bdgDiff > 0 ? bdgDiff : _targetBudget;

            Results.Add(id, new double[] {_residualRisk, _totalCost, _totalIngCost, riskDiff, bdgDiff });
        }
        
        private void GenerateCoverageActivities(Element element, int pdscost, int ingpdscost)
        {
            double mrreduction = _elements.ManagedRiskReductionFactor;
            CoverageActivity activity = null;

            switch (element.GetElementType())
            {

                case ElementType.Incomplete:
                case ElementType.Absent:
                {
                    double abb = (double) element.PotentialRisk*mrreduction;
                    activity = new CoverageActivity(element, ActivityType.PDS, pdscost, ingpdscost, abb);
                }
                break;

            }

            if (activity != null)
            {
                Activities.Add(activity);
            }
        }

        private void GenerateCoverageActivitiesForNotClassifiedElements(int totalcost, int ingtotalcost)
        {
            // Feature not implemented! ... generate VCI + PDS Activities for NotClassified elements
        }

    }
}
