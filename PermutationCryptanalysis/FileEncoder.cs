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
			using var writerEncoded = new BinaryWriter(File.Open(result, FileMode.Create));
			using var writerDecoded = new BinaryWriter(File.Open($"{encodedFile}-decoded{ext}", FileMode.Create));

			int counter = 0;

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			byte[] x = reader.ReadBytes(1);
			while (x.Length != 0)
			{
				counter++;
				Console.WriteLine($"{Math.Floor(counter / (double) reader.BaseStream.Length * 100)}%");

				byte y = machine.TransformOne(x.Single());
				writerEncoded.Write(y);

				byte xReverted = reversedMachine.TransformOne(y);
				writerDecoded.Write(xReverted);

				x = reader.ReadBytes(1);
			}
			
			Console.Clear();
			Console.WriteLine(stopwatch.Elapsed);
			Console.WriteLine(result);
		}
	}
}