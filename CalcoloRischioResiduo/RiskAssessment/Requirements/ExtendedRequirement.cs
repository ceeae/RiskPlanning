using System;
using System.Data;
using System.Linq;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class ExtendedRequirement : AbstractSlimRequirement
    {

        #region BIA
        
        // RID weights (3) weights 1..5
        private int _r = 1;
        private int _i = 1;
        private int _d = 1;

        public int r { get { return _r; } }
        public int d { get { return _d; } }
        public int i { get { return _i; } }

        // RID Potential Risk BIA factor
        private double rRPbia = 0.0;
        private double iRPbia = 0.0;
        private double dRPbia = 0.0;

        public double RPbia { get; private set; }

        public double RPbiaID { get; private set; }

        #endregion BIA

        #region COMPLIANCE

        // COMPLIANCE weights (35) weights 1..5
        private int[] _complianceWeights;

        public int[] complianceWeights { get { return _complianceWeights; } }

        // COMPLIANCE Potential Risk COMPLIANCE factor
        public double[] complianceWeightsRP;

        public double RPcompl { get; private set; }

        #endregion COMPLIANCE

        public ExtendedRequirement(long libraryID, double PAS, double Alpha) : base(libraryID, PAS, Alpha)
        {
            _complianceWeights = (int[]) Enumerable.Repeat(1, 35).ToArray();
            complianceWeightsRP = (double[]) Enumerable.Repeat(0.0, 35).ToArray();
        }

        public void SetRIDWeights(int rvalue, int ivalue, int dvalue)
        {
            _r = rvalue;
            _i = ivalue;
            _d = dvalue;
        }

        public void SetComplianceWeights(COMPLIANCEValues compliance)
        {
            _complianceWeights = compliance.values;
        }

        public void SetComplianceWeight(int index, int value)
        {
            CheckIndexValueRange(index);
            CheckWeightValueRange(value);

            _complianceWeights[index - 1] = value;
        }

        public int GetComplianceWeight(int index)
        {
            CheckIndexValueRange(index);
            return _complianceWeights[index - 1];
        }


        public void CalculateBIAPotentialRisk(BIAValues biaTotals, COMPLIANCEValues complTotals)
        {
            rRPbia = (double) r / biaTotals.r;
            iRPbia = (double) i / biaTotals.i;
            dRPbia = (double) d / biaTotals.d;

            RPbia = (rRPbia + iRPbia + dRPbia) * (GetPAS() + GetAlpha());
            RPbiaID = (iRPbia + dRPbia)*(GetPAS() + GetAlpha());
        }

        public void CalculateCOMPLIANCEPotentialRisk(BIAValues biaTotals, COMPLIANCEValues complTotals)
        {
            int i;
            for (i = 0; i < complianceWeightsRP.Length; i++)
            {
                complianceWeightsRP[i] = (double) _complianceWeights[i]/complTotals.values[i];
            }

            RPcompl = complianceWeightsRP.Sum() * (GetPAS() + GetAlpha());
        }

        private void CheckIndexValueRange(int index)
        {
            if (index > _complianceWeights.Length || index <= 0)
            {
                throw new OutOfRangeIndexException();
            }
        }

        private void CheckWeightValueRange(int value)
        {
            if (value <= 0 || value >= 6)
            {
                throw new WrongWeightValueException();
            }

        }
    }
}
