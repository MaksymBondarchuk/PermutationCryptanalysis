using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis
{
	internal static class Program
	{
		private const bool ArticleMode = false;
		
		private static void Main()
		{
			var machine = new Machine(m: 12, n: 8);
			Console.WriteLine(machine.InitialState);
			machine.OutputStateMatrix(ArticleMode);
			Console.WriteLine();
			machine.OutputOutputMatrix(ArticleMode);

			Console.WriteLine("--");

			var reversedMachine = new ReversedMachine(machine);
			Console.WriteLine(reversedMachine.InitialState);
			reversedMachine.OutputStateMatrix(ArticleMode);
			Console.WriteLine();
			reversedMachine.OutputOutputMatrix(ArticleMode);

			Console.WriteLine("--");
			// var inputs = new List<int> {0, 1, 2, 3};
			var inputs = new List<int> {0, 1, 2, 3, 4, 5, 6};
			List<int> outputs = machine.Transform(inputs).ToList();
			foreach (int o in outputs)
			{
				Console.Write($"{o,4}");
			}
			Console.WriteLine();
			
			Console.WriteLine("--");
			IEnumerable<int> restoredInputs = reversedMachine.Transform(outputs);
			foreach (int i in restoredInputs)
			{
				Console.Write($"{i,4}");
			}
			Console.WriteLine();
		}
	}
}