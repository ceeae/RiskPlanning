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

        public CompleteElement(Types elperimeter, PerimetersAnalysis elperimeters, RPvci vci, RPpds pds) // Classified by default
            : base(elperimeter, elperimeters, vci, pds)
        {

        }

        public override double GetResidualRisk()
        {
            return MathRound0(
                GetResidualRiskBIA() + GetResidualRiskCOMPL()
                );
        }

        public int GetResidualRiskBIA()
        {
            return MathRound0(
                    vci.GetPotentialRiskBIA() * pds.GetResidualRiskBIAFactor() / pds.GetPRBiaTotal()
                );
        }

        public int GetResidualRiskCOMPL()
        {
            return MathRound0(
                    vci.GetPotentialRiskCOMPLIANCE() * pds.GetResidualRiskCOMPLFactor() / pds.GetPRCOMPLTotal()
                );
        }

        public int GetManagedRisk()
        {
            return MathRound0(
                    GetManagedRiskBIA() + GetManagedRiskCOMPL()
                );
        }

        public int GetManagedRiskBIA()
        {
            return MathRound0(
                    vci.GetPotentialRiskBIA() * pds.GetManagedRiskBIAFactor() / pds.GetPRBiaTotal()
                );
        }

        public int GetManagedRiskCOMPL()
        {
            return MathRound0( 
                vci.GetPotentialRiskCOMPLIANCE() * pds.GetManagedRiskCOMPLFactor() / pds.GetPRCOMPLTotal()
                );
        }

        public static int MathRound0(double result)
        {
            return (int) Math.Round(result, 0);
        }

    }
}
