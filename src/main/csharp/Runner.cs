using System;
using System.IO;
using System.Collections.Generic;

namespace Net.XpForge.INotify
{
	// List of possible changes
	public enum Change {
		CREATE, MODIFY, DELETE, MOVE_FROM, MOVE_TO
	}

	/// Main class
	public class Runner
	{
		// Mappings
		protected static Dictionary<WatcherChangeTypes, Change> Changes = new Dictionary<WatcherChangeTypes, Change>();

		static Runner() {
			Changes[WatcherChangeTypes.Created]= Change.CREATE;
			Changes[WatcherChangeTypes.Changed]= Change.MODIFY;
			Changes[WatcherChangeTypes.Deleted]= Change.DELETE;
		}

		/// Callback for errors in watcher
		protected void OnWatcherError(object source, ErrorEventArgs e)
		{
			Console.Error.WriteLine("*** {0}", e.GetException());
		}

		/// Output method
		protected void Output(TextWriter writer, string[] tokens, FileSystemWatcher source, Change type, string name)
		{
			foreach (var token in tokens)
			{
				switch (token[0])
				{
					case 'e': writer.Write(type); break;
					case 'f': writer.Write(Path.Combine(source.Path, name)); break;
					case '"': writer.Write(token.Substring(1)); break;
					case 'w': writer.Write(source.Path); break;
					case 'T': writer.Write(DateTime.Now); break;
				}
			}
			writer.WriteLine();
		}

		/// Entry point
		public int Run(Arguments args)
		{
			using (var w = new FileSystemWatcher {
				Path = args.Path,
				IncludeSubdirectories = args.Recursive, 
				Filter = "*.*"
			}) {
				w.Error += new ErrorEventHandler(OnWatcherError);

				// Parse "events" argument
				WatcherChangeTypes changes = 0;
				if (args.Events.Contains("create")) 
				{
					changes |= WatcherChangeTypes.Created;
				}
				if (args.Events.Contains("modify"))
				{
					changes |= WatcherChangeTypes.Changed;
				}
				if (args.Events.Contains("delete"))
				{
					changes |= WatcherChangeTypes.Deleted;
				}
				if (args.Events.Contains("move"))
				{
					changes |= WatcherChangeTypes.Renamed;
				}

				// Main loop
				if (!args.Quiet) 
				{
					Console.Error.WriteLine(
						"===> {0} {1}{2} for {3}",
						args.Monitor ? "Monitoring" : "Watching", 
						args.Path, 
						args.Recursive ? " -r" : "", 
						String.Join(", ", args.Events.ToArray())
					);
				}
				w.EnableRaisingEvents = true;
				do 
				{
					var e = w.WaitForChanged(changes);
					if (WatcherChangeTypes.Renamed.Equals(e.ChangeType))
					{
						Output(Console.Out, args.Format, w, Change.MOVE_FROM, e.OldName);
						Output(Console.Out, args.Format, w, Change.MOVE_TO, e.Name);	
					}
					else
					{
						Output(Console.Out, args.Format, w, Changes[e.ChangeType], e.Name);
					}
				}
				while (args.Monitor);
			}
			return 0;
		}

		/// Entry point method
		public static int Main(string[] args)
		{
			var p = new ArgumentParser();

			// Show usage if no args or standard "help" args are given
			if (0 == args.Length || args[0].Equals("-?") || args[0].Equals("--help"))
			{
				p.PrintUsage("inotifywait", Console.Error);
				return 1;
			}

			// Run!
			return new Runner().Run(p.Parse(args));
		}
	}
}