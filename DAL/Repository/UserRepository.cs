using System.Linq;

namespace WarehouseProProject.DAL.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(WarehouseContext context) : base(context) { }

        // Hàm kiểm tra đăng nhập
        public User GetUserByCredentials(string username, string password)
        {
            return _context.User.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        // Hàm đăng ký tài khoản
        public bool RegisterUser(string username, string password)
        {
            bool exists = _context.User.Any(u => u.Username == username);
            if (exists)
                return false;

            User newUser = new User
            {
                Username = username,
                Password = password
            };

            _context.User.Add(newUser);
            _context.SaveChanges();
            return true;
        }
    }
}
