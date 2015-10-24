using System.IO;

namespace Utilities.Json
{
	public class JsonBool : JsonToken
	{
		public bool Value;

		public JsonBool(bool value)
		{
			this.Value = value;
		}

		public override void Write(TextWriter writer, int ident, bool allowIndent)
		{
			writer.Write(Value.ToString());
		}
	}
}
