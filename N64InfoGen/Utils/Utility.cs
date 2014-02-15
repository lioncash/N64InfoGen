using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace N64InfoGen.Utils
{
	/// <summary>
	/// Class that contains general utility methods
	/// </summary>
	internal static class Utility
	{
		/// <summary>
		/// Reads a given amount of bytes from the given stream.
		/// </summary>
		/// <param name="fs">The <see cref="Stream"/> to read from.</param>
		/// <param name="size">The number of bytes to read.</param>
		/// <returns>A byte array containing the bytes read.</returns>
		public static byte[] ReadBytes(Stream fs, int size)
		{
			byte[] arr = new byte[size];

			fs.Read(arr, 0, arr.Length);

			return arr;
		}

		/// <summary>
		/// Gets the MD5 hash of an entire file.
		/// </summary>
		/// <param name="filePath">Path to the file.</param>
		/// <returns>MD5 string</returns>
		public static string GetFileMD5(string filePath)
		{
			byte[] fBytes = File.ReadAllBytes(filePath);
			byte[] fileMD5 = MD5.Create().ComputeHash(fBytes);
			StringBuilder md5Builder = new StringBuilder();
			foreach (byte b in fileMD5)
			{
				md5Builder.Append(b.ToString("X"));
			}

			return md5Builder.ToString();
		}

		// Writes all of the parsed data to a text file.
		public static void WriteToFile(TextWriter tw, string fileName, string internalName, uint crc1, uint crc2,
		                               string fileMD5, char manufacturerID, string cartridgeID, char countryCode)
		{
			string crcOne = crc1.ToString("X");
			string crcTwo = crc2.ToString("X");

			// Handle case of truncated zero.
			// 8 == Length of the CRC strings.
			if (crcOne.Length < 8)
				HandleZeroTruncation(ref crcOne, 8-crcOne.Length);
			if (crcTwo.Length < 8)
				HandleZeroTruncation(ref crcTwo, 8-crcOne.Length);

			tw.WriteLine("------------------------------------------------------------");
			tw.WriteLine("File Name       : {0}", fileName);
			tw.WriteLine("Internal Name   : {0}", internalName);
			tw.WriteLine("CRC 1           : {0}", crcOne);
			tw.WriteLine("CRC 2           : {0}", crcTwo);
			tw.WriteLine("Entire File MD5 : {0}", fileMD5);
			tw.WriteLine("Manufacturer ID : {0} (0x{1:X})", manufacturerID, (int)manufacturerID);
			tw.WriteLine("Cartridge ID    : {0}", cartridgeID);
			tw.WriteLine("Country Code    : {0} (0x{1:X})", countryCode, (int)countryCode);
			tw.WriteLine("------------------------------------------------------------");
			tw.WriteLine("\n");
		}

		// Handles missing zeros.
		private static void HandleZeroTruncation(ref string s, int length)
		{
			for (int i = 0; i < length; i++)
			{
				s = s.Insert(0, "0");
			}
		}
	}
}
