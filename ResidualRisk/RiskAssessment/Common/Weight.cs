using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.RiskAssessment.Common;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;

namespace CalcoloRischioResiduo.RiskAssessment.Common
{
    public class Weight
    {
        public static int MIN = 1;
        public static int MAX = 5;

        public int Value { get; }

        public Weight(int value)
        {
            if (value < MIN || value > MAX)
            {
                throw new InvalidWeightValueException();
            }

            Value = value;
        }
    }
}

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class DefaultWeight : Weight
    {
        public DefaultWeight() : base(1)
        {
            
        }
    }
}
