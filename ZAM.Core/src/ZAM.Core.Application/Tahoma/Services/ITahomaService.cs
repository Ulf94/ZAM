namespace ZAM.Core.Application.Tahoma.Services;

using ZAM.Core.Application.Tahoma.Models;

public interface ITahomaService
{
    public Task<List<Device>> GetDevices();
    public Task<string> GetLoginCookie();
    public Task<Token> GetToken();

    public void SendAction(string label, string command, string deviceUrl);
}
