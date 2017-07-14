using System.Collections.Generic;
using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Requirements;
using Xunit;
using FluentAssertions;

namespace UnitTests
{
    public class RequirementsSetUnitTests
    {

        [Fact]
        public void CreateNewSet_ValidSet_ExpectedTotals()
        {
            
            RequirementsSet set = new RequirementsSet();

            set.Append(101, 4.8, 2.0, new int[38] {3, 1, 2, 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,});
            set.Append(102, 3.2, 0.0, new int[38] {3, 1, 2, 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,});
            set.Append(103, 1.0, 0.0, new int[3]  {5, 4, 1});
            set.Append(104, 1.0, 0.0, new int[3]  {3, 4, 1});
            set.Append(105, 1.0, 0.0, new int[3]  {3, 3, 1});

            List<int> totals = set.Totals();

            totals.Should().Equal( new int[38]
            {
                17, 13, 7,
                13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13,
                13, 13, 13, 13, 13, 13, 13,
            });

        }


    }
}
