using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace silver2687.Algorithms.Primes
{
    using PrimeType = System.Int64;

    public class MillerRabinTest
    {
        public class Deterministic
        {
            public bool IsPrime(PrimeType N)
            {
                var factors = Power2Factorizer.Factorize(N - 1);
                var witnesses = WitnessProvider.GetWitnesses(N);
                foreach (var a in witnesses)
                {
                    var rSet = Enumerable.Range(0, factors.s).ToList();
                    if (rSet.All(r =>
                        BigInteger.ModPow(a, ((PrimeType)Math.Pow(2, r)) * factors.d, N) != N - 1 &&
                        BigInteger.ModPow(a, factors.d, N) != 1))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class Probabilistic
        {
            public bool IsPrime(PrimeType N, int accuracy)
            {
                var NMinus1 = N - 1;

                if ((NMinus1 % 6 != 0) && ((N + 1) % 6 != 0))
                {
                    return false;
                }

                var factors = Power2Factorizer.Factorize(NMinus1);
                var witnesses = WitnessProvider.GetRandomWitnesses(N, accuracy);
                for (var i = 0; i < accuracy; i++)
                {
                    var a = witnesses[i];
                    var x = BigInteger.ModPow(a, factors.d, N);
                    if (x == 1 || x == NMinus1)
                        continue;
                    for (var r = 1; r < factors.s; r++)
                    {
                        x = BigInteger.ModPow(x, 2, N);
                        if (x == NMinus1)
                            break;
                    }
                    if (x != NMinus1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        class Power2Factorizer
        {
            public static Factorization Factorize(PrimeType evenNumber)
            {
                int s = 0;
                var d = evenNumber;
                do
                {
                    s++;
                    d >>= 1;//d /= 2;
                } while ((d & 1) == 0);//(d % 2 == 0);
                return new Factorization(s, d);
            }
        }

        struct Factorization
        {
            public PrimeType d;
            public int s;

            public Factorization(int s, PrimeType d)
            {
                this.s = s;
                this.d = d;
            }
        }

        class WitnessProvider
        {
            public static int[] GetRandomWitnesses(PrimeType N, int k)
            {
                var rand = new Random();
                var max = N - 1 > int.MaxValue ? int.MaxValue : (int)(N - 1);
                var a = new int[k];
                for (var i = 0; i < k; i++)
                {
                    a[i] = rand.Next(2, max);
                }
                return a;
            }

            public static int[] GetWitnesses(PrimeType N)
            {
                foreach (var aSet in witnesses)
                {
                    if (N < aSet.Key)
                    {
                        return aSet.Value;
                    }
                }
                throw new ArgumentOutOfRangeException(nameof(N));
            }

            private static IDictionary<PrimeType, int[]> witnesses = new Dictionary<PrimeType, int[]>
            {
                { 2047, new[]{ 2 } },
                { 1373653, new[]{ 2, 3 } },
                { 9080191, new[]{ 31, 73 } },
                { 25326001, new[]{ 2, 3, 5 } },
                { 3215031751, new[]{ 2, 3, 5, 7 } },
                { 4759123141, new[]{ 2, 7, 61 } },
                { 1122004669633, new[]{ 2, 13, 23, 1662803 } }
            };
        }
    }
}
