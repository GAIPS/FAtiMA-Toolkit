using System.Globalization;
using Utilities;
using WellFormedNames;

namespace WellFormedNames
{
    public class ComplexValue
    {
        public static ComplexValue Deserialize(string value)
        {
            var aux = value.RemoveWhiteSpace();
            if (aux.Contains(","))
            {
                var split = aux.Split(',');
                return new ComplexValue((Name)split[0], float.Parse(split[1], CultureInfo.InvariantCulture));
            }
            else
            {
                return new ComplexValue((Name)aux);
            }
        }

        public Name Value { get; private set; }
        public float Certainty { get; private set; }

        public ComplexValue(Name value)
        {
            this.Value = value;
            this.Certainty = 1.0f;
        }

        public ComplexValue(Name value, float certainty)
        {
            this.Value = value;
            this.Certainty = certainty;
        }

        public string Serialize()
        {
            return this.Value + ", " + this.Certainty.ToString(CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            ComplexValue v = obj as ComplexValue;
            if (v == null)
                return false;

            var result = this.Value == v.Value && this.Certainty == v.Certainty;
            return result;
        }

        public override string ToString()
        {
            return this.Serialize();
        }

    }
}
