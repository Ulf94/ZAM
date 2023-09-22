namespace ZAM.Core.Application.Tahoma.Services;

using ViewModels;

public interface ITahomaService
{
    public Task<Dictionary<string, List<Device>>> GetDevices();
    public Task<string> GetLoginCookie();
    public void SendTahomaAction(string label, string command, string deviceUrl);
}
