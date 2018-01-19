using SerializationUtilities;

namespace GAIPS.Rage
{
    public class CloneHelper
    {
        private static readonly JSONSerializer serializer = new JSONSerializer();

        public static T Clone<T> (T obj)
        {
            var json = serializer.SerializeToJson(obj);
            return serializer.DeserializeFromJson<T>(json);
        }
    }
}
