using System.Collections.Generic;

namespace PermutationCryptanalysis.Machine.Algorithms.Outputs
{
	public interface IOutputMatrixAlgorithm
	{
		List<List<int>> GenerateOutputMatrix(int m, int n);
	}
}