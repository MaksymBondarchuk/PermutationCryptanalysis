using System;
using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machine.Algorithms.InitialState;
using PermutationCryptanalysis.Machine.Algorithms.Outputs;
using PermutationCryptanalysis.Machine.Algorithms.States;
using PermutationCryptanalysis.Machine.Extensions;
using PermutationCryptanalysis.Machine.Interfaces;

namespace PermutationCryptanalysis
{
	public class InitialResettableTest
	{
		public void Run(int m, int n)
		{
			IInitialResettableMachine machine = GetMachine(m, n);

			List<List<int>> outputTable = HackOutputTable(machine, m, n);
			List<List<int>> stateTable = HackStateTable(machine, outputTable, m, n);
			int initialState = HackInitialState(machine, outputTable, n);

			if (outputTable.Count != m)
			{
				MachineWriter.WriteOneMatrix(outputTable, "Output matrix");
				MachineWriter.WriteOneMatrix(stateTable, "State matrix");
				throw new Exception($"Cannot get {m} unique output table rows");
			}

			if (stateTable.SelectMany(state => state).Any(state => state == -1))
			{
				MachineWriter.WriteOneMatrix(outputTable, "Output matrix");
				MachineWriter.WriteOneMatrix(stateTable, "State matrix");
				throw new Exception("Cannot get all states");
			}

			var hacked = new Machine.Machine(initialState, outputTable, stateTable, m, n);
			hacked.WriteMachine();

			Console.WriteLine($"Done in {machine.OperationsCounter} operations");
			Console.WriteLine($"Should have been done in {CalculateComplexity(m, n)} operations");
			Console.WriteLine($"Are machines equivalent? {hacked.IsEquivalentTo(machine, 4, 4)}");
		}

		private IInitialResettableMachine GetMachine(int m, int n)
		{
			// var outputMatrix = new List<List<int>>
			// {
			// 	new() {0, 3, 1, 2},
			// 	new() {1, 3, 0, 2},
			// 	new() {0, 2, 3, 1},
			// 	new() {2, 0, 3, 1}
			// };
			// var stateMatrix = new List<List<int>>
			// {
			// 	new() {1, 1, 2, 3},
			// 	new() {3, 3, 1, 2},
			// 	new() {0, 3, 0, 1},
			// 	new() {3, 0, 3, 0}
			// };
			var machine = new Machine.Machine(new RandomInitialStateAlgorithm(), new UniqueOutputRowsAlgorithm(), new ConnectedGraphStateAlgorithm(), m, n);
			machine.WriteMachine();
			machine.ResetCounter();
			return machine;
		}

		private List<List<int>> HackOutputTable(IInitialResettableMachine machine, int m, int n)
		{
			var table = new List<List<int>>();
			for (var state = 0; state < m; state++)
			{
				table.Add(HackOutputTableRow(machine, state, n));
			}

			return table;
		}

		private List<int> HackOutputTableRow(IInitialResettableMachine machine, int state, int n)
		{
			var row = new List<int>();
			for (var i = 0; i < n; i++)
			{
				machine.SetState(state);
				row.Add(machine.Transform(i));
			}

			return row;
		}

		private List<List<int>> HackStateTable(IInitialResettableMachine machine, List<List<int>> outputTable, int m, int n)
		{
			var table = new List<List<int>>();
			for (var state = 0; state < m; state++)
			{
				table.Add(HackStateTableRow(machine, outputTable, state, n));
			}

			return table;
		}

		private List<int> HackStateTableRow(IInitialResettableMachine machine, List<List<int>> outputTable, int state, int n)
		{
			var row = new List<int>();
			for (var input = 0; input < n; input++)
			{
				var outputRowForInput = new List<int>();
				for (var i = 0; i < n; i++)
				{
					machine.SetState(state);
					machine.Transform(input);
					outputRowForInput.Add(machine.Transform(i));
				}

				int newState = outputTable.SequenceIndexOf(outputRowForInput);
				row.Add(newState);
			}

			return row;
		}

		private int HackInitialState(IInitialResettableMachine machine, List<List<int>> outputTable, int n)
		{
			var row = new List<int>();
			for (var i = 0; i < n; i++)
			{
				machine.Reset();
				row.Add(machine.Transform(i));
			}

			return outputTable.SequenceIndexOf(row);
		}

		public static long CalculateComplexity(int m, int n)
		{
			return 2 * m * n + 3 * m * n * n + 2 * n;
		}
	}
}