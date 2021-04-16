using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis.Machine.Algorithms
{
	public class HaitArticleAlgorithm : IAlgorithm
	{
		public int GetInitialState(int m)
		{
			return 0;
		}

		public List<List<int>> GenerateStateMatrix(int m, int n)
		{
			if (m != 12 && n != 8)
			{
				throw new NotSupportedException("Only m = 12 and n = 8 supported");
			}

			return new List<List<int>>
			{
				new() {0, 0, 4, 10, 3, 10, 4, 3},
				new() {6, 10, 3, 3, 7, 11, 7, 2},
				new() {9, 11, 0, 6, 9, 4, 8, 5},
				new() {5, 6, 0, 4, 7, 7, 9, 6},
				new() {10, 11, 4, 2, 3, 6, 9, 3},
				new() {8, 6, 2, 8, 2, 5, 2, 5},
				new() {7, 7, 7, 8, 10, 6, 6, 0},
				new() {9, 1, 10, 4, 5, 0, 3, 11},
				new() {6, 1, 7, 5, 1, 11, 11, 9},
				new() {8, 9, 11, 10, 11, 6, 1, 8},
				new() {8, 10, 11, 0, 9, 8, 0, 10},
				new() {3, 11, 8, 3, 4, 5, 4, 4},
			};
		}

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			if (m != 12 && n != 8)
			{
				throw new NotSupportedException("Only m = 12 and n = 8 supported");
			}

			return new List<List<int>>
			{
				new() {0, 3, 5, 6, 1, 4, 2, 7},
				new() {5, 2, 3, 7, 4, 6, 1, 0},
				new() {3, 7, 2, 5, 1, 4, 0, 6},
				new() {0, 2, 7, 3, 4, 6, 5, 1},
				new() {0, 6, 5, 2, 1, 4, 3, 7},
				new() {2, 0, 5, 4, 3, 7, 6, 1},
				new() {3, 6, 2, 0, 5, 1, 4, 7},
				new() {3, 0, 7, 4, 1, 5, 2, 6},
				new() {4, 6, 3, 7, 1, 2, 5, 0},
				new() {3, 0, 6, 7, 4, 1, 2, 5},
				new() {5, 2, 1, 0, 4, 3, 7, 6},
				new() {5, 1, 6, 0, 2, 7, 4, 3},
			};
		}
	}
}