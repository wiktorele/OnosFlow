
using System.ComponentModel.DataAnnotations;

namespace OnosFlow.Models
{
    public class Config : IConfig
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Login kontrolera")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Hasło kontrolera")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Adres ip kontrolera")]
        public string IpAddress { get; set; }
    }
}
