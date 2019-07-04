using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLCPrinter.Models
{
    public class ContractService
    {
        public int SC_ID { get; set; }
        public int SC_C_ID { get; set; }
        public int SC_S_ID { get; set; }

        public virtual ICollection<CONTRACTE> CONTRACTE { get; set; }
        public virtual ICollection<PERSOANE> PERSOANE { get; set; }
        public virtual ICollection<LIBRARIE> LIBRARIE { get; set; }
    }
}