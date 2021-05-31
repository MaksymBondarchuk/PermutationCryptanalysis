using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PermutationCryptanalysis.Machine.Extensions;

namespace PermutationCryptanalysis
{
	public static class PathHelper
	{
		public static List<int> GetFirstPath(int m, int n)
		{
			var path = new List<int>(m);
			for (var state = 0; state < m; state++)
			{
				path.Add(0);
			}

			return path;
		}

		public static bool CanIncrement(List<int> path, int n)
		{
			return path.Any(state => state < n - 1);
		}

		public static List<int> Increment(List<int> path, int n)
		{
			var newPath = new List<int>(path);
			newPath[^1]++;
			for (int i = newPath.Count - 1; 0 <= i; i--)
			{
				if (newPath[i] == n)
				{
					newPath[i] = 0;
					if (newPath[i - 1] == n)
					{
						throw new Exception($"Cannot increment path '{path.ToHumanReadableString()}'");
					}
					newPath[i - 1]++;
				}
			}

			if (newPath.Any(node => n < node))
			{
				throw new Exception($"Invalid path generated: '{newPath.ToHumanReadableString()}'");
			}
			
			// Console.WriteLine($"New path is {newPath.ToHumanReadableString()}");
			return newPath;
		}
	}
}