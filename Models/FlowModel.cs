
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace OnosFlow.Models
{
    public class FlowModel
    {
        public Flow[] flows { get; set; }

    }
    public class Flow
    {
        //[Required]
        [RegularExpression("^[0-9]+")]
        public string id { get; set; }
        public string tableId { get; set; }
        public string appId { get; set; }
        public int priority { get; set; }
        public int timeout { get; set; }
        public bool isPermanent { get; set; }
        [Required]
        public string deviceId { get; set; }
        [Required]
        public string state { get; set; }
        public int life { get; set; }
        public int packets { get; set; }
        public int bytes { get; set; }
        public string liveType { get; set; }
        public long lastSeen { get; set; }

        public Treatment treatment { get; set; }
        public Selector selector { get; set; }
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

    //public enum InstructionTypes
    //{
    //    OUTPUT,
    //    TABLE,
    //    GROUP,
    //    METER,
    //    QUEUE
    //}


}