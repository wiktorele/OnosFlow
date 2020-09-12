using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnosFlow.Models
{
    public class FlowModel
    {
        public string id { get; set; }
        public string tableId { get; set; }
        public string appId { get; set; }
        public int groupId { get; set; }
        public int priority { get; set; }
        public int timeout { get; set; }
        public bool isPermanent { get; set; }
        public string deviceId { get; set; }
        public string state { get; set; }
        public int life { get; set; }
        public int packets { get; set; }
        public int bytes { get; set; }
        public string liveType { get; set; }
        public int lastSeen { get; set; }
        //public List<Treatment> treatment { get; set; }
        //public List<Selector> selector { get; set; }

    }
    /*
    public class Treatment
    {
        public string[] instructions { get; set; }
        public string[] deffered { get; set; }

    }

    public class Selector
    {
        public List<Criteria> instructions { get; set; }
    }

    public class Criteria
    {

    }*/
}
