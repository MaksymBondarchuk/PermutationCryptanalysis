using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machine.Algorithms.InitialState;
using PermutationCryptanalysis.Machine.Algorithms.Outputs;
using PermutationCryptanalysis.Machine.Algorithms.States;

namespace PermutationCryptanalysis.Machine
{
	public class Machine
	{
		#region Setup

		public int M { get; protected init; }

		public int N { get; protected init; }

		public IInitialStateAlgorithm InitialStateAlgorithm { get; }
		
		public IOutputMatrixAlgorithm OutputMatrixAlgorithm { get; }
		
		public IStateMatrixAlgorithm StateMatrixAlgorithm { get; }

		public int InitialState { get; protected init; }

		private int State { get; set; }

		public readonly List<List<int>> StateMatrix;

		public readonly List<List<int>> OutputMatrix;

		public Machine(IInitialStateAlgorithm initialStateAlgorithm, IOutputMatrixAlgorithm outputMatrixAlgorithm, IStateMatrixAlgorithm stateMatrixAlgorithm, int m, int n)
		{
			M = m;
			N = n;

			#region Generate

			InitialStateAlgorithm = initialStateAlgorithm;
			OutputMatrixAlgorithm = outputMatrixAlgorithm;
			StateMatrixAlgorithm = stateMatrixAlgorithm;

			InitialState = initialStateAlgorithm.GetInitialState(m);
			State = InitialState;
			OutputMatrix = outputMatrixAlgorithm.GenerateOutputMatrix(m, n);
			StateMatrix = stateMatrixAlgorithm.GenerateStateMatrix(m, n);

			#endregion
		}

		#endregion

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

		public int Transform(int input)
		{
			int output = GetOutput(State, input);
			State = GetState(State, input);
			return output;
		}

		public void Reset()
		{
			State = InitialState;
		}

		#endregion

		#region Ensure Bijectivness

		public bool IsBijective(int messageSizeFrom, int messageSizeTo)
		{
			for (int messageSize = messageSizeFrom; messageSize <= messageSizeTo; messageSize++)
			{
				var outputs = new List<List<int>>();

				List<int> inputs = GenerateFirstMessage(messageSize);
				Reset();
				while (CanBeIncremented(inputs))
				{
					// PrintList(inputs);

					List<int> current = Transform(inputs).ToList();
					// List<int> current2 = inputs.Select(Transform).ToList();

					// if (!current.SequenceEqual(current2))
					// {
					// Debugger.Break();
					// }

					if (outputs.Any(output => current.SequenceEqual(output)))
					{
						return false;
					}

					outputs.Add(current);
					IncrementMessage(inputs);
					Reset();
				}
			}

			return true;
		}

		private List<int> GenerateFirstMessage(int messageSize)
		{
			var inputs = new List<int>();
			for (var i = 0; i < messageSize; i++)
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