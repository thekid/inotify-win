using System;
using System.IO;
using System.Collections.Generic;

namespace Net.XpForge.INotify
{

	/// See also <a href="http://linux.die.net/man/1/inotifywait">inotifywait(1) - Linux man page</a>
	public class ArgumentParser
	{

		/// Helper method for parser
		protected string Value(string[] args, int i, string name)
		{
			if (i > args.Length)
			{
				throw new ArgumentException("Argument " + name + " requires a value");
			}
			return args[i];
		}

		/// Tokenizes "printf" format string into an array of strings
		protected string[] TokenizeFormat(string arg)
		{
			var result = new List<string>(new string[] { "\"" });
			var offset = 0;
			for (var i = 0; i < arg.Length; i++)
			{
				if ('%'.Equals(arg[i]))
				{
					result.Add(new String(arg[++i], 1));
					result.Add("\"");
					offset += 2;
				}
				else
				{
					result[offset] += arg[i];
				}
			}
			return result.ToArray();
		}

		/// Creates a new argument parser and parses the arguments
		public Arguments Parse(string[] args)
		{
			var result = new Arguments();
			for (var i= 0; i < args.Length; i++)
			{
				if ("--recursive".Equals(args[i]) || "-r".Equals(args[i]))
				{
					result.Recursive = true;
				} 
				else if ("--monitor".Equals(args[i]) || "-m".Equals(args[i]))
				{
					result.Monitor = true;
				}	
				else if ("--quiet".Equals(args[i]) || "-q".Equals(args[i]))
				{
					result.Quiet = true;
				}
				else if ("--event".Equals(args[i]) || "-e".Equals(args[i]))
				{
					result.Events = new List<string>(Value(args, ++i, "event").Split('-'));
				}
				else if ("--format".Equals(args[i]))
				{
					result.Format = TokenizeFormat(Value(args, ++i, "format"));
				}
				else 
				{
					result.Path = System.IO.Path.GetFullPath(args[i]);
				}
			}
			return result;
		}

		/// Usage
		public void PrintUsage(TextWriter writer)
		{
			writer.WriteLine("Usage: inotify-wait [options] path");
			writer.WriteLine();
			writer.WriteLine("Options:");
			writer.WriteLine("-r/--recursive:  Recursively watch all files and subdirectories inside path");
			writer.WriteLine("-m/--monitor:    Keep running until killed (e.g. via Ctrl+C)");
			writer.WriteLine("-q/--quiet:      Do not output information about actions");
			writer.WriteLine("-e/--event list: Which events (create, modify, delete, move) to watch, comma-separated. Default: all");
			writer.WriteLine("--format format: Format string for output.");
			writer.WriteLine();
			writer.WriteLine("Formats:");
			writer.WriteLine("%e             : Event name");
			writer.WriteLine("%f             : File name");
			writer.WriteLine("%w             : Path name");
			writer.WriteLine("%T             : Current date and time");
		}
	}
}