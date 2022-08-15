using Microsoft.AspNetCore.SignalR;
using SignalRTest.Data;

public class MyHub : Hub
{
    public static List<(string, string, string)> allClients = new List<(string, string, string)>();
    private IUserRepository repository;

    public MyHub(IUserRepository repository)
    {
        this.repository = repository;
    }

    public async Task SendMessage(string to, string message)
    {
        string id = Context.ConnectionId;
        var user = repository.GetUserByID(id);
        if (user != null)
        {
            var receiver_id = repository.GetUserByName(to).ConnectionId;
            var sender_name = user.Name;
            Clients.Client(receiver_id).SendAsync("receiveMessage", sender_name, message);
        }
    }

    public async Task LogIn(string name, string password) 
    {
        string id = Context.ConnectionId;
        var user = repository.GetUserByName(name);
        if (user != null && user.PassWord == password) 
        {
            repository.UpdateUserConncetionID(name, id);
            Clients.All.SendAsync("update");
            return;
        }
        Clients.Caller.SendAsync("receiveMessage", "server", $"{name} is no known user or password is incorrect.");
    }

    public async Task register(string name, string password)
    {
        string id = Context.ConnectionId;
        var callerByID = repository.GetUserByID(id);
        var callerByName = repository.GetUserByName(name);
        if (callerByID != null)
        {
            Clients.Caller.SendAsync("receiveMessage", "server", $"You have already logged in as {callerByID.Name}.");
        }
        else if (callerByName != null)
        {
            Clients.Caller.SendAsync("receiveMessage", "server", $"{callerByName.Name} is already taken, choose another name.");
        }
        else
        {
            repository.AddUser(name, password, id);
            System.Console.WriteLine($"Registered {name} with id {id}");
        }
        // Notify the rest of the newly joined user.
        Clients.All.SendAsync("update");
    }
}