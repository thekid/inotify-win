using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace De.Thekid.INotify
{
	// List of possible changes
	public enum Change
	{
		CREATE, MODIFY, DELETE, MOVED_FROM, MOVED_TO
	}

	/// Main class
	public class Runner
	{
		// Mappings
		protected static Dictionary<WatcherChangeTypes, Change> Changes = new Dictionary<WatcherChangeTypes, Change>();

		private List<Thread> _threads = new List<Thread>();
		private bool _eventOccured = false;
		private Semaphore _semaphore = new Semaphore(0, 1);
		private Mutex _mutex = new Mutex();
		private Arguments _args = null;

		static Runner()
		{
			Changes[WatcherChangeTypes.Created]= Change.CREATE;
			Changes[WatcherChangeTypes.Changed]= Change.MODIFY;
			Changes[WatcherChangeTypes.Deleted]= Change.DELETE;
		}

		public Runner(Arguments args)
		{
			_args = args;
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
					case 'f': writer.Write(name); break;
					case 'w': writer.Write(source.Path); break;
					case 'T': writer.Write(DateTime.Now); break;
					default: writer.Write(token); break;
				}
			}
			writer.WriteLine();
		}

		public void Processor(object data) {
			string path = (string)data;
			using (var w = new FileSystemWatcher {
				Path = path,
				IncludeSubdirectories = _args.Recursive,
				Filter = "*.*"
			}) {
				w.Error += new ErrorEventHandler(OnWatcherError);

				// Parse "events" argument
				WatcherChangeTypes changes = 0;
				if (_args.Events.Contains("create"))
				{
					changes |= WatcherChangeTypes.Created;
				}
				if (_args.Events.Contains("modify"))
				{
					changes |= WatcherChangeTypes.Changed;
				}
				if (_args.Events.Contains("delete"))
				{
					changes |= WatcherChangeTypes.Deleted;
				}
				if (_args.Events.Contains("move"))
				{
					changes |= WatcherChangeTypes.Renamed;
				}

				// Main loop
				if (!_args.Quiet)
				{
					Console.Error.WriteLine(
						"===> {0} for {1} in {2}{3} for {4}",
						_args.Monitor ? "Monitoring" : "Watching",
						changes,
						path,
						_args.Recursive ? " -r" : "",
						String.Join(", ", _args.Events.ToArray())
					);
				}
				w.EnableRaisingEvents = true;
				while (true)
				{
					var e = w.WaitForChanged(changes);
					_mutex.WaitOne();
					if (_eventOccured)
					{
						_mutex.ReleaseMutex();
						break;
					}
					if (null != _args.Exclude && _args.Exclude.IsMatch(e.Name))
					{
						_mutex.ReleaseMutex();
						continue;
					}
					if (WatcherChangeTypes.Renamed.Equals(e.ChangeType))
					{
						Output(Console.Out, _args.Format, w, Change.MOVED_FROM, e.OldName);
						Output(Console.Out, _args.Format, w, Change.MOVED_TO, e.Name);
					}
					else
					{
						Output(Console.Out, _args.Format, w, Changes[e.ChangeType], e.Name);
					}
					if (!_args.Monitor)
					{
						_eventOccured = true;
						_semaphore.Release();
					}
					_mutex.ReleaseMutex();
				}
			}
		}

		/// Entry point
		public int Run()
		{
			foreach (var path in _args.Paths)
			{
				Thread t = new Thread(new ParameterizedThreadStart(Processor));
				t.Start(path);
				_threads.Add(t);
			}
			_semaphore.WaitOne();
			foreach (var thread in _threads)
			{
				if (thread.IsAlive)
					thread.Abort();
				thread.Join();
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
			return new Runner(p.Parse(args)).Run();
		}
	}
}
