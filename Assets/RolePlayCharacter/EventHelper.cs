using System;
using WellFormedNames;
using AutobiographicMemory;

namespace RolePlayCharacter
{
    public class EventHelper
    {
        public static Name ActionStart(Name subject, Name actionName, Name target)
        {
            if (!subject.IsPrimitive)
                throw new ArgumentException("Subject needs to be a primitive", nameof(target));

            if (!target.IsPrimitive)
                throw new ArgumentException("Target needs to be a primitive", nameof(target));

            return Name.BuildName((Name)AMConsts.EVENT, (Name)AMConsts.ACTION_START, subject, actionName, target);
        }

   
        public static Name ActionEnd(Name subject, Name actionName, Name target)
        {
            if (!subject.IsPrimitive)
                throw new ArgumentException("Subject needs to be a primitive", nameof(target));

            if (!target.IsPrimitive)
                throw new ArgumentException("Target needs to be a primitive", nameof(target));

            return Name.BuildName((Name)AMConsts.EVENT, (Name)AMConsts.ACTION_END, subject, actionName, target);
        }

        public static Name PropertyChange(Name propertyName, Name value, Name subject)
        {
            if (!propertyName.IsComposed)
                throw new ArgumentException("Property needs to be a composed name", nameof(value));

            if (!subject.IsPrimitive)
                throw new ArgumentException("Subject needs to be a primitive", nameof(value));

            return Name.BuildName(
                (Name)AMConsts.EVENT,
                (Name)AMConsts.PROPERTY_CHANGE,
                subject,
                propertyName,
                value);
        }


        public static Name PropertyChange(string propertyName, string value, string subject)
        {
            return PropertyChange((Name)propertyName, (Name)value, (Name)subject);
        }

        public static Name ActionStart(string subject, string actionName, string target)
        {
            return ActionStart((Name)subject, (Name)actionName, (Name)target);
        }

        public static Name ActionEnd(string subject, string actionName, string target)
        {
            return ActionEnd((Name)subject, (Name)actionName, (Name)target);
        }
    }
}
