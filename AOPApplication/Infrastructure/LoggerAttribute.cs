using System;
using System.Reflection;

namespace AOPApplication.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggerAttribute : Attribute
    {
        public string ExceptionName { get; set; }
        public string Error { get; set; }

        public LoggerAttribute(string exceptionName,string error)
        {
            ExceptionName = exceptionName;
            Error = error;
        }

    }
}