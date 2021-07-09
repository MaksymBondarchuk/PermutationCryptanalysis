using System.Numerics;

namespace PermutationCryptanalysis.Calculators
{
	public static class MainBigCalculator
	{
		public static BigInteger Pow(int x, int y)
		{
			BigInteger res = x;
			for (var i = 1; i < y; i++)
			{
				res *= x;
			}

			return res;
		}
	}
}