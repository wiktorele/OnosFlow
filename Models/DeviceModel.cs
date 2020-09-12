using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnosFlow.Models
{
    public class DeviceModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public bool avilable { get; set; }
        public string role { get; set; }
        public string mfr { get; set; }
        public string hw { get; set; }
        public string sw { get; set; }
        public string serial { get; set; }
        public string driver { get; set; }
        public string chassisId { get; set; }
        public string lastUpdate { get; set; }
        public string humanReadableLastUpdate { get; set; }
        //public List annotiations { get; set; }

    }
}
