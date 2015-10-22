using System;
using System.Collections.Generic;

public sealed class SurrogateChain
{
	private class ChainNode
	{
		private List<ChainNode> m_childs;
		private Type m_serializationType;
		private ISerializationSurrogate m_surrogate;
		
		public ChainNode(Type serializationType, ISerializationSurrogate surrogate)
		{
			m_childs = new List<ChainNode>();
			m_serializationType = serializationType;
			m_surrogate = surrogate;
		}
		
		public ChainNode AddNewSurrogate(Type type, ISerializationSurrogate surrogate)
		{
			if(m_serializationType == type)
				throw new Exception("Duplicated surrogate type chain: "+type);
		
			if(m_serializationType.IsAssignableFrom(type))
			{
				for(int i=0;i<m_childs.Count;i++)
				{
					ChainNode newNode = m_childs[i].AddNewSurrogate(type,surrogate);
					if(newNode==null)
						continue;
					
					m_childs[i] = newNode;
					return this;
				}
				
				m_childs.Add(new ChainNode(type,surrogate));
				return this;
			}
			
			if(type.IsAssignableFrom(m_serializationType))
			{
				return null; //extends from him
			}
			
			//another branch (should not be a applied to root)
			return null;
		}
		
		public ISerializationSurrogate GetSurrogate(Type type)
		{
			foreach(ChainNode child in m_childs)
			{
				ISerializationSurrogate surrogate = child.GetSurrogate(type);
				if(surrogate!=null)
					return surrogate;
			}
			
			if(m_serializationType.IsAssignableFrom(type))
				return m_surrogate;
			
			return null;
		}
	}
	
	private ChainNode m_root = null;
	
	public SurrogateChain()
	{
		m_root = new ChainNode(typeof(object),new DefaultSerializationSurrogate());
	}
	
	public void AddSurrogate(Type type, ISerializationSurrogate surrogate)
	{
		m_root = m_root.AddNewSurrogate(type,surrogate);
	}
	
	public ISerializationSurrogate GetSerializationSurrogate(Type type)
	{
		if(m_root==null)
			return null;
		
		return m_root.GetSurrogate(type);
	}
}
