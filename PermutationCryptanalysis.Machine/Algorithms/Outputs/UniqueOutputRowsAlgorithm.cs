using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis.Machine.Algorithms.Outputs
{
	public class UniqueOutputRowsAlgorithm : IOutputMatrixAlgorithm
	{
		private readonly Random _random = new();

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			var outputMatrix = new List<List<int>>();

			for (var i = 0; i < m; i++)
			{
				var row = new List<int>();
				while (row.Count < n && !outputMatrix.Any(r => r.SequenceEqual(row)))
				{
					int y = _random.Next(n);
					if (!row.Contains(y))
					{
						row.Add(y);
					}
				}
				outputMatrix.Add(row);
			}

			return outputMatrix;
		}
	}
}