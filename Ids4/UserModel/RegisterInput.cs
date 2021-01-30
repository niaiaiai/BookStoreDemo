using System.ComponentModel.DataAnnotations;

namespace Ids4.UserModel
{
    public class RegisterInput
    {
        [Required]
        public string UserName { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
