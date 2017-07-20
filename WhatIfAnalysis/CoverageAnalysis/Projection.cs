namespace WhatIfAnalysis.CoverageAnalysis
{
    public class Projection
    {
        public double ResidualRisk { get; }

        public double TotalCost { get; }

        public double TotalIngCost { get;  }

        public Projection(double residualRisk, double totalCost, double totalIngCost)
        {
            ResidualRisk = residualRisk;

            TotalCost = totalCost;

            TotalIngCost = totalIngCost;

        }

    }
}
