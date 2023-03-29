namespace PermutationCryptanalysis.RealSpeedTests;

public static class MessageGenerator
{
	public static byte[] Generate(long length)
	{
		var result = new byte[length];
		var random = new Random();
		random.NextBytes(result);
		return result;
	}
}