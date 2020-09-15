
using System.ComponentModel.DataAnnotations;

namespace OnosFlow.Models
{
    public class Config : IConfig
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string IpAddress { get; set; }
    }
}
