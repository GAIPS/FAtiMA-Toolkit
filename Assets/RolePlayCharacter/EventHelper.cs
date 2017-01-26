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

        public static Name PropertyChanged(string propertyName, string value, string subject)
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

        public static Name AddAgent(string agent)
        {
            var n = (Name)agent;
            if (!n.IsPrimitive)
                throw new ArgumentException("The agent id needs to be a primitive", nameof(agent));

            return Name.BuildName((Name)AMConsts.EVENT, (Name)"Agent-Added", Name.SELF_SYMBOL, n, Name.SELF_SYMBOL);
        }

        public static Name RemoveAgent(string agent)
        {
            var agentName = (Name)agent;
            if (!agentName.IsPrimitive)
                throw new ArgumentException("The agent id needs to be a primitive", nameof(agent));
            return Name.BuildName((Name)AMConsts.EVENT, (Name)"Agent-Removed", Name.SELF_SYMBOL, agentName, Name.SELF_SYMBOL);
        }
    }
}
