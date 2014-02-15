using System;

namespace N64InfoGen
{
	/// <summary>
	/// Info dumper for N64 ROM files. Only compatible with Z64.
	/// TODO: Add support for V64 (easy as just checking for two bytes 
	/// and just not swapping bytes if they match a certain number.)
	/// </summary>
	internal sealed class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Invalid number of arguments.");
				DisplayUsage();
				return;
			}

			Parser.ParseFiles(args[0]);
		}

		private static void DisplayUsage()
		{
			Console.WriteLine("How to use it:");
			Console.WriteLine("    Individual file: n64crcgen romfile");
			Console.WriteLine("    Folder of ROMs : n64crcgen directory");
			Console.WriteLine();
			Console.WriteLine("Text file ROMCRCs.txt will be outputted into the same directory it's called in.");
		}
	}
}
