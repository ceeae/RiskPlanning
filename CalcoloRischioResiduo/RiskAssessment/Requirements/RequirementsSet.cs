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

        public double PRbiaTot => Math.Round(_prbiatot, 2);

        public double PRbiaIDTot => Math.Round(_prbiaidtot, 2);

        public double PRcomplTot => Math.Round(_prcompltot, 2);

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
            return MathRound2( this.Where(req => req.Adequate).Sum(req => req.PRbia) );
        }

        public double GetManagedRiskCOMPLFactor()
        {
            GetPotentialRiskDistributionFactors();
            return MathRound2(this.Where(req => req.Adequate).Sum(req => req.PRcompl));
        }

        public double GetResidualRiskBIAFactor()
        {
            GetPotentialRiskDistributionFactors();
            return MathRound2(this.Where(req => !req.Adequate).Sum(req => req.PRbia));
        }

        public double GetResidualRiskCOMPLFactor()
        {
            GetPotentialRiskDistributionFactors();
            return MathRound2(this.Where(req => !req.Adequate).Sum(req => req.PRcompl));
        }

        private static double MathRound2(double result)
        {
            return Math.Round(result, 2);
        }

    }
}
