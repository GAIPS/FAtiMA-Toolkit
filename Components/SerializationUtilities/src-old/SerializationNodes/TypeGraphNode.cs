using System;

public interface ITypeGraphNode
{
	Type Class {get;set;}
}

public sealed partial class SerializedGraph
{
	private class TypeGraphNode : ITypeGraphNode
	{
		private Type m_class;
		public Type Class
		{
			get{
				return m_class;
			}
			set
			{
				if(m_class == value)
					return;
				
				m_class = value;
			}
		}
	}
}