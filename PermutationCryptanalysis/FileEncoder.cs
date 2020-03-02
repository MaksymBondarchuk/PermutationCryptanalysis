using System;
using System.Collections;
using System.IO;
using PermutationCryptanalysis.Extensions;

namespace PermutationCryptanalysis
{
	public class FileEncoder
	{
		public static void EncodeFile(string filename, int bitness)
		{
			int n = Convert.ToInt32(Math.Pow(2, bitness));
			var machine = new Machine(n * 2, n);

			string? encodedFile = Path.GetFileNameWithoutExtension(filename);
			string? ext = Path.GetExtension(filename);

			using var reader = new BinaryReader(File.Open(filename, FileMode.Open));
			using var writer = new BinaryWriter(File.Open($"{encodedFile}-encoded{ext}", FileMode.Create));

			byte[] x = reader.ReadBytes(bitness);
			while (x.Length != 0)
			{
				int y = machine.TransformOne(new BitArray(x).ToInt());
				// writer.Write(y.ToBitArray(bitness));
			}
		}
	}
}