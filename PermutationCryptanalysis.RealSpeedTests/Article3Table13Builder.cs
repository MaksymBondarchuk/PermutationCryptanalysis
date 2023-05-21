using PermutationCryptanalysis.Helpers;
using PermutationCryptanalysis.Machines;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;

namespace PermutationCryptanalysis.RealSpeedTests;

// Час та швидкість перетворення за допомогою програмної реалізації підстановки - now table 12
public static class Article3Table13Builder
{
	public static async Task Build()
	{
		// r is sequence length in bytes (because d=8 (n=m=m1=m2=256) in articles
		long[] rValues = new[]
		{
			// 1024,
			// 2048,
			// 4096,
			// 8192,
			// 16384,
			// 32768,
			// 65536,
			// 131072,
			// 262144,
			// 524288,
			// 1048576,
			// 2097152,
			// 4194304,
			// 8388608,
			10_000_000,
			100_000_000,
			1_000_000_000,
			10_000_000_000,
			100_000_000_000,
			// 1_000_000_000_000,
			// 10_000_000_000_000
		};
		var article1Machine = new Machine(new RandomInitialStateAlgorithm(), new RandomNonRepeatingRowsOutputAlgorithm(), new RandomStateAlgorithm(), 256, 256);
		// Debug.Assert(article1Machine.IsBijective(4, 4));
		var article3Machine = new DoubledMachine(new RandomInitialStateAlgorithm(), new RandomNonRepeatingRowsOutputAlgorithm(), new RandomStateAlgorithm(), 256, 256);
		// Debug.Assert(article3Machine.IsBijective(4, 4));

		var resultRows = new List<string>{"r pow;r;T1;V1;T3;V3"};
		Console.WriteLine(resultRows[0]);
		byte[] message = MessageGenerator.Generate(1000);
		foreach (long r in rValues)
		{
			TimeSpan article1Time = TimeMeasurer.Measure(article1Machine, message, r / 1000);
			TimeSpan article3Time = TimeMeasurer.Measure(article3Machine, message, r / 1000);
			int article1Speed = CalculateSpeed(article1Time, r);
			int article3Speed = CalculateSpeed(article3Time, r);
			var row = $"{r.ToPowerOf10()};{r.ToReadableSize()};{article1Time.TotalSeconds.ToReadableTime()};{article1Speed};{article3Time.TotalSeconds.ToReadableTime()};{article3Speed}";
			Console.WriteLine(row);
			resultRows.Add(row);
		}
		await File.WriteAllLinesAsync("Table13.csv", resultRows);
	}

	private static int CalculateSpeed(TimeSpan time, long r)
	{
		return (int)(r /* байт */ / time.TotalSeconds / 1024 /* кілобайт */ / 1024 /* мегабайт */);
	}
}