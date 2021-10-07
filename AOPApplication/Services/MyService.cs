using AOPApplication.Infrastructure;
using System;
using System.Threading;

namespace AOPApplication.Services
{
    public class MyService : IMyService
    {
        public void DoSomething(string data, int i)
        {
            Console.WriteLine("DoSomething({0}, {1});", data, i);
        }

        [CacheMethod(SecondsToCache = 60)]
        public string GetLongRunningResult(string input)
        {
            Thread.Sleep(5000); // simulate a long running process
            return string.Format("Result of '{0}' returned at {1}", input, DateTime.Now);
        }

    }
}
