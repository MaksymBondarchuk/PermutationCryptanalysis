using System;
using System.Collections.Generic;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;

namespace PermutationCryptanalysis.Machines.Algorithms
{
	public class Article3Machine1Algorithm : IInitialStateAlgorithm, IOutputMatrixAlgorithm, IStateMatrixAlgorithm
	{
		public int GetInitialState(int m)
		{
			return 0;
		}

		public List<List<int>> GenerateStateMatrix(int m, int n)
		{
			if (m != 12 && n != 8)
			{
				throw new NotSupportedException("Only m1 = 12 and n = 8 supported");
			}

			return new List<List<int>>
			{
				new() {5, 1, 10, 11, 3, 7, 9, 5},
				new() {9, 2, 8, 10, 8, 0, 2, 2},
				new() {5, 10, 6, 10, 11, 3, 10, 1},
				new() {8, 2, 7, 6, 8, 4, 6, 7},
				new() {3, 0, 0, 5, 1, 0, 7, 4},
				new() {1, 6, 7, 3, 11, 4, 8, 10},
				new() {10, 9, 11, 10, 1, 4, 3, 7},
				new() {11, 11, 0, 5, 6, 1, 8, 6},
				new() {4, 2, 10, 4, 9, 11, 5, 9},
				new() {9, 11, 11, 9, 7, 8, 8, 10},
				new() {10, 7, 11, 2, 8, 8, 4, 4},
				new() {9, 6, 7, 2, 0, 10, 8, 2},
			};
		}

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			if (m != 12 && n != 8)
			{
				throw new NotSupportedException("Only m1 = 12 and n = 8 supported");
			}

			return new List<List<int>>
			{
				new() {1, 3, 5, 7, 4, 0, 6, 2},
				new() {1, 3, 7, 2, 4, 5, 6, 0},
				new() {4, 7, 5, 2, 6, 3, 1, 0},
				new() {0, 7, 5, 1, 6, 3, 2, 4},
				new() {0, 5, 2, 6, 4, 1, 3, 7},
				new() {7, 3, 4, 6, 5, 1, 2, 0},
				new() {3, 6, 5, 0, 2, 7, 1, 4},
				new() {3, 7, 4, 1, 2, 6, 0, 5},
				new() {5, 0, 2, 1, 7, 4, 6, 3},
				new() {5, 4, 7, 3, 2, 1, 0, 6},
				new() {0, 6, 4, 5, 2, 1, 3, 7},
				new() {0, 1, 6, 4, 3, 2, 7, 5},
			};
		}
	}
}