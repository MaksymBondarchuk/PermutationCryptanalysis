using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PermutationCryptanalysis
{
	public static class FileEncoder
	{
		public static void EncodeFile(string filename)
		{
			var machine = new Machine(byte.MaxValue + 1, byte.MaxValue + 1);
			var reversedMachine = new ReversedMachine(machine);

			string? encodedFile = Path.GetFileNameWithoutExtension(filename);
			string? ext = Path.GetExtension(filename);

			using var reader = new BinaryReader(File.Open(filename, FileMode.Open));
			string result = $"{Path.Combine(Path.GetDirectoryName(filename), encodedFile)}-encoded{ext}";
			string result1 = $"{Path.Combine(Path.GetDirectoryName(filename), encodedFile)}-decoded{ext}";
			using var writerEncoded = new BinaryWriter(File.Open(result, FileMode.Create));
			using var writerDecoded = new BinaryWriter(File.Open(result1, FileMode.Create));

			int counter = 0;

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			byte[] x = reader.ReadBytes(1);
			long length = (reader.BaseStream.Length / 10) * 10;
			long length10Percent = length / 10;
			while (x.Length != 0)
			{
				counter++;
				if (counter % length10Percent == 0)
				{
					Console.Clear();
					Console.WriteLine($"{counter * 100 / length}%");
				}

				byte y = machine.TransformOne(x.Single());
				writerEncoded.Write(y);

				byte xReverted = reversedMachine.TransformOne(y);
				writerDecoded.Write(xReverted);

				x = reader.ReadBytes(1);
			}

			Console.WriteLine(stopwatch.Elapsed);
			Console.WriteLine(result);
		}

		public static void EncodeFileOptimized(string filename)
		{
			var machine = new Machine(byte.MaxValue + 1, byte.MaxValue + 1);
			var reversedMachine = new ReversedMachine(machine);

			string? encodedFile = Path.GetFileNameWithoutExtension(filename);
			string? ext = Path.GetExtension(filename);

			using var reader = new BinaryReader(File.Open(filename, FileMode.Open));
			string encodedName = $"{Path.Combine(Path.GetDirectoryName(filename), encodedFile)}-encoded{ext}";
			string decodedName = $"{Path.Combine(Path.GetDirectoryName(filename), encodedFile)}-decoded{ext}";
			// using var writerEncoded = new BinaryWriter(File.Open(encodedName, FileMode.Create));
			// using var writerDecoded = new BinaryWriter(File.Open(decodedName, FileMode.Create));

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			byte[] inputs = File.ReadAllBytes(filename);
			var outputs = new byte[inputs.Length];
			var decoded = new byte[inputs.Length];
			for (int i = 0; i < inputs.Length; i++)
			{
				byte x = inputs[i];
				byte y = machine.TransformOne(x);
				byte xReverted = reversedMachine.TransformOne(y);
				outputs[i] = y;
				decoded[i] = xReverted;
			}

			File.WriteAllBytes(encodedName, outputs);
			File.WriteAllBytes(decodedName, decoded);
			Console.WriteLine(stopwatch.Elapsed);
			Console.WriteLine(encodedName);
		}
	}
}