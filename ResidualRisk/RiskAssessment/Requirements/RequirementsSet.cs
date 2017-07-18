using System;
using System.Collections.Generic;
using System.Linq;
using ResidualRisk.RiskAssessment.Common;

namespace ResidualRisk.RiskAssessment.Requirements
{
    public class RequirementsSet : List<Requirement>
    {

        // Distribution Factors (for each requirement):
        // long:        Id;
        // double[4]: PotentialRiskBIA, PotentialRiskBIAID, PotentialRiskCOMPL, VEF

        #region Properties Potential Risk Factors Totals (calculated - BIA, BIAID, COMPL - format 00.0)

        public List<int> Totals { get; private set; }

        public Dictionary<long, double[]> Distribution { get; private set; }

        public double TotalPotentialRiskBIA { get; private set; } = 0;

        public double TotalPotentialRiskBIAID { get; private set; } = 0;

        public double TotalPotentialRiskCOMPL { get; private set; } = 0;

        public double ManagedRiskBIA { get; private set; } = 0;

        public double ManagedRiskCOMPL { get; private set; } = 0;

        public double ResidualRiskBIA { get; private set; } = 0;

        public double ResidualRiskCOMPL { get; private set; } = 0;

        public double VEF { get; set; }

        #endregion calculated factors

        public void AddRequirement(int id, double pas, double alpha, bool adequate, int[] weights)
        {
            Requirement requirement = new Requirement(id, new WeightFraction(pas), new CorrectionFactor(alpha), adequate, weights);

            Add(requirement);

        }

        public void CalculateRisk()
        {
            ResetVariables();

            CalculateWeightsTotals();

            CalculateTotalPotentialRisk();

            CalculatePotentialRiskDistribution();

            CalculateManagedRisk();

            CalculateResidualRisk();
        }

        private void ResetVariables()
        {
            Totals = Enumerable.Repeat(0, Requirement.WEIGHTS_NUM).ToList();

            TotalPotentialRiskBIAID = 0;

            TotalPotentialRiskBIA = 0;

            TotalPotentialRiskCOMPL = 0;

            Distribution = new Dictionary<long, double[]>();

        }

        private void CalculateWeightsTotals()
        {
            foreach (var req in this)
            {
                Totals += req.Weights; // Use +operator: List<int> = List<int> + Weights
            }
        }

        private void CalculateTotalPotentialRisk()
        {
            foreach (var requirement in this)
            {
                requirement.CalculatePotentialRisk(Totals);

                TotalPotentialRiskBIA += requirement.PotentialRiskBIA;

                TotalPotentialRiskBIAID += requirement.PotentialRiskBIAID;

                TotalPotentialRiskCOMPL += requirement.PotentialRiskCOMPL;
            }
        }

        // Distribution Factors (for each requirement):
        // long:        Id;
        // double[4]: PotentialRiskBIA, PotentialRiskBIAID, PotentialRiskCOMPL, VEF

        private void CalculatePotentialRiskDistribution()
        {
            foreach (var requirement in this)
            {
                Distribution.Add(requirement.Id, new double[4]
                {
                    requirement.PotentialRiskBIA,
                    requirement.PotentialRiskBIAID,
                    requirement.PotentialRiskCOMPL,
                    requirement.PotentialRiskBIAID * VEF / TotalPotentialRiskBIAID
                });
                    
            }
        }

        private void CalculateManagedRisk()
        {
            ManagedRiskBIA = this.Where(req => req.Adequate).Sum(req => req.PotentialRiskBIA);

            ManagedRiskCOMPL = this.Where(req => req.Adequate).Sum(req => req.PotentialRiskCOMPL);
        }

        private void CalculateResidualRisk()
        {
            ResidualRiskBIA = this.Where(req => !req.Adequate).Sum(req => req.PotentialRiskBIA);

            ResidualRiskCOMPL = this.Where(req => !req.Adequate).Sum(req => req.PotentialRiskCOMPL);
        }
    }
}
