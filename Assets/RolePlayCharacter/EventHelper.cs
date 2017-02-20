using System;
using WellFormedNames;
using AutobiographicMemory;

namespace RolePlayCharacter
{
    public class EventHelper
    {
        public static Name ActionStart(string subject, string actionName, string target)
        {
            if (!((Name)subject).IsPrimitive)
                throw new ArgumentException("Subject needs to be a primitive", nameof(target));

            if (!((Name)target).IsPrimitive)
                throw new ArgumentException("Target needs to be a primitive", nameof(target));

            return Name.BuildName((Name)AMConsts.EVENT, (Name)AMConsts.ACTION_START,
                (Name)subject, (Name)actionName, (Name)target);
        }

        public static Name ActionEnd(string subject, string actionName, string target)
        {
            if (!((Name)subject).IsPrimitive)
                throw new ArgumentException("Subject needs to be a primitive", nameof(target));

            if (!((Name)target).IsPrimitive)
                throw new ArgumentException("Target needs to be a primitive", nameof(target));

            return Name.BuildName((Name)AMConsts.EVENT, (Name)AMConsts.ACTION_END,
                (Name)subject, (Name)actionName, (Name)target);
        }

        public static Name PropertyChange(string propertyName, string value, string subject)
        {
            if (!((Name)propertyName).IsComposed)
                throw new ArgumentException("Property needs to be a composed name", nameof(value));

            if (!((Name)value).IsPrimitive)
                throw new ArgumentException("Value needs to be a primitive", nameof(value));

            if (!((Name)subject).IsPrimitive)
                throw new ArgumentException("Subject needs to be a primitive", nameof(value));

            return Name.BuildName(
                (Name)AMConsts.EVENT,
                (Name)AMConsts.PROPERTY_CHANGE,
                (Name)subject,
                (Name)propertyName,
                (Name)value);
        }
    }
}
