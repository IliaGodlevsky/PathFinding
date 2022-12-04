using NUnit.Framework;
using Shared.Primitives.ValueRange;
using Shared.Random.Realizations;
using System.Collections;

namespace Shared.Random.Tests
{
    internal static class NextIntMethodTestCaseData
    {
        private const string PseudoRandom = "PseudoRandom";
        private const string KnuthRandom = "KnuthRandom";
        private const string CryptoRandom = "CryptoRandom";

        public static IEnumerable Data
        {
            get
            {
                yield return GenerateTestCaseData(new PseudoRandom(), 5000, 0, 100, PseudoRandom);
                yield return GenerateTestCaseData(new PseudoRandom(), 100000, 0, 450, PseudoRandom);
                yield return GenerateTestCaseData(new PseudoRandom(), 1000000, 0, int.MaxValue, PseudoRandom);
                yield return GenerateTestCaseData(new KnuthRandom(), 5000, 0, 500, KnuthRandom);
                yield return GenerateTestCaseData(new KnuthRandom(), 50000, int.MinValue, int.MaxValue, KnuthRandom);
                yield return GenerateTestCaseData(new KnuthRandom(), 500000, 50, 100, KnuthRandom);
                yield return GenerateTestCaseData(new CryptoRandom(), 5000, 0, 100000, CryptoRandom);
                yield return GenerateTestCaseData(new CryptoRandom(), 50000, 2, 5, CryptoRandom);
                yield return GenerateTestCaseData(new CryptoRandom(), 250000, 10, 20, CryptoRandom);
            }
        }

        private static TestCaseData GenerateTestCaseData(IRandom random, int iterations, int upperValueOfRange, int lowerValueOfRange, string generatorName)
        {
            var range = new InclusiveValueRange<int>(upperValueOfRange, lowerValueOfRange);
            return new TestCaseData(random, iterations, range)
                .SetArgDisplayNames(generatorName, $"Iterations: {iterations}", $"Range: {range}");
        }
    }
}
