using DevInSales.Models;

namespace DevInSales.Seeds
{
    public class AuthorizationProfileSeed
    {
        public static List<Profile> Seed { get; set; } = new List<Profile>()
        { 
            new Profile(2, "Usuário"),
            new Profile(3, "Gerente"),
            new Profile(4, "Administrador")
        };
    }
}
