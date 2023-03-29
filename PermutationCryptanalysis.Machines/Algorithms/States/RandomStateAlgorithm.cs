using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis.Machines.Algorithms.States
{
	public class RandomStateAlgorithm : IStateMatrixAlgorithm
	{
		private readonly Random _random = new();

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
	}
}