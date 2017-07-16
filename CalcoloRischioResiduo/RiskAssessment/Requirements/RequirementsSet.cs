using System;
using System.Collections.Generic;
using System.Linq;
using CalcoloRischioResiduo.RiskAssessment.Common;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class RequirementsSet : List<Requirement>
    {

        private bool calcTotalsneeded = true;
        private bool calcDistribneeded = true;

        private List<int> totals = null;

        private Dictionary<long, double[]> distrib = null;

        public double VEF { get; set; }

        #region Potential Risk Factors Totals (calculated - BIA, BIAID, COMPL - format 00.0)

        private double _prbiatot = 0;
        private double _prbiaidtot = 0;
        private double _prcompltot = 0;

        public double PRbiaTot => Math.Round(_prbiatot, 2);

        public double PRbiaIDTot => Math.Round(_prbiaidtot, 2);

        public double PRcomplTot => Math.Round(_prcompltot, 2);

        #endregion calculated factors

        public void AddRequirement(int id, double pas, double alpha, int[] values)
        {
            Requirement requirement = new Requirement(id, new FractionWeight(pas), new CorrectionFactor(alpha), values);
            Add(requirement);

            calcTotalsneeded = true;
            calcDistribneeded = true;
        }

        public List<int> CalculateTotals()
        {

            if (!calcTotalsneeded && totals != null)
            {
                return totals;
            }

            totals = Enumerable.Repeat(0, Requirement.WEIGHTS_NUM).ToList();

            foreach (var req in this)
            {
                totals += req.ReqWeights;
            }

            calcTotalsneeded = false;

            return totals;
        }

        public Dictionary<long, double[]> GetPotentialRiskDistributionFactors()
        {
            if (!calcDistribneeded && distrib != null)
            {
                return distrib;
            }

            _prbiaidtot = 0;
            _prbiatot = 0;
            _prcompltot = 0;

            distrib = new Dictionary<long, double[]>();

            totals = CalculateTotals();

            foreach (var req in this)
            {

                req.CalculatePotentialRiskFactors(totals);

                distrib.Add(req.Id, new double[3] { req.PRbia, req.PRbiaID, req.PRcompl });

                _prbiatot += req.PRbia;
                _prbiaidtot += req.PRbiaID;
                _prcompltot += req.PRcompl;
            }

            calcDistribneeded = false;
            return distrib;
        }

        public Dictionary<long, double> GetVEFDistribution()
        {
            return new Dictionary<long, double>
            {
                    {101, 853.60},
                    {102, 546.30},
                    {103, 212.11},
                    {104, 212.11},
                    {105, 175.89},
            };
        }
    }
}
