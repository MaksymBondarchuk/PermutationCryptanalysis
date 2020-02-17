using System;

namespace PermutationCryptanalysis
{
	internal static class Program
	{
		private static void Main()
		{
			var machine = new Machine(5, 4);
			machine.OutputStateMatrix();
			Console.WriteLine();
			machine.OutputOutputMatrix();
		}
	}
}