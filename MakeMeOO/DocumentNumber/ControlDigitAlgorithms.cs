using System.Collections.Generic;

namespace MakeMeOO
{
    internal static class ControlDigitAlgorithms
    {
        public static ControlDigitAlgorithm ForAccountingDepartment =>
            new ControlDigitAlgorithm(x => x.DigitsFromHighest(), MultiplyingFactors, 7);

        public static ControlDigitAlgorithm ForSalesDepartment =>
            new ControlDigitAlgorithm(x => x.DigitsFromLowest(), MultiplyingFactors, 9);

        private static IEnumerable<int> MultiplyingFactors
        {
            get
            {
                int factor = 3;
                while (true)
                {
                    yield return 3;
                    factor = 4 - factor;
                }
            }
        }
    }
}