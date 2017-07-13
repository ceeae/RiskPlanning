using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public abstract class AbstractElement : IElement
    {
        protected bool _classified = false;
        protected SlimVCI _vci = null;
        protected SlimPDS _pds = null;

        protected PerimetersAnalysis _perimeters = null;

        protected Types Perimeter { get; set; }

        #region constructors

        public AbstractElement()
        {
            Initialize(false, null, null);
        }

        public AbstractElement(bool isClassified)
        {
            Initialize(isClassified, null, null);
        }

        public AbstractElement(SlimVCI vci)
        {
            Initialize(true, vci, null);
        }

        public AbstractElement(SlimVCI vci, SlimPDS pds)
        {
            Initialize(true, vci, pds);
        }

        private void Initialize(bool isClassified, SlimVCI vci, SlimPDS pds)
        {
            _classified = isClassified;
            _vci = vci;
            _pds = pds;
        }

        #endregion

        public void AssociateWith(PerimetersAnalysis perimeters)
        {
            _perimeters = perimeters;
        }

        public bool IsAssociated()
        {
            return _perimeters != null;
        }

        public bool BelongsToAnalyzedPerimeter()
        {
            if (!IsAssociated()) return false;

            Perimeter perimeter = GetAssociatedPerimeter();
            return perimeter?.IsAnalyzed() ?? false;
        }

        public Perimeter GetAssociatedPerimeter()
        {
            return _perimeters.FindByType(Perimeter);
        }

        public bool IsClassified()
        {
            return _classified;
        }

        public abstract double EstimateResidualRisk();

    }
}
