using System;
using System.Collections.Generic;

public interface ISerializationData
{
	void SetField(string name, object value);
	object GetField(string name);
	T GetField<T>(string name);
}

public partial class DefaultSerializationSurrogate
{
	private sealed class FieldEntry
	{
		public readonly string FieldName;
		public readonly object FieldValue;
		
		public FieldEntry(string name, object value)
		{
			this.FieldName = name;
			this.FieldValue = value;
		}
	}
	private sealed class SerializationData_impl : ISerializationData
	{
		private Dictionary<string,object> m_fields = new Dictionary<string, object>();
		
		public void SetField (string name, object value)
		{
			if(m_fields.ContainsKey(name))
				throw new ArgumentException("Duplicated field name found",name);
			
			m_fields[name] = value;
		}
		
		public object GetField (string name)
		{
			object value;
			if(!m_fields.TryGetValue(name,out value))
				return null; //Value not found. Return null
				
			return value;
		}
		
		public T GetField<T> (string name)
		{
			object value;
			if(!m_fields.TryGetValue(name,out value))
				return default(T); //Value not found. Return default value of type
			
			Type t = value.GetType();
			if(typeof(T).IsAssignableFrom(t))
				return (T)value;
			
			//Can't return the object right away. A conversion may be required.
			return (T)Convert.ChangeType(value,typeof(T));
		}
		
		public IEnumerator<FieldEntry> GetFields()
		{
			foreach(KeyValuePair<string,object> pair in m_fields)
				yield return new FieldEntry(pair.Key,pair.Value);
		}
	}
}