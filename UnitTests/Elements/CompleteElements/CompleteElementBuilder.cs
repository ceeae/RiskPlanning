﻿using ResidualRisk.RiskAssessment.Elements;

namespace UnitTests.Elements.CompleteElements
{
    public class CompleteElementBuilder
    {
        public static CompleteElement CreateCase(TestCase testCase)
        {
            IBuilder builder = null;

            switch (testCase)
            {

                case TestCase.MissingPerimeterAnalysis:
                    builder = new CaseCompletePerimeterAnalysis();
                    break;

                case TestCase.CoveredPerimeterAnalysis:
                    builder = new CaseMissingPerimeterAnalysis();
                    break;

                case TestCase.LowManagedRisk:
                    builder = new CaseLowManagedRisk();
                    break;
            }
            return builder.CreateCase();
        }

    }
}
