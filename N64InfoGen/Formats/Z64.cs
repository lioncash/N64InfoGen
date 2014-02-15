using System.IO;
using System.Text;
using N64InfoGen.Utils;

namespace N64InfoGen.Formats
{
	/// <summary>
	/// Class for parsing .z64 files.
	/// </summary>
	internal static class Z64
	{
		/// <summary>
		/// Parses a single .z64 file.
		/// </summary>
		/// <param name="file">The path to the .z64 file.</param>
		/// <param name="tw"><see cref="TextWriter"/> that writes the info to the text file.</param>
		public static void ParseFile(string file, TextWriter tw)
		{
			using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
			{
				// File MD5
				string fileMD5 = Utility.GetFileMD5(file);

				// Seek to embedded CRCs
				br.BaseStream.Seek(0x10, SeekOrigin.Begin);
				uint crc1 = br.ReadUInt32().SwapBytes();
				uint crc2 = br.ReadUInt32().SwapBytes();

				// Seek to internal name.
				br.BaseStream.Seek(0x20, SeekOrigin.Begin);
				byte[] internalNbytes = Utility.ReadBytes(br.BaseStream, 20);
				string internalName = Encoding.GetEncoding("shift_jis").GetString(internalNbytes);
				string fileName = Path.GetFileName(file);

				// Seek to manufacturer ID.
				br.BaseStream.Seek(0x3B, SeekOrigin.Begin);
				char manufacturerID = br.ReadChar();
				string cartridgeID = new string(br.ReadChars(2));
				char countryCode = br.ReadChar();

				// Write it all to the text file.
				Utility.WriteToFile(tw, fileName, internalName, crc1, crc2, fileMD5, manufacturerID, cartridgeID, countryCode);
			}
		}
	}
}
