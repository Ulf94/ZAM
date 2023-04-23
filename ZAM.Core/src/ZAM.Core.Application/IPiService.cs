namespace ZAM.Core.Application;

public interface IPiService
{
    public Task GetRoomTemperature(int roomNumber);
}
