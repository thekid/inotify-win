using System.IO;

namespace De.Thekid.INotify
{
    public abstract class Token
    {
        public char Kind { get; set; }

        public string Value { get; set; }
    }

    public class Format : Token
    {
        public Format(char specifier)
        {
            this.Kind = specifier;
            this.Value = null;
        }
    }

    public class Literal : Token
    {
        public Literal(string value)
        {
            this.Kind = (char)0;
            this.Value = value;
        }
    }
}