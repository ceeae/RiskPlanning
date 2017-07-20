using WhatIfAnalysis.Elements;

namespace WhatIfAnalysis.CoverageAnalysis
{
    public class CoverageAction
    {
        private readonly Element _element;

        #region properties

        public ActionType ActionType { get; }

        public int Cost { get; }

        public int IngCost { get; }

        public double ManagedRiskReduction { get; }

        public double Importance { get; }

        #endregion

        public CoverageAction(Element element, ActionType actionType, int cost, int ingCost, double managedRiskReduction)
        {
            _element = element;

            ActionType = actionType;

            Cost = cost;

            IngCost = ingCost;

            ManagedRiskReduction = managedRiskReduction;

            Importance = (double) managedRiskReduction/(cost + ingCost);
        }

        public long GetActionId()  // ActionId = ElementId
        {
            return _element.Id;
        }

        public bool IsPDS()
        {
            return ActionType == ActionType.PDS;
        }
    }
}
