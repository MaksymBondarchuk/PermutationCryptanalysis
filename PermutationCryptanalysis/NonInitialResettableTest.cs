using System;
using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machine;
using PermutationCryptanalysis.Machine.Algorithms.InitialState;
using PermutationCryptanalysis.Machine.Algorithms.Outputs;
using PermutationCryptanalysis.Machine.Algorithms.States;
using PermutationCryptanalysis.Machine.Extensions;
using PermutationCryptanalysis.Machine.Interfaces;

namespace PermutationCryptanalysis
{
	public class NonInitialResettableTest
	{
		private readonly Dictionary<List<int>, List<int>> _cache = new();
		
		public void Run(int m, int n)
		{
			IResettableMachine machine = GetMachine(m, n);

			#region Init with -1

			var outputTable = new List<List<int>>();
			var stateTable = new List<List<int>>();
			for (var i = 0; i < m; i++)
			{
				// outputTable.Add(new List<int>());
				stateTable.Add(new List<int>());
				for (var j = 0; j < n; j++)
				{
					// outputTable[i].Add(-1);
					stateTable[i].Add(-1);
				}
			}

			#endregion

			outputTable.AddDistinct(HackFirstOutputRow(machine, m, n));
			for (List<int> path = PathHelper.GetFirstPath(m, n); PathHelper.CanIncrement(path, n); path = PathHelper.Increment(path, n))
			{
				var state = 0;
				for (var i = 1; i <= path.Count; i++)
				{
					List<int> thisPath = path.Take(i).ToList();
					List<int> outputRow = GetOneOutputRow(machine, m, n, thisPath);
					outputTable.AddDistinct(outputRow);

					int newState = outputTable.SequenceIndexOf(outputRow);
					int input = thisPath.Last();
					if (stateTable[state][input] != -1 && stateTable[state][input] != newState)
					{
						throw new Exception($"Something went wrong for a path '{path.ToHumanReadableString()}' and local path '{thisPath.ToHumanReadableString()}'");
					}

					stateTable[state][input] = newState;
					state = newState;
				}
			}

			MachineWriter.WriteOneMatrix(outputTable, "Output matrix");
			MachineWriter.WriteOneMatrix(stateTable, "State matrix");
			if (outputTable.Count != m)
			{
				throw new Exception($"Cannot get {m} unique output table rows");
			}

			if (stateTable.SelectMany(state => state).Any(state => state == -1))
			{
				throw new Exception($"Cannot get all states");
			}

			var hacked = new Machine.Machine(0, outputTable, stateTable, m, n);
			Console.WriteLine($"Done in {machine.OperationsCounter} operations");
			Console.WriteLine($"Is machines equivalent? {hacked.IsEquivalentTo(machine, 4, 4)}");
		}

		private IResettableMachine GetMachine(int m, int n)
		{
			
			var outputMatrix = new List<List<int>>
			{
				new() {0, 3, 1, 2},
				new() {1, 3, 0, 2},
				new() {0, 2, 3, 1},
				new() {2, 0, 3, 1}
			};
			var stateMatrix = new List<List<int>>
			{
				new() {1, 1, 2, 3},
				new() {3, 3, 1, 2},
				new() {0, 3, 0, 1},
				new() {3, 0, 3, 0}
			};
			var machine = new Machine.Machine(new RandomInitialStateAlgorithm(), new UniqueOutputRowsAlgorithm(), new ConnectedGraphStateAlgorithm(), m, n);
			machine.WriteMachine();
			machine.ResetCounter();
			return machine;
		}

		private List<int> HackFirstOutputRow(IResettableMachine machine, int m, int n)
		{
			var firstRow = new List<int>();
			for (var i = 0; i < n; i++)
			{
				firstRow.Add(machine.Transform(i));
				machine.Reset();
			}

			return firstRow;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="machine"></param>
		/// <param name="m"></param>
		/// <param name="n"></param>
		/// <param name="input"></param>
		/// <param name="offset">Offset from initial state</param>
		/// <returns></returns>
		private List<int> GetOneOutputRow(IResettableMachine machine, int m, int n, int input, int offset)
		{
			var row = new List<int>();
			for (var i = 0; i < n; i++)
			{
				for (var j = 0; j < offset; j++)
				{
					machine.Transform(input);
				}

				row.Add(machine.Transform(i));
				machine.Reset();
			}

			return row;
		}

		private List<int> GetOneOutputRow(IResettableMachine machine, int m, int n, List<int> path)
		{
			foreach (var (cachedPath, cachedOutputRow) in _cache)
			{
				if (cachedPath.SequenceEqual(path))
				{
					return cachedOutputRow;
				}
			}
			
			var row = new List<int>();
			for (var i = 0; i < n; i++)
			{
				foreach (int input in path)
				{
					machine.Transform(input);
				}

				row.Add(machine.Transform(i));
				machine.Reset();
			}
			
			_cache.Add(path, row);

			return row;
		}
	}
}