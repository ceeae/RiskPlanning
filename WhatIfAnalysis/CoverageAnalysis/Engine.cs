using System;
using System.Collections.Generic;
using System.Linq;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.CoverageAnalysis
{
    public class Engine
    {
        #region private variables

        private ElementsSet _elements;

        private bool _supportNCAnalysis = false;

        private double residualrisk = 0;

        private double totalcosts = 0;

        private double totalingcosts = 0;

        private int targetBudget = 0;

        private int targetRisk = 0;

        #endregion

        public List<CoverageActivity> activities = new List<CoverageActivity>();

        public Dictionary<long, double[]> results;

        public double ResidualRisk => residualrisk;

        public double TotalCosts => totalcosts;

        public double TotalIngCosts => totalingcosts;

        public Engine( ElementsSet elements)
        {
            _elements = elements;
        }

        public void SupportNotClassifiedAnalysis(bool support)
        {
            _supportNCAnalysis = support;
        }

        public void ApplyWhatIfAnalysis(int pdscost, int ingpdscost, int notclassified, int budget, int targetrisk)
        {
            targetBudget = budget;
            targetRisk = targetrisk;

            _elements.MakePerimeterAnalysis();

            activities = new List<CoverageActivity>();

            _elements.ForEach( element => GenerateCoverageActivities(element, pdscost, ingpdscost));

            if (_supportNCAnalysis)
            {
                GenerateCoverageActivitiesForNotClassifiedElements(0, 0); // Not implemented!
            }

            activities = activities.OrderByDescending(activity => activity._order).ToList();

            CreateResidualRiskAndCostsFigures(notclassified);

        }

        public KeyValuePair<long, double[]> GetTargetRisk()
        {
            return results.OrderBy(e => e.Value[3]).FirstOrDefault();
        }

        public KeyValuePair<long, double[]> GetTargetBudget()
        {
            return results.OrderBy(e => e.Value[4]).FirstOrDefault();
        }

        private void CreateResidualRiskAndCostsFigures(int notclassified)
        {

            double potentialrisk = (double)notclassified * _elements.AvgPR_C3 + _elements.PR_Tot;

            residualrisk = potentialrisk - _elements.MR_Tot;

            totalcosts = 0;

            results = new Dictionary<long, double[]>();
            
            activities.ForEach(CalculateResidualRiskAndCostsFigures );
        }

        private void CalculateResidualRiskAndCostsFigures(CoverageActivity activity)
        {
            long id = activity.GetElementId();

            residualrisk = residualrisk - (double) activity._mrreduction;

            totalcosts += (double) activity._cost;

            totalingcosts += (double) activity._ingcost;

            // Calculate diff relative to target (easier to select result)
            double riskDiff = residualrisk - targetRisk;
            riskDiff = riskDiff > 0 ? riskDiff : residualrisk;

            double bdgDiff = targetBudget - totalcosts;
            bdgDiff = bdgDiff > 0 ? bdgDiff : targetBudget;

            results.Add(id, new double[] {residualrisk, totalcosts, totalingcosts, riskDiff, bdgDiff });
        }
        
        private void GenerateCoverageActivities(Element element, int pdscost, int ingpdscost)
        {
            double mrreduction = _elements.MRReductionFactor;
            CoverageActivity activity = null;

            switch (element.GetType())
            {

                case ElementType.Incomplete:
                case ElementType.Absent:
                {
                    double abb = (double) element.PR*mrreduction;
                    activity = new CoverageActivity(element, ActivityType.PDS, pdscost, ingpdscost, abb);
                }
                break;

            }

            if (activity != null)
            {
                activities.Add(activity);
            }
        }

        private void GenerateCoverageActivitiesForNotClassifiedElements(int totalcost, int ingtotalcost)
        {
            // Feature not implemented! ... generate VCI + PDS activities for NotClassified elements
        }

    }
}
