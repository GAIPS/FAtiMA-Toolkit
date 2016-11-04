using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
	//TODO find a better implementation
	public class OneToOneDictionary<TKey,TValue> : IDictionary<TKey,TValue>
	{
		private Dictionary<TKey, TValue> m_link1;
		private Dictionary<TValue, TKey> m_link2;

		public OneToOneDictionary(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if(keyComparer!=null)
				m_link1 = new Dictionary<TKey, TValue>(keyComparer);
			else
				m_link1 = new Dictionary<TKey, TValue>();

			if(valueComparer!=null)
				m_link2 = new Dictionary<TValue, TKey>(valueComparer);
			else
				m_link2 = new Dictionary<TValue, TKey>();
		}

		public void Add(TKey key, TValue value)
		{
			if (m_link1.ContainsKey(key))
				throw new Exception("key is already associated to a value"); //TODO better exception

			if(m_link2.ContainsKey(value))
				throw new Exception("value is already associated to a key"); //TODO better exception

			m_link1.Add(key, value);
			m_link2.Add(value, key);
		}

		public bool ContainsKey(TKey key)
		{
			return m_link1.ContainsKey(key);
		}

		public bool ContainsValue(TValue value)
		{
			return m_link2.ContainsKey(value);
		}

		public ICollection<TKey> Keys
		{
			get { return m_link1.Keys; }
		}

		public bool Remove(TKey key)
		{
			TValue v;
			if (m_link1.TryGetValue(key, out v))
			{
				m_link1.Remove(key);
				m_link2.Remove(v);
				return true;
			}
			return false;
		}

		public bool RemoveValue(TValue value)
		{
			TKey k;
			if (m_link2.TryGetValue(value, out k))
			{
				m_link2.Remove(value);
				m_link1.Remove(k);
				return true;
			}
			return false;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return m_link1.TryGetValue(key, out value);
		}

		public bool TryGetKey(TValue value, out TKey key)
		{
			return m_link2.TryGetValue(value, out key);
		}

		public ICollection<TValue> Values
		{
			get { return m_link1.Values; }
		}

		public TValue this[TKey key]
		{
			get
			{
				return m_link1[key];
			}
			set
			{
				Add(key, value);
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Add(item.Key, item.Value);
		}

		public void Clear()
		{
			m_link1.Clear();
			m_link2.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return m_link1.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			using (var it = m_link1.GetEnumerator())
			{
				while (it.MoveNext() && arrayIndex < array.Length)
				{
					array[arrayIndex] = it.Current;
					arrayIndex++;
				}
			}
		}

		public int Count
		{
			get { return m_link1.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			TValue v;
			if (!m_link1.TryGetValue(item.Key, out v))
				return false;

			if (!v.Equals(item.Value))
				return false;

			m_link1.Remove(item.Key);
			m_link2.Remove(item.Value);
			return true;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return m_link1.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
