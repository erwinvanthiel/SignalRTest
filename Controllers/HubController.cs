using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTest.Data;
using SignalRTest.Models;

namespace SignalRTest.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HubController : Controller
    {
        private readonly IHubContext<MyHub> hubContext;
        private IUserRepository repository;
        public HubController(IHubContext<MyHub> hubContext, IUserRepository repository)
        {
            this.hubContext = hubContext;
            this.repository = repository;
        }

        [HttpGet("/clients")]
        public IActionResult getClients()
        {
            string clients = "";
            foreach (var client in repository.GetAllUsers())
            {
                clients += client.Name + ", ";
            }
            return Ok(clients);
        }

        [HttpPost("/addUser/{name}/{pwd}")]
        public IActionResult AddUser(string name, string pwd, string id)
        {
            repository.AddUser(name, pwd, id);
            return Ok();
        }

        [HttpGet("/getUser/{name}")]
        public IActionResult GetUserByname(string name)
        {
            var user = repository.GetUserByName(name);
            return Ok(user);
        }




    }
}
