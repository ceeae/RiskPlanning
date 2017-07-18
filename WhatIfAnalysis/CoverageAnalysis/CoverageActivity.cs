using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.CoverageAnalysis
{
    public class CoverageActivity
    {
        private readonly Element _element;

        #region properties

        public  ActivityType ActivityType { get; }

        public int Cost { get; }

        public int IngCost { get; }

        public double ManagedRiskReduction;

        public double Ranking = 0;
        #endregion

        public CoverageActivity(Element element, ActivityType activityType, int cost, int ingCost, double managedRiskReduction)
        {
            _element = element;

            ActivityType = activityType;

            Cost = cost;

            IngCost = ingCost;

            ManagedRiskReduction = managedRiskReduction;

            Ranking = (double) managedRiskReduction/(cost + ingCost);
        }

        public long GetElementId()
        {
            return _element.Id;
        }

        public bool IsPDS()
        {
            return ActivityType == ActivityType.PDS;
        }
    }
}
