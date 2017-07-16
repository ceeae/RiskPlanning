using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{

    public class CompleteElement : AbstractElement
    {

        public CompleteElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters, RPvci vci, RPpds pds) // Classified by default
            : base(elperimeter, elperimeters, vci, pds)
        {

        }

        public void SetVEF(double vef)
        {
            pds.SetVEF(vef);    
        }

        public override double GetResidualRisk()
        {
            return GetResidualRiskBIA() + GetResidualRiskCOMPL();
        }

        public double GetResidualRiskBIA()
        {
            return vci.GetPotentialRiskBIA() * pds.GetResidualRiskBIAFactor() / pds.GetPRBiaTotal();
        }

        public double GetResidualRiskCOMPL()
        {
            return vci.GetPotentialRiskCOMPLIANCE() * pds.GetResidualRiskCOMPLFactor() / pds.GetPRCOMPLTotal();
        }

        public double GetManagedRisk()
        {
            return GetManagedRiskBIA() + GetManagedRiskCOMPL();
        }

        public double GetManagedRiskBIA()
        {
            return vci.GetPotentialRiskBIA() * pds.GetManagedRiskBIAFactor() / pds.GetPRBiaTotal();
        }

        public double GetManagedRiskCOMPL()
        {
            return vci.GetPotentialRiskCOMPLIANCE() * pds.GetManagedRiskCOMPLFactor() / pds.GetPRCOMPLTotal();
        }

        public Dictionary<long, double[]> GetPotentialRiskDistributionFactors()
        {
            return pds.GetPotentialRiskDistributionFactors();
        }

    }
}
