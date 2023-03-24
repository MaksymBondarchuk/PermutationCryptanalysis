using System;
using System.Collections.Generic;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;

namespace PermutationCryptanalysis.Machines.Algorithms
{
	public class Article3Machine2Algorithm : IInitialStateAlgorithm, IOutputMatrixAlgorithm, IStateMatrixAlgorithm
	{
		public int GetInitialState(int m)
		{
			return 0;
		}

		public List<List<int>> GenerateStateMatrix(int m, int n)
		{
			if (m != 12 && n != 8)
			{
				throw new NotSupportedException("Only m1 = 10 and n = 8 supported");
			}

			return new List<List<int>>
			{
				new() {0, 5, 3, 5, 4, 5, 2, 1},
				new() {7, 6, 5, 2, 7, 7, 1, 1},
				new() {2, 3, 8, 3, 5, 9, 8, 6},
				new() {4, 8, 0, 4, 4, 4, 3, 8},
				new() {1, 8, 2, 8, 9, 5, 7, 5},
				new() {0, 2, 6, 9, 7, 3, 2, 3},
				new() {3, 4, 7, 7, 8, 9, 1, 7},
				new() {3, 3, 6, 7, 4, 0, 6, 8},
				new() {6, 2, 8, 5, 2, 8, 9, 2},
				new() {4, 9, 5, 1, 7, 0, 8, 3},
			};
		}

		public List<List<int>> GenerateOutputMatrix(int m, int n)
		{
			if (m != 12 && n != 8)
			{
				throw new NotSupportedException("Only m1 = 10 and n = 8 supported");
			}

			return new List<List<int>>
			{
				new() {4, 6, 3, 0, 7, 1, 2, 5},
				new() {4, 6, 3, 5, 2, 1, 7, 0},
				new() {5, 7, 1, 2, 6, 0, 3, 4},
				new() {5, 2, 4, 1, 0, 3, 6, 7},
				new() {5, 1, 7, 4, 0, 3, 6, 2},
				new() {6, 1, 2, 3, 0, 7, 4, 5},
				new() {0, 3, 4, 2, 6, 1, 5, 7},
				new() {2, 6, 1, 3, 5, 4, 0, 7},
				new() {4, 1, 3, 2, 7, 6, 5, 0},
				new() {6, 0, 1, 2, 5, 7, 4, 3},
			};
		}
	}
}