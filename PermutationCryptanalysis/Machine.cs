using System;
using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Algorithms;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		public int M { get; protected init; }
		public int N { get; protected init; }
		
		public IAlgorithm Algorithm { get; protected init; }
		public int InitialState { get; protected init; }

		public readonly List<List<int>> StateMatrix;

		public readonly List<List<int>> OutputMatrix;

		public Machine(IAlgorithm algorithm, int m, int n)
		{
			M = m;
			N = n;

			#region Generate

			Algorithm = algorithm;

			InitialState = algorithm.GetInitialState(m);
			StateMatrix = algorithm.GenerateStateMatrix(m, n);
			OutputMatrix = algorithm.GenerateOutputMatrix(m, n);

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