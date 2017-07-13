using System;
using System.Collections.Generic;
using System.Linq;


namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class ExtentedRequirements : List<ExtendedRequirement>
    {

        public double CalculatePotentialRiskBIAFactor()
        {
            BIAValues biaTotals = GetBIATotal();
            COMPLIANCEValues complTotals = GetCOMPLIANCETotal();

            double RPbia = 0;

            foreach (var extreq in this)
            {
                extreq.CalculateBIAPotentialRisk(biaTotals, complTotals);
                RPbia += extreq.RPbia;
            }

            return Math.Round(RPbia, 1);
        }

        public double CalculatePotentialRiskBIAIDFactor()
        {
            BIAValues biaTotals = GetBIATotal();
            COMPLIANCEValues complTotals = GetCOMPLIANCETotal();

            double RPbiaID = 0;

            foreach (var extreq in this)
            {
                extreq.CalculateBIAPotentialRisk(biaTotals, complTotals);
                RPbiaID += extreq.RPbiaID;
            }

            return Math.Round(RPbiaID, 1);
        }

        public double CalculatePotentialRiskCOMPLIANCEFactor()
        {
            BIAValues biaTotals = GetBIATotal();
            COMPLIANCEValues complTotals = GetCOMPLIANCETotal();

            double RPcompl = 0;

            foreach (var extreq in this)
            {
                extreq.CalculateCOMPLIANCEPotentialRisk(biaTotals, complTotals);
                RPcompl += extreq.RPcompl;
            }

            return Math.Round(RPcompl, 1);
        }

        public COMPLIANCEValues GetCOMPLIANCETotal()
        {
            int[] totals = null;

            foreach (var extreq in this)
            {
                if (totals == null)
                {
                    totals = extreq.complianceWeights;
                }
                else
                {
                    // Sum totals
                    totals = (int[]) totals.Zip(extreq.complianceWeights, (x, y) => x + y).ToArray().Clone();
                }
            }
            return new COMPLIANCEValues(totals);
        }

        public BIAValues GetBIATotal()
        {
            int r = 0;
            int d = 0;
            int i = 0;

            foreach (var extreq in this)
            {
                r += extreq.r;
                i += extreq.i;
                d += extreq.d;
            }

            return new BIAValues(r, i, d);
        }

        public Dictionary<long, double> GetRequirementsRiskPotentialBIA()
        {
            double dummy = CalculatePotentialRiskBIAFactor();

            Dictionary<long, double> result = new Dictionary<long, double>();
            foreach (var extreq in this)
            {
                result.Add(extreq.LibraryId(), Math.Round(extreq.RPbia, 1));
            }

            return result;
        }
        
        public Dictionary<long, double> GetRequirementsRiskPotentialBIAID()
        {
            double dummy = CalculatePotentialRiskBIAFactor();

            Dictionary<long, double> result = new Dictionary<long, double>();
            foreach (var extreq in this)
            {
                result.Add(extreq.LibraryId(), Math.Round(extreq.RPbiaID, 1));
            }

            return result;
        }

        public Dictionary<long, double> GetRequirementsRiskPotentialCOMPLIANCE()
        {
            double dummy = CalculatePotentialRiskCOMPLIANCEFactor();

            Dictionary<long, double> result = new Dictionary<long, double>();
            foreach (var extreq in this)
            {
                result.Add(extreq.LibraryId(), Math.Round(extreq.RPcompl, 1));
            }

            return result;
        }
    }
}
