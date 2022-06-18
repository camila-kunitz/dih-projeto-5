using DevInSales.Models;

namespace DevInSales.Services
{
    public interface IUserRepository
    {
        User? ValidarCredenciais(string email, string password);
    }
}