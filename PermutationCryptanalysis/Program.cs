using System;

namespace PermutationCryptanalysis
{
	internal static class Program
	{
		private static void Main()
		{
			var machine = new Machine(4, 4);
			machine.OutputStateMatrix();
			Console.WriteLine();
			machine.OutputOutputMatrix();
			
			Console.WriteLine("--");
			
			var reversedMachine = new ReversedMachine(machine);
			reversedMachine.OutputStateMatrix();
			Console.WriteLine();
			reversedMachine.OutputOutputMatrix();
		}
	}
}