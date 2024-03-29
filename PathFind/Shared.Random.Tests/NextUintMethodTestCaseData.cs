﻿using NUnit.Framework;
using Shared.Random.Realizations;
using System.Collections;

namespace Shared.Random.Tests
{
    internal static class NextUintMethodTestCaseData
    {
        private const string PseudoRandom = "PseudoRandom";
        private const string KnuthRandom = "KnuthRandom";
        private const string CryptoRandom = "CryptoRandom";
        private const string XorRandom = "XorshiftRandom";

        public static IEnumerable Data
        {
            get
            {
                yield return GenerateTestCaseData(new CongruentialRandom(), 5000, 0, PseudoRandom);
                yield return GenerateTestCaseData(new CongruentialRandom(), 100000, 0, PseudoRandom);
                yield return GenerateTestCaseData(new CongruentialRandom(), 1000000, 0, PseudoRandom);
                yield return GenerateTestCaseData(new KnuthRandom(), 5000, 0, KnuthRandom);
                yield return GenerateTestCaseData(new KnuthRandom(), 50000, 5, KnuthRandom);
                yield return GenerateTestCaseData(new KnuthRandom(), 500000, 50, KnuthRandom);
                yield return GenerateTestCaseData(new CryptoRandom(), 5000, 0, CryptoRandom);
                yield return GenerateTestCaseData(new CryptoRandom(), 50000, 2, CryptoRandom);
                yield return GenerateTestCaseData(new CryptoRandom(), 250000, 15, CryptoRandom);
                yield return GenerateTestCaseData(new XorshiftRandom(), 5000, 0, XorRandom);
                yield return GenerateTestCaseData(new XorshiftRandom(), 100000, 0, XorRandom);
                yield return GenerateTestCaseData(new XorshiftRandom(), 1000000, 0, XorRandom);
            }
        }

        private static TestCaseData GenerateTestCaseData(IRandom random, int iterations, int tolerance, string generatorName)
        {
            return new TestCaseData(random, iterations, tolerance)
                .SetArgDisplayNames(generatorName, $"Iterations: {iterations}", $"Tolerance: {tolerance}");
        }
    }
}
