using System;

namespace ValueDomains
{
	public sealed class Variable
	{
		public IDomain ValueDomain { get; }

		private Value _value;
		public Value Value {
			get { return _value; }
			set
			{
				if(_value.Equals(value))
					return;

				if(!value.ValueDomain.Equals(ValueDomain))
					throw new Exception($"Invalid value domain. Expected {ValueDomain}");

				_value = value;
			}
		}

		public Variable(IDomain domain) : this(domain,domain.DefaultValue)
		{

		}

		public Variable(IDomain domain, Value value)
		{
			if(!domain.Equals(value.ValueDomain))
				throw new Exception("Value and domain mismatch");

			ValueDomain = domain;
			_value = value;
		}
	}
}