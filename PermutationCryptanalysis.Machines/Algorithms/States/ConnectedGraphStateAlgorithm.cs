#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machines.Extensions;

namespace PermutationCryptanalysis.Machines.Algorithms.States
{
	// Алгоритм генерування таблиці переходів як таблиці зв’язаного пронумерованого однонаправленого графа 
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

						// All paths that end in this state. Clone them and add new generated state to each of them
						List<List<int>> pathForThisState = pathForPreviousState.Where(p => p.Last() == i).ToList().Clone();
						foreach (List<int> path in pathForThisState)
						{
							path.Add(state);
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