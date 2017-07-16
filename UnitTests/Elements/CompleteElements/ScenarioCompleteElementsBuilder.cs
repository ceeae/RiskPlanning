using System.Resources;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Elements;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace UnitTests.Elements.CompleteElements
{
    public class ScenarioCompleteElementsBuilder
    {
        public static CompleteElement CreateFromScenario(CompleteScenarioTypes scenarioType)
        {
            IScenarioBuilder builder = null;

            switch (scenarioType)
            {

                case CompleteScenarioTypes.One:
                    builder = new ScenarioOne();
                    break;

                case CompleteScenarioTypes.Two:
                    builder = new ScenarioTwo();
                    break;
                  
            }
            return builder.CreateScenario();
        }

    }
}
