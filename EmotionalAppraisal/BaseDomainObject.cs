using System;

namespace EmotionalAppraisal
{
	[Serializable]
    public class BaseDomainObject
	{
		[NonSerialized]
		private Guid m_id;
        public Guid Id {
	        get { return m_id; }
        }

        protected BaseDomainObject()
        {
            m_id = Guid.NewGuid();
        }

		protected BaseDomainObject(Guid id)
		{
			m_id = id;
		}
	}
}
