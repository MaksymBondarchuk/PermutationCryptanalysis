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

		#region Transform

		public IEnumerable<int> Transform(IEnumerable<int> inputs)
		{
			var outputs = new List<int>();

			int state = InitialState;

			foreach (int input in inputs)
			{
				outputs.Add(GetOutput(state, input));

				state = GetState(state, input);
			}

			return outputs;
		}

		#endregion

		#region Output

		public void OutputStateMatrix(bool articleMode = false)
		{
			OutputOneMatrix(StateMatrix, articleMode);
		}

		public void OutputOutputMatrix(bool articleMode = false)
		{
			OutputOneMatrix(OutputMatrix, articleMode);
		}

		private void OutputOneMatrix(List<List<int>> matrix, bool articleMode = false)
		{
			foreach (List<int> row in matrix)
			{
				foreach (int cell in row)
				{
					if (articleMode)
					{
						Console.Write($"{cell + 1,4}");
					}
					else
					{
						Console.Write($"{cell,4}");
					}
				}

				Console.WriteLine();
			}
		}

		#endregion
		
		private int GetState(int state, int input)
		{
			return StateMatrix[state][input];
		}

		private int GetOutput(int state, int input)
		{
			return OutputMatrix[state][input];
		}
	}
}