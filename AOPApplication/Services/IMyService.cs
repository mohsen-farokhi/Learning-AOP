using AOPApplication.Infrastructure;

namespace AOPApplication.Services
{
    public interface IMyService
    {
        void DoSomething(string data, int i);

        string GetLongRunningResult(string input);
    }
}
