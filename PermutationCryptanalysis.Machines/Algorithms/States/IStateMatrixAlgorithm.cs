using System.Collections.Generic;

namespace PermutationCryptanalysis.Machines.Algorithms.States
{
	public interface IStateMatrixAlgorithm
	{
		List<List<int>> GenerateStateMatrix(int m, int n);
	}
}