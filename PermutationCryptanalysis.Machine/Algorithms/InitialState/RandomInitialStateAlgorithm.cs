using System;

namespace PermutationCryptanalysis.Machine.Algorithms.InitialState
{
	public class RandomInitialStateAlgorithm : IInitialStateAlgorithm
	{
		private readonly Random _random = new();

		public int GetInitialState(int m)
		{
			return _random.Next(m);
		}
	}
}