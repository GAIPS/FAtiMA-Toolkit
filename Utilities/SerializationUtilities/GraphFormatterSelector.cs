using System;

namespace SerializationUtilities
{
	public sealed class GraphFormatterSelector
	{
		private GraphFormatterSelector m_next = null;
		private TypeSelector<IGraphFormatter> m_formatters = new TypeSelector<IGraphFormatter>();

		public void ChainSelector(GraphFormatterSelector selector)
		{
			if (m_next != null)
				m_next.ChainSelector(selector);
			else
				m_next = selector;
		}

		public GraphFormatterSelector GetNextSelector()
		{
			return m_next;
		}

		public IGraphFormatter GetFormatter(Type type)
		{
			IGraphFormatter result = m_formatters.GetValue(type);
			if (result != null)
				return result;

			return m_next != null ? m_next.GetFormatter(type) : null;
		}

		public void AddFormatter(Type type, bool useInChildren, IGraphFormatter formatter)
		{
			m_formatters.AddValue(type,useInChildren,formatter);
		}
	}
}
