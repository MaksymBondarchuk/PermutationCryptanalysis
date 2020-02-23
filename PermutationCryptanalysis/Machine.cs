using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		public int M { get; protected set; }
		public int N { get; protected set; }
		public int InitialState { get; protected set; }

		public readonly List<List<int>> StateMatrix = new List<List<int>>();

		public readonly List<List<int>> OutputMatrix = new List<List<int>>();

		private readonly Random _random = new Random();

		public Machine(int m, int n)
		{
			M = m;
			N = n;

			#region Generate

			InitialState = _random.Next(M);

			for (int i = 0; i < M; i++)
			{
				StateMatrix.Add(new List<int>());
				OutputMatrix.Add(new List<int>());
				for (int j = 0; j < N; j++)
				{
					StateMatrix[i].Add(_random.Next(M));
				}

				while (OutputMatrix[i].Count < N)
				{
					int y = _random.Next(N);
					if (!OutputMatrix[i].Contains(y))
					{
						OutputMatrix[i].Add(y);
					}
				}
			}

			#endregion
		}

		#region Output

		public void OutputStateMatrix()
		{
			OutputOneMatrix(StateMatrix);
		}

		public void OutputOutputMatrix()
		{
			OutputOneMatrix(OutputMatrix);
		}

		private void OutputOneMatrix(List<List<int>> matrix)
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