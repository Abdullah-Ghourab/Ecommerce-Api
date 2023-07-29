using System.ComponentModel.DataAnnotations;

namespace KShop.Core.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
