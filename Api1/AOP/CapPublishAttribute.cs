using System;
using System.Linq;
using System.Reflection;

namespace Api1.AOP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CapPublishAttribute : Attribute
    {
        public string EventName { get; set; }

        public CapPublishAttribute()
        {
        }

        public static CapPublishAttribute GetAttributeByMethodInfo(MethodInfo mi)
        {
            var attrs = mi.GetCustomAttributes(true).OfType<CapPublishAttribute>().ToArray();
            if (attrs.Any())
            {
                CapPublishAttribute customAttribute = attrs.First();
                return customAttribute;
            }
            return null;
        }
    }
}
