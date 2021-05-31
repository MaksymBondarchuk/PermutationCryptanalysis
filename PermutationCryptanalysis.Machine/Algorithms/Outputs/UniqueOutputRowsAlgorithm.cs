#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis.Machine.Algorithms.Outputs
{
	public class UniqueOutputRowsAlgorithm : IOutputMatrixAlgorithm
	{
		#region Setup

		private readonly Random _random = new();

		private readonly List<List<int>>? _existing;

		public UniqueOutputRowsAlgorithm(List<List<int>>? existing = null)
		{
			_existing = existing;
		}

		#endregion

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			var outputMatrix = new List<List<int>>();

			for (var i = 0; i < m; i++)
			{
				var row = new List<int>();
				do
				{
					row.Clear();
					while (row.Count < n)
					{
						int output = _existing?[i][row.Count] ?? _random.Next(m);
						if (!row.Contains(output))
						{
							row.Add(output);
						}
					}
				} while (outputMatrix.Any(r => r.SequenceEqual(row)));


				outputMatrix.Add(row);
			}

			return outputMatrix;
		}
	}
}