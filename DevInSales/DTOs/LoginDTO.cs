using System.ComponentModel.DataAnnotations;

namespace DevInSales.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo {0} precisa ser informado.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} precisa ser informado.")]
        public string Password { get; set; }
    }
}