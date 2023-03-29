namespace PermutationCryptanalysis.Helpers;

public static class FormattingHelper
{
	public static string ToReadableSize(this long bytes)
	{
		if (bytes < 1024)
		{
			return $"{bytes} Б";
		}

		long kb = bytes / 1024;
		if (kb < 1024)
		{
			return $"{kb} КБ";
		}

		long mb = kb / 1024;
		if (mb < 1024)
		{
			return $"{mb} МБ";
		}

		long gb = mb / 1024;
		if (gb < 1024)
		{
			return $"{gb} ГБ";
		}

		long tb = gb / 1024;
		if (tb < 1024)
		{
			return $"{tb} ТБ";
		}

		throw new Exception($"Unsupported value {tb} TB");
	}

	public static string ToReadableTime(this double seconds)
	{
		if (1 < seconds)
		{
			if (seconds < 60)
			{
				return $"{seconds.ToClosestCeilingInt()} с";
			}

			double m = seconds / 60;
			if (m < 60)
			{
				return $"{m.ToClosestCeilingInt()} хв";
			}

			double h = m / 60;
			if (h < 60)
			{
				return $"{h.ToClosestCeilingInt()} год";
			}

			double d = h / 24;
			if (d < 365)
			{
				return $"{d.ToClosestCeilingInt()} днів";
			}

			long y = (d / 365).ToClosestCeilingInt();
			string yearsWord = y is >= 11 and <= 20
				? "років"
				: y % 10 == 1
					? "рік"
					: y % 10 is >= 2 and <= 4
						? "роки"
						: "років";
			return $"{y} {yearsWord}";
		}

		double ms = seconds * 1000;
		if (1 < ms)
		{
			return $"{ms.ToClosestCeilingInt()} мс";
		}

		double microSeconds = ms * 1000;
		if (1 < microSeconds)
		{
			return $"{microSeconds.ToClosestCeilingInt()} мкс";
		}

		double nanoSeconds = microSeconds * 1000;
		if (1 < nanoSeconds)
		{
			return $"{nanoSeconds.ToClosestCeilingInt()} нс";
		}

		throw new Exception($"Unsupported value {seconds} s");
	}

	public static int ToPowerOf10(this long number)
	{
		long x = number;
		var pow = 0;
		while (10 <= x)
			// while (x != 0)
		{
			pow++;
			x = x / 10;
		}

		return pow;
	}

	private static long ToClosestCeilingInt(this double value)
	{
		return Convert.ToInt64(Math.Ceiling(value));
	}
}