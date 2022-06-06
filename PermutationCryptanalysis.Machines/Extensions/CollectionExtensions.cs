using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermutationCryptanalysis.Machines.Extensions
{
	public static class CollectionExtensions
	{
		public static List<List<T>> Clone<T>(this List<List<T>> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			var duplicate = new List<List<T>>(collection.Count);
			duplicate.AddRange(collection.Select(path => new List<T>(path)));
			return duplicate;
		}

		public static List<List<T>> SequenceDistinct<T>(this List<List<T>> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			var distinct = new List<List<T>>();
			foreach (List<T> item in collection.Where(item => !distinct.Any(i => i.SequenceEqual(item))))
			{
				distinct.Add(item);
			}

			return distinct;
		}

		public static void AddDistinct<T>(this List<List<T>> collection, List<T> value)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (!collection.Any(item => item.SequenceEqual(value)))
			{
				collection.Add(value);
			}
		}

		public static int SequenceIndexOf<T>(this List<List<T>> collection, List<T> value)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			List<List<T>> matches = collection.Where(item => item.SequenceEqual(value)).ToList();
			if (1 < matches.Count)
			{
				throw new ArgumentException("More than 1 match found. Collection is not distinct", nameof(collection));
			}
			if (!matches.Any())
			{
				throw new ArgumentException("No matches found", nameof(collection));
			}

			return collection.IndexOf(matches.Single());
		}

		public static string ToHumanReadableString(this List<int> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}
			
			var sb = new StringBuilder();
			foreach (int item in collection)
			{
				sb.Append($"{item,4}");
			}

			return sb.ToString();
		}
	}
}