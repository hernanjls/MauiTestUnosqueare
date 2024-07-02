
namespace TechnicalAxos_HernanLagrava.Services
{
    public interface IMainThreadInvoker
    {
        void BeginInvokeOnMainThread(Action action);
    }

    public class MainThreadInvoker : IMainThreadInvoker
    {
        public void BeginInvokeOnMainThread(Action action)
        {
            MainThread.BeginInvokeOnMainThread(action);
        }
    }

}
