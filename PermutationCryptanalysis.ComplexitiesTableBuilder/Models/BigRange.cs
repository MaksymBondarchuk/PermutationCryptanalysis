using System;

namespace PermutationCryptanalysis.ComplexitiesTableBuilder.Models
{
	public class BigRange
	{
		public uint FromBin { get; init; }

		public uint FromDec => Convert.ToUInt32(Math.Floor(FromBin * Math.Log2(10)));

		public uint ToBin { get; init; }
		
		public uint ToDec => Convert.ToUInt32(Math.Ceiling(ToBin * Math.Log2(10)));
	}
}