using System;
using System.Collections;

namespace PermutationCryptanalysis.Extensions
{
	public static class BitArrayExtensions
	{
		public static int ToInt(this BitArray bitArray)
		{
			int len = Math.Min((sizeof(int)) * 8, bitArray.Count);
			int n = 0;

			for (int i = 0; i < len; i++)
			{
				{
					if (bitArray.Get(i))
					{
						n |= 1 << i;
					}
				}
			}

			return n;
		}

		public static void Print(this IEnumerable myList, int myWidth)
		{
			int i = myWidth;
			foreach (object obj in myList)
			{
				if (i <= 0)
				{
					i = myWidth;
					Console.WriteLine();
				}

				i--;
				Console.Write("{0,8}", obj);
			}

			Console.WriteLine();
		}
	}
}