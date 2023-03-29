using PermutationCryptanalysis.Helpers;
using PermutationCryptanalysis.Machines;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;

namespace PermutationCryptanalysis.Tests;

public class Tests
{
	[SetUp]
	public void Setup()
	{
	}

	[TestCase(4, 4, 4, 4, ExpectedResult = true)]
	[TestCase(4, 6, 4, 4, ExpectedResult = true)]
	[TestCase(6, 4, 4, 4, ExpectedResult = true)]
	[TestCase(8, 8, 4, 4, ExpectedResult = true)]
	public bool Test_Machine_is_Bijective(int m, int n, int messageSizeFrom, int messageSizeTo)
	{
		// Assume
		var machine = new Machine(new RandomInitialStateAlgorithm(), new RandomNonRepeatingRowsOutputAlgorithm(), new UniqueStateColumnsAlgorithm(), m, n);

		// Act
		return machine.IsBijective(messageSizeFrom, messageSizeTo);
	}

	[TestCase(4, 4, 4, 4, ExpectedResult = true)]
	[TestCase(4, 6, 4, 4, ExpectedResult = true)]
	[TestCase(6, 4, 4, 4, ExpectedResult = true)]
	[TestCase(8, 8, 4, 4, ExpectedResult = true)]
	public bool Test_Random_States_Machine_is_Bijective(int m, int n, int messageSizeFrom, int messageSizeTo)
	{
		// Assume
		var machine = new Machine(new RandomInitialStateAlgorithm(), new RandomNonRepeatingRowsOutputAlgorithm(), new RandomStateAlgorithm(), m, n);

		// Act
		return machine.IsBijective(messageSizeFrom, messageSizeTo);
	}

	[TestCase(4, 4, 4, 4, ExpectedResult = true)]
	[TestCase(4, 6, 4, 4, ExpectedResult = true)]
	[TestCase(6, 4, 4, 4, ExpectedResult = true)]
	[TestCase(8, 8, 4, 4, ExpectedResult = true)]
	public bool Test_DoubledMachine_is_Bijective(int m, int n, int messageSizeFrom, int messageSizeTo)
	{
		// Assume
		var machine = new DoubledMachine(new RandomInitialStateAlgorithm(), new RandomNonRepeatingRowsOutputAlgorithm(), new UniqueStateColumnsAlgorithm(), m, n);

		// Act
		return machine.IsBijective(messageSizeFrom, messageSizeTo);
	}
}