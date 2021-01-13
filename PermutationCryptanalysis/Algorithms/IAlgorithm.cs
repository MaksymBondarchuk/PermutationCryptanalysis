using System.Collections.Generic;

namespace PermutationCryptanalysis.Algorithms
{
	public interface IAlgorithm
	{
		int GetInitialState(int m);
		
		List<List<int>> GenerateStateMatrix(int m, int n);
		
		List<List<int>> GenerateOutputMatrix(int m, int n);
	}
}