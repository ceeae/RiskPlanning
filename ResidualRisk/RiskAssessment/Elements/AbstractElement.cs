using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Analysis;
using ResidualRisk.RiskAssessment.Exceptions;

namespace ResidualRisk.RiskAssessment.Elements
{
    public abstract class AbstractElement : IElement
    {

        protected readonly ElementTypes classification = ElementTypes.NotClassified;

        protected PerimeterType Perimeter { get; }

        protected readonly RiskPlanningVCI vci;

        protected readonly RiskPlanningPDS pds;

        protected readonly PerimetersAnalysis perimeters;

        #region constructors
        
        protected AbstractElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters)  // Not classified
        {
            Perimeter = elperimeter;
            if (elperimeters == null)
            {
                throw new InvalidNullArgumentException();
            }
            perimeters = elperimeters;
        }

        protected AbstractElement(ElementTypes elclassification, PerimeterType elperimeter, PerimetersAnalysis elperimeters) // For classified elements
            : this (elperimeter, elperimeters)
        {
            classification = elclassification;
        }

        protected AbstractElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters, RiskPlanningVCI elvci)
            : this(ElementTypes.Classified, elperimeter, elperimeters)
        {
            if (elvci == null)
            {
                throw new InvalidNullArgumentException();
            }
            vci = elvci;
        }

        protected AbstractElement(PerimeterType elperimeter, PerimetersAnalysis elperimeters, RiskPlanningVCI elvci, RiskPlanningPDS elpds)
            : this(elperimeter, elperimeters, elvci)
        {
            if (elpds == null)
            {
                throw new InvalidNullArgumentException();
            }
            pds = elpds;
        }

        #endregion

        public bool HasPerimeterAnalysis()
        {
            if (perimeters == null) return false;

            Perimeter perimeter = TakeAssociatedPerimeter();
            return perimeter?.IsAnalyzed() ?? false;
        }

        public Perimeter TakeAssociatedPerimeter()
        {
            return perimeters.FindByType(Perimeter);
        }

        public virtual double GetResidualRisk()
        {
            if (HasPerimeterAnalysis())
            {
                return TakeAssociatedPerimeter().GetResidualRiskEstimate(classification);
            }

            return RiskPlanningVCI.VCI_MAX;
        }
    }
}
