using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatIfAnalysis.Elements
{

    public class ElementsSet : List<Element>
    {
        #region private variables

        private int _prtot = 0;

        private int _mrtot = 0;

        private int _nIc = 0;

        private int _pric = 0;

        private int _prc = 0;

        private int _nC3 = 0;

        private int _prc3 = 0;

        private double _avgPR = 0;

        private double _avgPrC3 = 0;

        private double _withVCI = 0;

        private double _mrredfactor = 0;

        #endregion

        public int PR_Tot => _prtot;

        public int MR_Tot => _mrtot;

        public int N_IC => _nIc;

        public int N_C3 => _nC3;

        public int PR_IC => _pric;

        public int PR_C => _prc;

        public int PR_C3 => _prc3;

        public double AvgPR => _avgPR;

        public double AvgPR_C3 => _avgPrC3;

        public double WithVCI => _withVCI;

        public double MRReductionFactor => _mrredfactor;

        public void MakePerimeterAnalysis()
        {
            _nIc = this.Count(IsIncompleteOrComplete);

            _pric = this.Sum(element => IsIncompleteOrComplete(element) ? element.PR : 0);

            _prc = this.Sum(element => IsComplete(element) ? element.PR : 0);

            _nC3 = this.Count(IsC3);

            _prc3 = this.Sum(element => IsC3(element) ? element.PR : 0);

            _avgPR = (double)_pric / _nIc;

            _avgPrC3 = (double)_prc3 / _nC3;

            _withVCI = (double)_nIc / this.Count();

            UpdateAbsentElementsPR();

            CalculateTotals();

            _mrredfactor = (double) _mrtot/_prc;

        }


        public void UpdateAbsentElementsPR()
        {
            this.ForEach(AssignPREstimateToAbsentElements );
        }

        public void CalculateTotals()
        {
            _prtot = this.Sum(element => element.PR);
            _mrtot = this.Sum(element => element.MR);
        }

        private void AssignPREstimateToAbsentElements(Element element)
        {
            if (IsAbsent(element))
            {
                element.SetPRAsEstimate(GetPREstimate());
            }
        }

        private int GetPREstimate()
        {
            double estimate = IsPerimeterAnalysed() ? AvgPR : AvgPR_C3;
            return (int) estimate;
        }

        private bool IsPerimeterAnalysed()
        {
            return WithVCI >= 0.75;
        }

        private bool IsIncompleteOrComplete(Element element)
        {
            return element.GetType() == ElementType.Incomplete ||
                   element.GetType() == ElementType.Complete;
        }

        private bool IsComplete(Element element)
        {
            return element.GetType() == ElementType.Complete;
        }
        private bool IsAbsent(Element element)
        {
            return element.GetType() == ElementType.Absent;
        }

        private bool IsC3(Element element)
        {
            return element.VCIClass == ElementVCIClass.C3;
        }

    }


}
