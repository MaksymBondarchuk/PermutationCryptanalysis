using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis.Machine.Algorithms
{
	public class UniqueOutputRowsAlgorithm : IAlgorithm
	{
		private readonly Random _random = new();

		public int GetInitialState(int m)
		{
			return _random.Next(m);
		}

		public List<List<int>> GenerateStateMatrix(int m, int n)
		{
			var stateMatrix = new List<List<int>>();

			for (var i = 0; i < m; i++)
			{
				stateMatrix.Add(new List<int>());
				for (var j = 0; j < n; j++)
				{
					stateMatrix[i].Add(_random.Next(m));
				}
			}

			return stateMatrix;
		}

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			var outputMatrix = new List<List<int>>();

			for (var i = 0; i < m; i++)
			{
				outputMatrix.Add(new List<int>());

				while (outputMatrix[i].Count < n)
				{
					int y = _random.Next(n);
					if (!outputMatrix[i].Contains(y))
					{
						outputMatrix[i].Add(y);
					}
				}
			}

			return outputMatrix;
		}
	}
}