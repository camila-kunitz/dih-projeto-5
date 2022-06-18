using DevInSales.Context;
using DevInSales.Models;

namespace DevInSales.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlContext _context;

        public UserRepository(SqlContext context)
        {
            _context = context;
        }

        public User? ValidarCredenciais(string email, string password)
        {
            return _context.User.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }
    }
}
