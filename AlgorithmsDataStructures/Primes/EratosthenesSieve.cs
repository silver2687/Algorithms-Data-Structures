using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures.Primes
{
    using PrimeType = System.Int32;

    public class EratosthenesSieve
    {
        private IList<PrimeType> GeneratePrimes(int upperBound)
        {
            var primes = new List<PrimeType>();
            var upperBoundRoot = (int)Math.Sqrt(upperBound);
            var isComposite = new bool[upperBound + 1];
            for (var m = 2; m <= upperBoundRoot; m++)
            {
                if (!isComposite[m])
                {
                    primes.Add(m);
                    for (var k = m * m; k <= upperBound; k += m)
                    {
                        isComposite[k] = true;
                    }
                }
            }
            for (var m = upperBoundRoot; m <= upperBound; m++)
            {
                if (!isComposite[m])
                {
                    primes.Add(m);
                }
            }
            return primes;
        }
    }
}
