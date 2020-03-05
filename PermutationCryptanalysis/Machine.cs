using System;
using System.Collections.Generic;

namespace PermutationCryptanalysis
{
	public class Machine
	{
		#region Initialization

		public int M { get; protected set; }
		public int N { get; protected set; }
		public int InitialState { get; protected set; }

		protected int State { get; set; }

		public readonly List<List<byte>> StateMatrix = new List<List<byte>>();

		public readonly List<List<byte>> OutputMatrix = new List<List<byte>>();

		private readonly Random _random = new Random();

		public Machine(int m, int n)
		{
			M = m;
			N = n;

			#region Generate

			// InitialState = 1;
			InitialState = (byte) _random.Next(M);
			State = InitialState;
			// StateMatrix = new List<List<byte>>
			// {
			// 	new List<byte>{0, 11, 4, 10, 3, 10, 4, 3},
			// 	new List<byte>{6, 10,3,3,7,11,7,2},
			// 	new List<byte>{9,11,0,6,9,4,8,5},
			// 	new List<byte>{5,6,0,4,7,7,9,6},
			// 	new List<byte>{10,11,4,2,3,6,9,3},
			// 	new List<byte>{8,6,2,8,2,5,2,5},
			// 	new List<byte>{7,7,7,8,10,6,6,0},
			// 	new List<byte>{9,1,10,4,5,0,3,11},
			// 	new List<byte>{6,1,7,5,1,11,11,9},
			// 	new List<byte>{8,9,11,10,11,6,1,8},
			// 	new List<byte>{8,10,11,0,9,8,0,10},
			// 	new List<byte>{3,11,8,3,4,5,4,4},
			// };
			// OutputMatrix = new List<List<byte>>
			// {
			// 	new List<byte>{0,3,5,6,1,4,2,7},
			// 	new List<byte>{5,2,3,7,4,6,1,0},
			// 	new List<byte>{3,7,2,5,1,4,0,6},
			// 	new List<byte>{0,2,7,3,4,6,5,1},
			// 	new List<byte>{0,6,5,2,1,4,3,7},
			// 	new List<byte>{2,0,5,4,3,7,6,1},
			// 	new List<byte>{3,6,2,0,5,1,4,7},
			// 	new List<byte>{3,0,7,4,1,5,2,6},
			// 	new List<byte>{4,6,3,7,1,2,5,0},
			// 	new List<byte>{3,0,6,7,4,1,2,5},
			// 	new List<byte>{5,2,1,0,4,3,7,6},
			// 	new List<byte>{5,1,6,0,2,7,4,3},
			// };

			for (int i = 0; i < M; i++)
			{
				StateMatrix.Add(new List<byte>());
				OutputMatrix.Add(new List<byte>());
				for (int j = 0; j < N; j++)
				{
					StateMatrix[i].Add((byte) _random.Next(M));
				}

				while (OutputMatrix[i].Count < N)
				{
					byte y = (byte) _random.Next(N);
					if (!OutputMatrix[i].Contains(y))
					{
						OutputMatrix[i].Add(y);
					}
				}
			}

			#endregion
		}

		#endregion

		#region Transform

		public IEnumerable<byte> Transform(IEnumerable<byte> inputs)
		{
			var outputs = new List<byte>();

			int state = InitialState;

			foreach (byte input in inputs)
			{
				outputs.Add(GetOutput(state, input));

				state = GetState(state, input);
			}

			return outputs;
		}

		public byte TransformOne(byte input)
		{
			byte output = GetOutput(State, input);

			State = GetState(State, input);

			return output;
		}

		public void Reset()
		{
			State = InitialState;
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

		private void OutputOneMatrix(List<List<byte>> matrix, bool articleMode = false)
		{
			foreach (List<byte> row in matrix)
			{
				foreach (byte cell in row)
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

		#endregion

		#region States & Outputs

		private byte GetState(int state, byte input)
		{
			return StateMatrix[state][input];
		}

		private byte GetOutput(int state, byte input)
		{
			try
			{
				return OutputMatrix[state][input];
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		#endregion
	}
}