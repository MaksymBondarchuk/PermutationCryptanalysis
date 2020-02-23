using System;

namespace PermutationCryptanalysis
{
	internal static class Program
	{
		private static void Main()
		{
			var machine = new Machine(12, 8);
			machine.OutputStateMatrix(true);
			Console.WriteLine();
			machine.OutputOutputMatrix(true);
			
			Console.WriteLine("--");
			
			var reversedMachine = new ReversedMachine(machine);
			reversedMachine.OutputStateMatrix(true);
			Console.WriteLine();
			reversedMachine.OutputOutputMatrix(true);
		}
	}
}