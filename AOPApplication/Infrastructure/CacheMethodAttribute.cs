using System;

namespace AOPApplication.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheMethodAttribute : Attribute
    {
        public CacheMethodAttribute()
        {
            // مقدار پيش فرض
            SecondsToCache = 10;
        }

        public double SecondsToCache { get; set; }
    }
}
