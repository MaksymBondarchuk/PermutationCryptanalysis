using System.Collections.Generic;

namespace PermutationCryptanalysis.Machines.Interfaces
{
	public interface IMachine
	{
		// Number of rows in states' and outputs' matrices
		public int M { get; }

		// Number of columns in states' and outputs' matrices
		public int N { get; }

		public int OperationsCounter { get; }

		IEnumerable<int> Transform(IEnumerable<int> inputs);

		int Transform(int input);
	}
}