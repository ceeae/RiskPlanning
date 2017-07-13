﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment.Analysis;

namespace CalcoloRischioResiduo.RiskAssessment.Elements
{
    public class NotClassifiedElement : Element
    {
        public NotClassifiedElement(Types perimeter, PerimetersAnalysis perimeters) : base()
        {
            this.Perimeter = perimeter;
            this.AssociateWith(perimeters);
        }

        public override double EstimateResidualRisk()
        {
            if (BelongsToAnalyzedPerimeter())
            {
                return GetAssociatedPerimeter().EstimatedResidualRisk(IsClassified());
            }

            return SlimVCI.VCIMAX;
        }
    }
}
