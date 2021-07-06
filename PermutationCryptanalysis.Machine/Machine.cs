using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machine.Algorithms.InitialState;
using PermutationCryptanalysis.Machine.Algorithms.Outputs;
using PermutationCryptanalysis.Machine.Algorithms.States;
using PermutationCryptanalysis.Machine.Interfaces;

namespace PermutationCryptanalysis.Machine
{
	public class Machine: IInitialResettableMachine
	{
		#region Setup

		public int M { get; protected init; }

		public int N { get; protected init; }

		public IInitialStateAlgorithm InitialStateAlgorithm { get; }

		public IOutputMatrixAlgorithm OutputMatrixAlgorithm { get; }

		public IStateMatrixAlgorithm StateMatrixAlgorithm { get; }

		public int InitialState { get; protected init; }

		private int State { get; set; }

		public readonly List<List<int>> OutputMatrix;

		public readonly List<List<int>> StateMatrix;

		public int OperationsCounter { get; private set; }

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

		public Machine(int initialState, List<List<int>> outputMatrix, List<List<int>> stateMatrix, int m, int n)
		{
			M = m;
			N = n;

			#region Generate

			InitialState = initialState;
			State = InitialState;
			OutputMatrix = outputMatrix;
			StateMatrix = stateMatrix;

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
				OperationsCounter++;
				outputs.Add(GetOutput(state, input));

				state = GetState(state, input);
			}

			return outputs;
		}

		public int Transform(int input)
		{
			OperationsCounter++;
			int output = GetOutput(State, input);
			State = GetState(State, input);
			return output;
		}

		public void Reset()
		{
			OperationsCounter++;
			State = InitialState;
		}

		public void SetState(int state)
		{
			OperationsCounter++;
			State = state;
		}

		public void ResetCounter()
		{
			OperationsCounter = 0;
		}

		#endregion

		#region Bijectivness

		public bool IsBijective(int messageSizeFrom, int messageSizeTo)
		{
			for (int messageSize = messageSizeFrom; messageSize <= messageSizeTo; messageSize++)
			{
				var outputs = new List<List<int>>();

				List<int> inputs = GenerateFirstMessage(messageSize);
				Reset();
				while (CanBeIncremented(inputs))
				{
					List<int> current = Transform(inputs).ToList();

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

		#region Equivalence

		public bool IsEquivalentTo(IResettableMachine other, int messageSizeFrom, int messageSizeTo)
		{
			for (int messageSize = messageSizeFrom; messageSize <= messageSizeTo; messageSize++)
			{
				List<int> inputs = GenerateFirstMessage(messageSize);
				Reset();
				other.Reset();
				while (CanBeIncremented(inputs))
				{
					List<int> thisOutput = Transform(inputs).ToList();
					List<int> otherOutput = other.Transform(inputs).ToList();

					if (!thisOutput.SequenceEqual(otherOutput))
					{
						return false;
					}

					IncrementMessage(inputs);
					Reset();
					other.Reset();
				}
			}

			return true;
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