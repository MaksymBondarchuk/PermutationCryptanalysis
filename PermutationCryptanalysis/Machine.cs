using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		public int M { get; protected init; }
		public int N { get; protected init; }
		public int InitialState { get; protected init; }

		public readonly List<List<int>> StateMatrix = new();

		public readonly List<List<int>> OutputMatrix = new();

		private readonly Random _random = new();

		public Machine(int m, int n)
		{
			M = m;
			N = n;

			#region Generate

			// InitialState = 0;
			InitialState = _random.Next(M);
			// StateMatrix = new List<List<int>>
			// {
			// 	new List<int>{0, 0, 4, 10, 3, 10, 4, 3},
			// 	new List<int>{6, 10,3,3,7,11,7,2},
			// 	new List<int>{9,11,0,6,9,4,8,5},
			// 	new List<int>{5,6,0,4,7,7,9,6},
			// 	new List<int>{10,11,4,2,3,6,9,3},
			// 	new List<int>{8,6,2,8,2,5,2,5},
			// 	new List<int>{7,7,7,8,10,6,6,0},
			// 	new List<int>{9,1,10,4,5,0,3,11},
			// 	new List<int>{6,1,7,5,1,11,11,9},
			// 	new List<int>{8,9,11,10,11,6,1,8},
			// 	new List<int>{8,10,11,0,9,8,0,10},
			// 	new List<int>{3,11,8,3,4,5,4,4},
			// };
			// OutputMatrix = new List<List<int>>
			// {
			// 	new List<int>{0,3,5,6,1,4,2,7},
			// 	new List<int>{5,2,3,7,4,6,1,0},
			// 	new List<int>{3,7,2,5,1,4,0,6},
			// 	new List<int>{0,2,7,3,4,6,5,1},
			// 	new List<int>{0,6,5,2,1,4,3,7},
			// 	new List<int>{2,0,5,4,3,7,6,1},
			// 	new List<int>{3,6,2,0,5,1,4,7},
			// 	new List<int>{3,0,7,4,1,5,2,6},
			// 	new List<int>{4,6,3,7,1,2,5,0},
			// 	new List<int>{3,0,6,7,4,1,2,5},
			// 	new List<int>{5,2,1,0,4,3,7,6},
			// 	new List<int>{5,1,6,0,2,7,4,3},
			// };

			// InitialState = 0;
			// StateMatrix = new List<List<int>>
			// {
			// 	new List<int> {0, 1, 2, 3},
			// 	new List<int> {3, 0, 1, 2},
			// 	new List<int> {2, 3, 0, 1},
			// 	new List<int> {1, 2, 3, 0}
			// };;
			// OutputMatrix = new List<List<int>>
			// {
			// 	new List<int> {0, 1, 2, 3},
			// 	new List<int> {3, 0, 1, 2},
			// 	new List<int> {2, 3, 0, 1},
			// 	new List<int> {1, 2, 3, 0}
			// };

			for (var i = 0; i < M; i++)
			{
				StateMatrix.Add(new List<int>());
				OutputMatrix.Add(new List<int>());
			}

			for (var j = 0; j < N; j++)
			{
				for (var i = 0; i < M; i++)
				{
					OutputMatrix[i].Add(_random.Next(N));
				}
			}

			for (var j = 0; j < N; j++)
			{
				var i = 0;
				int leftToAdd = M;
				while (leftToAdd != 0)
				{
					int s = _random.Next(M);
					if (StateMatrix.All(row => row.Count <= j || row[j] != s))
					{
						StateMatrix[i].Add(s);
						i++;
						leftToAdd--;
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

		#region Ensure Permutation

		public bool EnsurePermutation(int messageSizeFrom, int messageSizeTo)
		{
			for (int messageSize = messageSizeFrom; messageSize <= messageSizeTo; messageSize++)
			{
				var outputs = new List<List<int>>();

				List<int> inputs = GenerateFirstMessage();
				while (CanBeIncremented(inputs))
				{
					PrintList(inputs);
					
					List<int> current = Transform(inputs).ToList();
					
					if (outputs.Any(output => current.SequenceEqual(output)))
					{
						return false;
					}
					
					outputs.Add(current);
					IncrementMessage(inputs);
				}
			}

			return true;
		}

		private List<int> GenerateFirstMessage()
		{
			var inputs = new List<int>();
			for (var i = 0; i < N; i++)
			{
				inputs.Add(0);
			}
			return inputs;
		}

		private bool CanBeIncremented(List<int> inputs)
		{
			return inputs.All(x => x < N);
		}

		private void IncrementMessage(List<int> inputs)
		{
			inputs[^1]++;
			for (int i = inputs.Count - 1; 0 < i; i--)
			{
				if (inputs[i] == N)
				{
					inputs[i] = 0;
					inputs[i - 1]++;
				}
			}
		}
		
		private static void PrintList(IEnumerable<int> list)
		{
			foreach (int i in list)
			{
				Console.Write($"{i,4}");
			}

			Console.WriteLine();
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