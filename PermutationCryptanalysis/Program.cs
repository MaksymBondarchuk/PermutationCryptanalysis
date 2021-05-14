using System;
using System.Collections.Generic;
using System.Linq;
using PermutationCryptanalysis.Machine;
using PermutationCryptanalysis.Machine.Algorithms;

namespace PermutationCryptanalysis
{
	internal static class Program
	{
		private const bool ArticleMode = false;
		private const int M = 4;
		private const int N = 4;

		private static void Main()
		{
			// var machine = new Machine.Machine(new UniqueOutputRowsAlgorithm(), m: M, n: N);
			// Console.WriteLine(machine.InitialState);
			// machine.OutputStateMatrix(ArticleMode);
			// Console.WriteLine("---------------------------");
			// machine.OutputOutputMatrix(ArticleMode);
			// Console.WriteLine(machine.IsBijective(4, 4));

			var test = new NonInitialResettableTest();
			test.Run(M, N, ArticleMode);

			// var inputs = new List<int> {0, 1, 2, 3};
			// List<int>? outputsDirect = machine.Transform(inputs).ToList();
			// foreach (int i in outputsDirect)
			// {
			// 	Console.Write($"{i,4}");
			// }
			// Console.WriteLine();
			// Console.WriteLine("--");
			//
			// var reversedMachine = new ReversedMachine(machine);
			// Console.WriteLine(reversedMachine.InitialState);
			// reversedMachine.OutputStateMatrix(ArticleMode);
			// Console.WriteLine();
			// reversedMachine.OutputOutputMatrix(ArticleMode);
			// Console.WriteLine(machine.IsBijective(4, 4));
			//
			// List<int>? outputsReverse = reversedMachine.Transform(outputsDirect).ToList();
			// foreach (int i in outputsReverse)
			// {
			// 	Console.Write($"{i,4}");
			// }
			// Console.WriteLine();

			// Console.WriteLine("--");
			// // var inputs = new List<int> {0, 1, 2, 3};
			// var rnd = new Random();
			// var inputs = new List<int>();
			// int count = rnd.Next(35);
			// for (int i = 0; i < count; i++)
			// {
			// 	inputs.Add(rnd.Next(N));
			// }
			//
			// // var inputs = new List<int> {0, 1, 2, 3, 4, 5, 6};
			// // var inputs = new List<int> {0, 4, 2, 7, 3, 6};
			// foreach (int i in inputs)
			// {
			// 	Console.Write($"{i+1,4}");
			// }
			// Console.WriteLine();
			//
			// List<int> outputs = machine.Transform(inputs).ToList();
			// foreach (int o in outputs)
			// {
			// 	Console.Write($"{o+1,4}");
			// }
			// Console.WriteLine();
			//
			// Console.WriteLine("--");
			// IEnumerable<int> restoredInputs = reversedMachine.Transform(outputs);
			// foreach (int i in restoredInputs)
			// {
			// 	Console.Write($"{i+1,4}");
			// }
			// Console.WriteLine();
		}
	}
}