using Utilities;
using WellFormedNames;

namespace WellFormedNames
{
    public class UncertainValue
    {
        public static UncertainValue Deserialize(string value)
        {
            var aux = value.RemoveWhiteSpace();
            if (aux.Contains(","))
            {
                var split = aux.Split(',');
                return new UncertainValue((Name)split[0], float.Parse(split[1]));
            }
            else
            {
                return new UncertainValue((Name)aux);
            }
        }

        public Name Value { get; private set; }
        public float Certainty { get; private set; }

        public UncertainValue(Name value)
        {
            this.Value = value;
            this.Certainty = 1.0f;
        }

        public UncertainValue(Name value, float certainty)
        {
            this.Value = value;
            this.Certainty = certainty;
        }

        public string Serialize()
        {
            return this.Value + ", " + this.Certainty;
        }

    }
}
