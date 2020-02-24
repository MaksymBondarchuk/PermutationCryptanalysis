using System.Collections.Generic;

namespace PermutationCryptanalysis
{
	public class ReversedMachine: Machine
	{
		public ReversedMachine(Machine directMachine) : base(directMachine.M, directMachine.N)
		{
			M = directMachine.M;
			N = directMachine.N;

			#region Generate

			InitialState = directMachine.InitialState;
			
			StateMatrix.Clear();
			OutputMatrix.Clear();
			for (int i = 0; i < M; i++)
			{
				StateMatrix.Add(new List<int>());
				OutputMatrix.Add(new List<int>());
				
				// for (int j = 0; j < N; j++)
				// {
				// 	StateMatrix[i].Add(directMachine.StateMatrix[i][j]);
				// }

				for (int j = 0; j < N; j++)
				{
					for (int k = 0; k < N; k++)
					{
						if (directMachine.OutputMatrix[i][k] == j)
						{
							OutputMatrix[i].Add(k);
							// StateMatrix[i].Add(k);
							StateMatrix[i].Add(directMachine.StateMatrix[i][k]);
							break;
						}
					}
				}
			}
			
			#endregion
		}
	}
}