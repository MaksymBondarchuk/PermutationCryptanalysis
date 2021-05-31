#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PermutationCryptanalysis.Machine.Extensions;

namespace PermutationCryptanalysis.Machine.Algorithms.States
{
	public class ConnectedGraphStateAlgorithm : IStateMatrixAlgorithm
	{
		#region Setup

		private readonly Random _random = new();

		private readonly List<List<int>>? _existing;

		public ConnectedGraphStateAlgorithm(List<List<int>>? existing = null)
		{
			_existing = existing;
		}

		#endregion

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

					List<List<int>> pathForPreviousState = paths.Clone();

					paths = new List<List<int>>();
					for (var j = 0; j < n; j++)
					{
						int state = _existing?[i][j] ?? _random.Next(m);
						stateMatrix[i].Add(state);

						List<List<int>> pathForThisState = pathForPreviousState.Where(p => p.Last() == i).ToList().Clone();
						foreach (List<int> path in pathForThisState)
						{
							path.Add(state);
							if (path.Count == 3 && path[0] == 0 && path[1] == 2 && path[2] == 1)
							{
								Debugger.Break();
							}
						}

						paths.AddRange(pathForThisState);
					}
				}
			}

			List<int> pathWithAllNodes = paths.First(p => p.Distinct().Count() == m && p.First() == p.Last());
			Console.WriteLine($"Path with all nodes: {pathWithAllNodes.ToHumanReadableString()}");

			return stateMatrix;
		}
	}
}