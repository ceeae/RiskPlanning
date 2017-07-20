namespace WhatIfAnalysis.ReductionAnalysis
{
    public class ReductionAction
    {
        public long ActionId { get; }

        public long ElementId { get;  }

        public string Name { get; }

        public int Cost { get; }

        public int IngCost { get; } = 0;

        public double ManagedRiskBIA { get; }

        public double ManagedRiskCOMPL { get;  }

        public double ManagedRiskReduction { get;  }

        public double Importance { get;  }

        public ReductionAction(long actionId, long elementId,  string name, int cost, double managedRiskBia, double managedRiskCompl)
        {

            ElementId = elementId;

            ActionId = actionId;

            Name = name;

            Cost = cost;

            ManagedRiskBIA = managedRiskBia;

            ManagedRiskCOMPL = managedRiskCompl;

            ManagedRiskReduction = ManagedRiskBIA + ManagedRiskCOMPL;

            Importance = ManagedRiskReduction / Cost;
        }
    }
}
