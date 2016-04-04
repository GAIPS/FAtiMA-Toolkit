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
		private class ActionEvent : BaseEvent, IEventRecord, ICustomSerialization
		{
            public static bool IsActionEvent(Name eventName)
            {
                return eventName.GetNTerm(1).ToString() == "Action";
            }

            public Name Action { get; private set; }
            public string Target { get; private set; }
		    public Name Property { get {throw new Exception("Invalid Call");} }
		    public string NewValue { get {throw new Exception("Invalid Call");} }

		    public ActionEvent(uint id, Name eventName, ulong timestamp) : base(id, eventName, timestamp)
			{
				Action = eventName.GetNTerm(3);
                var targetName = eventName.GetNTerm(4);
				Target = targetName == Name.NIL_SYMBOL ? null : targetName.ToString();
	        }

		    public void GetObjectData(ISerializationData dataHolder)
			{
				dataHolder.SetValue("Id", Id);
				dataHolder.SetValue("Type", Type);
				dataHolder.SetValue("Subject", Subject);
    			dataHolder.SetValue("Action", Action);
				dataHolder.SetValue("Target", Target);
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
                Action = dataHolder.GetValue<Name>("Action");
			    Target = dataHolder.GetValue<string>("Target");
		        Timestamp = dataHolder.GetValue<ulong>("Timestamp");
                if(m_linkedEmotions==null)
					m_linkedEmotions=new HashSet<string>();
				else
					m_linkedEmotions.Clear();
				var le = dataHolder.GetValue<string[]>("LinkedEmotions");
				if(le!=null && le.Length>0)
					m_linkedEmotions.UnionWith(le);

				EventName = Name.BuildName(EVT_NAME,(Name)Type,(Name)Subject,Action,(Name)Target);
			}
		}
	}
}