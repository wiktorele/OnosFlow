﻿
namespace OnosFlow.Models
{
    public partial class FlowModel
    {
        public Flow[] flows { get; set; }

        public class Flow
        {
            public string id { get; set; }
            public int priority { get; set; }
            public int timeout { get; set; }
            public string deviceId { get; set; }
            public string state { get; set; }
            //public Treatment treatment { get; set; }
            //public Selector selector { get; set; }
        }
        public class Treatment
        {
            public Instruction[] instructions { get; set; }
            public object[] deferred { get; set; }
            public bool clearDeferred { get; set; }
        }

        public class Instruction
        {
            public string type { get; set; }
            public string port { get; set; }
        }

        public class Selector
        {
            public Criterion[] criteria { get; set; }
        }

        public class Criterion
        {
            public string type { get; set; }
            public int port { get; set; }
            public string mac { get; set; }
            public string ethType { get; set; }
        }

    }

}