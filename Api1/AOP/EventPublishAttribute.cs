using System;
using System.Linq;
using System.Reflection;

namespace Api1.AOP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventPublishAttribute : Attribute
    {
        public string EventName { get; set; }

        public EventPublishAttribute()
        {
        }

        public static EventPublishAttribute GetAttributeByMethodInfo(MethodInfo mi)
        {
            var attrs = mi.GetCustomAttributes(true).OfType<EventPublishAttribute>().ToArray();
            if (attrs.Any())
            {
                EventPublishAttribute customAttribute = attrs.First();
                return customAttribute;
            }
            return null;
        }
    }
}
