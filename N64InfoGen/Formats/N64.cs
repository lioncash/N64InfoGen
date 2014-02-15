using System;
using System.IO;
using System.Text;
using N64InfoGen.Utils;

namespace N64InfoGen.Formats
{
	/// <summary>
	/// Class for parsing .n64 files.
	/// Note that .n64 files are middle-endian (for whatever reason).
	/// </summary>
	internal static class N64
	{
		/// <summary>
		/// Parses a single .n64 file.
		/// </summary>
		/// <param name="file">The path to the file.</param>\
		/// <param name="tw"><see cref="TextWriter"/> that writes the info to the text file.</param>
		public static void ParseFile(string file, TextWriter tw)
		{
			using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
			{
				// File MD5
				string fileMD5 = Utility.GetFileMD5(file);
				string fileName = Path.GetFileName(file);

				// Embedded CRCs
				br.BaseStream.Seek(0x10, SeekOrigin.Begin);
				byte[] crc1bytes = br.ReadBytes(4).SwapBytes(); // 32-bit Little endian
				byte[] crc2bytes = br.ReadBytes(4).SwapBytes(); // 32-bit Little endian
				uint crc1 = BitConverter.ToUInt32(crc1bytes, 0).SwapBytes(); // Kick it around to Big endian
				uint crc2 = BitConverter.ToUInt32(crc2bytes, 0).SwapBytes(); // Kick it around to Big endian

				// Internal name
				br.BaseStream.Seek(0x20, SeekOrigin.Begin);
				byte[] internalNBytes = br.ReadBytes(20).SwapBytes();
				string internalName = Encoding.GetEncoding("shift_jis").GetString(internalNBytes);

				// Manufacturer ID.
				br.BaseStream.Seek(0x38, SeekOrigin.Begin);
				byte[] manufacturerIDBytes = br.ReadBytes(4).SwapBytes();
				char manufacturerID = (char) manufacturerIDBytes[3];

				// Cartridge ID
				string cartID = new string(br.ReadChars(2).SwapChars());

				// Country Code
				byte[] countryCodeBytes = br.ReadBytes(2);
				char countryCode = (char)countryCodeBytes[1];

				Utility.WriteToFile(tw, fileName, internalName, crc1, crc2, fileMD5, manufacturerID, cartID, countryCode);
			}
		}
	}
}
