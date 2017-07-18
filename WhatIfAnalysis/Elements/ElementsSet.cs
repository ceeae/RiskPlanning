using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatIfAnalysis.Elements
{

    public class ElementsSet : List<Element>
    {
        #region private variables

        private int _totalPotentialRisk = 0;

        private int _totalManagedRisk = 0;

        private int _countOfIncompleteAndComplete = 0;

        private int _potentialRiskOfIncompleteAndComplete = 0;

        private int _potentialRiskOfComplete = 0;

        private int _countOfC3Class = 0;

        private int _potentialRiskOfC3Class = 0;

        private double _averagePotentialRisk = 0;

        private double _averagePotentialRiskOfC3Class = 0;

        private double _fractionOfIncompleteAndComplete = 0;        // own a VCI analysis

        private double _managedRiskReductionFactor = 0;

        #endregion

        #region properties

        public int TotalPotentialRisk => _totalPotentialRisk;

        public int TotalManagedRisk => _totalManagedRisk;

        public int CountOfIncompleteAndComplete => _countOfIncompleteAndComplete;

        public int CountOfC3Class => _countOfC3Class;

        public int PotentialRiskOfIncompleteAndComplete => _potentialRiskOfIncompleteAndComplete;

        public int PotentialRiskOfComplete => _potentialRiskOfComplete;

        public int PotentialRiskOfC3Class => _potentialRiskOfC3Class;

        public double AveragePotentialRisk => _averagePotentialRisk;

        public double AveragePotentialRiskOfC3Class => _averagePotentialRiskOfC3Class;

        public double FractionOfIncompleteAndComplete => _fractionOfIncompleteAndComplete;

        public double ManagedRiskReductionFactor => _managedRiskReductionFactor;

        #endregion

        public void Analyze()
        {
            CalculateRiskAndCountValues();

            UpdatePotentialRiskOfAbsentElements();  // as estimate from functional perimeter

            CalculateTotals();

            CalculateManagedRiskReductionFactor();
        }

        private void CalculateRiskAndCountValues()
        {
            _countOfIncompleteAndComplete = this.Count(IsIncompleteOrComplete);

            _countOfC3Class = this.Count(IsC3Class);

            _potentialRiskOfIncompleteAndComplete = this.Sum(element => IsIncompleteOrComplete(element) ? element.PotentialRisk : 0);

            _potentialRiskOfComplete = this.Sum(element => IsComplete(element) ? element.PotentialRisk : 0);

            _potentialRiskOfC3Class = this.Sum(element => IsC3Class(element) ? element.PotentialRisk : 0);

            _averagePotentialRisk = (double) _potentialRiskOfIncompleteAndComplete / _countOfIncompleteAndComplete;

            _averagePotentialRiskOfC3Class = (double) _potentialRiskOfC3Class / _countOfC3Class;

            _fractionOfIncompleteAndComplete = (double) _countOfIncompleteAndComplete / this.Count();

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
            _totalPotentialRisk = this.Sum(element => element.PotentialRisk);

            _totalManagedRisk = this.Sum(element => element.ManagedRisk);
        }

        private void CalculateManagedRiskReductionFactor()
        {
            _managedRiskReductionFactor = (double)_totalManagedRisk / _potentialRiskOfComplete;
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
