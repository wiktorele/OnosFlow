using System.ComponentModel;

namespace OnosFlow.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("User")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
