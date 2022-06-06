using PermutationCryptanalysis.Machines;

namespace PermutationCryptanalysis.Helpers
{
	public static class MachineWriter
	{
		public static void WriteMachine(this Machine machine, bool articleMode = false)
		{
			Console.WriteLine($"Initial State is {machine.InitialState}");
			machine.WriteOutputMatrix("Output Matrix:", articleMode);
			Console.WriteLine();
			machine.WriteStateMatrix("State Matrix:", articleMode);
			Console.WriteLine($"Is machine bijective? {machine.IsBijective(4, 4)}");
		}

		public static void WriteStateMatrix(this Machine machine, string? title = null, bool articleMode = false)
		{
			WriteOneMatrix(machine.StateMatrix, title, articleMode);
		}

		public static void WriteOutputMatrix(this Machine machine, string? title = null, bool articleMode = false)
		{
			WriteOneMatrix(machine.OutputMatrix, title, articleMode);
		}

		public static void WriteOneMatrix(List<List<int>> matrix, string? title = null, bool articleMode = false)
		{
			if (!string.IsNullOrWhiteSpace(title))
			{
				Console.WriteLine(title);
			}
			
			ConsoleColor backgroundColor = Console.BackgroundColor;
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.Write("    ");
			// Console.BackgroundColor = ConsoleColor.Gray;
			Console.ForegroundColor = ConsoleColor.Green;
			for (var i = 0; i < matrix.First().Count; i++)
			{
				if (articleMode)
				{
					Console.Write($"{i + 1,4}");
				}
				else
				{
					Console.Write($"{i,4}");
				}
			}

			Console.WriteLine();
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;

			// Console.WriteLine(new string('_', matrix.First().Count * 4));

			foreach (List<int> row in matrix)
			{
				// Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Green;
				if (articleMode)
				{
					Console.Write($"{matrix.IndexOf(row) + 1,4}");
				}
				else
				{
					Console.Write($"{matrix.IndexOf(row),4}");
				}

				Console.BackgroundColor = backgroundColor;
				Console.ForegroundColor = foregroundColor;

				foreach (int cell in row)
				{
					if (articleMode)
					{
						Console.Write($"{cell + 1,4}");
					}
					else
					{
						Console.Write($"{cell,4}");
					}
				}

				Console.WriteLine();
			}
		}
	}
}