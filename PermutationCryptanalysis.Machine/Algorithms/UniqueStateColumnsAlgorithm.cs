using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis.Machine.Algorithms
{
	public class UniqueStateColumnsAlgorithm : IAlgorithm
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

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			var outputMatrix = new List<List<int>>();
			for (var i = 0; i < m; i++)
			{
				outputMatrix.Add(new List<int>());
			}

			for (var j = 0; j < n; j++)
			{
				for (var i = 0; i < m; i++)
				{
					outputMatrix[i].Add(_random.Next(n));
				}
			}

			return outputMatrix;
		}
	}
}