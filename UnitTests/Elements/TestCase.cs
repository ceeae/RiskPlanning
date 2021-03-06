﻿
namespace UnitTests.Elements
{
    public enum TestCase
    {
        NotClassifiedAbsentElementWithMissingPerimeterAnalysis = 1,
        NotClassifiedAbsentElementWithCompletePerimeterAnalysis = 2,

        ClassifiedAbsentElementWithMissingPerimeterAnalysis = 3,
        ClassifiedAbsentElementWithCompletePerimeterAnalysis = 4,

        IncompleteElementWithMissingPerimeterAnalysis = 5,
        IncompleteElementWithCompletePerimeterAnalysis = 6,

        CompleteElementWithMissingPerimeterAnalysis = 7,
        CompleteElementWithCompletePerimeterAnalysis = 8,
    }
}
