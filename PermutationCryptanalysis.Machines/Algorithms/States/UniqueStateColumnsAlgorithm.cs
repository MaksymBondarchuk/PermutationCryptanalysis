using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis.Machines.Algorithms.States
{
	public class UniqueStateColumnsAlgorithm : IStateMatrixAlgorithm
	{
		private readonly Random _random = new();

		public List<List<int>> GenerateStateMatrix(int m, int n)
		{
			var stateMatrix = new List<List<int>>();

			for (var i = 0; i < m; i++)
			{
				stateMatrix.Add(new List<int>());
			}

			for (var j = 0; j < n; j++)
			{
				var i = 0;
				int leftToAdd = m;
				while (leftToAdd != 0)
				{
					int s = _random.Next(m);
					if (stateMatrix.All(row => row.Count <= j || row[j] != s))
					{
						stateMatrix[i].Add(s);
						i++;
						leftToAdd--;
					}
				}
			}

			return stateMatrix;
		}
	}
}