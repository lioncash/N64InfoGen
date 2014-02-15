namespace N64InfoGen.Utils
{
	/// <summary>
	/// Utility class providing extension methods for byte swapping.
	/// </summary>
	public static class ByteSwapUtil
	{
		public static short SwapBytes(this short x)
		{
			return (short) SwapBytes((ushort) x);
		}

		public static ushort SwapBytes(this ushort x)
		{
			return (ushort)((x & 0x00FF) << 8 | 
			                (x & 0xFF00) >> 8);
		}

		public static int SwapBytes(this int x)
		{
			return (int) SwapBytes((uint)x);
		}

		public static uint SwapBytes(this uint x)
		{
			return ((x & 0x000000FF) << 24) |
			       ((x & 0x0000FF00) <<  8) |
			       ((x & 0x00FF0000) >>  8) |
			       ((x & 0xFF000000) >> 24);
		}

		public static long SwapBytes(this long x)
		{
			return (long) SwapBytes((ulong) x);
		}

		public static ulong SwapBytes(this ulong x)
		{
			return (x & 0x00000000000000FFUL) << 56 |
			       (x & 0x000000000000FF00UL) << 40 |
			       (x & 0x0000000000FF0000UL) << 24 |
			       (x & 0x00000000FF000000UL) <<  8 |
			       (x & 0x000000FF00000000UL) >>  8 |
			       (x & 0x0000FF0000000000UL) >> 24 |
			       (x & 0x00FF000000000000UL) >> 40 |
			       (x & 0xFF00000000000000UL) >> 56;
		}

		public static byte[] SwapBytes(this byte[] arr)
		{
			for (int i = 0; i < arr.Length; i += 2)
			{
				byte temp = arr[i];
				arr[i+0] = arr[i+1];
				arr[i+1] = temp;
			}

			return arr;
		}

		public static char[] SwapChars(this char[] arr)
		{
			for (int i = 0; i < arr.Length; i += 2)
			{
				char temp = arr[i];
				arr[i+0] = arr[i+1];
				arr[i+1] = temp;
			}

			return arr;
		}
	}
}
