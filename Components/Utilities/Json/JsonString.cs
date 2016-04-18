using System.IO;
using System.Text;

namespace Utilities.Json
{
	public class JsonString : JsonToken
	{
		public string String;

		public JsonString(string str)
		{
			this.String = str;
		}

		public JsonString(string format, params object[] args) : this(string.Format(format,args)){}

		public override void Write(TextWriter writer, int ident, bool allowIndent)
		{
			writer.Write('\"');
			StringBuilder builder = new StringBuilder(String);
			builder.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\b", "\\b").Replace("\f", "\\f").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
			writer.Write(builder.ToString());
			writer.Write('\"');
		}
	}

		
}
