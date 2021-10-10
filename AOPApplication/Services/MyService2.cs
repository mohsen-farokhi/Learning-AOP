using System;
using System.Threading;
using AOPApplication.Exceptions;
using AOPApplication.Infrastructure;

namespace AOPApplication.Services
{
    public class MyService2 : IMyService
    {
        public void DoSomething(string data, int i)
        {
            Console.WriteLine("DoSomething({0}, {1});", data, i);
        }

        [Logger("CustomNetworkException", "خطا در شبکه")]
        public string GetLongRunningResult(string input)
        {
            return string.Format("Result of '{0}' returned at {1}", input, DateTime.Now);
        }

    }
}