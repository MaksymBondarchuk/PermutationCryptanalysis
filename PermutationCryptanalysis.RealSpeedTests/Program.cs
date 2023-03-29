using System.Text;

namespace PermutationCryptanalysis.RealSpeedTests
{
	internal static class Program
	{
		private static async Task Main()
		{
			Console.OutputEncoding = Encoding.UTF8;
			await Article3Table13Builder.Build();
		}
	}
}