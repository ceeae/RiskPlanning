using CalcoloRischioResiduo.RiskAssessment.Common;
using Xunit;
using FluentAssertions;

namespace UnitTests.Common
{
    public class TubleUnitTests
    {

        [Fact]
        public void AlwaysReturnTrue()
        {
            Tuble T1 = new Tuble(new double[2] { 1.0, 1.0 });

            Tuble T5 = new Tuble(new double[2] { 5.0, 5.0 });

            Tuble result = T1/T5;

            result[0].Should().Be(0.2);

        }
    }
}
