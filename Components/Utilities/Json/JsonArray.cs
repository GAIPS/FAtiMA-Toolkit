using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
	public class JsonArray : JsonToken, IList<JsonToken>
	{
		private List<JsonToken> m_elements;
		public bool AllElementsInSeparatedLines = false;

		public JsonArray()
		{
			m_elements = new List<JsonToken>();
		}

		public JsonArray(IEnumerable<JsonToken> elements)
		{
			m_elements = new List<JsonToken>(elements);
		}

		public int IndexOf(JsonToken item)
		{
			return m_elements.IndexOf(item);
		}

		public void Insert(int index, JsonToken item)
		{
			m_elements.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			m_elements.RemoveAt(index);
		}

		public JsonToken this[int index]
		{
			get
			{
				return m_elements[index];
			}
			set
			{
				m_elements[index] = value;
			}
		}

		public void Add(JsonToken item)
		{
			m_elements.Add(item);
		}

		public void Clear()
		{
			m_elements.Clear();
		}

		public bool Contains(JsonToken item)
		{
			return m_elements.Contains(item);
		}

		public void CopyTo(JsonToken[] array, int arrayIndex)
		{
			m_elements.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return m_elements.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(JsonToken item)
		{
			return m_elements.Remove(item);
		}

		public IEnumerator<JsonToken> GetEnumerator()
		{
			return m_elements.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override void Write(TextWriter writer, int ident, bool allowIndent)
		{
			writer.Write("[");
			int max = m_elements.Count;
			int cnt = 0;
			ident++;
			foreach (JsonToken v in m_elements)
			{
				if (v == null)
				{
					writer.Write("null");
				}
				else
				{
					if (v is JsonObject)
					{
						writer.WriteLine();
						v.Write(writer, ident - 1, allowIndent);
					}
					else
					{
						if (AllElementsInSeparatedLines)
						{
							writer.WriteLine();
							writeIdentation(writer, ident);
						}
						v.Write(writer, ident, allowIndent);
					}
				}

				cnt++;
				if (cnt < max)
					writer.Write(", ");
			}
			ident--;
			if (AllElementsInSeparatedLines)
			{
				writer.WriteLine();
				writeIdentation(writer, ident);
			}
			writer.Write("]");
		}
	}
}
