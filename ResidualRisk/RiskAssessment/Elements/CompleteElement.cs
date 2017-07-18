﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Analysis;

namespace ResidualRisk.RiskAssessment.Elements
{

    public class CompleteElement : AbstractElement
    {

        public CompleteElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters, RiskPlanningVCI vci, RiskPlanningPDS pds) // Classified by default
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
            return vci.GetPotentialRiskBIA() * pds.GetResidualRiskBIAFactor() / pds.GetPotentialRiskBIATotal();
        }

        public double GetResidualRiskCOMPL()
        {
            return vci.GetPotentialRiskCOMPLIANCE() * pds.GetResidualRiskCOMPLFactor() / pds.GetPotentialRiskCOMPLTotal();
        }

        public double GetManagedRisk()
        {
            return GetManagedRiskBIA() + GetManagedRiskCOMPL();
        }

        public double GetManagedRiskBIA()
        {
            return vci.GetPotentialRiskBIA() * pds.GetManagedRiskBIAFactor() / pds.GetPotentialRiskBIATotal();
        }

        public double GetManagedRiskCOMPL()
        {
            return vci.GetPotentialRiskCOMPLIANCE() * pds.GetManagedRiskCOMPLFactor() / pds.GetPotentialRiskCOMPLTotal();
        }

        public Dictionary<long, double[]> GetPotentialRiskDistributionFactors()
        {
            return pds.GetPotentialRiskDistributionFactors();
        }

    }
}
