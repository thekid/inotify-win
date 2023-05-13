using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace De.Thekid.INotify
{
    public class Arguments
    {
        private static string[] defaultEvents = new string[] { "create", "modify", "delete", "move" };
        private List<string> events = new List<string>();
        private List<string> paths = new List<string>();
        private string[] format = new string[] { "w", " ", "e", " ", "f" };

        /// -r
        public bool Recursive { get; set; }

        /// -m
        public bool Monitor { get; set; }

        /// -q
        public bool Quiet { get; set; }

        /// -f
        public string[] Format {
            get { return format; }
            set { format = value; }
        }

        /// --include[i]
        public Regex Include { get; set; }

        /// --exclude[i]
        public Regex Exclude { get; set; }

        /// -e
        public void AddEvents(IEnumerable<string> names)
        {
            foreach (var name in names)
            {
                if (defaultEvents.Contains(name))
                {
                    events.Add(name);
                }
                else
                {
                    throw new ArgumentException("Unknown event name '" + name + "'");
                }
            }
        }

        /// -e
        public List<string> Events
        {
            get { return 0 == events.Count ? new List<string>(defaultEvents) : events.Distinct().ToList(); }
        }

        /// All arguments not starting with "-"
        public List<string> Paths {
            get { return paths; }
        }
      }
}
