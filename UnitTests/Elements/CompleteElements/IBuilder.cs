using ResidualRisk.FunctionalPerimeters;
using ResidualRisk.RiskAssessment.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Elements.CompleteElements
{
    public interface IBuilder
    {
        CompleteElement CreateCase();
    }
}
