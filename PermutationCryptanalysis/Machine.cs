using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		private readonly int _m;
		private readonly int _n;
		private readonly int _initialState;

		private readonly List<List<int>> _stateMatrix = new List<List<int>>();

		private readonly List<List<int>> _outputMatrix = new List<List<int>>();

		private readonly Random _random = new Random();

		public Machine(int m, int n)
		{
			_m = m;
			_n = n;

			#region Generate

			_initialState = _random.Next(_m);

			for (int i = 0; i < _m; i++)
			{
				_stateMatrix.Add(new List<int>());
				_outputMatrix.Add(new List<int>());
				for (int j = 0; j < _n; j++)
				{
					_stateMatrix[i].Add(_random.Next(_m));
				}

				while (_outputMatrix[i].Count < _n)
				{
					int y = _random.Next(_n);
					if (!_outputMatrix[i].Contains(y))
					{
						_outputMatrix[i].Add(y);
					}
				}
			}

			#endregion
		}

		#region Output

		public void OutputStateMatrix()
		{
			OutputMatrix(_stateMatrix);
		}

		public void OutputOutputMatrix()
		{
			OutputMatrix(_outputMatrix);
		}

		private void OutputMatrix(List<List<int>> matrix)
		{
			foreach (List<int> row in matrix)
			{
				foreach (int cell in row)
				{
					Console.Write($"{cell,4}");
				}

				Console.WriteLine();
			}
		}

		#endregion
	}
}