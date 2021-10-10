using AOPApplication.Interceptors;
using AOPApplication.Services;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System;

namespace AOPApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            // **************************************************
            //kernel
            //    .Bind<IMyService>().To<MyService>()
            //    .Intercept().With<LoggingInterceptor>();

            //var myService = kernel.Get<IMyService>();

            //myService.DoSomething("Test", 1);

            //Console.ReadKey();
            // **************************************************

            // **************************************************
            //kernel
            //    .Bind<IMyService>().To<MyService>()
            //    .Intercept().With<CacheInterceptor>();  
            kernel
                .Bind<IMyService>().To<MyService2>()
                .Intercept().With<LoggerInterceptor>();

            var myService = kernel.Get<IMyService>();

            Console.WriteLine(myService.GetLongRunningResult("Test"));

            Console.WriteLine(myService.GetLongRunningResult("Test"));

            Console.ReadKey();
            // **************************************************
        }
    }
}
