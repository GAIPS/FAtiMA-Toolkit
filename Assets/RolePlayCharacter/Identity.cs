using System;
using WellFormedNames;

namespace RolePlayCharacter
{
    public class Identity
    {
        public Name Name { get; set; }
        public Name Category { get; set; }
        public float Salience { get; set; }

        public Identity(Name name, Name category, float salience)
        {
            this.Name = name;
            this.Category = category;
            if(salience <= 0 || salience > 1.0f)
            {
                throw new ArgumentOutOfRangeException("Salience must be a value that is > 0 and <= 1");
            }else
            {
                this.Salience = salience;
            }
        }
    }
}
