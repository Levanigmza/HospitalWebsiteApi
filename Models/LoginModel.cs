using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebsiteApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="User Name is requeired")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requeired")]
        public string Password { get; set; }

    }
}
