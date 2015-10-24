using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
	public class JsonObject : JsonToken
	{
		private Dictionary<string, JsonToken> m_fields;

		public JsonObject()
		{
			m_fields = new Dictionary<string, JsonToken>();
		}

		public JsonToken this[string key]
		{
			get
			{
				JsonToken o;
				if (m_fields.TryGetValue(key, out o))
					return o;

				return null;
			}
			set
			{
				m_fields[key] = value;
			}
		}

		public IEnumerator<KeyValuePair<string, JsonToken>> GetEnumerator()
		{
			return m_fields.GetEnumerator();
		}

		public override void Write(TextWriter writer, int ident, bool allowIndent)
		{
			if (allowIndent)
				writeIdentation(writer, ident);
			writer.Write('{');
			if (allowIndent)
				writer.WriteLine();

			int max = m_fields.Count;
			int cnt = 0;
			ident++;
			foreach (KeyValuePair<string, JsonToken> pair in m_fields)
			{
				if (allowIndent)
					writeIdentation(writer, ident);
				writer.Write('"');
				writer.Write(pair.Key);
				writer.Write("\":");
				if (allowIndent)
				{
					if (pair.Value is JsonObject)
					{
						writer.WriteLine();
					}
					else
						writer.Write(' ');
				}
				if (pair.Value == null)
					writer.Write("null");
				else
					pair.Value.Write(writer, ident + 1, allowIndent);
				cnt++;
				if (cnt < max)
				{
					writer.Write(',');
					if (allowIndent)
						writer.WriteLine();
				}
			}
			ident--;
			if (allowIndent)
			{
				writer.WriteLine();
				writeIdentation(writer, ident);
			}
			writer.Write("}");
		}
	}
}
