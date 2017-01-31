using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Json
{
	public static class JsonParser
	{
		private class ReadContext
		{
			public uint LineNumber = 0;
			public uint CharacterCount = 0;

			public override string ToString()
			{
				return $"Invalid JSON format at line {LineNumber}, character {CharacterCount}: ";
			}
		}

		public static JsonToken Parse(string jsonString)
		{
			using (var reader = new StringReader(jsonString))
			{
				return ReadValue(reader,new ReadContext());
			}
		}

		public static JsonToken Parse(Stream stream)
		{
			using (StreamReader reader = new StreamReader(stream))
			{
				return ReadValue(reader,new ReadContext());
			}
		}

		private static JsonToken ReadValue(TextReader reader, ReadContext context)
		{
			JsonToken node = null;
			readEmptyCharacters(reader,context);
			char start = (char)reader.Peek();
			switch (start)
			{
				case '{':
					node = ReadObject(reader,context);
					break;
				case '[':
					node = ReadArray(reader,context);
					break;
				case '\"':
					{
						string str = ReadString(reader,context);
						node = new JsonString(str);
					}
					break;
				default:
					node = ReadPrimitive(reader,context);
					break;
			}
			return node;
		}

		private static JsonObject ReadObject(TextReader reader, ReadContext context)
		{
			JsonObject obj = new JsonObject();
			reader.Read();	// read '{'
			context.CharacterCount++;
			readEmptyCharacters(reader,context);
			char c = (char)reader.Peek();
			if (c == '}')
			{
				reader.Read();
				context.CharacterCount++;

				return obj;
			}

			while (reader.Peek()>=0)
			{
				if (c != '"')
					throw new IOException(context+" string expected");

				string field = ReadString(reader,context);
				readEmptyCharacters(reader,context);
				c = (char)reader.Read();
				if (c != ':')
					throw new IOException(context+$" expected ':', found '{c}'");
				context.CharacterCount++;

				JsonToken value = ReadValue(reader,context);
				readEmptyCharacters(reader,context);

				obj[field] = value;

				c = (char)reader.Read();
				if (c == '}')
				{
					context.CharacterCount++;
					return obj;
				}

				if (c != ',')
					throw new IOException(context+ $" expected ',', found '{c}'");

				context.CharacterCount++;
				readEmptyCharacters(reader,context);
				c = (char)reader.Peek();
			}

			throw new IOException("End of Stream Reached without finishing parsing object");
		}

		private static JsonToken ReadArray(TextReader reader, ReadContext context)
		{
			JsonArray array = new JsonArray();
			reader.Read();	// read '['
			context.CharacterCount++;
			readEmptyCharacters(reader,context);

			while ((char)reader.Peek() != ']')
			{
				JsonToken val = ReadValue(reader,context);
				array.Add(val);

				readEmptyCharacters(reader,context);
				char c = (char)reader.Peek();
				if (c == ',')
				{
					reader.Read();
					context.CharacterCount++;
				}
				else if (c != ']')
					throw new IOException(context+ $" Invalid array separator. Expected ',' or ']', but found '{c}'");
			}
			reader.Read(); //Read ']'
			context.CharacterCount++;
			return array;
		}

		private const string NUMBER_PATTERN = @"^(\+|-)?(0|[1-9]\d*)(\.\d+)?(e(-|\+)?\d+)?$";
		private static readonly Regex _numberRegex = new Regex(NUMBER_PATTERN);

		private static JsonToken ReadPrimitive(TextReader reader, ReadContext context)
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			try
			{
				while (reader.Peek()>=0)
				{
					char c = (char)reader.Peek();
					if ((c == ',') || (c == ']') || (c == '}') || Char.IsWhiteSpace(c))
						break;

					builder.Append((char)reader.Read());
					context.CharacterCount++;
				}

				string primitive = builder.ToString().ToLower();

				if (primitive == "true")
					return new JsonBool(true);

				if (primitive == "false")
					return new JsonBool(false);

				if (primitive == "null")
					return null;

				var m = _numberRegex.Match(primitive);
				if (m.Success)
				{
					if (m.Groups[3].Success || m.Groups[4].Success)
					{
						decimal vd;
						if(decimal.TryParse(primitive,NumberStyles.Any,CultureInfo.InvariantCulture,out vd))
							return new JsonNumber(vd);
						return new JsonNumber(double.Parse(primitive, CultureInfo.InvariantCulture));
					}

					if(m.Groups[1].Success && m.Groups[1].Value=="-")
						return new JsonNumber(long.Parse(primitive, CultureInfo.InvariantCulture));

					ulong vl;
					if(ulong.TryParse(primitive,NumberStyles.Any, CultureInfo.InvariantCulture, out vl))
						return new JsonNumber(vl);
					return new JsonNumber(decimal.Parse(primitive, CultureInfo.InvariantCulture));
				}

				throw new IOException(context+ $" Invalid primitive \"{primitive}\"");
			}
			finally
			{
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
			}
		}

		private static readonly char[] hexBuffer = new char[4];
		private static string ReadString(TextReader reader, ReadContext context)
		{
			reader.Read();	// read '"'
			context.CharacterCount++;
			StringBuilder builder = new StringBuilder();
			bool isControl = false;
			while (reader.Peek()>=0)
			{
				char c = (char)reader.Read();
				if (c == '\n' || c == '\r')
					throw new IOException(context+" new line in the middle of a string");

				context.CharacterCount++;

				if (isControl)
				{
					switch (c)
					{
						case '"':
							builder.Append('"');
							break;
						case '\\':
							builder.Append('\\');
							break;
						case '/':
							builder.Append('/');
							break;
						case 'b':
							builder.Append('\b');
							break;
						case 'f':
							builder.Append('\f');
							break;
						case 'n':
							builder.Append('\n');
							break;
						case 'r':
							builder.Append('\r');
							break;
						case 't':
							builder.Append('\t');
							break;
						case 'u':
							{
								if (reader.ReadBlock(hexBuffer, 0, 4) < 4)
									throw new IOException(context+" invalid hexadecimal char format");
								context.CharacterCount += 4;

								string hexString = new string(hexBuffer);
								long result = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

								char hexChar = (char)result;
								builder.Append(hexChar);
							}

							break;
						default:
							throw new IOException(context+ $" invalid control character '\\{c}'");
					}
					isControl = false;
				}
				else
				{
					if (c == '"')
						break;

					if (c == '\\')
					{
						isControl = true;
					}
					else
					{
						builder.Append(c);
					}
				}
			}
			return builder.ToString();
		}

		private static void readEmptyCharacters(TextReader reader,ReadContext context)
		{
			bool isLineBreak = false;
			while (reader.Peek() >= 0 && Char.IsWhiteSpace((char)reader.Peek()))
			{
				var c = (char)reader.Read();
				if (c == '\r')
				{
					isLineBreak = true;
				}
				else if (c == '\n')
				{
					context.LineNumber++;
					context.CharacterCount = 0;
					isLineBreak = false;
				}
				else
				{
					if (isLineBreak)
					{
						context.LineNumber++;
						context.CharacterCount = 0;
					}
					context.CharacterCount++;
					isLineBreak = false;
				}
			}

			if (isLineBreak)
			{
				context.LineNumber++;
				context.CharacterCount = 0;
			}
		}
	}
}
