using System;
using System.Text.RegularExpressions;
using Utilities;

namespace WellFormedNames
{
    public enum LiteralType
    {
        Root,
        Param,
    }

    public class Literal : IEquatable<Literal>
    {
        public string description;
        public readonly LiteralType type;
        public int depth;

        
        public Literal(string desc, LiteralType t = LiteralType.Param, int depth = 0)
        {
            this.description = desc;
            this.type = t;
            this.depth = depth;
        }
        
        public bool Equals(Literal other)
        {
            if (this.description.EqualsIgnoreCase(other.description) &&
                this.depth == other.depth &&
                this.type == other.type)
                return true;
            else return false;
        }

        public override string ToString()
        {
            return description;
        }
    }
}
