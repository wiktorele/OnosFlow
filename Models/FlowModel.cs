﻿
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
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
        [Display(Name = "Id przepływu")]
        public string id { get; set; }
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
        [Required]
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
        public Criterion[] criteria { get; set; }
    }

    public class Criterion
    {
        public ICollection<CriterionType> Types { get; set; }
        public string type { get; set; }


        public Criterion()
        {
            Types = new List<CriterionType>()
            {
                new CriterionType() {Type = "ETH_TYPE"},
                new CriterionType() {Type = "ETH_DST"},
                new CriterionType() {Type = "ETH_SRC"},
                new CriterionType() {Type = "IN_PORT"}
            };
        }
    }

    public class CriterionType
    {
        [Required]
        public string Type { get; set; }
    }


}