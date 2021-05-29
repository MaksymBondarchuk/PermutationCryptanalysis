using System.Collections.Generic;

namespace PermutationCryptanalysis.Machine.Algorithms.States
{
	public interface IStateMatrixAlgorithm
	{
		List<List<int>> GenerateStateMatrix(int m, int n);
	}
}