using System.ComponentModel.DataAnnotations;

namespace GoLive.Models.UserDtos
{
    public class UserCreate
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}