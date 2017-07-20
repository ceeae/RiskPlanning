using ResidualRisk.FunctionalPerimeters;

namespace ResidualRisk.RiskAssessment.Elements
{
    public class AbsentElement : AbstractElement
    {
        public AbsentElement(PerimeterType perimeterType, PerimetersAnalysis perimeters) 
            : base(ElementTypes.Classified, perimeterType, perimeters)
        {

        }
    }
}
