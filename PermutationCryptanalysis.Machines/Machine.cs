using System.Collections.Generic;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;
using PermutationCryptanalysis.Machines.Interfaces;

namespace PermutationCryptanalysis.Machines
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

		public int GetState(int state, int input)
		{
			return StateMatrix[state][input];
		}

		public int GetOutput(int state, int input)
		{
			return OutputMatrix[state][input];
		}
	}
}