using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.CoverageAnalysis
{
    public class CoverageActivity
    {
        private readonly Element _element;

        public  ActivityType _activityType;

        public int _cost;

        public int _ingcost;

        public double _mrreduction;

        public double _order = 0;

        public CoverageActivity(Element element, ActivityType activityType, int cost, int ingcost, double mrreduction)
        {
            _element = element;
            _activityType = activityType;
            _cost = cost;
            _ingcost = ingcost;
            _mrreduction = mrreduction;

            _order = (double) mrreduction/(cost + ingcost);
        }

        public long GetElementId()
        {
            return _element.Id;
        }
    }
}
