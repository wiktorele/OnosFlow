
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        [Display(Name = "Id przepływu")]
        public string id { get; set; }
        [Display(Name = "Id tablicy")]
        public string tableId { get; set; }
        public string appId { get; set; }
        [Display(Name = "Priorytet")]
        public int priority { get; set; }
        [Display(Name = "Czas trwania")]
        public int timeout { get; set; }
        [Display(Name = "Permanentny")]
        public bool isPermanent { get; set; }
        [Required]
        [Display(Name = "Id urządzenia")]
        public string deviceId { get; set; }

        [Display(Name = "Stan")]
        public string state { get; set; }
        public int life { get; set; }
        [Display(Name = "Ilość pakietów")]
        public int packets { get; set; }
        [Display(Name = "Ilość bajtów")]
        public int bytes { get; set; }
        public string liveType { get; set; }
        [Display(Name = "Ostatnio widziany")]
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
        [Display(Name = "Typ instrukcji")]
        public string type { get; set; }
        public string port { get; set; }
    }

    public class Selector
    {
        public IList<Criterion> criteria { get; set; }

        //public ICollection<Criterion> criteria { get; set; }
    }

    public class Criterion
    {
        [Display(Name = "Typ kryterium")]
        public string type { get; set; }
        [Display(Name = "Typ ETH")]
        public string ethType { get; set; }
        [Display(Name = "Adres mac")]
        public string mac { get; set; }
        [Display(Name = "Port")]
        public int port { get; set; }
        [Display(Name = "Adres ip")]
        public string ip { get; set; }
        [Display(Name = "Protokół warstwy transportowej")]
        public int protocol { get; set; }
        [Display(Name = "Port tcp")]
        public string tcpPort { get; set; }
        [Display(Name = "Port udp")]
        public string udpPort { get; set; }

        public static IEnumerable<string> GetTypes()
        {
            return new[] { "ETH_TYPE", "ETH_DST", "ETH_SRC", "IPV4_SRC", "IPV4_DST", "IPV6_SRC", "IPV6_DST", "IN_PORT", "IP_PROTO" };
        }
    }

    //public class Criterion1
    //{
    //    public string type { get; set; }

    //    public string eth_dst { get; set; }

    //}
    //public class Criterion2
    //{
    //    public string type { get; set; }

    //    public string eth_src { get; set; }

    //}

    //public class CriterionType
    //{
    //    [Required]
    //    public string Type { get; set; }
    //}


}