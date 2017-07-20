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
        
        protected AbstractElement(PerimeterType perimeterType, PerimetersAnalysis perimeters)  // Not classified
        {
            Perimeter = perimeterType;
            if (perimeters == null)
            {
                throw new InvalidNullArgumentException();
            }
            this.perimeters = perimeters;
        }

        protected AbstractElement(ElementTypes elclassification, PerimeterType perimeterType, PerimetersAnalysis perimeters) // For classified elements
            : this (perimeterType, perimeters)
        {
            classification = elclassification;
        }

        protected AbstractElement(PerimeterType perimeterType, PerimetersAnalysis perimeters, RiskPlanningVCI elvci)
            : this(ElementTypes.Classified, perimeterType, perimeters)
        {
            if (elvci == null)
            {
                throw new InvalidNullArgumentException();
            }
            vci = elvci;
        }

        protected AbstractElement(PerimeterType perimeterType, PerimetersAnalysis perimeters, RiskPlanningVCI elvci, RiskPlanningPDS elpds)
            : this(perimeterType, perimeters, elvci)
        {
            if (elpds == null)
            {
                throw new InvalidNullArgumentException();
            }
            pds = elpds;
        }

        #endregion

        public bool IsAssociatedToAnalyzedPerimeter()
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
            if (IsAssociatedToAnalyzedPerimeter())
            {
                return TakeAssociatedPerimeter().GetResidualRiskEstimate(classification);
            }

            return RiskPlanningVCI.VCI_MAX;
        }
    }
}
