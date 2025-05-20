using WarehouseProProject.DAL.Repository;

namespace WarehouseProProject.BLL
{
    public class UserBLL
    {
        private readonly UserRepository _userRepository;
        private User _currentUser;
        public void Logout()
        {
            _currentUser = null;
        }
        public UserBLL(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Hàm kiểm tra đăng nhập
        public bool ValidateLogin(string username, string password)
        {
            var user = _userRepository.GetUserByCredentials(username, password);
            return user != null;
        }

        // Hàm đăng ký tài khoản
        public bool RegisterUser(string username, string password)
        {
            return _userRepository.RegisterUser(username, password);
        }
    }
}
