using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using PermutationCryptanalysis.ComplexitiesTableBuilder.Models;

namespace PermutationCryptanalysis.ComplexitiesTableBuilder
{
	internal static class Program
	{
		private const char CsvDelimiter = ';';
		
		private static async Task Main()
		{
			var combinations = new List<Tuple<int, int>>
			{
				new(4, 4),
				new(4, 8),
				new(4, 16),
				new(16, 4),
				new(16, 16),
				new(16, 32),
				new(16, 64),
				new(32, 16),
				new(32, 32),
				new(32, 64),
				new(64, 32),
				new(64, 64),
				new(64, 128),
				new(64, 256),
				new(128, 64),
				new(128, 128),
				new(128, 256),
				new(256, 16),
				new(256, 32),
				new(256, 64),
				new(256, 128),
				new(256, 256)
			};

			var sb = new StringBuilder();
			sb.AppendLine($"n{CsvDelimiter}m{CsvDelimiter}a1{CsvDelimiter}a2 from (pow for O(10^x)){CsvDelimiter}a2 to (pow for O(10^x))");
			foreach ((int n, int m) in combinations)
			{
				long a1 = InitialResettableTest.CalculateComplexity(m, n);
				BigInteger a2 = NonInitialResettableTest.CalculateComplexityBig(m, n);
				
				var a2Range = new BigRange
				{
					FromBin = (uint)a2.GetBitLength() - 1,
					ToBin = (uint)a2.GetBitLength()
				};
				
				sb.AppendLine($"{n}{CsvDelimiter}{m}{CsvDelimiter}{a1}{CsvDelimiter}{a2Range.FromDec}{CsvDelimiter}{a2Range.ToDec}");
			}

			await File.WriteAllTextAsync("Table3.csv", sb.ToString());
			Console.WriteLine("Results written to Table3.csv");
		}
	}
}