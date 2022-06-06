using PermutationCryptanalysis.Machines.Interfaces;

namespace PermutationCryptanalysis.Helpers;

public static class MachineCharacteristicsExtensions
{
	#region Bijectivness

	public static bool IsBijective(this IResettableMachine machine, int messageSizeFrom, int messageSizeTo)
	{
		for (int messageSize = messageSizeFrom; messageSize <= messageSizeTo; messageSize++)
		{
			var outputs = new List<List<int>>();

			List<int> inputs = GenerateFirstMessage(messageSize);
			machine.Reset();
			while (CanBeIncremented(machine, inputs))
			{
				List<int> current = machine.Transform(inputs).ToList();

				if (outputs.Any(output => current.SequenceEqual(output)))
				{
					return false;
				}

				outputs.Add(current);
				IncrementMessage(machine, inputs);
				machine.Reset();
			}
		}

		return true;
	}

	#endregion

	#region Equivalence

	public static bool IsEquivalentTo(this IResettableMachine machine, IResettableMachine other, int messageSizeFrom, int messageSizeTo)
	{
		for (int messageSize = messageSizeFrom; messageSize <= messageSizeTo; messageSize++)
		{
			List<int> inputs = GenerateFirstMessage(messageSize);
			machine.Reset();
			other.Reset();
			while (CanBeIncremented(machine, inputs))
			{
				List<int> thisOutput = machine.Transform(inputs).ToList();
				List<int> otherOutput = other.Transform(inputs).ToList();

				if (!thisOutput.SequenceEqual(otherOutput))
				{
					return false;
				}

				IncrementMessage(machine, inputs);
				machine.Reset();
				other.Reset();
			}
		}

		return true;
	}

	#endregion

	#region Messages

	private static List<int> GenerateFirstMessage(int messageSize)
	{
		var inputs = new List<int>();
		for (var i = 0; i < messageSize; i++)
		{
			inputs.Add(0);
		}

		return inputs;
	}

	private static bool CanBeIncremented(IMachine machine, List<int> inputs)
	{
		return inputs.All(x => x < machine.N);
	}

	private static void IncrementMessage(IMachine machine, List<int> inputs)
	{
		inputs[^1]++;
		for (int i = inputs.Count - 1; 0 < i; i--)
		{
			if (inputs[i] == machine.N)
			{
				inputs[i] = 0;
				inputs[i - 1]++;
			}
		}
	}

	#endregion
}