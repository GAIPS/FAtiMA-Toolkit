using System;

namespace Utilities
{
	public class DirtyValue<T>
	{
		private T value;
		private Func<T> valueCalculator;
		private bool isDirty;

		public T Value
		{
			get
			{
				if (this.isDirty)
				{
					this.value = this.valueCalculator();
					this.isDirty = false;
				}
				return this.value;
			}
		}

		public event Action<DirtyValue<T>> OnDirty;

		public DirtyValue(Func<T> valueCalculationFunction)
		{
			if (valueCalculationFunction == null)
				throw new ArgumentNullException("valueCalculationFunction");

			this.valueCalculator = valueCalculationFunction;
			this.isDirty = true;
		}

		public void SetDirty()
		{
			var prev = this.isDirty;
			this.isDirty = true;

			if (!prev)
			{
				if (OnDirty != null)
					OnDirty(this);
			}
		}

		public static implicit operator T(DirtyValue<T> value)
		{
			return value.Value;
		}
	}
}
