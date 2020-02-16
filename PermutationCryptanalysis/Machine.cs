using System.Collections.Generic;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		private readonly int _m;
		private readonly int _n;
		
		private readonly List<List<int>> _stateMatrix;

		private readonly List<List<int>> _outputMatrix;

		public Machine(int m, int n)
		{
			_m = m;
			_n = n;

			for (var i = 0; i < _m; i++)
			{
				for (var j = 0; j < _n; j++)
				{
					
				}
			}
		}
	}
}