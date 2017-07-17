using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.CoverageAnalysis
{
    public class Engine
    {

        private ElementsSet _elements;

        private bool _supportNCAnalysis = false;

        public List<CoverageActivity> activities = new List<CoverageActivity>();

        public Dictionary<long, double[]> results;

        private double residualrisk = 0;
        private double totalcosts = 0;
        private double totalingcosts = 0;


        public Engine( ElementsSet elements)
        {
            _elements = elements;
        }

        public void SupportNotClassifiedAnalysis(bool support)
        {
            _supportNCAnalysis = support;
        }

        public void ApplyWhatIfAnalysis(int pdscost, int ingpdscost, int notclassified)
        {
            _elements.MakePerimeterAnalysis();

            activities = new List<CoverageActivity>();

            _elements.ForEach( element => GenerateCoverageActivities(element, pdscost, ingpdscost));

            if (_supportNCAnalysis)
            {
                GenerateCoverageActivitiesForNotClassified(0, 0); // Not implemented!
            }

            activities = activities.OrderByDescending(activity => activity._order).ToList();

            CreateResidualRiskAndCostsFigures(notclassified);

        }

        private void CreateResidualRiskAndCostsFigures(int notclassified)
        {

            double potentialrisk = (double)notclassified * _elements.AvgPR_C3 + _elements.PR_Tot;
            residualrisk = potentialrisk - _elements.MR_Tot;
            totalcosts = 0;
            results = new Dictionary<long, double[]>();

            activities.ForEach(CalculateResidualRiskAndCostsFigures );
        }

        public void CalculateResidualRiskAndCostsFigures(CoverageActivity activity)
        {
            long id = activity.GetElementId();
            residualrisk = residualrisk - (double)activity._mrreduction;
            totalcosts += (double) activity._cost;
            totalingcosts += (double) activity._ingcost;
            results.Add(activity.GetElementId(), new double[] {residualrisk, totalcosts, totalingcosts});
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

        private void GenerateCoverageActivitiesForNotClassified(int totalcost, int ingtotalcost)
        {
            // Feature not implemented! ... generate VCI + PDS activities for NotClassified elements
        }
    }
}
