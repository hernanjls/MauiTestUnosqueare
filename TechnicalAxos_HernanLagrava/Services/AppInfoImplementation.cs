
namespace TechnicalAxos_HernanLagrava.Services
{
    public interface ICustomAppInfo
    {
        string PackageName { get; }
    }
    public class AppInfoImplementation : ICustomAppInfo
    {
        public string PackageName => AppInfo.PackageName;
    }
}
