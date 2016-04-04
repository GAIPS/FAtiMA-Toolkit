using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		[Serializable]
		private class PropertyChangeEvent: BaseEvent, IEventRecord, ICustomSerialization
		{
		    public static bool IsPropertyChangeEvent(Name eventName)
		    {
		        return eventName.GetNTerm(1).ToString() == Constants.PROPERTY_CHANGE_EVENT;
		    }

            public Name Action { get { throw new Exception("Invalid Call");} }
		    public string Target { get {throw new Exception("Invalid Call");} }
		    public Name Property { get; private set; }
    	    public string NewValue { get; private set; }
    
			public PropertyChangeEvent(uint id, Name eventName, ulong timestamp) : base(id, eventName, timestamp)
			{
			    Type = Constants.PROPERTY_CHANGE_EVENT;
                Property = eventName.GetNTerm(3);
                var newValue = eventName.GetNTerm(4);
				NewValue = newValue == Name.NIL_SYMBOL ? null : newValue.ToString();
            }


            public void GetObjectData(ISerializationData dataHolder)
			{
				dataHolder.SetValue("Id", Id);
				dataHolder.SetValue("Type", Type);
				dataHolder.SetValue("Subject", Subject);
                dataHolder.SetValue("Property", Property);
			    dataHolder.SetValue("NewValue", NewValue);
                dataHolder.SetValue("Timestamp", Timestamp);
				if (m_linkedEmotions.Count > 0)
				{
					dataHolder.SetValue("LinkedEmotions",m_linkedEmotions.ToArray());
				}
			}

			public void SetObjectData(ISerializationData dataHolder)
			{
				Id = dataHolder.GetValue<uint>("Id");
				Type = dataHolder.GetValue<string>("Type");
                Subject = dataHolder.GetValue<string>("Subject");
                Property = dataHolder.GetValue<Name>("Property");
                NewValue = dataHolder.GetValue<string>("NewValue");
				Timestamp = dataHolder.GetValue<ulong>("Timestamp");

				if(m_linkedEmotions==null)
					m_linkedEmotions=new HashSet<string>();
				else
					m_linkedEmotions.Clear();
				var le = dataHolder.GetValue<string[]>("LinkedEmotions");
				if(le!=null && le.Length>0)
					m_linkedEmotions.UnionWith(le);

				EventName = Name.BuildName(EVT_NAME,(Name)Type,(Name)Subject,Property,(Name)NewValue);
			}
		}
	}
}