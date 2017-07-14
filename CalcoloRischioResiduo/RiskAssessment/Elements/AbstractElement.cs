using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public abstract class AbstractElement : IElement
    {
        protected ElementTypes classification = ElementTypes.NotClassified;
        protected SlimVCI _vci = null;
        protected SlimPDS _pds = null;

        protected PerimetersAnalysis _perimeters = null;

        protected Types Perimeter { get; set; }

        #region constructors

        public AbstractElement()
        {
            Initialize(ElementTypes.NotClassified, null, null);
        }

        public AbstractElement(ElementTypes classification)
        {
            Initialize(classification, null, null);
        }

        public AbstractElement(SlimVCI vci)
        {
            Initialize(ElementTypes.Classified, vci, null);
        }

        public AbstractElement(SlimVCI vci, SlimPDS pds)
        {
            Initialize(ElementTypes.Classified, vci, pds);
        }

        private void Initialize(ElementTypes type, SlimVCI vci, SlimPDS pds)
        {
            classification = type;
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
            return classification == ElementTypes.Classified;
        }

        public abstract double EstimateResidualRisk();

    }
}
