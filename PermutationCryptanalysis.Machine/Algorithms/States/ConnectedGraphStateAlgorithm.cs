using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis.Machine.Algorithms.States
{
	public class ConnectedGraphStateAlgorithm : IStateMatrixAlgorithm
	{
		private readonly Random _random = new();

		public List<List<int>> GenerateStateMatrix(int m, int n)
		{
			var stateMatrix = new List<List<int>>();

			var paths = new List<List<int>>();

			while (!paths.Any(p => p.Distinct().Count() == m && p.First() == p.Last()))
			{
				stateMatrix.Clear();
				paths.Clear();
				
				// Set Initial State (relative) as starting point
				for (var j = 0; j < n; j++)
				{
					paths.Add(new List<int> {0});
				}

				// Generate State Matrix and track paths
				for (var i = 0; i < m; i++)
				{
					stateMatrix.Add(new List<int>());

					List<List<int>> pathForPreviousState = DuplicatePaths(paths);

					paths = new List<List<int>>();
					for (var j = 0; j < n; j++)
					{
						int state = _random.Next(m);
						stateMatrix[i].Add(state);

						List<List<int>> pathForThisState = DuplicatePaths(pathForPreviousState);
						foreach (List<int> path in pathForThisState)
						{
							path.Add(state);
						}
						paths.AddRange(pathForThisState);
					}
				}
			}

			return stateMatrix;
		}

		private static List<List<T>> DuplicatePaths<T>(List<List<T>> paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException(nameof(paths));
			}
			
			var duplicate = new List<List<T>>(paths.Count);
			duplicate.AddRange(paths.Select(path => new List<T>(path)));
			return duplicate;
		}
	}
}