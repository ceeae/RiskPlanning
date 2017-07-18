using System;
using System.Collections.Generic;
using System.Linq;
using ResidualRisk.RiskAssessment.Common;

namespace ResidualRisk.RiskAssessment.Requirements
{
    public class RequirementsSet : List<Requirement>
    {

        #region private variables

        private List<int> _totals;

        // Distribution Factors (for each requirement):
        // long:        Id;
        // double[4]: PotentialRiskBIA, PotentialRiskBIAID, PotentialRiskCOMPL, VEF

        private Dictionary<long, double[]> _distribution;

        private bool _totalCalculationIsNeeded = true;

        private bool _distributionCalculationIsNeeded = true;

        #endregion

        #region Properties Potential Risk Factors Totals (calculated - BIA, BIAID, COMPL - format 00.0)

        public List<int> Totals => _totals;

        public double TotalPotentialRiskBIA { get; private set; } = 0;

        public double TotalPotentialRiskBIAID { get; private set; } = 0;

        public double TotalPotentialRiskCOMPL { get; private set; } = 0;

        public double VEF { get; set; }

        #endregion calculated factors

        public void AddRequirement(int id, double pas, double alpha, bool adequate, int[] weights)
        {
            Requirement requirement = new Requirement(id, new FractionWeight(pas), new CorrectionFactor(alpha), adequate, weights);

            Add(requirement);

            _totalCalculationIsNeeded = true;
            _distributionCalculationIsNeeded = true;
        }


        private void CalculateWeightsTotals()
        {
            if (!TotalCalculationIsNeeded()) return;

            ResetTotals();

            SumRequirementsWeights();

        }

        private bool TotalCalculationIsNeeded()
        {
            return _totalCalculationIsNeeded || _totals == null;
        }

        private void SumRequirementsWeights()
        {
            foreach (var req in this)
            {
                _totals += req.Weights; // Use +operator: List<int> = List<int> + Weights
            }
        }

        private void ResetTotals()
        {
            _totals = Enumerable.Repeat(0, Requirement.WEIGHTS_NUM).ToList();

            _totalCalculationIsNeeded = false;
        }
        

        // Distribution Factors (for each requirement):
        // long:        Id;
        // double[4]: PotentialRiskBIA, PotentialRiskBIAID, PotentialRiskCOMPL, VEF

        public Dictionary<long, double[]> GetPotentialRiskDistribution()
        {
            if (!DistributionCalculationIsNeeded()) return _distribution;
 
            ResetTotalPotentialRiskAndDistributionVariables();

            CalculateWeightsTotals();

            CalculateTotalPotentialRiskAndDistribution();

            return _distribution;
        }

        private void CalculateTotalPotentialRiskAndDistribution()
        {
            foreach (var requirement in this)
            {
                requirement.CalculatePotentialRiskFactors(_totals);

                _distribution.Add(requirement.Id, new double[4]
                {
                    requirement.PotentialRiskBIA, requirement.PotentialRiskBIAID, requirement.PotentialRiskCOMPL, VEF*requirement.PotentialRiskBIAID
                });
                    
                // Potential Risk BIA, BIAID, COMPL factors

                TotalPotentialRiskBIA += requirement.PotentialRiskBIA;

                TotalPotentialRiskBIAID += requirement.PotentialRiskBIAID;

                TotalPotentialRiskCOMPL += requirement.PotentialRiskCOMPL;
            }
        }

        private void ResetTotalPotentialRiskAndDistributionVariables()
        {
            TotalPotentialRiskBIAID = 0;

            TotalPotentialRiskBIA = 0;

            TotalPotentialRiskCOMPL = 0;

            _distribution = new Dictionary<long, double[]>();

            _distributionCalculationIsNeeded = false;
        }

        private bool DistributionCalculationIsNeeded()
        {
            return _distributionCalculationIsNeeded || _distribution == null;
        }
        
        public double GetManagedRiskBIA()
        {
            GetPotentialRiskDistribution();

            return this.Where(req => req.Adequate).Sum(req => req.PotentialRiskBIA);
        }

        public double GetManagedRiskCOMPL()
        {
            GetPotentialRiskDistribution();

            return this.Where(req => req.Adequate).Sum(req => req.PotentialRiskCOMPL);
        }

        public double GetResidualRiskBIA()
        {
            GetPotentialRiskDistribution();

            return this.Where(req => !req.Adequate).Sum(req => req.PotentialRiskBIA);
        }

        public double GetResidualRiskCOMPL()
        {
            GetPotentialRiskDistribution();

            return this.Where(req => !req.Adequate).Sum(req => req.PotentialRiskCOMPL);
        }

    }
}
