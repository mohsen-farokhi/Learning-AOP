using AOPApplication.Infrastructure;
using Ninject.Extensions.Interception;
using System;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AOPApplication.Interceptors
{
    public class CacheInterceptor : IInterceptor
    {
        private static object _lock = new object();

        public void Intercept(IInvocation invocation)
        {
            cacheMethod(invocation);
        }

        private static void cacheMethod(IInvocation invocation)
        {
            var cacheMethodAttribute = getCacheMethodAttribute(invocation);
            if (cacheMethodAttribute == null)
            {
                // متد جاری توسط ویژگی کش شدن مزین نشده است
                // بنابراین آن‌را اجرا کرده و کار را خاتمه می‌دهیم
                invocation.Proceed();
                return;
            }

            // دراینجا مدت زمان کش شدن متد از ویژگی کش دریافت می‌شود
            var cacheDuration = ((CacheMethodAttribute)cacheMethodAttribute).SecondsToCache;

            // برای ذخیره سازی اطلاعات در کش نیاز است یک کلید منحصربفرد را
            //  بر اساس نام متد و پارامترهای ارسالی به آن تهیه کنیم
            var cacheKey = getCacheKey(invocation);

            var cache = HttpRuntime.Cache;
            var cachedResult = cache.Get(cacheKey);

            if (cachedResult != null)
            {
                // اگر نتیجه بر اساس کلید تشکیل شده در کش موجود بود
                // همان را بازگشت می‌دهیم
                invocation.ReturnValue = cachedResult;
            }
            else
            {
                lock (_lock)
                {
                    // در غیر اینصورت ابتدا متد را اجرا کرده
                    invocation.Proceed();

                    if (invocation.ReturnValue == null)
                        return;

                    // سپس نتیجه آن‌را کش می‌کنیم
                    cache.Insert(key: cacheKey,
                                 value: invocation.ReturnValue,
                                 dependencies: null,
                                 absoluteExpiration: DateTime.Now.AddSeconds(cacheDuration),
                                 slidingExpiration: TimeSpan.Zero);
                }
            }
        }

        private static Attribute getCacheMethodAttribute(IInvocation invocation)
        {
            var myType = invocation.Request.Target.GetType();
            MemberInfo[] myMembers = myType.GetMembers();

            for (int i = 0; i < myMembers.Length; i++)
            {
                var attributes = 
                    myMembers[i].GetCustomAttributes(true);

                var cacheAttribute = attributes.FirstOrDefault
                    (c => c.GetType() == typeof(CacheMethodAttribute));

                if (cacheAttribute != null)
                {
                    return (Attribute)cacheAttribute;
                }
            }

            return null;
        }

        private static string getCacheKey(IInvocation invocation)
        {
            var cacheKey = invocation.Request.Method.Name;

            foreach (var argument in invocation.Request.Arguments)
            {
                cacheKey += ":" + argument;
            }

            // todo: بهتر است هش این کلید طولانی بازگشت داده شود
            // کار کردن با هش سریعتر خواهد بود
            return cacheKey;
        }

    }


}
