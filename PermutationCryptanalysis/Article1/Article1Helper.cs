using System;
using PermutationCryptanalysis.Helpers;
using PermutationCryptanalysis.Machines;
using PermutationCryptanalysis.Machines.Algorithms;
using PermutationCryptanalysis.Machines.Algorithms.InitialState;
using PermutationCryptanalysis.Machines.Algorithms.Outputs;
using PermutationCryptanalysis.Machines.Algorithms.States;

namespace PermutationCryptanalysis.Article1;

public static class Article1Helper
{
	private const int M1 = 12;
	private const int M2 = 10;
	private const int N = 8;
	
	public static void BuildDirectMachines()
	{
		// This code was used to once generate tables (they are in "University\PhD\Publications\3. Article #3\Tables.txt" file)
		// Running it again won't give any result (as results were saved to Tables.txt)
		
		var machine1 = new Machine(new RandomInitialStateAlgorithm(), new RandomNonRepeatingRowsOutputAlgorithm(), new ConnectedGraphStateAlgorithm(), M1, N);
		machine1.WriteMachine(true);
		Console.WriteLine(machine1.IsBijective(4, 4));
	}
	
	public static void BuildReverseMachines()
	{
		var machine1 = new Machine(new RandomInitialStateAlgorithm(), new HaitArticleAlgorithm(), new HaitArticleAlgorithm(), M1, N);
		var machine3 = new ReversedMachine(machine1);
		machine3.WriteMachine(true);
		Console.WriteLine(machine3.IsBijective(4, 4));
	}
}