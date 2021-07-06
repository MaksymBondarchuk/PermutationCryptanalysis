namespace PermutationCryptanalysis.Calculators
{
	public static class MainCalculator
	{
		public static long Pow(int x, int y)
		{
			long res = x;
			for (var i = 1; i < y; i++)
			{
				checked
				{
					res *= x;
				}
			}

			return res;
		}
	}
}