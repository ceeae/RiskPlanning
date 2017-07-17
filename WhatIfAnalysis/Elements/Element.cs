using WhatIfAnalysis.CoverageAnalysis;

namespace WhatIfAnalysis.Elements
{
    public class Element
    {

        private int _pr = 0;

        public long Id { get; }

        public int PR => _pr;

        public int MR { get;  }

        public ElementVCIClass VCIClass = ElementVCIClass.C3;

        public Element(long id, int prvalue, int mrvalue)
        {
            _pr = prvalue;
            MR = mrvalue;
            Id = id;

            if (prvalue < 250)
            {
                VCIClass = ElementVCIClass.C1;

            } else if (prvalue >= 250 && prvalue < 400)
            {
                VCIClass = ElementVCIClass.C2;
            }
        }

        public void SetPRAsEstimate(int value)
        {
            _pr = value;
        }

        public ElementType GetType()
        {
            if (PR == 0)
            {
                return ElementType.Absent;

            }
            else if (PR != 0 && MR == 0)
            {
                return ElementType.Incomplete;
            }

            return ElementType.Complete;
        }

    }
}
