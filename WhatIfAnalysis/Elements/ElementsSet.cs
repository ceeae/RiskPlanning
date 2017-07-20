using System.Collections.Generic;
using System.Linq;

namespace WhatIfAnalysis.Elements
{

    public class ElementsSet : List<Element>
    {
        #region Properties

        public int TotalPotentialRisk { get; private set; } = 0;

        public int TotalManagedRisk { get; private set; } = 0;

        public int CountOfIncompleteAndComplete { get; private set; } = 0;

        public int CountOfC3Class { get; private set; } = 0;

        public int PotentialRiskOfIncompleteAndComplete { get; private set; } = 0;

        public int PotentialRiskOfComplete { get; private set; } = 0;

        public int PotentialRiskOfC3Class { get; private set; } = 0;

        public double AveragePotentialRisk { get; private set; } = 0;

        public double AveragePotentialRiskOfC3Class { get; private set; } = 0;

        public double FractionOfIncompleteAndComplete { get; private set; } = 0;

        public double ManagedRiskReductionFactor { get; private set; } = 0;

        #endregion

        public void CalculatePerimeterRiskFactorsAndUpdateAbsentElements()
        {
            CalculatePotentialRisksAndCounters();

            UpdatePotentialRiskOfAbsentElements();  // as estimate from functional perimeter

            CalculateTotals();

            CalculateManagedRiskReductionFactor();
        }

        private void CalculatePotentialRisksAndCounters()
        {
            CountOfIncompleteAndComplete = this.Count(IsIncompleteOrComplete);

            CountOfC3Class = this.Count(IsC3Class);

            PotentialRiskOfIncompleteAndComplete = this.Sum(element => IsIncompleteOrComplete(element) ? element.PotentialRisk : 0);

            PotentialRiskOfComplete = this.Sum(element => IsComplete(element) ? element.PotentialRisk : 0);

            PotentialRiskOfC3Class = this.Sum(element => IsC3Class(element) ? element.PotentialRisk : 0);

            AveragePotentialRisk = (double) PotentialRiskOfIncompleteAndComplete / CountOfIncompleteAndComplete;

            AveragePotentialRiskOfC3Class = (double) PotentialRiskOfC3Class / CountOfC3Class;

            FractionOfIncompleteAndComplete = (double) CountOfIncompleteAndComplete / this.Count();

        }

        private void UpdatePotentialRiskOfAbsentElements()
        {
            this.ForEach(UpdatePotentialRiskOfAbsentElement );
        }

            private void UpdatePotentialRiskOfAbsentElement(Element element)
        {
            if (IsAbsent(element))
            {
                element.SetPotentialRisk(EstimatePotentialRisk());
            }
        }

            private int EstimatePotentialRisk()
        {
            double estimate = IsPerimeterAnalysed() ? AveragePotentialRisk : AveragePotentialRiskOfC3Class;
            return (int)estimate;
        }

        private void CalculateTotals()
        {
            TotalPotentialRisk = this.Sum(element => element.PotentialRisk);

            TotalManagedRisk = this.Sum(element => element.ManagedRisk);
        }

        private void CalculateManagedRiskReductionFactor()
        {
            ManagedRiskReductionFactor = (double)TotalManagedRisk / PotentialRiskOfComplete;
        }

        private bool IsPerimeterAnalysed()
        {
            return FractionOfIncompleteAndComplete >= 0.75;
        }

        private bool IsIncompleteOrComplete(Element element)
        {
            ElementType elementType = element.GetElementType();

            return elementType == ElementType.Incomplete || elementType == ElementType.Complete;
        }

        private bool IsComplete(Element element)
        {
            return element.GetElementType() == ElementType.Complete;
        }

        private bool IsAbsent(Element element)
        {
            return element.GetElementType() == ElementType.Absent;
        }

        private bool IsC3Class(Element element)
        {
            return element.VciClass == VCIClass.C3;
        }

    }


}
