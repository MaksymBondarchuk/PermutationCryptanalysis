using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis
{
	internal static class Program
	{
		private const bool ArticleMode = true;
		private const byte M = 12;
		private const byte N = 8;

		private static void Main()
		{
			FileEncoder.EncodeFile("C:\\Users\\maksym.bondarchuk\\Downloads\\P80226-084449.jpg");
			// FileEncoder.EncodeFile("data.txt");
			return;

			var machine = new Machine(byte.MaxValue + 1, byte.MaxValue + 1);
			// Console.WriteLine(machine.InitialState);
			// machine.OutputStateMatrix(ArticleMode);
			// Console.WriteLine();
			// machine.OutputOutputMatrix(ArticleMode);

			// Console.WriteLine("--");

			var reversedMachine = new ReversedMachine(machine);
			// Console.WriteLine(reversedMachine.InitialState);
			// reversedMachine.OutputStateMatrix(ArticleMode);
			// Console.WriteLine();
			// reversedMachine.OutputOutputMatrix(ArticleMode);

			// Console.WriteLine("--");
			var rnd = new Random();
			var inputs = new List<byte>{0, 255};
			int count = rnd.Next(35);
			for (int i = 0; i < count; i++)
			{
				inputs.Add((byte)rnd.Next(N));
			}
			foreach (int i in inputs)
			{
				Console.Write($"{i,4}");
			}
			Console.WriteLine();

			var outputs = new List<byte>();
			foreach (byte y in inputs.Select(i => machine.TransformOne(i)))
			{
				outputs.Add(y);
				Console.Write($"{y,4}");
			}
			Console.WriteLine();

			Console.WriteLine("--");
			foreach (byte x in outputs.Select(o => reversedMachine.TransformOne(o)))
			{
				Console.Write($"{x,4}");
			}
			Console.WriteLine();
		}
	}
}