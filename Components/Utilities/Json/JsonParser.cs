using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Json
{
	public static class JsonParser
	{
		public static JsonToken Parse(string jsonString)
		{
			using (var stream = new MemoryStream())
			{
				using (var writer = new StreamWriter(stream))
				{
					writer.Write(jsonString);
					writer.Flush();
				}
				stream.Position = 0;
				return Parse(stream);
			}
		}

		public static JsonToken Parse(Stream stream)
		{
			using (StreamReader reader = new StreamReader(stream))
			{
				return ReadValue(reader);
			}
		}

		private static JsonToken ReadValue(StreamReader reader)
		{
			JsonToken node = null;
			readEmptyCharacters(reader);
			char start = (char)reader.Peek();
			switch (start)
			{
				case '{':
					node = ReadObject(reader);
					break;
				case '[':
					node = ReadArray(reader);
					break;
				case '\"':
					{
						string str = ReadString(reader);
						node = new JsonString(str);
					}
					break;
				default:
					node = ReadPrimitive(reader);
					break;
			}
			return node;
		}

		private static JsonObject ReadObject(StreamReader reader)
		{
			JsonObject obj = new JsonObject();
			reader.Read();	// read '{'
			readEmptyCharacters(reader);
			char c = (char)reader.Peek();
			if (c == '}')
			{
				reader.Read();
				return obj;
			}

			while (!reader.EndOfStream)
			{
				if (c != '"')
					throw new IOException("Invalid JSON format");

				string field = ReadString(reader);
				readEmptyCharacters(reader);
				c = (char)reader.Read();
				if (c != ':')
					throw new IOException("Invalid JSON format");

				JsonToken value = ReadValue(reader);
				readEmptyCharacters(reader);

				obj[field] = value;

				c = (char)reader.Read();
				if (c == '}')
					return obj;

				if (c != ',')
					throw new IOException("Invalid JSON format");

				readEmptyCharacters(reader);
				c = (char)reader.Peek();
			}

			throw new IOException("End of Stream Reached without finishing parsing object");
		}

		private static JsonToken ReadArray(StreamReader reader)
		{
			JsonArray array = new JsonArray();
			reader.Read();	// read '['
			readEmptyCharacters(reader);
			while ((char)reader.Peek() != ']')
			{
				JsonToken val = ReadValue(reader);
				array.Add(val);

				readEmptyCharacters(reader);
				char c = (char)reader.Peek();
				if (c == ',')
					reader.Read();
				else if (c != ']')
					throw new IOException("Invalid JSON format: Invalid array separator. Expected ',' or ']' - Found'" + c + "'");
			}
			reader.Read(); //Read ']'
			return array;
		}

		private const string NUMBER_PATTERN = @"^(\+|-)?(0|[1-9]\d*)(\.\d+)?(e(-|\+)?\d+)?$";
		private static readonly Regex _numberRegex = new Regex(NUMBER_PATTERN);

		private static JsonToken ReadPrimitive(StreamReader reader)
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			try
			{
				while (!reader.EndOfStream)
				{
					char c = (char)reader.Peek();
					if ((c == ',') || (c == ']') || (c == '}') || Char.IsWhiteSpace(c))
						break;

					builder.Append((char)reader.Read());
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
						if(decimal.TryParse(primitive,out vd))
							return new JsonNumber(vd);
						return new JsonNumber(double.Parse(primitive));
					}

					if(m.Groups[1].Success && m.Groups[1].Value=="-")
						return new JsonNumber(long.Parse(primitive));

					ulong vl;
					if(ulong.TryParse(primitive,out vl))
						return new JsonNumber(vl);
					return new JsonNumber(decimal.Parse(primitive));
				}

				throw new IOException("Invalid JSON format: Invalid primitive \"" + primitive + "\"");
			}
			finally
			{
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
			}
		}

		private static readonly char[] hexBuffer = new char[4];
		private static string ReadString(StreamReader reader)
		{
			reader.Read();	// read '"'
			StringBuilder builder = new StringBuilder();
			bool isControl = false;
			while (!reader.EndOfStream)
			{
				char c = (char)reader.Read();
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
									throw new IOException("Invalid JSON format.");

								string hexString = new string(hexBuffer);
								long result = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

								char hexChar = (char)result;
								builder.Append(hexChar);
							}

							break;
						default:
							throw new IOException("Invalid JSON format.");
					}
					isControl = false;
				}
				else
				{
					if (c == '"')
						break;
					else if (c == '\\')
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

		private static void readEmptyCharacters(StreamReader reader)
		{
			while (!reader.EndOfStream && Char.IsWhiteSpace((char)reader.Peek()))
			{
				reader.Read();
			}
		}
	}
}
