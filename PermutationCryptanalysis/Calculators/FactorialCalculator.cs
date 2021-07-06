using System.Collections.Generic;

namespace PermutationCryptanalysis.Calculators
{
	public class FactorialCalculator
	{
		private static readonly Dictionary<long, long> Cache = new();
		
		public static long Calculate(long n)
		{
			if (n is 0 or 1)
			{
				return 1;
			}
			
			if (Cache.ContainsKey(n))
			{
				return Cache[n];
			}

			checked
			{
				long res = n * Calculate(n - 1);
			
				Cache.Add(n, res);

				return res;
			}
		}
	}
}