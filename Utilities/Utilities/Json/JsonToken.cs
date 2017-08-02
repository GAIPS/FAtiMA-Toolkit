using System.IO;

namespace Utilities.Json
{
	public abstract class JsonToken
	{
		public void Write(TextWriter writer, bool allowIndent)
		{
			Write(writer, 0, allowIndent);
		}

		public abstract void Write(TextWriter writer, int ident, bool allowIndent);

		protected void writeIdentation(TextWriter writer, int ident)
		{
			while (ident > 0)
			{
				writer.Write("\t");
				ident--;
			}
		}

		public string ToString(bool allowIndentation = false)
		{
			string str;
			using (StringWriter stream = new StringWriter())
			{
				Write(stream, allowIndentation);
				str = stream.ToString();
			}
			return str;
		}
	}

		
}
