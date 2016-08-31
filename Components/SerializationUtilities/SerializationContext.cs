using System.Collections.Generic;

namespace SerializationUtilities
{
	public class SerializationContext : ISerializationContext
	{
		private Stack<object> m_contextStack = new Stack<object>();
		public object Context { get; set; }

		public void PushContext()
		{
			m_contextStack.Push(Context);
		}

		public void PopContext()
		{
			Context = m_contextStack.Pop();
		}
	}
}