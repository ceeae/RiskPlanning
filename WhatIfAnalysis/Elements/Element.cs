namespace WhatIfAnalysis.Elements
{
    public class Element
    {
        public long Id { get; }

        public int PotentialRisk { get; private set; } = 0;

        public VCIClass VciClass;

        public int ManagedRisk { get;  }

        public Element(long id, int potentialRisk, int managedRisk)
        {
            Id = id;

            PotentialRisk = potentialRisk;

            ManagedRisk = managedRisk;

            VciClass = GetVCIClass(potentialRisk);
        }

        public void SetPotentialRisk(int newPotentialRisk)
        {
            PotentialRisk = newPotentialRisk;
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

        private static VCIClass GetVCIClass(int potentialRisk)
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

    }
}
