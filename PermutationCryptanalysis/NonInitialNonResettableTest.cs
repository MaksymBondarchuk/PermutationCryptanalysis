using System;
using System.Collections.Generic;
using System.Diagnostics;
using PermutationCryptanalysis.Machine;
using PermutationCryptanalysis.Machine.Algorithms.InitialState;
using PermutationCryptanalysis.Machine.Algorithms.Outputs;
using PermutationCryptanalysis.Machine.Algorithms.States;

namespace PermutationCryptanalysis
{
	public class NonInitialNonResettableTest
	{
		public void Run(int m, int n, bool articleMode)
		{
			var machine = new Machine.Machine(new RandomInitialStateAlgorithm(), new UniqueOutputRowsAlgorithm(), new ConnectedGraphStateAlgorithm(), m, n);
			machine.WriteMachine();

			List<List<int>> outputTable = HackOutputTable(machine, m, n);
		}

		private List<List<int>> HackOutputTable(Machine.Machine machine, int m, int n)
		{
			var outputTable = new List<List<int>>();
			for (var i = 0; i < m; i++)
			{
				outputTable.Add(new List<int>());
				for (var j = 0; j < n; j++)
				{
					outputTable[i].Add(-1);
				}
			}

			for (var i = 0; i < n; i++)
			{
				HackOutputTableColumn(machine, m, n, i);
			}

			return outputTable;
		}

		private void HackOutputTableColumn(Machine.Machine machine, int m, int n, int columnIndex)
		{
			var column = new List<int>(2 * m);
			for (var i = 0; i < 2 * m; i++)
			{
				column.Add(machine.Transform(columnIndex));
			}

			for (var i = 0; i < m; i++)
			{
				if (column[i] != column[m + i])
				{
					Console.WriteLine($"Bad column {columnIndex}:");
					for (var j = 0; j < 2 * m; j++)
					{
						Console.WriteLine(column[j]);
					}

					Debugger.Break();
					throw new Exception("Bad");
				}
			}

			Console.WriteLine($"{columnIndex} all good");
		}
	}
}