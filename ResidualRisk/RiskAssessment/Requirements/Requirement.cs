﻿using System;
using System.Collections.Generic;
using System.Linq;
using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class Requirement
    {
        public const int WEIGHTS_NUM = 3 + 35;  // indexed by 0..

        public long Id { get; }

        public FractionWeight PAS { get; }

        public CorrectionFactor Alpha { get; }

        public bool Adequate { get; }

        public Weights ReqWeights { get;  }     // RID + 35 compliance ReqWeights

  
        #region Potential Risk Factors (calculated - BIA, BIAID, COMPL - format 00.00)

        private double _prbia = 0;
        private double _prbiaid = 0;
        private double _prcompl = 0;

        public double PRbia => _prbia;

        public double PRbiaID => _prbiaid;

        public double PRcompl => _prcompl;

        #endregion calculated factors

        public Requirement(long id, FractionWeight pas, CorrectionFactor alpha, bool adequate)
        {
            if (id <= 0)
            {
                throw new InvalidKeyException();
            }

            Id = id;
            PAS = pas;
            Alpha = alpha;
            Adequate = adequate;

            ReqWeights = new Weights(Enumerable.Repeat((Weight)new DefaultWeight(), WEIGHTS_NUM).ToList()); 
        }

        public Requirement(long id, FractionWeight pas, CorrectionFactor alpha, bool adequate, int[] values) 
            : this(id, pas, alpha, adequate)
        {
            InitializeWeightsWithIntArray(values);
        }

        public void InitializeWeightsWithIntArray(int startIndex, int[] values)
        {
            int i;
            for (i = startIndex;
                i <= startIndex + values.Length - 1;
                i++)
            {
                ReqWeights[i] = new Weight(values[i - startIndex]);
            }
        }

        public void InitializeWeightsWithIntArray(int[] values)
        {
            InitializeWeightsWithIntArray(0, values);
        }

        public void CalculatePotentialRiskFactors(List<int> totals)
        {
            double correctedPAS = PAS.Value + Alpha.Value;

            List<double> f = DivideWeightsBy(totals);                       // f = ReqWeights / CalculateWeightsTotals

            _prbia =    f.Take(3).Sum()         * correctedPAS;
            _prbiaid =  f.Skip(1).Take(2).Sum() * correctedPAS;
            _prcompl =  f.Skip(3).Sum()         * correctedPAS;
        }


        private List<double> DivideWeightsBy(List<int> totals)
        {
            int i;
            List<double> fraction = Enumerable.Repeat((double) 0, WEIGHTS_NUM).ToList();

            for (i = 0; i < WEIGHTS_NUM - 1; i++)
            {
                fraction[i] = (double) ReqWeights[i].Value/totals[i];
            }
            return fraction;
        }

    }
}