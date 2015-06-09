using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using N64InfoGen.Formats;

namespace N64InfoGen
{
	/// <summary>
	/// Class that links together all of the individual format parsers.
	/// </summary>
	internal static class Parser
	{
		private static readonly string outputDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "ROMCRCs.txt";

		/// <summary>
		/// Parses a whole directory tree of N64 ROM files or a single file.
		/// </summary>
		/// <param name="searchPath">Starting directory to search, or path to a single file.</param>
		public static void ParseFiles(string searchPath)
		{
			FileAttributes fatts = File.GetAttributes(searchPath);
			if ((fatts & FileAttributes.Directory) == FileAttributes.Directory)
			{
				// Get all the files in the dir tree.
				List<string> files = Directory.EnumerateFiles(searchPath, ".", SearchOption.AllDirectories)
				    .Where(s => s.EndsWith(".n64", true, null) ||
				                s.EndsWith(".v64", true, null) ||
				                s.EndsWith(".z64", true, null)).ToList();

				using (TextWriter tw = new StreamWriter(outputDir, true))
				{
					foreach (string file in files)
					{
						if (file.EndsWith(".n64", true, null) ||
						    file.EndsWith(".v64", true, null))
						{
							// For the information generated, .v64 can be treated the same as .n64
							N64.ParseFile(file, tw);
						}
						else if (file.EndsWith(".z64", true, null))
						{
							Z64.ParseFile(file, tw);
						}
					}
				}
			}
			else // Single file parsing
			{
				using (TextWriter tw = new StreamWriter(outputDir, true))
				{
					if (searchPath.EndsWith(".n64", true, null) || 
					    searchPath.EndsWith(".v64", true, null))
					{
						// For the information generated, .v64 can be treated the same as .n64
						N64.ParseFile(searchPath, tw);
					}
					else if (searchPath.EndsWith(".z64", true, null))
					{
						Z64.ParseFile(searchPath, tw);
					}
				}
			}
		}
	}
}
