using System;
using System.Collections.Generic;
using System.Linq;
using CalcoloRischioResiduo.RiskAssessment.Common;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class RequirementsSet : List<Requirement>
    {

        public double VEF { get; set; }

        private bool calcTotalsneeded = true;

        private bool calcDistribneeded = true;

        private List<int> totals = null;

        private Dictionary<long, double[]> distrib = null;

        #region Potential Risk Factors Totals (calculated - BIA, BIAID, COMPL - format 00.0)

        private double _prbiatot = 0;
        private double _prbiaidtot = 0;
        private double _prcompltot = 0;

        public double PRbiaTot => _prbiatot;

        public double PRbiaIDTot => _prbiaidtot;

        public double PRcomplTot => _prcompltot;

        #endregion calculated factors

        public void AddRequirement(int id, double pas, double alpha, bool adequate, int[] values)
        {
            Requirement requirement = new Requirement(id, new FractionWeight(pas), new CorrectionFactor(alpha), adequate, values);
            Add(requirement);

            calcTotalsneeded = true;
            calcDistribneeded = true;
        }

        public List<int> CalculateWeightsTotals()
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

            totals = CalculateWeightsTotals();

            foreach (var req in this)
            {

                req.CalculatePotentialRiskFactors(totals);

                distrib.Add(req.Id, new double[4] { req.PRbia, req.PRbiaID, req.PRcompl, VEF * req.PRbiaID }); // Potential Risk BIA, BIAID, COMPL factors

                _prbiatot += req.PRbia;
                _prbiaidtot += req.PRbiaID;
                _prcompltot += req.PRcompl;
            }

            calcDistribneeded = false;

            return distrib;
        }

        public double GetManagedRiskBIAFactor()
        {
            GetPotentialRiskDistributionFactors();
            return this.Where(req => req.Adequate).Sum(req => req.PRbia);
        }

        public double GetManagedRiskCOMPLFactor()
        {
            GetPotentialRiskDistributionFactors();
            return this.Where(req => req.Adequate).Sum(req => req.PRcompl);
        }

        public double GetResidualRiskBIAFactor()
        {
            GetPotentialRiskDistributionFactors();
            return this.Where(req => !req.Adequate).Sum(req => req.PRbia);
        }

        public double GetResidualRiskCOMPLFactor()
        {
            GetPotentialRiskDistributionFactors();
            return this.Where(req => !req.Adequate).Sum(req => req.PRcompl);
        }

    }
}
