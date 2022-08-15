using SignalRTest.Models;

namespace SignalRTest.Data
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;
        public SqlUserRepository(ChatDbContext context)
        {
            this._context = context;
        }

        public void AddUser(string name, string pwd, string id)
        {
            User user = new User();
            user.Name = name;
            user.PassWord = pwd;
            user.ConnectionId = id;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUserConncetionID(string name, string connectionID)
        {
            var user = _context.Users.Where(u => u.Name == name).First();
            user.ConnectionId = connectionID;
            _context.SaveChanges();
        }

        public User GetUserByID(string id)
        {
            var user = _context.Users.Where(u => u.ConnectionId == id);
            if (user.Count() > 0) 
            {
                return user.First();
            }
            return null;
        }


        public User GetUserByName(string name)
        {
            var user = _context.Users.Where(u => u.Name == name);
            if (user.Count() > 0)
            {
                return user.First();
            }
            return null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Select(x => x).ToList();
        }
    }

    public interface IUserRepository
    {
        User GetUserByName(string name);
        void AddUser(string name, string pwd, string id);
        User GetUserByID(string id);
        void UpdateUserConncetionID(string name, string connectionID);
        IEnumerable<User> GetAllUsers();
    }

}
