using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;
using PermutationCryptanalysis.Machines.Interfaces;

namespace PermutationCryptanalysis.Machines;

/// <summary>
/// Experiment for article #3 where #1 machine's outputs are #2 machine's inputs + machine #2 is reversed (idea to transform only messages that are blocks of inputs)
/// </summary>
public class DoubledMachine : IResettableMachine
{
	#region Setup

	private readonly Machine _machine1;
	private readonly Machine _machine2;

	public int M { get; }

	public int N { get; }

	public int OperationsCounter => _machine1.OperationsCounter + _machine2.OperationsCounter;

	public DoubledMachine(
		IInitialStateAlgorithm initialStateAlgorithm,
		IOutputMatrixAlgorithm outputMatrixAlgorithm,
		IStateMatrixAlgorithm stateMatrixAlgorithm,
		int m,
		int n)
	{
		_machine1 = new Machine(initialStateAlgorithm, outputMatrixAlgorithm, stateMatrixAlgorithm, m, n);
		_machine2 = new Machine(initialStateAlgorithm, outputMatrixAlgorithm, stateMatrixAlgorithm, m, n);

		M = m;
		N = n;
	}

	public DoubledMachine(
		IInitialStateAlgorithm initialStateAlgorithm1,
		IOutputMatrixAlgorithm outputMatrixAlgorithm1,
		IStateMatrixAlgorithm stateMatrixAlgorithm1,
		IInitialStateAlgorithm initialStateAlgorithm2,
		IOutputMatrixAlgorithm outputMatrixAlgorithm2,
		IStateMatrixAlgorithm stateMatrixAlgorithm2,
		int m,
		int n)
	{
		_machine1 = new Machine(initialStateAlgorithm1, outputMatrixAlgorithm1, stateMatrixAlgorithm1, m, n);
		_machine2 = new Machine(initialStateAlgorithm2, outputMatrixAlgorithm2, stateMatrixAlgorithm2, m, n);

		M = m;
		N = n;
	}

	#endregion

	#region Transform

	public IEnumerable<int> Transform(IEnumerable<int> inputs)
	{
		// todo: Питання: чи ок, що щоразу після перетворення вхідного повідомлення скидаємо стани в початкові?
		var outputs = new List<int>();

		var stack = new Stack<int>();
		foreach (int input in inputs)
		{
			stack.Push(_machine1.Transform(input));
		}

		while (stack.Any())
		{
			int input = stack.Pop();
			outputs.Add(_machine2.Transform(input));
		}

		return outputs;
	}

	public int Transform(int input)
	{
		int output1 = _machine1.Transform(input);
		return _machine2.Transform(output1);
	}

	#endregion

	public void ResetCounter()
	{
		_machine1.ResetCounter();
		_machine2.ResetCounter();
	}

	public void Reset()
	{
		_machine1.Reset();
		_machine2.Reset();
	}
}