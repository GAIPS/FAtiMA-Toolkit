using System;
using System.Globalization;
using System.Reflection;

public class TimestampSurrogate : ISerializationSurrogate
{
	private static readonly string[] TIME_FORMATS = new string[]{
		@"d/M/yyyy@H:m:s.fff",
		@"d/M/yyyy@H:m:s",
		@"d/M/yyyy@H:m",
		@"d/M/yyyy",
	};
	
	#region ISerializationSurrogate implementation
	public void GetObjectData (object obj, ObjectGraphNode holder, SerializedGraph graph)
	{
		DateTime time = (DateTime)obj;
		time = time.ToUniversalTime();
		string format = @"d/M/yyyy";
		if(time.Hour>0 || time.Minute>0 || time.Second>0 || time.Millisecond>0)
		{
			format +=@"@H:m";
			if(time.Second>0 || time.Millisecond>0)
			{
				format+=@":s";
				if(time.Millisecond>0)
					format+=@".fff";
			}
		}
		
		string timestamp = time.ToString(format);
		holder["date"]=new StringGraphNode(timestamp);
	}
	
	public void SetObjectData (ref object obj, ObjectGraphNode node, SerializedGraph graph)
	{
		StringGraphNode timestamp = node["date"] as StringGraphNode;
		
		DateTime t = DateTime.ParseExact(timestamp.Value,TIME_FORMATS,null,DateTimeStyles.None);
		obj = DateTime.SpecifyKind(t,DateTimeKind.Utc);
		
		//execute carbon copy
		/*
		FieldInfo[] fields = typeof(DateTime).GetFields(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
		foreach(FieldInfo f in fields)
		{
			object o = f.GetValue(t);
			f.SetValue(time,o);
		}
		
		throw new Exception(time.ToString());
		*/
	}
	
	#endregion
}

