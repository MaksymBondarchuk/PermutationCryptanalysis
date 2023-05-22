using System.Diagnostics;
using PermutationCryptanalysis.Machines.Interfaces;

namespace PermutationCryptanalysis.RealSpeedTests;

public static class TimeMeasurer
{
	public static TimeSpan Measure(IMachine machine, int[] message, long transformSameMessageTimes)
	{
		Stopwatch stopwatch = new();
		stopwatch.Start();
		// long onePercentSize = transformSameMessageTimes / 100;
		// Console.WriteLine($"One percent pow is {onePercentSize.ToPowerOf10()}");
		for (var i = 0; i < transformSameMessageTimes; i++)
		{
			// if ((i + 1) % onePercentSize == 0)
			// {
				// Console.WriteLine($"Progress: {(i + 1) / onePercentSize}%. Elapsed: {stopwatch.Elapsed}");
			// }

			// foreach (byte x in message)
			// {
				// machine.Transform(x);
			// }
			
			machine.Transform(message);
		}

		stopwatch.Stop();
		return stopwatch.Elapsed;
	}
}