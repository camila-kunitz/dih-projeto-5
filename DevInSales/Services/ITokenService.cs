using DevInSales.Models;

namespace DevInSales.Services
{
    public interface ITokenService
    {
        string GerarToken(User user);
    }
}