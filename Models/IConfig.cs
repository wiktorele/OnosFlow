using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnosFlow.Models
{
    interface IConfig
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
