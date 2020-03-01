using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		public int M { get; protected set; }
		public int N { get; protected set; }
		public int InitialState { get; protected set; }
		
		public int Bitness { get; }

		public readonly List<List<int>> StateMatrix = new List<List<int>>();

		public readonly 
		public readonly List<List<BitArray>> OutputMatrix = new List<List<BitArray>>();

		private readonly Random _random = new Random();

		public Machine(int m, int n, int bitness)
		{
			M = m;
			N = n;
			Bitness = bitness;

			#region Generate

			InitialState = 1;
			InitialState = _random.Next(M);
			// StateMatrix = new List<List<int>>
			// {
			// 	new List<int>{0, 11, 4, 10, 3, 10, 4, 3},
			// 	new List<int>{6, 10,3,3,7,11,7,2},
			// 	new List<int>{9,11,0,6,9,4,8,5},
			// 	new List<int>{5,6,0,4,7,7,9,6},
			// 	new List<int>{10,11,4,2,3,6,9,3},
			// 	new List<int>{8,6,2,8,2,5,2,5},
			// 	new List<int>{7,7,7,8,10,6,6,0},
			// 	new List<int>{9,1,10,4,5,0,3,11},
			// 	new List<int>{6,1,7,5,1,11,11,9},
			// 	new List<int>{8,9,11,10,11,6,1,8},
			// 	new List<int>{8,10,11,0,9,8,0,10},
			// 	new List<int>{3,11,8,3,4,5,4,4},
			// };
			// OutputMatrix = new List<List<int>>
			// {
			// 	new List<int>{0,3,5,6,1,4,2,7},
			// 	new List<int>{5,2,3,7,4,6,1,0},
			// 	new List<int>{3,7,2,5,1,4,0,6},
			// 	new List<int>{0,2,7,3,4,6,5,1},
			// 	new List<int>{0,6,5,2,1,4,3,7},
			// 	new List<int>{2,0,5,4,3,7,6,1},
			// 	new List<int>{3,6,2,0,5,1,4,7},
			// 	new List<int>{3,0,7,4,1,5,2,6},
			// 	new List<int>{4,6,3,7,1,2,5,0},
			// 	new List<int>{3,0,6,7,4,1,2,5},
			// 	new List<int>{5,2,1,0,4,3,7,6},
			// 	new List<int>{5,1,6,0,2,7,4,3},
			// };
			
			for (int i = 0; i < M; i++)
			{
				StateMatrix.Add(new List<int>());
				OutputMatrix.Add(new List<BitArray>());
				for (int j = 0; j < N; j++)
				{
					StateMatrix[i].Add(_random.Next(M));
				}
			
				while (OutputMatrix[i].Count < N)
				{
					BitArray y = GenerateRandom();
					if (!OutputMatrix[i].Any(o => o.ToString().Equals(y.ToString())))
					{
						OutputMatrix[i].Add(y);
					}
				}
			}

			#endregion
		}

		#region Transform

		public IEnumerable<BitArray> Transform(IEnumerable<BitArray> inputs)
		{
			var outputs = new List<BitArray>();

			int state = InitialState;

			foreach (BitArray input in inputs)
			{
				outputs.Add(GetOutput(state, input));

				state = GetState(state, input);
			}

			return outputs;
		}

		#endregion

		#region Output

		public void OutputStateMatrix(bool articleMode = false)
		{
			OutputOneMatrix(StateMatrix, articleMode);
		}

		public void OutputOutputMatrix(bool articleMode = false)
		{
			OutputOneMatrix(OutputMatrix, articleMode);
		}

		private void OutputOneMatrix(List<List<int>> matrix, bool articleMode = false)
		{
			foreach (List<int> row in matrix)
			{
				foreach (int cell in row)
				{
					if (articleMode)
					{
						Console.Write($"{cell + 1,4}");
					}
					else
					{
						Console.Write($"{cell,4}");
					}
				}

				Console.WriteLine();
			}
		}

		private void OutputOneMatrix(List<List<BitArray>> matrix, bool articleMode = false)
		{
			foreach (List<BitArray> row in matrix)
			{
				foreach (BitArray cell in row)
				{
					if (articleMode)
					{
						Console.Write($"{cell,4}");
					}
					else
					{
						Console.Write($"{cell,4}");
					}
				}

				Console.WriteLine();
			}
		}

		#endregion
		
		private BitArray GetState(int state, BitArray input)
		{
			return StateMatrix[state][input];
		}

		private BitArray GetOutput(int state, BitArray input)
		{
			return OutputMatrix[state][input];
		}

		private BitArray GenerateRandom()
		{
			var value = new bool[Bitness];
			for (int b = 0; b < Bitness; b++)
			{
				value[b] = _random.NextDouble() < 0.5;
			}
			return new BitArray(value);
		}
	}
}