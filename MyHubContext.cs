using Microsoft.AspNetCore.SignalR;

public class MyHubContext : IHubContext
{
    public IHubClients Clients => this.Clients;

    public IGroupManager Groups => this.Groups;

}
