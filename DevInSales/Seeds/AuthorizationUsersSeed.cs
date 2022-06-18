using DevInSales.Models;

namespace DevInSales.Seeds
{
    public class AuthorizationUsersSeed
    {
        public static List<User> Seed { get; set; } = new List<User>() { new User()
        {
            Id = 5,
            Name = "João Usuário",
            BirthDate = new DateTime(2000, 01, 01),
            Email = "joao@mail.com",
            Password = "joao@123",
            ProfileId = 2
        }, new User()
        {
            Id = 6,
            Name = "Maria Gerente",
            BirthDate = new DateTime(2000, 02, 02),
            Email = "maria@mail.com",
            Password = "maria@123",
            ProfileId = 3
        }, new User()
        {
            Id = 7,
            Name = "José Administrador",
            BirthDate = new DateTime(2000, 03, 03),
            Email = "jose@mail.com",
            Password = "jose@123",
            ProfileId = 4
        }
        };
    }
}
