using System.Collections;
using System.Linq;

namespace PermutationCryptanalysis.Extensions
{
	public static class IntExtensions
	{
		public static BitArray ToBitArray(this int number, int bitness)
		{
			var bitArray = new BitArray(new[] {number}) {Length = bitness};
			return bitArray;
			// var a = bitArray.Take<>(bitness);
			// return (new BitArray(new[] { number })).Take<bool>(bitness);
		}
	}
}