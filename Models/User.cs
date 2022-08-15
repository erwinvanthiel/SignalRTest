using System.ComponentModel.DataAnnotations;

namespace SignalRTest.Models
{
    public class User
    { 
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string PassWord { get; set; }
        public string ConnectionId { get; set; }
    }
}
