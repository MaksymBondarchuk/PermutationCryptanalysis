using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis.Machines.Algorithms.Outputs
{
	public class RandomOutputAlgorithm : IOutputMatrixAlgorithm
	{
		private readonly Random _random = new();

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