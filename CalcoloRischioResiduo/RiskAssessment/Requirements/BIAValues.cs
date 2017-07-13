

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class BIAValues
    {
        public int r { get; }
        public int i { get; }
        public int d { get; }

        public BIAValues(int rvalue, int ivalue, int dvalue)
        {
            r = rvalue;
            i = ivalue;
            d = dvalue;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BIAValues tot = (BIAValues) obj;
            return tot.r == r && tot.d == d && tot.i == i;
        }
    }
}
