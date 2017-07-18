namespace WhatIfAnalysis.Elements
{
    public class Element
    {

        private int _potentialRisk = 0;

        public long Id { get; }

        public int PotentialRisk => _potentialRisk;

        public int ManagedRisk { get;  }

        public VCIClass VciClass;

        public Element(long id, int potentialRisk, int managedRisk)
        {
            Id = id;
            _potentialRisk = potentialRisk;
            ManagedRisk = managedRisk;
            VciClass = getVciClass(potentialRisk);
        }

        private VCIClass getVciClass(int potentialRisk)
        {
            if (potentialRisk < 250)
            {
                return VCIClass.C1;

            }
            else if (potentialRisk >= 250 && potentialRisk < 400)
            {
                return VCIClass.C2;
            }
            return VCIClass.C3;
        }

        public void SetPotentialRisk(int newPotentialRisk)
        {
            _potentialRisk = newPotentialRisk;
        }

        public ElementType GetElementType()
        {
            if (PotentialRisk == 0)
            {
                return ElementType.Absent;

            }
            else if (PotentialRisk != 0 && ManagedRisk == 0)
            {
                return ElementType.Incomplete;
            }

            return ElementType.Complete;
        }

    }
}
