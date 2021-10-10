using Ninject.Extensions.Interception;
using System;
using System.Linq;
using System.Reflection;
using AOPApplication.Exceptions;
using AOPApplication.Infrastructure;

namespace AOPApplication.Interceptors
{
    public class LoggerInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                Console.WriteLine("Logging On Start.");
                invocation.Proceed(); //فراخوانی متد اصلی در اینجا صورت می‌گیرد

                Console.WriteLine("Logging On Success.");
            }
            catch (Exception ex)
            {
                //log
                Console.WriteLine("Logging errors.");

                GetExceptionType(invocation);

            }
            finally
            {
                Console.WriteLine("Logging On Exit.");
            }
        }
        private static void GetExceptionType(IInvocation invocation ) 
        {
            var myType = invocation.Request.Target.GetType();
            var myMembers = myType.GetMembers();

            foreach (var memberInfo in myMembers)
            {
                var attributes =
                    memberInfo.GetCustomAttributes(true);

                var loggerAttribute = attributes.FirstOrDefault
                    (c => c.GetType() == typeof(LoggerAttribute));

                if (loggerAttribute != null)
                {
                    switch (((LoggerAttribute)loggerAttribute).ExceptionName)
                    {
                        case "CustomNetworkException":
                            throw new CustomNetworkException(((LoggerAttribute)loggerAttribute).Error);
                        case "CustomSqlException": throw new CustomSqlException(((LoggerAttribute)loggerAttribute).Error);
                        default: throw new Exception("Can not define exception class");
                    }
                }

            }
        }

    }
}
