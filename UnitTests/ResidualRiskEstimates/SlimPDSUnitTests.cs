using CalcoloRischioResiduo.RiskAssessment.Analysis;
using CalcoloRischioResiduo.RiskAssessment.Requirements;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;

using Xunit;
using FluentAssertions;

namespace UnitTests.ResidualRiskEstimates
{
    public class SlimPDSUnitTests
    {
        public SlimPDSUnitTests()
        {
            
        }

        [Fact]
        public void NewSlimPDS_WithoutRequirements_RaisesException()
        {
            ExtentedRequirements requirements = null;
            SlimPDS pds = new SlimPDS(requirements);

            Assert.Throws<MissingRequirementsException>(() => pds.GetResidualRiskValue());
        }


    }
}
