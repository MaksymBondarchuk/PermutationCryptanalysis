using System.Collections.Generic;

namespace PermutationCryptanalysis.Machine.Interfaces
{
	public interface IMachine
	{
		public int OperationsCounter { get; }
		
		IEnumerable<int> Transform(IEnumerable<int> inputs);

		int Transform(int input);

		void ResetCounter();

		bool IsBijective(int messageSizeFrom, int messageSizeTo);
	}
}