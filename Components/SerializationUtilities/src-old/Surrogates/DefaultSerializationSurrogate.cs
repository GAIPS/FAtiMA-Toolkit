using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class DefaultSerializationSurrogate : ISerializationSurrogate
{
	private static readonly Type ICUSTOM_SERIALIZATION_TYPE = typeof(ICustomSerialization);
	private const BindingFlags FIELD_FLAGS = BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic;
	
	private const string BACKING_FIELD_PATTERN = "^<(.+?)>k__BackingField$";
	private static readonly Regex BACKING_FIELD_REGEX = new Regex(BACKING_FIELD_PATTERN);
	
	private static bool SkipField(FieldInfo field)
	{
		return field.GetCustomAttributes(typeof(NonSerializedAttribute),false).Length>0;
	}
	
	private static string processFieldName(string originalName)
	{
		Match m = BACKING_FIELD_REGEX.Match(originalName);
		return m.Success?m.Groups[1].Value:originalName;
	}

	#region ISerializationSurrogate implementation
	
	public void GetObjectData(object obj, ObjectGraphNode holder, SerializedGraph graph)
	{
		Type objType = obj.GetType();
		
		if(ICUSTOM_SERIALIZATION_TYPE.IsAssignableFrom(objType))
		{
			SerializationData_impl dataHolder = new SerializationData_impl();
			((ICustomSerialization)obj).SerializeData(dataHolder);
			IEnumerator<FieldEntry> it = dataHolder.GetFields();
			while(it.MoveNext())
			{
				if(it.Current.FieldValue==null)
					continue;
				
				SerializationGraphNode fieldNode = graph.BuildNode(it.Current.FieldValue,null,holder);
				holder[it.Current.FieldName] = fieldNode;
			}
		}
		else
		{
			FieldInfo[] fields = objType.GetFields(FIELD_FLAGS);
			int num = fields.Length;
			for(int i=0;i<num;i++)
			{
				FieldInfo field = fields[i];
				if(SkipField(field))
					continue;
				
				object fieldValue = field.GetValue(obj);
				if(fieldValue == null)
					continue;
				
				SerializationGraphNode fieldNode = graph.BuildNode(fieldValue,field.FieldType,holder);
				
				string fieldName = processFieldName(field.Name);
				holder[fieldName] = fieldNode;
			}
		}
	}

	public void SetObjectData(ref object obj, ObjectGraphNode node, SerializedGraph graph)
	{
		Type objType = obj.GetType();
		
		if(ICUSTOM_SERIALIZATION_TYPE.IsAssignableFrom(objType))
		{
			SerializationData_impl dataHolder = new SerializationData_impl();
			ObjectGraphNodeEnumerator it = node.GetEnumerator();
			while(it.MoveNext())
			{
				if(it.FieldNode == null)
					continue;
				
				object fieldValue = it.FieldNode.RebuildObject(graph,null);
				dataHolder.SetField(it.FieldName,fieldValue);
			}
			
			((ICustomSerialization)obj).DeserializeData(dataHolder);
		}
		else
		{
			FieldInfo[] fields = objType.GetFields(FIELD_FLAGS);
			int num = fields.Length;
			for(int i=0;i<num;i++)
			{
				FieldInfo field = fields[i];
				if(SkipField(field))
					continue;
				
				string fieldName = processFieldName(field.Name);
				SerializationGraphNode fieldNode = node[fieldName];
				if(fieldNode==null)
					continue;
				
				object fieldValue = fieldNode.RebuildObject(graph,field.FieldType);
				field.SetValue(obj,fieldValue);
			}
		}
	}

	#endregion
}

